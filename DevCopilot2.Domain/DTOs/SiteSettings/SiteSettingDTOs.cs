using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.SiteSettings;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using DevCopilot2.Domain.DTOs.Paging;

namespace DevCopilot2.Domain.DTOs.SiteSettings
{

    public class FilterSiteSettingsDto : BaseFilterDto
    {
        #region properties

		public List<SiteSettingListDto> SiteSettings { get; set; } = new List<SiteSettingListDto>();

        public SortSiteSettingType? SortProperty { get; set; }

        #endregion

        #region methods

        public FilterSiteSettingsDto  SetSiteSettings(List<SiteSettingListDto> siteSettings)
		{
			this.SiteSettings = siteSettings;
			return this;
		}

		public FilterSiteSettingsDto  SetPaging(BasePaging paging)
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

    public class SiteSettingListDto : BaseListDto<long>
    {

        [Display(Name="SMSApiKey")]
        public string SMSApiKey { get; set; } = null!;

        [Display(Name="SMSTemplateName")]
        public string SMSTemplateName { get; set; } = null!;

        [Display(Name="FavIconName")]
        public string FavIconName { get; set; } = null!;

        [Display(Name="SiteName")]
        public string SiteName { get; set; } = null!;

        [Display(Name="LogoImageName")]
        public string LogoImageName { get; set; } = null!;

    }

    public class BaseUpsertSiteSettingDto
    {
        [Display(Name = "SMSApiKey")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string SMSApiKey { get; set; } = null!;

        [Display(Name = "SMSTemplateName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(100, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string SMSTemplateName { get; set; } = null!;

        [Display(Name = "SiteName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(100, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string SiteName { get; set; } = null!;

    }

    public class CreateSiteSettingDto: BaseUpsertSiteSettingDto
    {
        [Display(Name = "FavIconName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public IFormFile FavIconNameFile { get; set; } = null!;

        [Display(Name = "LogoImageName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public IFormFile LogoImageNameFile { get; set; } = null!;

    }

    public class UpdateSiteSettingDto: BaseUpsertSiteSettingDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public long Id { get; set; } = 0;

        [Display(Name = "FavIconName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string FavIconName { get; set; } = null!;

        [Display(Name = "LogoImageName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string LogoImageName { get; set; } = null!;

        [Display(Name = "FavIconName")]

        public IFormFile? FavIconNameFile { get; set; } = null!;

        [Display(Name = "LogoImageName")]

        public IFormFile? LogoImageNameFile { get; set; } = null!;

    }

}
