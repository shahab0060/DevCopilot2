using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PersianPhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            string? phoneNumber = value as string;
            if (phoneNumber is not null && phoneNumber.Length == 11)
            {
                if (phoneNumber.StartsWith("09") && long.TryParse(phoneNumber, out _))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
