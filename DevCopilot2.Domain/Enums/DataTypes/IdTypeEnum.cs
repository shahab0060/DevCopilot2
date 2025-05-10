using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.DataTypes;

namespace DevCopilot2.Domain.Enums.DataTypes
{
    public enum IdTypeEnum
    {
        [Display(ResourceType = typeof(IdTypeEnumResources), Name = nameof(IdTypeEnumResources.Long))]
        Long = 0,
        [Display(ResourceType = typeof(IdTypeEnumResources), Name = nameof(IdTypeEnumResources.Int))]
        Int = 1,
        [Display(ResourceType = typeof(IdTypeEnumResources), Name = nameof(IdTypeEnumResources.Byte))]
        Byte = 2,
        [Display(ResourceType = typeof(IdTypeEnumResources), Name = nameof(IdTypeEnumResources.Guid))]
        Guid = 3,
    }
}
