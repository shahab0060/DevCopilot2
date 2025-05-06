using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Projects
{
    public enum SortProjectType
    {

        [Display(Name = "EnglishName")]
        EnglishName,

        [Display(Name = "Title")]
        Title,

        [Display(Name = "Location")]
        Location,

        [Display(Name = "Architecture")]
        Architecture,

        [Display(Name = "AuthorPhoneNumber")]
        AuthorPhoneNumber,

        [Display(Name = "AuthorId")]
        AuthorId,

        [Display(Name = "ReactProjectLocation")]
        ReactProjectLocation,
    }
}
