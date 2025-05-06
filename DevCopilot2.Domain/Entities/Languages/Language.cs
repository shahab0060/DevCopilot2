using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Entities.Projects;
using DevCopilot2.Domain.Entities.Entities;
using DevCopilot2.Domain.Entities.Properties;

namespace DevCopilot2.Domain.Entities.Languages
{
    public partial class Language : EntityId<int>
    {

        #region properties

        [Display(Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(150, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string Name { get; set; } = null!;

        [Display(Name = "Culture")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(150, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string Culture { get; set; } = null!;

        [Display(Name = "DefaultPluralSuffix")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string DefaultPluralSuffix { get; set; } = null!;

        #endregion

        #region relations

        public virtual ICollection<ProjectEnumPropertySelectedLanguage> ProjectEnumPropertySelectedLanguages { get; set; } = new List<ProjectEnumPropertySelectedLanguage>();

        public virtual ICollection<ProjectSelectedLanguage> ProjectSelectedLanguages { get; set; } = new List<ProjectSelectedLanguage>();

        public virtual ICollection<EntitySelectedLanguage> EntitySelectedLanguages { get; set; } = new List<EntitySelectedLanguage>();

        public virtual ICollection<PropertySelectedLanguage> PropertySelectedLanguages { get; set; } = new List<PropertySelectedLanguage>();

        #endregion
    }
}
