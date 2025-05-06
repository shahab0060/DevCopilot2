using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevCopilot2.Domain.Entities.Projects
{
    public partial class ProjectEnumProperty : EntityId<int>
    {

        #region properties

        [Display(Name = "ProjectEnumId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int ProjectEnumId { get; set; } 

        [Display(Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Name { get; set; } = null!;

        [Display(Name = "Order")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Order { get; set; } 

        #endregion

        #region relations

        public virtual ICollection<ProjectEnumPropertySelectedLanguage> ProjectEnumPropertySelectedLanguages { get; set; } = new List<ProjectEnumPropertySelectedLanguage>();

        [ForeignKey(nameof(ProjectEnumId))]
        public virtual ProjectEnum ProjectEnum { get; set; } = null!;

        #endregion
    }
}
