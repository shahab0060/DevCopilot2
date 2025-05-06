using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Paging;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.DTOs.Crawlers
{
    public class FilterInstagramPostsDto : BaseFilterDto
    {
        #region Properties

        public List<InstagramPostListDto> InstagramPosts { get; set; }

        #endregion

        #region methods

        public FilterInstagramPostsDto SetInstagramPosts(List<InstagramPostListDto> InstagramPosts)
        {
            this.InstagramPosts = InstagramPosts;
            return this;
        }

        public FilterInstagramPostsDto SetPaging(BasePaging paging)
        {
            PageId = Pager.NormalizePageId(paging.PageId, paging.PageCount);
            AllEntitiesCount = paging.AllEntitiesCount;
            StartPage = paging.StartPage;
            EndPage = paging.EndPage;
            HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            TakeEntity = paging.TakeEntity;
            SkipEntity = paging.SkipEntity;
            PageCount = paging.PageCount;
            return this;
        }

        #endregion
    }
    public class InstagramPostListDto : BaseListDto<long>
    {
        public InstagramPostListDto()
        {
            ImageUrls = new List<string>();
            Comments = new List<string>();
        }
        public List<string> ImageUrls { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public List<string> Comments { get; set; }
        public string Url { get; set; }
    }

    public class ImportFromInstagramDto
    {
        [Display(Name = "لینک پیچ اینستاگرام")]
        [Required]
        [DataType(DataType.Url)]
        public string PageUrl { get; set; }
    }
}
