using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.Attributes
{
    public class PersianCalenderAttribute : RegularExpressionAttribute
    {
        public PersianCalenderAttribute() : base(@"^(?<year>(?:[0-9]{2})?[0-9]{2})/(?<month>[0-3]?[0-9])/(?<day>[0-3]?[0-9])$")
        {
            ErrorMessage = "تاریخ وارد شده معتبر نمی باشد";
        }
    }
}
