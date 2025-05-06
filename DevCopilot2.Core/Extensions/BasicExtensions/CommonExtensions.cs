using DevCopilot2.Domain.DTOs.Paging;
using iText.Barcodes.Dmcode;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace DevCopilot2.Core.Extensions.BasicExtensions
{
    public static class CommonExtensions
    {
        #region url

        static string FixUrlSpacesWithDash(this string url)
        => url.ToLower().Trim().Replace(" ", "-").Replace("%", "-").Replace("&", "")
           .Replace("--", "-")
           .Replace(";", "-")
           .Replace("\u200C", "-");

        public static string ToUrl(this string url)
            => url.ToLower().Trim().FixUrlSpacesWithDash();

        #endregion

        #region enums

        public static string? GetEnumName(this System.Enum myEnum)
        {
            var enumDisplayName = myEnum.GetType().GetMember(myEnum.ToString()).FirstOrDefault();
            if (enumDisplayName is not null)
                return enumDisplayName.GetCustomAttribute<DisplayAttribute>()?.GetName();
            return null;
        }


        #endregion

        #region Convert Bool To Icon

        public static string ConvertBoolToIcon(this bool value)
        => value ? "check_circle" : "highlight_off";

        public static string? ConvertBoolToIcon(this bool? value)
        => (value is not null) ? ConvertBoolToIcon((bool)value) : null;

        #endregion

        #region Convert Bool To Text Color

        public static string ConvertBoolToTextColor(this bool value)
        => value ? "text-success" : "text-danger";

        public static string ConvertBoolToText(this bool value)
        => value ? "بله" : "خیر";

        public static string? ConvertBoolToText(this bool? value)
       => value is null ? null : value.Value ? "بله" : "خیر";

        public static string? ConvertBoolToTextColor(this bool? value)
        => (value is not null) ? ConvertBoolToTextColor((bool?)value) : null;


        #endregion

        #region calculate index

        public static int CalculateIndex(this int index, BasePaging paging)
            => (paging.PageId - 1) * paging.TakeEntity + index;

        #endregion

        public static StringBuilder ReplaceLastOccurance(
            this StringBuilder sb, char oldValue = ',', char newValue = ' ')
        {
            int lastCommaIndex = sb.ToString().LastIndexOf(oldValue);
            if (lastCommaIndex >= 0)
            {
                sb = sb.Remove(lastCommaIndex, 1); // Remove the last comma
                sb = sb.Insert(lastCommaIndex, newValue); // Insert the replacement string            }
            }
            return sb;
        }
        public static bool IsEuqal(this List<long> firstList, List<long> secondList) =>
            firstList.Distinct().OrderBy(e => e)
            .SequenceEqual(secondList.Distinct().OrderBy(e => e));
    }
}
