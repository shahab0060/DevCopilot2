using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Entities.Entities;
using DevCopilot2.Domain.Entities.Templates;
using System.ComponentModel.DataAnnotations.Schema;
using DevCopilot2.Domain.Entities.Users;
using DevCopilot2.Domain.Enums.Project;

namespace DevCopilot2.Domain.Entities.Projects
{
    public partial class Project : EntityId<int>
    {

        #region properties

        [Display(Name = "EnglishName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(250, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string EnglishName { get; set; } = null!;

        [Display(Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Title { get; set; } = null!;

        [Display(Name = "Location")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Location { get; set; } = null!;

        [Display(Name = "Architecture")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public ArchitectureType Architecture { get; set; } 

        [Display(Name = "AuthorId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public long AuthorId { get; set; } 

        [Display(Name = "ReactProjectLocation")]

        public string? ReactProjectLocation { get; set; } = null!;

        #endregion

        #region relations

        public virtual ICollection<Entity> Entities { get; set; } = new List<Entity>();

        public virtual ICollection<ProjectEnum> ProjectEnums { get; set; } = new List<ProjectEnum>();

        public virtual ICollection<Template> Templates { get; set; } = new List<Template>();

        public virtual ICollection<ProjectArea> ProjectAreas { get; set; } = new List<ProjectArea>();

        public virtual ICollection<ProjectSelectedLanguage> ProjectSelectedLanguages { get; set; } = new List<ProjectSelectedLanguage>();

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; } = null!;

        #endregion
    }
}
