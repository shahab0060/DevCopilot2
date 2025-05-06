using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Projects
{
    public enum SortProjectEnumType
    {

        [Display(Name = "ProjectTitle")]
        ProjectTitle,

        [Display(Name = "ProjectId")]
        ProjectId,

        [Display(Name = "EnglishName")]
        EnglishName,

        [Display(Name = "FolderName")]
        FolderName,

        [Display(Name = "AuthorPhoneNumber")]
        AuthorPhoneNumber,

        [Display(Name = "AuthorId")]
        AuthorId,
    }
}
