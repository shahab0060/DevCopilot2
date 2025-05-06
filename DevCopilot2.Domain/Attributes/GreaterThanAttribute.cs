using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public GreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is null)
                return new ValidationResult($"Unknown property: {_comparisonProperty}");
            var currentValue = (int)value;

            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (comparisonProperty == null)
            {
                return new ValidationResult($"Unknown property: {_comparisonProperty}");
            }

            var comparisonValue = comparisonProperty.GetValue(validationContext.ObjectInstance) as int?;

            if (comparisonValue.HasValue && currentValue <= comparisonValue.Value)
            {
                var displayName = validationContext.DisplayName;
                var comparisonDisplayName = comparisonProperty.Name;

                var comparisonDisplayAttribute = comparisonProperty.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (comparisonDisplayAttribute.Length > 0)
                {
                    comparisonDisplayName = ((DisplayAttribute)comparisonDisplayAttribute[0]).Name;
                }

                var errorMessage = FormatErrorMessage(displayName, comparisonDisplayName);
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _comparisonProperty);
        }

        public string FormatErrorMessage(string displayName, string comparisonDisplayName)
        {
            return string.Format(ErrorMessageString, displayName, comparisonDisplayName);
        }

        public string GetComparisonProperty()
        {
            return _comparisonProperty;
        }
    }

}
