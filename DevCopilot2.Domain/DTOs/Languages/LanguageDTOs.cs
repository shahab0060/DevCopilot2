using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Languages;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;

namespace DevCopilot2.Domain.DTOs.Languages
{

    public class FilterLanguagesDto : BaseFilterDto
    {
        #region properties

		public List<LanguageListDto> Languages { get; set; } = new List<LanguageListDto>();

        public SortLanguageType? SortProperty { get; set; }

        #endregion

        #region methods

        public FilterLanguagesDto  SetLanguages(List<LanguageListDto> languages)
		{
			this.Languages = languages;
			return this;
		}

		public FilterLanguagesDto  SetPaging(BasePaging paging)
		{
			PageId = paging.PageId;
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

    public class LanguageListDto : BaseListDto<int>
    {

        [Display(Name="Name")]
        public string Name { get; set; } = null!;

        [Display(Name="Culture")]
        public string Culture { get; set; } = null!;

        [Display(Name="DefaultPluralSuffix")]
        public string DefaultPluralSuffix { get; set; } = null!;

    }

    public class BaseUpsertLanguageDto
    {
        [Display(Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(150, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string Name { get; set; } = null!;

        [Display(Name = "Culture")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(150, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string Culture { get; set; } = null!;

        [Display(Name = "DefaultPluralSuffix")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string DefaultPluralSuffix { get; set; } = null!;

    }

    public class CreateLanguageDto: BaseUpsertLanguageDto
    {

    }

    public class UpdateLanguageDto: BaseUpsertLanguageDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

    }

}
