using DevCopilot2.Domain.DTOs.Paging;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;

namespace DevCopilot2.Domain.DTOs.Common
{
    public class BaseFilterDto : BasePaging
    {
        [Display(ResourceType = typeof(BaseListDtoResources), Name = nameof(BaseListDtoResources.BaseSortEntityType))]
        public BaseSortEntityType BaseSortEntityType { get; set; }

        [Display(ResourceType = typeof(BaseListDtoResources), Name = nameof(BaseListDtoResources.SortType))]
        public SortType SortType { get; set; }

        [Display(ResourceType = typeof(BaseListDtoResources), Name = nameof(BaseListDtoResources.Search))]
        public string? Search { get; set; }

        [Display(ResourceType = typeof(BaseListDtoResources), Name = nameof(BaseListDtoResources.FromDate))]
        public string? FromDate { get; set; }

        [Display(ResourceType = typeof(BaseListDtoResources), Name = nameof(BaseListDtoResources.ToDate))]
        public string? ToDate { get; set; }

    }
}
