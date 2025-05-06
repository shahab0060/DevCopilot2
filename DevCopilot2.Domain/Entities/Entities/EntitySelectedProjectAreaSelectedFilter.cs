using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using DevCopilot2.Domain.Entities.Properties;

namespace DevCopilot2.Domain.Entities.Entities
{
    public partial class EntitySelectedProjectAreaSelectedFilter : EntityId<int>
    {

        #region properties

        [Display(Name = "EntitySelectedProjectAreaId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int EntitySelectedProjectAreaId { get; set; } 

        [Display(Name = "PropertyId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int PropertyId { get; set; } 

        [Display(Name = "Value")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Value { get; set; } = null!;

        #endregion

        #region relations

        [ForeignKey(nameof(EntitySelectedProjectAreaId))]
        public virtual EntitySelectedProjectArea EntitySelectedProjectArea { get; set; } = null!;

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; } = null!;

        #endregion
    }
}
