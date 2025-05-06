using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Entities
{
    public enum SortEntityRelationType
    {

        [Display(Name = "PrimaryPropertyName")]
        PrimaryPropertyName,

        [Display(Name = "PrimaryPropertyId")]
        PrimaryPropertyId,

        [Display(Name = "SecondaryEntityPluralName")]
        SecondaryEntityPluralName,

        [Display(Name = "SecondaryEntityId")]
        SecondaryEntityId,

        [Display(Name = "MiddleEntityPluralName")]
        MiddleEntityPluralName,

        [Display(Name = "MiddleEntityId")]
        MiddleEntityId,

        [Display(Name = "RelationType")]
        RelationType,

        [Display(Name = "InputType")]
        InputType,

        [Display(Name = "FillingType")]
        FillingType,

        [Display(Name = "FillingCode")]
        FillingCode,
    }
}
