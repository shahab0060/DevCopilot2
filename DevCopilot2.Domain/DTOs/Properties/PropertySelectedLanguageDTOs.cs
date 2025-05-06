using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Properties;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;

namespace DevCopilot2.Domain.DTOs.Properties
{

    public class FilterPropertySelectedLanguagesDto : BaseFilterDto
    {
        #region properties

		public List<PropertySelectedLanguageListDto> PropertySelectedLanguages { get; set; } = new List<PropertySelectedLanguageListDto>();

        public SortPropertySelectedLanguageType? SortProperty { get; set; }

                [Display(Name = "PropertyId")]
        public int? PropertyId { get; set; }
        [Display(Name = "LanguageId")]
        public int? LanguageId { get; set; }

        #endregion

        #region methods

        public FilterPropertySelectedLanguagesDto  SetPropertySelectedLanguages(List<PropertySelectedLanguageListDto> propertySelectedLanguages)
		{
			this.PropertySelectedLanguages = propertySelectedLanguages;
			return this;
		}

		public FilterPropertySelectedLanguagesDto  SetPaging(BasePaging paging)
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

    public class PropertySelectedLanguageListDto : BaseListDto<int>
    {

        [Display(Name="PropertyId")]
        public string PropertyName { get; set; } = null!;

        [Display(Name="PropertyId")]
        public int PropertyId { get; set; } 

        [Display(Name="LanguageId")]
        public string LanguageName { get; set; } = null!;

        [Display(Name="LanguageId")]
        public int LanguageId { get; set; } 

        [Display(Name="Title")]
        public string Title { get; set; } = null!;

    }

    public class BaseUpsertPropertySelectedLanguageDto
    {
        [Display(Name = "PropertyId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int PropertyId { get; set; } = 0;

        [Display(Name = "LanguageId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int LanguageId { get; set; } 

        [Display(Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(250, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string Title { get; set; } = null!;

    }

    public class CreatePropertySelectedLanguageDto: BaseUpsertPropertySelectedLanguageDto
    {

    }

    public class UpdatePropertySelectedLanguageDto: BaseUpsertPropertySelectedLanguageDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

    }

}
