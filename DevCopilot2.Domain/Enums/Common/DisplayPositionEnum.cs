
using System;

using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Common;

namespace DevCopilot2.Domain.Enums.Common
{

    public enum DisplayPosition
    {
                [Display(ResourceType = typeof(DisplayPositionResources), Name = nameof(DisplayPositionResources.Header))]
        Header = 0,
        [Display(ResourceType = typeof(DisplayPositionResources), Name = nameof(DisplayPositionResources.Footer))]
        Footer = 1,
    }

}
