using System.ComponentModel.DataAnnotations;


namespace DevCopilot2.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ValidNationalCodeAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;

            string? text = value as string;
            if (text is not null)
            {
                if (text.Length == 10)
                {
                    int index = text.Length;
                    int control = 0;
                    int sum = 0;
                    foreach (var item in text)
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
            }

            return false;
        }
    }

}
