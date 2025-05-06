using System;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.Enums.Projects
{
    public enum ChangeProjectResult
    {
        NotFound,
        Success,

        [Display(Name = "EnglishName")]
        EnglishNameExists,

        [Display(Name = "Title")]
        TitleExists,

        [Display(Name = "Location")]
        LocationExists,
    }
}
