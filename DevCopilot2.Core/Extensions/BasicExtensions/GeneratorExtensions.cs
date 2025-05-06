namespace DevCopilot2.Core.Extensions.BasicExtensions
{
    public static class GeneratorExtensions
    {
        #region external links

        public static string GenerateTelegramLink(this string pageUrl)
          => $"https://t.me/share/url?url={pageUrl}";
        public static string GenerateWhatsAppLink(this string pageUrl)
           => $"https://wa.me/?text={pageUrl}";
        public static string GenerateTwitterLink(this string pageUrl)
           => $"https://twitter.com/intent/tweet?url={pageUrl}";
        public static string GenerateFaceBookLink(this string pageUrl)
          => $"https://www.facebook.com/sharer/sharer.php?m2w&s=100&p[url]={pageUrl}";
        public static string GeneratePinterestLink(this string pageUrl)
            => $"https://www.pinterest.com/pin/create/button/?url={pageUrl}";
        public static string GenerateLinkedinLink(this string pageUrl)
            => $"https://www.linkedin.com/shareArticle?mini=true&url={pageUrl}";

        #endregion
    }
}
