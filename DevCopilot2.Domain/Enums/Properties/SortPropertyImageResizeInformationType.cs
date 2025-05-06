using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Properties
{
    public enum SortPropertyImageResizeInformationType
    {

        [Display(Name = "PropertyName")]
        PropertyName,

        [Display(Name = "PropertyId")]
        PropertyId,

        [Display(Name = "Name")]
        Name,

        [Display(Name = "Width")]
        Width,

        [Display(Name = "Height")]
        Height,
    }
}
