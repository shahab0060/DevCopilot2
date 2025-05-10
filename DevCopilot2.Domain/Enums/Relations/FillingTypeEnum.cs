using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Relations;

namespace DevCopilot2.Domain.Enums.Relations
{
    public enum FillingTypeEnum
    {
        [Display(ResourceType = typeof(FillingTypeEnumResources), Name = nameof(FillingTypeEnumResources.Input))]
        Input = 0,
        [Display(ResourceType = typeof(FillingTypeEnumResources), Name = nameof(FillingTypeEnumResources.Routing))]
        Routing = 1,
        [Display(ResourceType = typeof(FillingTypeEnumResources), Name = nameof(FillingTypeEnumResources.Custom))]
        Custom = 2,
    }
}
