using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.DTOs.Common;
using DevCopilot2.Domain.Resources.Enums.Common;

namespace DevCopilot2.Domain.DTOs.Common
{
    public class BaseListDto<T> where T : struct
    {
        [Display(ResourceType = typeof(BaseListDtoResources), Name = nameof(BaseListDtoResources.Id))]
        //[Display(Name ="Id")]
        public T Id { get; set; }

        //[Display(Name ="CreateDate")]
        [Display(ResourceType = typeof(BaseListDtoResources), Name = nameof(BaseListDtoResources.CreateDate))]
        public DateTime CreateDate { get; set; }

        //[Display(Name = "LatestEditDate")]
        [Display(ResourceType = typeof(BaseListDtoResources), Name = nameof(BaseListDtoResources.LatestEditDate))]
        public DateTime LatestEditDate { get; set; }

        //[Display(Name = "EditCounts")]
        [Display(ResourceType = typeof(BaseListDtoResources), Name = nameof(BaseListDtoResources.EditCounts))]
        public int EditCounts { get; set; }

        //[Display(Name = "AuthorId")]
        [Display(ResourceType = typeof(BaseListDtoResources), Name = nameof(BaseListDtoResources.AuthorId))]
        public long AuthorId { get; set; }

        //[Display(Name = "AuthorName")]
        [Display(ResourceType = typeof(BaseListDtoResources), Name = nameof(BaseListDtoResources.AuthorName))]
        public string AuthorName { get; set; }

    }
}
