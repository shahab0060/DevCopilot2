using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Projects
{
    public enum SortProjectEnumPropertySelectedLanguageType
    {

        [Display(Name = "LanguageName")]
        LanguageName,

        [Display(Name = "LanguageId")]
        LanguageId,

        [Display(Name = "ProjectEnumPropertyName")]
        ProjectEnumPropertyName,

        [Display(Name = "ProjectEnumPropertyId")]
        ProjectEnumPropertyId,

        [Display(Name = "Title")]
        Title,
    }
}
