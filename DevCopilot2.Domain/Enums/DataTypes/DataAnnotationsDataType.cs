using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.DataTypes;

namespace DevCopilot2.Domain.Enums.DataTypes
{
    public enum DataAnnotationsDataType
    {
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.DataAnnotationDataType))]
        DataAnnotationDataType = 1,
    }
}
