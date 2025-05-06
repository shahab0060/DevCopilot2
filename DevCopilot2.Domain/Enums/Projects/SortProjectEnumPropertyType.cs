using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Projects
{
    public enum SortProjectEnumPropertyType
    {

        [Display(Name = "ProjectEnumEnglishName")]
        ProjectEnumEnglishName,

        [Display(Name = "ProjectEnumId")]
        ProjectEnumId,

        [Display(Name = "Name")]
        Name,

        [Display(Name = "Order")]
        Order,
    }
}
