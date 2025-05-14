using DevCopilot2.Domain.DTOs.Data;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.DTOs.Imports
{

    public class ImportProjectEnumsFromLocationDto
    {
        [Display(Name = "Project")]
        //[Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        public int? ProjectId { get; set; }

        [Display(Name = "Enums")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        public List<CreateProjectEnumDto> ProjectEnumsList { get; set; } = new List<CreateProjectEnumDto>();

        [Display(Name = "PhysicalLocation")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        public string PhysicalLocation { get; set; } = null!;

        [Display(Name = "AuthorId")]
        public int AuthorId { get; set; } = 1;

        [Display(Name = "AddDefaultLanguageValues")]
        public bool AddDefaultLanguageValues { get; set; }

    }

    public class ImportEntitiesFromDbDto
    {
        [Display(Name = "ConnectionString")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        public ConnectionStringListDto ConnectionString { get; set; } = new ConnectionStringListDto();

        [Display(Name = "ExcludePropertiesNamesList")]
        public List<string> ExcludePropertiesNamesList { get; set; } = new List<string>() { "Id", "EditCounts", "LatestEditDate", "CreateDate", "DeletedOn", "IsDelete" };

        [Display(Name = "ProjectId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        public int ProjectId { get; set; }

        [Display(Name = "AddDefaultValueLanguages")]
        public bool AddDefaultValueLanguages { get; set; }

        [Display(Name = "DefaultAreas")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        public List<CreateEntitySelectedProjectAreaDto> DefaultAreasList { get; set; } = new List<CreateEntitySelectedProjectAreaDto>();

        [Display(Name = "EntitiesList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        public List<CreateEntityDto> EntitiesList { get; set; } = new List<CreateEntityDto>();

    }
}
