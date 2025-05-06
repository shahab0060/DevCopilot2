using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Entities
{
    public enum SortEntitySelectedProjectAreaType
    {

        [Display(Name = "EntityPluralName")]
        EntityPluralName,

        [Display(Name = "EntityId")]
        EntityId,

        [Display(Name = "ProjectAreaTitle")]
        ProjectAreaTitle,

        [Display(Name = "ProjectAreaId")]
        ProjectAreaId,

        [Display(Name = "HasIndex")]
        HasIndex,

        [Display(Name = "HasCreate")]
        HasCreate,

        [Display(Name = "HasUpdate")]
        HasUpdate,

        [Display(Name = "HasDelete")]
        HasDelete,

        [Display(Name = "HasApi")]
        HasApi,

        [Display(Name = "HasWeb")]
        HasWeb,
    }
}
