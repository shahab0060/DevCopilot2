
using System;

using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Common;

namespace DevCopilot2.Domain.Enums.Common
{

    public enum BaseSortEntityType
    {
                [Display(ResourceType = typeof(BaseSortEntityTypeResources), Name = nameof(BaseSortEntityTypeResources.Default))]
        Default = 1,
        [Display(ResourceType = typeof(BaseSortEntityTypeResources), Name = nameof(BaseSortEntityTypeResources.Newest))]
        Newest = 2,
        [Display(ResourceType = typeof(BaseSortEntityTypeResources), Name = nameof(BaseSortEntityTypeResources.LatestUpdate))]
        LatestUpdate = 3,
    }

}
