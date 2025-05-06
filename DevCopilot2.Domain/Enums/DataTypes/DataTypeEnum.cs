using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.DataTypes;

namespace DevCopilot2.Domain.Enums.DataTypes
{
    public enum DataTypeEnum
    {
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.String))]
        String,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Int))]
        Int,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Long))]
        Long,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Bool))]
        Bool,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Guid))]
        Guid,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Enum))]
        Enum,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.DateTime))]
        DateTime,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Decimal))]
        Decimal,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Double))]
        Double,
        [Display(ResourceType = typeof(DataTypeEnumResources), Name = nameof(DataTypeEnumResources.Byte))]
        Byte,
    }
}
