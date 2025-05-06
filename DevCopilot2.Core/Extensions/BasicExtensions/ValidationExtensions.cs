namespace DevCopilot2.Core.Extensions.BasicExtensions
{
    public static class ValidationExtensions
    {
        public static bool IsEmailValid(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPhoneNumberValid(this string? phoneNumber)
       => phoneNumber != null && phoneNumber.Length == 11 && phoneNumber.StartsWith("09") && phoneNumber.IsNumber() ? true : false;

        public static bool IsNumber(this string format)
        {
            string allowableNumbers = "1234567890";

            foreach (char c in format)
            {
                // This is using String.Contains for .NET 2 compat.,
                //   hence the requirement for ToString()
                if (!allowableNumbers.Contains(c.ToString()))
                    return false;
            }

            return true;
        }

        public static bool IsNationalCodeValid(this string nationalCode)
        {
            if (nationalCode.Length == 10)
            {
                int index = nationalCode.Length;
                int control = 0;
                int sum = 0;
                foreach (var item in nationalCode)
                {
                    if (int.TryParse(item.ToString(), out int number))
                    {
                        if (index == 1)
                        {
                            control = number;
                        }
                        else
                        {
                            sum += number * index;
                        }
                        index--;
                    }
                    else
                    {
                        return false;
                    }
                }
                int r = sum % 11;
                if ((r < 2 && r == control) || (r >= 2 && 11 - r == control))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
