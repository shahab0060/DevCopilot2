using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Projects
{
    public enum SortProjectAreaType
    {

        [Display(Name = "ProjectTitle")]
        ProjectTitle,

        [Display(Name = "ProjectId")]
        ProjectId,

        [Display(Name = "EnglishName")]
        EnglishName,

        [Display(Name = "Title")]
        Title,

        [Display(Name = "TemplateTitle")]
        TemplateTitle,

        [Display(Name = "TemplateId")]
        TemplateId,
    }
}
