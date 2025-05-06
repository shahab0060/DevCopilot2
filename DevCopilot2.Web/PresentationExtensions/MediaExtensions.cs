namespace DevCopilot2.Web.PresentationExtensions
{
    public static class MediaExtensions
    {
        public static async Task<IFormFile> GetIFormFileFromUrl(this string imageUrl)
        {
            var mimeTypes = new Dictionary<string, string>
    {
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".png", "image/png" },
        { ".gif", "image/gif" },
        { ".bmp", "image/bmp" },
        { ".tiff", "image/tiff" },
        { ".jfif", "image/jpeg" },
        { ".webp", "image/webp" }
    };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(imageUrl);
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var fileName = Path.GetFileName(imageUrl);
                    var fileExtension = Path.GetExtension(fileName).ToLower(); var contentType = mimeTypes.ContainsKey(fileExtension) ? mimeTypes[fileExtension] : "application/octet-stream";
                    var formFile = new FormFile(stream, 0, stream.Length, null, fileName)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = contentType
                    };
                    return formFile;
                }
            }
            return null;
        }
    }
}
