
using System;

using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Common;

namespace DevCopilot2.Domain.Enums.Common
{

    public enum SortType
    {
                [Display(ResourceType = typeof(SortTypeResources), Name = nameof(SortTypeResources.Descending))]
        Descending = 1,
        [Display(ResourceType = typeof(SortTypeResources), Name = nameof(SortTypeResources.Ascending))]
        Ascending = 2,
    }

}
