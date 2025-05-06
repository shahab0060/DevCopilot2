using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Projects;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;

namespace DevCopilot2.Domain.DTOs.Projects
{

    public class FilterProjectEnumPropertySelectedLanguagesDto : BaseFilterDto
    {
        #region properties

		public List<ProjectEnumPropertySelectedLanguageListDto> ProjectEnumPropertySelectedLanguages { get; set; } = new List<ProjectEnumPropertySelectedLanguageListDto>();

        public SortProjectEnumPropertySelectedLanguageType? SortProperty { get; set; }

                [Display(Name = "LanguageId")]
        public int? LanguageId { get; set; }
        [Display(Name = "ProjectEnumPropertyId")]
        public int? ProjectEnumPropertyId { get; set; }

        #endregion

        #region methods

        public FilterProjectEnumPropertySelectedLanguagesDto  SetProjectEnumPropertySelectedLanguages(List<ProjectEnumPropertySelectedLanguageListDto> projectEnumPropertySelectedLanguages)
		{
			this.ProjectEnumPropertySelectedLanguages = projectEnumPropertySelectedLanguages;
			return this;
		}

		public FilterProjectEnumPropertySelectedLanguagesDto  SetPaging(BasePaging paging)
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

    public class ProjectEnumPropertySelectedLanguageListDto : BaseListDto<int>
    {

        [Display(Name="LanguageId")]
        public string LanguageName { get; set; } = null!;

        [Display(Name="LanguageId")]
        public int LanguageId { get; set; } 

        [Display(Name="ProjectEnumPropertyId")]
        public string ProjectEnumPropertyName { get; set; } = null!;

        [Display(Name="ProjectEnumPropertyId")]
        public int ProjectEnumPropertyId { get; set; } 

        [Display(Name="Title")]
        public string Title { get; set; } = null!;

    }

    public class BaseUpsertProjectEnumPropertySelectedLanguageDto
    {
        [Display(Name = "LanguageId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int LanguageId { get; set; } 

        [Display(Name = "ProjectEnumPropertyId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int ProjectEnumPropertyId { get; set; } = 0;

        [Display(Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(450, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string Title { get; set; } = null!;

    }

    public class CreateProjectEnumPropertySelectedLanguageDto: BaseUpsertProjectEnumPropertySelectedLanguageDto
    {

    }

    public class UpdateProjectEnumPropertySelectedLanguageDto: BaseUpsertProjectEnumPropertySelectedLanguageDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

    }

}
