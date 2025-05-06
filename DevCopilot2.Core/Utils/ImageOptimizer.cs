using AngleSharp.Dom;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Entities.Users;
using DocumentFormat.OpenXml.Drawing.Charts;
using FluentFTP;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Net;

namespace DevCopilot2.Core.Utils
{
    public static class ImageOptimizer
    {
        public static Image Resize(this Image image, string fileName,
            MediaInformationDto media)
        {
            image.Mutate(x => x.Resize(new SixLabors.ImageSharp.Size()
            {
                Width = media.Width ?? 100,
                Height = media.Height ?? 100
            }));
            return image;
        }

        public static async Task<List<UploadFtpDto>> ResizeAndUploadViaFtp
            (this Image image, List<MediaInformationDto> medias
            , string imageName)
        {
            List<UploadFtpDto> uploads = new List<UploadFtpDto>();
            string extension = imageName.GetExtension();
            if (extension == ".svg") return new List<UploadFtpDto>();
            foreach (var media in medias)
            {
                Image resizedImage = image.Resize(imageName, media);
                using var memoryStream = new MemoryStream();
                await image.SaveAsync(memoryStream, image.GetIncoder(imageName));
                var clonedStream = new MemoryStream(memoryStream.ToArray());
                uploads.Add(new UploadFtpDto()
                {
                    FileName = imageName,
                    Media = media,
                    Stream = clonedStream
                });
                UploadFtpDto? uploadWebp = await resizedImage.ToWebp(media, imageName);
                if (uploadWebp is not null)
                    uploads.Add(uploadWebp);
            }
            return uploads;
        }

        public static string GetExtension(this string fileName)
        {
            string extension = Path.GetExtension(fileName);
            if (extension.Length > 6) return ".jpg";
            return extension;
        }

    }
}
