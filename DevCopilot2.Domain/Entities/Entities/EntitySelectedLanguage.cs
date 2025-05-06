using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using DevCopilot2.Domain.Entities.Languages;

namespace DevCopilot2.Domain.Entities.Entities
{
    public partial class EntitySelectedLanguage : EntityId<int>
    {

        #region properties

        [Display(Name = "EntityId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int EntityId { get; set; } 

        [Display(Name = "LanguageId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int LanguageId { get; set; } 

        [Display(Name = "SingularTitle")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(250, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string SingularTitle { get; set; } = null!;

        [Display(Name = "PluralTitle")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(250, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string PluralTitle { get; set; } = null!;

        #endregion

        #region relations

        [ForeignKey(nameof(EntityId))]
        public virtual Entity Entity { get; set; } = null!;

        [ForeignKey(nameof(LanguageId))]
        public virtual Language Language { get; set; } = null!;

        #endregion
    }
}
