using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Entities
{
    public enum SortEntityType
    {

        [Display(Name = "SingularName")]
        SingularName,

        [Display(Name = "PluralName")]
        PluralName,

        [Display(Name = "FolderName")]
        FolderName,

        [Display(Name = "InheritedEntityPluralName")]
        InheritedEntityPluralName,

        [Display(Name = "InheritedEntityId")]
        InheritedEntityId,

        [Display(Name = "IdType")]
        IdType,

        [Display(Name = "ServiceName")]
        ServiceName,

        [Display(Name = "AuthorPhoneNumber")]
        AuthorPhoneNumber,

        [Display(Name = "AuthorId")]
        AuthorId,

        [Display(Name = "ProjectTitle")]
        ProjectTitle,

        [Display(Name = "ProjectId")]
        ProjectId,

        [Display(Name = "IsExcluded")]
        IsExcluded,

        [Display(Name = "AddToMenu")]
        AddToMenu,
    }
}
