using DevCopilot2.Domain.DTOs.Common;
using FluentFTP;
using Microsoft.AspNetCore.Http;

namespace DevCopilot2.Core.Extensions.BasicExtensions
{
    public static class FtpExtensions
    {
        static string ftpHost = "";
        static string ftpUser = "";
        static string ftpPass = "";

        static FtpExtensions()
        {
            LoadFtpSettings();
        }

        private static void LoadFtpSettings()
        {
            FtpServerDto ftpSettings = AppSettingsExtensions.GetFtpServer();
            ftpHost = ftpSettings.Host;
            ftpUser = ftpSettings.User;
            ftpPass = ftpSettings.Pass;
        }

        public static void UploadViaFtp(this List<UploadFtpDto> uploads)
        {
            using (var ftpClient = new FtpClient(ftpHost, ftpUser, ftpPass))
            {
                ftpClient.Port = 21;
                //await Task.Run(() => ftpClient.Connect());
                ftpClient.Connect();
                foreach (UploadFtpDto upload in uploads)
                {
                    upload.SetPath();
                    string directory = Path.GetDirectoryName(upload.FilePath)!.Replace("\\", "/");
                    // if (!await Task.Run(() => ftpClient.DirectoryExists(directory)))
                    //   await Task.Run(() => ftpClient.CreateDirectory(directory, true));
                    if (!ftpClient.DirectoryExists(directory))
                        ftpClient.CreateDirectory(directory, true);
                    //await Task.Run(() => ftpClient.UploadStream(upload.Stream, upload.FilePath));
                    ftpClient.UploadStream(upload.Stream, upload.FilePath);
                }
                ftpClient.Disconnect();
                //await Task.Run(() => ftpClient.Disconnect());
            }
        }

        public async static Task UploadViaFtp(this IFormFile file,
                MediaInformationDto media, string imageName)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                // FTP upload

                string ftpPath = media.SetAddress + imageName;

                using (var ftpClient = new FtpClient(ftpHost, ftpUser, ftpPass))
                {
                    ftpClient.Port = 21;
                    await Task.Run(() => ftpClient.Connect());
                    string directory = Path.GetDirectoryName(ftpPath)!.Replace("\\", "/");
                    if (!await Task.Run(() => ftpClient.DirectoryExists(directory)))
                    {
                        await Task.Run(() => ftpClient.CreateDirectory(directory, true));
                    }
                    await Task.Run(() => ftpClient.UploadStream(stream, ftpPath));
                    await Task.Run(() => ftpClient.Disconnect());
                }
            }
        }

        public static async Task UploadViaFtp(this string path, string imageName)
        {
            using (var client = new FtpClient(ftpHost))
            {
                client.Credentials = new System.Net.NetworkCredential(ftpUser, ftpPass);
                client.Connect();
                if (!client.DirectoryExists(path))
                    client.CreateDirectory(path, true);
                string fullPath = Path.Combine(path, imageName);
                using (var fileStream = File.OpenRead(fullPath))
                {
                    client.UploadStream(fileStream, fullPath);
                }

                client.Disconnect();
            }
        }


        public async static Task FtpDeleteRange(this List<ChangeFtpFile> changes)
        {
            using (var client = new FtpClient(ftpHost, ftpUser, ftpPass))
            {
                client.Port = 21;
                await Task.Run(() => client.Connect());

                foreach (var change in changes)
                {
                    foreach (var media in change.Medias)
                    {
                        string filePath = Path.Combine(media.SetAddress, change.FileName);
                        await client.FtpDelete(filePath);
                        string webpImageName = change.FileName.ConvertImageNameToWebP();
                        if (webpImageName != change.FileName)
                        {
                            string webpFilePath = Path.Combine(media.SetAddress, webpImageName);
                            await client.FtpDelete(webpFilePath);
                        }
                    }
                }

                await Task.Run(() => client.Disconnect());
            }
            using (var client = new FtpClient(ftpHost))
            {
                client.Credentials = new System.Net.NetworkCredential(ftpUser, ftpPass);
                await Task.Run(() => client.Connect());

                await Task.Run(() => client.Disconnect());
            }
        }

        public async static Task FtpDelete(this FtpClient client, string filePath)
        {
            if (await Task.Run(() => client.FileExists(filePath)))
            {
                await Task.Run(() => client.DeleteFile(filePath));
            }
        }
    }
}
