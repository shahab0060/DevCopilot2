using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.Enums.Projects;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.DTOs.Projects
{

    public class FilterProjectSelectedLanguagesDto : BaseFilterDto
    {
        #region properties

		public List<ProjectSelectedLanguageListDto> ProjectSelectedLanguages { get; set; } = new List<ProjectSelectedLanguageListDto>();

        public SortProjectSelectedLanguageType? SortProperty { get; set; }

                [Display(Name = "ProjectId")]
        public int? ProjectId { get; set; }
        [Display(Name = "LanguageId")]
        public int? LanguageId { get; set; }

        #endregion

        #region methods

        public FilterProjectSelectedLanguagesDto  SetProjectSelectedLanguages(List<ProjectSelectedLanguageListDto> projectSelectedLanguages)
		{
			this.ProjectSelectedLanguages = projectSelectedLanguages;
			return this;
		}

		public FilterProjectSelectedLanguagesDto  SetPaging(BasePaging paging)
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

    public class ProjectSelectedLanguageListDto : BaseListDto<int>
    {

        [Display(Name="ProjectId")]
        public string ProjectTitle { get; set; } = null!;

        [Display(Name="ProjectId")]
        public int ProjectId { get; set; } 

        [Display(Name="LanguageId")]
        public string LanguageName { get; set; } = null!;

        [Display(Name = "LanguageCulture")]
        public string LanguageCulture { get; set; } = null!;

        [Display(Name="LanguageId")]
        public int LanguageId { get; set; } 

    }

    public class BaseUpsertProjectSelectedLanguageDto
    {
        [Display(Name = "ProjectId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int ProjectId { get; set; } = 0;

        [Display(Name = "LanguageId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int LanguageId { get; set; } 

    }

    public class CreateProjectSelectedLanguageDto: BaseUpsertProjectSelectedLanguageDto
    {

    }

    public class UpdateProjectSelectedLanguageDto: BaseUpsertProjectSelectedLanguageDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

    }

}
