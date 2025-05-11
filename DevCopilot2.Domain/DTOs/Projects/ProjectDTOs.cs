using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Projects;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.Enums.Project;

namespace DevCopilot2.Domain.DTOs.Projects
{

    public class FilterProjectsDto : BaseFilterDto
    {
        #region properties

        public List<ProjectListDto> Projects { get; set; } = new List<ProjectListDto>();

        public SortProjectType? SortProperty { get; set; }

        [Display(Name = "AuthorId")]
        public long? AuthorId { get; set; }

        #endregion

        #region methods

        public FilterProjectsDto SetProjects(List<ProjectListDto> projects)
        {
            this.Projects = projects;
            return this;
        }

        public FilterProjectsDto SetPaging(BasePaging paging)
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

    public class ProjectListDto : BaseListDto<int>
    {

        [Display(Name = "UseResources")]
        public bool UseResources { get; set; } = true;

        [Display(Name = "UseFluent")]
        public bool UseFluent { get; set; } = false;

        [Display(Name = "EnglishName")]
        public string EnglishName { get; set; } = null!;

        [Display(Name = "Title")]
        public string Title { get; set; } = null!;

        [Display(Name = "Location")]
        public string Location { get; set; } = null!;

        [Display(Name = "Architecture")]
        public ArchitectureType Architecture { get; set; }

        [Display(Name = "AuthorId")]
        public string AuthorPhoneNumber { get; set; } = null!;

        [Display(Name = "AuthorId")]
        public long AuthorId { get; set; }

        [Display(Name = "ReactProjectLocation")]
        public string? ReactProjectLocation { get; set; } = null!;

        [Display(Name = "ProjectAreasList")]
        public List<ProjectAreaListDto> ProjectAreasList { get; set; } = new List<ProjectAreaListDto>();

        [Display(Name = "ProjectSelectedLanguagesList")]
        public List<ProjectSelectedLanguageListDto> ProjectSelectedLanguagesList { get; set; } = new List<ProjectSelectedLanguageListDto>();

    }

    public class BaseUpsertProjectDto
    {
        [Display(Name = "EnglishName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(250, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string EnglishName { get; set; } = null!;

        [Display(Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Title { get; set; } = null!;

        [Display(Name = "Location")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Location { get; set; } = "some location";

        [Display(Name = "Architecture")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public ArchitectureType Architecture { get; set; }

        [Display(Name = "AuthorId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public long AuthorId { get; set; } = 0;

        [Display(Name = "ReactProjectLocation")]

        public string? ReactProjectLocation { get; set; } = null!;

    }

    public class CreateProjectDto : BaseUpsertProjectDto
    {
        [Display(Name = "ProjectAreasList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<CreateProjectAreaDto> ProjectAreasList { get; set; } = new List<CreateProjectAreaDto>();

        [Display(Name = "ProjectSelectedLanguagesList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<CreateProjectSelectedLanguageDto> ProjectSelectedLanguagesList { get; set; } = new List<CreateProjectSelectedLanguageDto>();

    }

    public class UpdateProjectDto : BaseUpsertProjectDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

        [Display(Name = "ProjectAreasList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<UpdateProjectAreaDto> ProjectAreasList { get; set; } = new List<UpdateProjectAreaDto>();

        [Display(Name = "ProjectSelectedLanguagesList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<UpdateProjectSelectedLanguageDto> ProjectSelectedLanguagesList { get; set; } = new List<UpdateProjectSelectedLanguageDto>();

    }

}
