using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.DataTypes;

namespace DevCopilot2.Domain.Enums.DataTypes
{
    public enum IdTypeEnum
    {
        [Display(ResourceType = typeof(IdTypeEnumResources), Name = nameof(IdTypeEnumResources.IdType))]
        IdType = 1,
    }
}
