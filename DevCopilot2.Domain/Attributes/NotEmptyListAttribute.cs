using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.Attributes
{
    public class NotEmptyListAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = (value as IEnumerable<object>)?.ToList() ?? new List<object>();
            return list != null && list.Count > 0; // Ensure list is not null and has items
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name);
        }
    }

}
