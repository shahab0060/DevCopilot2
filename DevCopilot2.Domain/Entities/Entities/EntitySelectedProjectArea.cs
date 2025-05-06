using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using DevCopilot2.Domain.Entities.Projects;

namespace DevCopilot2.Domain.Entities.Entities
{
    public partial class EntitySelectedProjectArea : EntityId<int>
    {

        #region properties

        [Display(Name = "EntityId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int EntityId { get; set; } 

        [Display(Name = "ProjectAreaId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int ProjectAreaId { get; set; } 

        [Display(Name = "HasIndex")]

        public bool HasIndex { get; set; } 

        [Display(Name = "HasCreate")]

        public bool HasCreate { get; set; } 

        [Display(Name = "HasUpdate")]

        public bool HasUpdate { get; set; } 

        [Display(Name = "HasDelete")]

        public bool HasDelete { get; set; } 

        [Display(Name = "HasApi")]

        public bool HasApi { get; set; } 

        [Display(Name = "HasWeb")]

        public bool HasWeb { get; set; } 

        #endregion

        #region relations

        public virtual ICollection<EntitySelectedProjectAreaSelectedFilter> EntitySelectedProjectAreaSelectedFilters { get; set; } = new List<EntitySelectedProjectAreaSelectedFilter>();

        [ForeignKey(nameof(EntityId))]
        public virtual Entity Entity { get; set; } = null!;

        [ForeignKey(nameof(ProjectAreaId))]
        public virtual ProjectArea ProjectArea { get; set; } = null!;

        #endregion
    }
}
