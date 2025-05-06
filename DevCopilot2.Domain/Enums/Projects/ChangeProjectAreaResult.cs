using System;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.Enums.Projects
{
    public enum ChangeProjectAreaResult
    {
        NotFound,
        Success,

        [Display(Name = "EnglishName")]
        EnglishNameExists,

        [Display(Name = "Title")]
        TitleExists,
    }
}
