using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Entities
{
    public enum SortEntitySelectedProjectAreaSelectedFilterType
    {

        [Display(Name = "EntitySelectedProjectAreaHasWeb")]
        EntitySelectedProjectAreaHasWeb,

        [Display(Name = "EntitySelectedProjectAreaId")]
        EntitySelectedProjectAreaId,

        [Display(Name = "PropertyName")]
        PropertyName,

        [Display(Name = "PropertyId")]
        PropertyId,

        [Display(Name = "Value")]
        Value,
    }
}
