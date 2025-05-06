using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using DevCopilot2.Domain.Entities.Languages;

namespace DevCopilot2.Domain.Entities.Properties
{
    public partial class PropertySelectedLanguage : EntityId<int>
    {

        #region properties

        [Display(Name = "PropertyId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int PropertyId { get; set; } 

        [Display(Name = "LanguageId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int LanguageId { get; set; } 

        [Display(Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(250, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string Title { get; set; } = null!;

        #endregion

        #region relations

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; } = null!;

        [ForeignKey(nameof(LanguageId))]
        public virtual Language Language { get; set; } = null!;

        #endregion
    }
}
