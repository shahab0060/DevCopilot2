using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Projects;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;

namespace DevCopilot2.Domain.DTOs.Projects
{

    public class FilterProjectAreasDto : BaseFilterDto
    {
        #region properties

		public List<ProjectAreaListDto> ProjectAreas { get; set; } = new List<ProjectAreaListDto>();

        public SortProjectAreaType? SortProperty { get; set; }

                [Display(Name = "ProjectId")]
        public int? ProjectId { get; set; }
        [Display(Name = "TemplateId")]
        public int? TemplateId { get; set; }

        #endregion

        #region methods

        public FilterProjectAreasDto  SetProjectAreas(List<ProjectAreaListDto> projectAreas)
		{
			this.ProjectAreas = projectAreas;
			return this;
		}

		public FilterProjectAreasDto  SetPaging(BasePaging paging)
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

    public class ProjectAreaListDto : BaseListDto<int>
    {

        [Display(Name="ProjectId")]
        public string ProjectTitle { get; set; } = null!;

        [Display(Name="ProjectId")]
        public int ProjectId { get; set; } 

        [Display(Name="EnglishName")]
        public string EnglishName { get; set; } = null!;

        [Display(Name="Title")]
        public string Title { get; set; } = null!;

        [Display(Name="TemplateId")]
        public string TemplateTitle { get; set; } = null!;

        [Display(Name="TemplateId")]
        public int TemplateId { get; set; } 

    }

    public class BaseUpsertProjectAreaDto
    {
        [Display(Name = "ProjectId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int ProjectId { get; set; } = 0;

        [Display(Name = "EnglishName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string EnglishName { get; set; } = null!;

        [Display(Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Title { get; set; } = null!;

        [Display(Name = "TemplateId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int TemplateId { get; set; } 

    }

    public class CreateProjectAreaDto: BaseUpsertProjectAreaDto
    {

    }

    public class UpdateProjectAreaDto: BaseUpsertProjectAreaDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

    }

}
