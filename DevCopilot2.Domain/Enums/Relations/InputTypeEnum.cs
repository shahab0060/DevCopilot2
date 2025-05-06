using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Relations;

namespace DevCopilot2.Domain.Enums.Relations
{
    public enum InputTypeEnum
    {
        [Display(ResourceType = typeof(InputTypeEnumResources), Name = nameof(InputTypeEnumResources.InputType))]
        InputType = 1,
    }
}
