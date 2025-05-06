
using System;

using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Common;

namespace DevCopilot2.Domain.Enums.Common
{

    public enum BaseFilterEntityType
    {
                [Display(ResourceType = typeof(BaseFilterEntityTypeResources), Name = nameof(BaseFilterEntityTypeResources.All))]
        All = 1,
        [Display(ResourceType = typeof(BaseFilterEntityTypeResources), Name = nameof(BaseFilterEntityTypeResources.Deleted))]
        Deleted = 2,
        [Display(ResourceType = typeof(BaseFilterEntityTypeResources), Name = nameof(BaseFilterEntityTypeResources.NotDeleted))]
        NotDeleted = 3,
    }

}
