using DevCopilot2.Core.Utils;
using DevCopilot2.Domain.DTOs.Common;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace DevCopilot2.Core.Extensions.BasicExtensions
{
    public static class FileExtensions
    {
        #region upload local image

        public static async Task<string?> UploadLocalImageAsync(this IFormFile? file, List<MediaInformationDto> medias,
            string preferedName,
             int editCounts,
             string? deleteImageName = null)
        {
            if (editCounts > 0)
                preferedName = $"{preferedName}-v{editCounts}";
            MediaInformationDto? originalMediaInformation = medias
                .OrderByDescending(a => a.Width == null)
                .FirstOrDefault();
            if (originalMediaInformation is null) return null;
            if (!Directory.Exists(originalMediaInformation.SetAddress))
                Directory.CreateDirectory(originalMediaInformation.SetAddress);
            if (file is null) return null;
            string newImageName = preferedName.ToUrl() + Path.GetExtension(file.FileName);
            await newImageName.DeleteFile(originalMediaInformation);
            if (file.Length > 0)
            {
                using (var stream = System.IO.File.Create(originalMediaInformation.SetAddress + newImageName))
                {
                    await file.CopyToAsync(stream);
                }
            }
            await originalMediaInformation.ConvertToWebP(newImageName);
            await originalMediaInformation.ResizeImage(medias, newImageName, deleteImageName);
            return newImageName;
        }

        #endregion

        #region resize and webp

        public static async Task ResizeImage(this MediaInformationDto originalMedia, List<MediaInformationDto> resizesInformation,
    string fileName, string? deleteFileName)
        {
            foreach (var resize in resizesInformation)
            {
                await originalMedia.ResizeImage(resize, fileName, deleteFileName);
            }
        }

        public static async Task ResizeImage(this MediaInformationDto originalMedia, MediaInformationDto resizeImage, string fileName, string? deleteFileName)
        {
            if (resizeImage.Width is not > 0 || resizeImage.Height is not > 0) return;
            await deleteFileName!.DeleteFile(resizeImage);
            await deleteFileName!.ConvertImageNameToWebP()!.DeleteFile(resizeImage);
            if (!Directory.Exists(resizeImage.SetAddress))
                Directory.CreateDirectory(resizeImage.SetAddress);
            originalMedia.ImageResizer(resizeImage, fileName);
            await resizeImage.ConvertToWebP(fileName);
        }

        public static void ImageResizer(this Image image, string fileName, MediaInformationDto media)
        {
            image.ImageResizer(fileName, media);
        }

        public static void ImageResizer(this MediaInformationDto inputMedia, MediaInformationDto outputMedia, string fileName)
        {
            string inputImagePath = Path.Combine(inputMedia.SetAddress, fileName);
            string outputImagePath = Path.Combine(outputMedia.SetAddress, fileName);

            using (var image = Image.Load(inputImagePath))
            {
                Size size = new Size(outputMedia.Width!.Value, outputMedia.Height!.Value);
                image.Mutate(x => x.Resize(size));

                string fileExtension = Path.GetExtension(inputImagePath);
                switch (fileExtension.ToLower())
                {
                    case ".png":
                        {
                            image.Save(outputImagePath, new PngEncoder
                            {

                            });
                            break;
                        }
                    case ".jpg":
                    case ".jpeg":
                        {
                            image.Save(outputImagePath, new JpegEncoder
                            {
                                Quality = 100
                            });
                            break;
                        }
                    case ".webp":
                        {
                            image.Save(outputImagePath, new WebpEncoder
                            {
                                Quality = 100
                            });
                            break;
                        }
                    default:
                        break;
                }
            }
        }


        public static async Task ConvertToWebP(this Image image, MediaInformationDto media, string imageName)
        {
            await image.SaveAsWebpAsync(path: media.SetAddress + imageName.ConvertImageNameToWebP(),
                new WebpEncoder()
                {
                    Method = WebpEncodingMethod.BestQuality,
                    Quality = 100
                });
        }

        public static async Task ConvertToWebP(this MediaInformationDto media, string imageName)
        {
            Image image = await Image.LoadAsync(media.SetAddress + imageName);
            await ConvertToWebP(image, media, imageName);
        }

        #endregion

        public static async Task<string?> UploadImage(this IFormFile? file, string path,
               string preferedName,
                int editCounts,
                string? deleteImageName = null)
        {
            if (editCounts > 0)
                preferedName = $"{preferedName}-v{editCounts}";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            MediaInformationDto media = new MediaInformationDto()
            {
                GetAddress = path,
                SetAddress = path
            };
            await preferedName.DeleteFile(media);
            await deleteImageName.DeleteFile(media);
            if (file is null) return null;
            string newImageName = preferedName.ToUrl() + Path.GetExtension(file.FileName);
            if (file.Length > 0)
            {
                using (var stream = System.IO.File.Create(path + newImageName))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return newImageName;
        }

        public static async Task<string?> UploadImageAsync(this IFormFile? file,
               List<MediaInformationDto> medias,
               string preferedName,
               int editCounts,
               string? deleteImageName = null)
        {
            if (file is null) return null;
            medias = medias.OrderByDescending(a => a.Width == null).ToList();
            var originalMedia = medias.First();
            preferedName = preferedName.ToUrl();
            if (editCounts > 0)
                preferedName = $"{preferedName}-v{editCounts}";
            //if (!Directory.Exists(originalMedia.SetAddress))
            //  Directory.CreateDirectory(originalMedia.SetAddress);
            string newImageName = preferedName.ToUrl() + file.FileName.GetExtension();
            List<ChangeFtpFile> deleteFiles = [new ChangeFtpFile() { FileName = newImageName, Medias = medias }];
            if (!string.IsNullOrEmpty(deleteImageName))
                deleteFiles.Add(new ChangeFtpFile() { FileName = deleteImageName, Medias = medias });
            await deleteFiles.FtpDeleteRange();
            if (file.Length > 0)
            {
                List<UploadFtpDto> uploads = new List<UploadFtpDto>();
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0;
                using var image = await Image.LoadAsync(stream);
                uploads.Add(new UploadFtpDto()
                {
                    FileName = newImageName,
                    Media = originalMedia,
                    Stream = stream
                });
                UploadFtpDto? webpUploadFtp = await image.ToWebp(originalMedia, newImageName);
                if (webpUploadFtp != null)
                    uploads.Add(webpUploadFtp);
                var resizeMedias = medias
                    .Where(a => a.Width > 0 && a.Height > 0)
                    .ToList();
                if (resizeMedias.Any())
                {
                    List<UploadFtpDto> resizeUploads = await image.ResizeAndUploadViaFtp(resizeMedias, newImageName);
                    uploads.AddRange(resizeUploads);
                }
                uploads.UploadViaFtp();
            }

            return newImageName;
        }

        public static async Task<string?> UploadFileAsync(this IFormFile? file,
               MediaInformationDto media,
               string preferedName,
               int editCounts,
               string? deleteFileName = null)
        {
            if (file is null) return null;
            preferedName = preferedName.ToUrl();
            if (editCounts > 0)
                preferedName = $"{preferedName}-v{editCounts}";
            //if (!Directory.Exists(originalMedia.SetAddress))
            //  Directory.CreateDirectory(originalMedia.SetAddress);
            string newFileName = preferedName.ToUrl() + file.FileName.GetExtension();
            List<ChangeFtpFile> deleteFiles =
                [new ChangeFtpFile() { FileName = newFileName, Medias = new List<MediaInformationDto>() }];
            if (!string.IsNullOrEmpty(deleteFileName))
                deleteFiles.Add(new ChangeFtpFile()
                {
                    FileName = deleteFileName,
                    Medias = new List<MediaInformationDto>() { media }
                });
            await deleteFiles.FtpDeleteRange();
            if (file.Length > 0)
            {
                List<UploadFtpDto> uploads = new List<UploadFtpDto>();
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0;
                uploads.Add(new UploadFtpDto()
                {
                    FileName = newFileName,
                    Media = media,
                    Stream = stream
                });
                uploads.UploadViaFtp();
            }

            return newFileName;
        }

        public static async Task<string?> UploadVideo(this IFormFile? file, string path,
               string preferedName,
                int editCounts,
                string? deleteImageName = null)
        {
            if (editCounts > 0)
                preferedName = $"{preferedName}-v{editCounts}";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //preferedName.DeleteFile(path);
            if (file is null) return null;
            string newImageName = preferedName.ToUrl() + Path.GetExtension(file.FileName);
            if (file.Length > 0)
            {
                using (var stream = System.IO.File.Create(path + newImageName))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return newImageName;
        }




        async static Task DeleteFile(this string? fileName, MediaInformationDto media)
        {
            if (string.IsNullOrEmpty(fileName)) return;
            var filePath = Path.Combine(media.SetAddress, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
            //List<ChangeFtpFile> files = new List<ChangeFtpFile>()
            //{
            //    new ChangeFtpFile()
            //    {
            //        FileName = fileName,
            //        Medias= new List<MediaInformationDto>(){media}
            //    }
            //};
            //await files.FtpDeleteRange();
        }

        public static IImageEncoder GetIncoder(this Image image, string filePath)
        {
            try
            {
                return image.DetectEncoder(filePath);
            }
            catch (Exception)
            {
                return new JpegEncoder();
            }
        }

        public static void DeleteFile(this string fileFullPath)
        {
            if (File.Exists(fileFullPath))
                File.Delete(fileFullPath);
        }

        public static void RenameFiles(this string directoryPath, string oldName, string newName)
        {
            foreach (var filePath in Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories))
            {
                string fileName = Path.GetFileName(filePath);
                string newFileName = fileName.Replace(oldName, newName);
                string newFilePath = filePath.Replace(fileName, newFileName);
                if (filePath != newFilePath)
                    if (!File.Exists(newFilePath))
                    {
                        //if (!Directory.Exists(newFilePath))
                        //  Directory.CreateDirectory(newFilePath);
                        File.Move(filePath, newFilePath);
                    }
            }
        }

        public static void RenameFolders(this string directoryPath, string oldName, string newName)
        {
            foreach (var path in Directory.GetDirectories(directoryPath, "*.*", SearchOption.AllDirectories))
            {
                string newPath = path.Replace(oldName, newName);
                if (path != newPath)
                    if (!Directory.Exists(newPath))
                        Directory.Move(path, newPath);
            }
        }

        public static void ReplaceTextInFiles(this string directoryPath, string oldText, string newText)
        {
            foreach (var filePath in Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories))
            {
                string fileContent = File.ReadAllText(filePath);
                if (fileContent.Contains(oldText))
                {
                    fileContent = fileContent.Replace(oldText, newText);
                    File.WriteAllText(filePath, fileContent);
                }
            }
        }

    }

}
