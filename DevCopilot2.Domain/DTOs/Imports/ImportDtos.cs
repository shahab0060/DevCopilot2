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
}
