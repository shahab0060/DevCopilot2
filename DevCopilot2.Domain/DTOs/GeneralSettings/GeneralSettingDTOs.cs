using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.GeneralSettings;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;

namespace DevCopilot2.Domain.DTOs.GeneralSettings
{

    public class FilterGeneralSettingsDto : BaseFilterDto
    {
        #region properties

		public List<GeneralSettingListDto> GeneralSettings { get; set; } = new List<GeneralSettingListDto>();

        public SortGeneralSettingType? SortProperty { get; set; }

        #endregion

        #region methods

        public FilterGeneralSettingsDto  SetGeneralSettings(List<GeneralSettingListDto> generalSettings)
		{
			this.GeneralSettings = generalSettings;
			return this;
		}

		public FilterGeneralSettingsDto  SetPaging(BasePaging paging)
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

    public class GeneralSettingListDto : BaseListDto<int>
    {

        [Display(Name="DefaultSolutionName")]
        public string DefaultSolutionName { get; set; } = null!;

        [Display(Name="DefaultSolutionLocation")]
        public string DefaultSolutionLocation { get; set; } = null!;

        [Display(Name="DefaultReactJsSolutionName")]
        public string? DefaultReactJsSolutionName { get; set; } = null!;

        [Display(Name="DefaultReactSolutionLocation")]
        public string? DefaultReactSolutionLocation { get; set; } = null!;

    }

    public class BaseUpsertGeneralSettingDto
    {
        [Display(Name = "DefaultSolutionName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string DefaultSolutionName { get; set; } = null!;

        [Display(Name = "DefaultSolutionLocation")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string DefaultSolutionLocation { get; set; } = null!;

        [Display(Name = "DefaultReactJsSolutionName")]

        public string? DefaultReactJsSolutionName { get; set; } = null!;

        [Display(Name = "DefaultReactSolutionLocation")]

        public string? DefaultReactSolutionLocation { get; set; } = null!;

    }

    public class CreateGeneralSettingDto: BaseUpsertGeneralSettingDto
    {

    }

    public class UpdateGeneralSettingDto: BaseUpsertGeneralSettingDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

    }

}
