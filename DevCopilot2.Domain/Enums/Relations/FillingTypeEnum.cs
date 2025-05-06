using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Relations;

namespace DevCopilot2.Domain.Enums.Relations
{
    public enum FillingTypeEnum
    {
        [Display(ResourceType = typeof(FillingTypeEnumResources), Name = nameof(FillingTypeEnumResources.FillingType))]
        FillingType = 1,
    }
}
