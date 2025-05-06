using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Properties
{
    public enum SortPropertySelectedLanguageType
    {

        [Display(Name = "PropertyName")]
        PropertyName,

        [Display(Name = "PropertyId")]
        PropertyId,

        [Display(Name = "LanguageName")]
        LanguageName,

        [Display(Name = "LanguageId")]
        LanguageId,

        [Display(Name = "Title")]
        Title,
    }
}
