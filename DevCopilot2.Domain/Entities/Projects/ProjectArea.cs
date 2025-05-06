using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Entities.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using DevCopilot2.Domain.Entities.Templates;

namespace DevCopilot2.Domain.Entities.Projects
{
    public partial class ProjectArea : EntityId<int>
    {

        #region properties

        [Display(Name = "ProjectId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int ProjectId { get; set; } 

        [Display(Name = "EnglishName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string EnglishName { get; set; } = null!;

        [Display(Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Title { get; set; } = null!;

        [Display(Name = "TemplateId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int TemplateId { get; set; } 

        #endregion

        #region relations

        public virtual ICollection<EntitySelectedProjectArea> EntitySelectedProjectAreas { get; set; } = new List<EntitySelectedProjectArea>();

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; } = null!;

        [ForeignKey(nameof(TemplateId))]
        public virtual Template Template { get; set; } = null!;

        #endregion
    }
}
