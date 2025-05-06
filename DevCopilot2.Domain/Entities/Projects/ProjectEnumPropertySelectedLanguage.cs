using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using DevCopilot2.Domain.Entities.Languages;

namespace DevCopilot2.Domain.Entities.Projects
{
    public partial class ProjectEnumPropertySelectedLanguage : EntityId<int>
    {

        #region properties

        [Display(Name = "LanguageId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int LanguageId { get; set; } 

        [Display(Name = "ProjectEnumPropertyId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int ProjectEnumPropertyId { get; set; } 

        [Display(Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(450, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string Title { get; set; } = null!;

        #endregion

        #region relations

        [ForeignKey(nameof(LanguageId))]
        public virtual Language Language { get; set; } = null!;

        [ForeignKey(nameof(ProjectEnumPropertyId))]
        public virtual ProjectEnumProperty ProjectEnumProperty { get; set; } = null!;

        #endregion
    }
}
