using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Relations;

namespace DevCopilot2.Domain.Enums.Relations
{
    public enum InputTypeEnum
    {
        [Display(ResourceType = typeof(InputTypeEnumResources), Name = nameof(InputTypeEnumResources.Select))]
        Select = 0,
        [Display(ResourceType = typeof(InputTypeEnumResources), Name = nameof(InputTypeEnumResources.SecondaryEntitySelect))]
        SecondaryEntitySelect = 1,
        [Display(ResourceType = typeof(InputTypeEnumResources), Name = nameof(InputTypeEnumResources.Hidden))]
        Hidden = 2,
        [Display(ResourceType = typeof(InputTypeEnumResources), Name = nameof(InputTypeEnumResources.NoInput))]
        NoInput = 3,
        [Display(ResourceType = typeof(InputTypeEnumResources), Name = nameof(InputTypeEnumResources.FieldsInRelationEntityPage))]
        FieldsInRelationEntityPage = 4,
    }
}
