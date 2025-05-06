using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Properties
{
    public enum SortPropertyType
    {

        [Display(Name = "Name")]
        Name,

        [Display(Name = "DataType")]
        DataType,

        [Display(Name = "MaxLength")]
        MaxLength,

        [Display(Name = "MinLength")]
        MinLength,

        [Display(Name = "RangeFrom")]
        RangeFrom,

        [Display(Name = "RangeTo")]
        RangeTo,

        [Display(Name = "IsRequired")]
        IsRequired,

        [Display(Name = "ProjectEnumEnglishName")]
        ProjectEnumEnglishName,

        [Display(Name = "ProjectEnumId")]
        ProjectEnumId,

        [Display(Name = "DataAnnotationDataType")]
        DataAnnotationDataType,

        [Display(Name = "IsUnique")]
        IsUnique,

        [Display(Name = "IsUpdatable")]
        IsUpdatable,

        [Display(Name = "ShowInList")]
        ShowInList,

        [Display(Name = "IsFilterContain")]
        IsFilterContain,

        [Display(Name = "IsFilterEqual")]
        IsFilterEqual,

        [Display(Name = "Order")]
        Order,

        [Display(Name = "UseEditor")]
        UseEditor,

        [Display(Name = "EntityPluralName")]
        EntityPluralName,

        [Display(Name = "EntityId")]
        EntityId,

        [Display(Name = "AuthorPhoneNumber")]
        AuthorPhoneNumber,

        [Display(Name = "AuthorId")]
        AuthorId,

        [Display(Name = "ForceMapperCode")]
        ForceMapperCode,

        [Display(Name = "ExcludeFromListDto")]
        ExcludeFromListDto,
    }
}
