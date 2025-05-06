using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Domain.DTOs.Common;
using FluentFTP;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;

namespace DevCopilot2.Core.Utils
{
    public static class WebpExtensions
    {
        public static async Task<UploadFtpDto?> ToWebp(this Image image, MediaInformationDto media,
            string fileName)
        {
            string extension = Path.GetExtension(fileName);
            if (extension == ".svg") return null;
            using var memoryStream = new MemoryStream();
            var webpEncoder = new WebpEncoder
            {
                Quality = 100,
                Method = WebpEncodingMethod.BestQuality,
                NearLossless = true,
                NearLosslessQuality = 100
            };
            await image.SaveAsync(memoryStream, webpEncoder);
            var clonedStream = new MemoryStream(memoryStream.ToArray());
            return new UploadFtpDto()
            {
                FileName = fileName.ConvertImageNameToWebP(),
                Media = media,
                Stream = clonedStream
            };
        }
    }
}
