
using System;

using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Common;

namespace DevCopilot2.Domain.Enums.Common
{

    public enum BaseSiteSortType
    {
                [Display(ResourceType = typeof(BaseSiteSortTypeResources), Name = nameof(BaseSiteSortTypeResources.Newest))]
        Newest = 1,
        [Display(ResourceType = typeof(BaseSiteSortTypeResources), Name = nameof(BaseSiteSortTypeResources.MostViewed))]
        MostViewed = 2,
    }

}
