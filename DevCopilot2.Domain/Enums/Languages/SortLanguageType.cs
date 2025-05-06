using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Languages
{
    public enum SortLanguageType
    {

        [Display(Name = "Name")]
        Name,

        [Display(Name = "Culture")]
        Culture,

        [Display(Name = "DefaultPluralSuffix")]
        DefaultPluralSuffix,
    }
}
