using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using DevCopilot2.Domain.Entities.Properties;
using DevCopilot2.Domain.Enums.Relations;

namespace DevCopilot2.Domain.Entities.Entities
{
    public partial class EntityRelation : EntityId<int>
    {

        #region properties

        [Display(Name = "PrimaryPropertyId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int PrimaryPropertyId { get; set; } 

        [Display(Name = "SecondaryEntityId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int SecondaryEntityId { get; set; } 

        [Display(Name = "MiddleEntityId")]

        public int? MiddleEntityId { get; set; } 

        [Display(Name = "RelationType")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public RelationTypeEnum RelationType { get; set; } 

        [Display(Name = "InputType")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public InputTypeEnum InputType { get; set; } 

        [Display(Name = "FillingType")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public FillingTypeEnum FillingType { get; set; } 

        [Display(Name = "FillingCode")]

        public string? FillingCode { get; set; } = null!;

        #endregion

        #region relations

        [ForeignKey(nameof(PrimaryPropertyId))]
        public virtual Property PrimaryProperty { get; set; } = null!;

        [ForeignKey(nameof(SecondaryEntityId))]
        public virtual Entity SecondaryEntity { get; set; } = null!;

        [ForeignKey(nameof(MiddleEntityId))]
        public virtual Entity? MiddleEntity { get; set; } = null!;

        #endregion
    }
}
