using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.DataTypes;

namespace DevCopilot2.Domain.Enums.DataTypes
{
    public enum DataTypeEnum
    {
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.DataType))]
        DataType = 1,
    }
}
