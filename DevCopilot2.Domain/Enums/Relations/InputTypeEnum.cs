using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Relations;

namespace DevCopilot2.Domain.Enums.Relations
{
    public enum InputTypeEnum
    {
        [Display(ResourceType = typeof(InputTypeEnumResources), Name = nameof(InputTypeEnumResources.Select))]
        Select,
        [Display(ResourceType = typeof(InputTypeEnumResources), Name = nameof(InputTypeEnumResources.SecondaryEntitySelect))]
        SecondaryEntitySelect,
        [Display(ResourceType = typeof(InputTypeEnumResources), Name = nameof(InputTypeEnumResources.Hidden))]
        Hidden,
        [Display(ResourceType = typeof(InputTypeEnumResources), Name = nameof(InputTypeEnumResources.NoInput))]
        NoInput,
        [Display(ResourceType = typeof(InputTypeEnumResources), Name = nameof(InputTypeEnumResources.FieldsInRelationEntityPage))]
        FieldsInRelationEntityPage
    }
}
