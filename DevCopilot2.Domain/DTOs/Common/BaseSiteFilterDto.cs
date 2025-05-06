using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.Enums.Common;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.DTOs.Common
{
    public class BaseSiteFilterDto : BasePaging
    {
        [Display(Name = $"مرتب سازی")]
        public BaseSiteSortType BaseSiteSortType { get; set; }

        public BasePaginationDto ToBasePagination()
            => new BasePaginationDto(TakeEntity, SkipEntity);
    }
}
