using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Projects
{
    public enum SortProjectSelectedLanguageType
    {

        [Display(Name = "ProjectTitle")]
        ProjectTitle,

        [Display(Name = "ProjectId")]
        ProjectId,

        [Display(Name = "LanguageName")]
        LanguageName,

        [Display(Name = "LanguageId")]
        LanguageId,
    }
}
