using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Relations;

namespace DevCopilot2.Domain.Enums.Relations
{
    public enum FillingTypeEnum
    {
        [Display(ResourceType = typeof(FillingTypeEnumResources), Name = nameof(FillingTypeEnumResources.Input))]
        Input,

        [Display(ResourceType = typeof(FillingTypeEnumResources), Name = nameof(FillingTypeEnumResources.Routing))]
        Routing,

        [Display(ResourceType = typeof(FillingTypeEnumResources), Name = nameof(FillingTypeEnumResources.Custom))]
        Custom
    }
}
