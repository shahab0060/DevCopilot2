using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevCopilot2.Domain.Entities.Properties
{
    public partial class PropertyImageResizeInformation : EntityId<int>
    {

        #region properties

        [Display(Name = "PropertyId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int PropertyId { get; set; } 

        [Display(Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Name { get; set; } = null!;

        [Display(Name = "Width")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [Range(1, 10000, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RangeErrorMessage))]

        public int Width { get; set; } 

        [Display(Name = "Height")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [Range(1, 10000, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RangeErrorMessage))]

        public int Height { get; set; } 

        #endregion

        #region relations

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; } = null!;

        #endregion
    }
}
