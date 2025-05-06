using Ganss.Xss;

namespace DevCopilot2.Core.Security
{
    public static class XssSecurity
    {
        public static string? SanitizeText(this string? text)
        {
            if (text is null) return text;
            var htmlSanitizer = new HtmlSanitizer();

            htmlSanitizer.KeepChildNodes = true;

            htmlSanitizer.AllowDataAttributes = true;

            return htmlSanitizer.Sanitize(text).Trim();
        }
    }
}
