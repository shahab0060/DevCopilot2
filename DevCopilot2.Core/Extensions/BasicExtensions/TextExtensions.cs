using DevCopilot2.Core.Security;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DevCopilot2.Core.Extensions.BasicExtensions
{
    public static class TextExtensions
    {
        public static string GetFirstWords(this string text, int n)
        {
            if (string.IsNullOrWhiteSpace(text) || n <= 0)
            {
                return string.Empty;
            }

            string[] words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length < n)
            {
                return string.Join(" ", words);
            }

            return string.Join(" ", words, 0, n);
        }
        public static string ToRegionName(this string text)
       => text.AddSpacesBetweenCapitals('-').ToLower();
        public static string CreateEmptyLines(this int count)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                stringBuilder.AppendLine("");
            }
            return stringBuilder.ToString();
        }
        public static string AddSpacesBetweenCapitals(this string input, char space = ' ')
        {
            if (string.IsNullOrEmpty(input))
                return input;

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];
                if (char.IsUpper(currentChar))
                {
                    // Add a space before the uppercase character
                    if (i > 0)
                        result.Append(space);
                    result.Append(currentChar);
                }
                else
                {
                    result.Append(currentChar);
                }
            }

            return result.ToString();
        }
        public static string? ToTitle(this string? title) => title.CapitalizeFirstCharacterOfEachWord().SanitizeText();

        public static string? ToText(this string? text) => text.SanitizeText();
        public static string Join(this List<string> list, string separator = " & ") =>
        string.Join(separator, list);
        public static string ReplaceLastOccurrence(this string source, char find, char replace)
        {
            int place = source.LastIndexOf(find);
            if (place == -1)
                return source;
            return source.Remove(place, 1).Insert(place, replace.ToString());
        }

        public static string ToFirstCharLower(this string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return char.ToLower(text[0]) + text.Substring(1);
        }

        public static string ToFirstCharUpper(this string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return char.ToUpper(text[0]) + text.Substring(1);
        }
        public static string ToPersian(this bool value)
            => value ? "بله" : "خیر";

        public static string Truncate(this string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return text.Length <= maxLength ? text : text.Substring(0, maxLength) + "...";
        }

        public static string GenerateRandomEnglishString(this int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string? CapitalizeFirstCharacterOfEachWord(this string? text)
        {
            if (text is null) return null;
            //TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            //return textInfo.ToTitleCase(text);
            return text;
        }

        public static bool IsValidUrl(this string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            // Regular expression pattern for matching a URL
            string pattern = @"^(https?://)?([\da-z.-]+)\.([a-z.]{2,6})([/\w.-]*)*/?$";

            return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
        }

        public static string ConvertImageNameToWebP(this string imageName)
        {
            string pathExtension = Path.GetExtension(imageName);
            if (pathExtension == ".svg") return imageName;
            return Path.GetFileNameWithoutExtension(imageName) + ".webp";
        }

        public static string GetEnglishNumbers(this string s)
        {
            return s.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");
        }

        public static string ToAmount(this int? price) =>
            price > 0 ?
            ((int)price).ToAmount() : "";
        public static string ToAmount(this long? price) =>
            price > 0 ?
            ((long)price).ToAmount() : "";
        public static string ToAmount(this int price) => price.ToString("#,0") + " تومان ";
        public static string ToAmount(this long price) => price.ToString("#,0") + " تومان ";

        public static string ToCultureDisplayName(this string cultureDisplayName)
        {
            return cultureDisplayName.ToLower() switch
            {
                "fa-ir" => "فارسی",
                "ar-sa" => "العربیه",
                "en-us" => "English",
                _ => ""
            };
        }
        public static List<string> SplitByChar(this string text, char splitChar = ',')
        {
            if (string.IsNullOrEmpty(text)) return new List<string>();

            string[] parts = text.Split(new[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);
            List<string> result = new List<string>(parts);

            return result;
        }

    }
}
