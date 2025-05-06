using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Projects;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;

namespace DevCopilot2.Domain.DTOs.Projects
{

    public class FilterProjectEnumsDto : BaseFilterDto
    {
        #region properties

		public List<ProjectEnumListDto> ProjectEnums { get; set; } = new List<ProjectEnumListDto>();

        public SortProjectEnumType? SortProperty { get; set; }

                [Display(Name = "ProjectId")]
        public int? ProjectId { get; set; }
        [Display(Name = "AuthorId")]
        public long? AuthorId { get; set; }
        [Display(Name = "EnglishName")]
        public string? EnglishName { get; set; }
        [Display(Name = "FolderName")]
        public string? FolderName { get; set; }

        #endregion

        #region methods

        public FilterProjectEnumsDto  SetProjectEnums(List<ProjectEnumListDto> projectEnums)
		{
			this.ProjectEnums = projectEnums;
			return this;
		}

		public FilterProjectEnumsDto  SetPaging(BasePaging paging)
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

    public class ProjectEnumListDto : BaseListDto<int>
    {

        [Display(Name="ProjectId")]
        public string ProjectTitle { get; set; } = null!;

        [Display(Name="ProjectId")]
        public int? ProjectId { get; set; } 

        [Display(Name="EnglishName")]
        public string EnglishName { get; set; } = null!;

        [Display(Name="FolderName")]
        public string FolderName { get; set; } = null!;

        [Display(Name="AuthorId")]
        public string AuthorPhoneNumber { get; set; } = null!;

        [Display(Name="AuthorId")]
        public long AuthorId { get; set; } 

        [Display(Name="ProjectEnumPropertiesList")]
        public List<ProjectEnumPropertyListDto> ProjectEnumPropertiesList { get; set; } = new List<ProjectEnumPropertyListDto>();

    }

    public class BaseUpsertProjectEnumDto
    {
        [Display(Name = "ProjectId")]

        public int? ProjectId { get; set; } 

        [Display(Name = "EnglishName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string EnglishName { get; set; } = null!;

        [Display(Name = "FolderName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string FolderName { get; set; } = null!;

        [Display(Name = "AuthorId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public long AuthorId { get; set; } = 0;

    }

    public class CreateProjectEnumDto: BaseUpsertProjectEnumDto
    {
        [Display(Name = "ProjectEnumPropertiesList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<CreateProjectEnumPropertyDto> ProjectEnumPropertiesList { get; set; } =new List<CreateProjectEnumPropertyDto>();

    }

    public class UpdateProjectEnumDto: BaseUpsertProjectEnumDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

        [Display(Name = "ProjectEnumPropertiesList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<UpdateProjectEnumPropertyDto> ProjectEnumPropertiesList { get; set; } =new List<UpdateProjectEnumPropertyDto>();

    }

}
