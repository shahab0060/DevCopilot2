using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.DataTypes;

namespace DevCopilot2.Domain.Enums.DataTypes
{
    public enum DataTypeEnum
    {
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.String))]
        String = 0,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Int))]
        Int = 1,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Long))]
        Long = 2,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Bool))]
        Bool = 3,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Guid))]
        Guid = 4,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Enum))]
        Enum = 5,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.DateTime))]
        DateTime = 6,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Decimal))]
        Decimal = 7,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Double))]
        Double = 8,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Byte))]
        Byte = 9,
    }
}
