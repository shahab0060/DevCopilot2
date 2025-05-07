using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Projects;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;

namespace DevCopilot2.Domain.DTOs.Projects
{

    public class FilterProjectEnumPropertiesDto : BaseFilterDto
    {
        #region properties

        public List<ProjectEnumPropertyListDto> ProjectEnumProperties { get; set; } = new List<ProjectEnumPropertyListDto>();

        public SortProjectEnumPropertyType? SortProperty { get; set; }

        [Display(Name = "ProjectEnumId")]
        public int? ProjectEnumId { get; set; }

        #endregion

        #region methods

        public FilterProjectEnumPropertiesDto SetProjectEnumProperties(List<ProjectEnumPropertyListDto> projectEnumProperties)
        {
            this.ProjectEnumProperties = projectEnumProperties;
            return this;
        }

        public FilterProjectEnumPropertiesDto SetPaging(BasePaging paging)
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

    public class ProjectEnumPropertyListDto : BaseListDto<int>
    {

        [Display(Name = "ProjectEnumId")]
        public string ProjectEnumEnglishName { get; set; } = null!;

        [Display(Name = "ProjectEnumId")]
        public int ProjectEnumId { get; set; }

        [Display(Name = "ProjectId")]
        public int ProjectId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        [Display(Name = "Order")]
        public int Order { get; set; }

        [Display(Name = "ProjectEnumPropertySelectedLanguagesList")]
        public List<ProjectEnumPropertySelectedLanguageListDto> ProjectEnumPropertySelectedLanguagesList { get; set; } = new List<ProjectEnumPropertySelectedLanguageListDto>();

    }

    public class BaseUpsertProjectEnumPropertyDto
    {
        [Display(Name = "ProjectEnumId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int ProjectEnumId { get; set; } = 0;

        [Display(Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Name { get; set; } = null!;

        [Display(Name = "Order")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Order { get; set; }

    }

    public class CreateProjectEnumPropertyDto : BaseUpsertProjectEnumPropertyDto
    {
        [Display(Name = "ProjectEnumPropertySelectedLanguagesList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<CreateProjectEnumPropertySelectedLanguageDto> ProjectEnumPropertySelectedLanguagesList { get; set; } = new List<CreateProjectEnumPropertySelectedLanguageDto>();

    }

    public class UpdateProjectEnumPropertyDto : BaseUpsertProjectEnumPropertyDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

        [Display(Name = "ProjectEnumPropertySelectedLanguagesList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<UpdateProjectEnumPropertySelectedLanguageDto> ProjectEnumPropertySelectedLanguagesList { get; set; } = new List<UpdateProjectEnumPropertySelectedLanguageDto>();

    }

}
