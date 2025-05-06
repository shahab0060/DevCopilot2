
using System;

using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Common;

namespace DevCopilot2.Domain.Enums.Common
{

    public enum DeviceTypeEnum
    {
                [Display(ResourceType = typeof(DeviceTypeEnumResources), Name = nameof(DeviceTypeEnumResources.Desktop))]
        Desktop = 1,
        [Display(ResourceType = typeof(DeviceTypeEnumResources), Name = nameof(DeviceTypeEnumResources.Tablet))]
        Tablet = 2,
        [Display(ResourceType = typeof(DeviceTypeEnumResources), Name = nameof(DeviceTypeEnumResources.Mobile))]
        Mobile = 3,
    }

}
