using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.Entities.SiteSettings
{
    public partial class SiteSetting : EntityId<long>
    {

        #region properties

        [Display(Name = "SMSApiKey")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string SMSApiKey { get; set; } = null!;

        [Display(Name = "SMSTemplateName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(100, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string SMSTemplateName { get; set; } = null!;

        [Display(Name = "FavIconName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string FavIconName { get; set; } = null!;

        [Display(Name = "SiteName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(100, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string SiteName { get; set; } = null!;

        [Display(Name = "LogoImageName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string LogoImageName { get; set; } = null!;

        #endregion

    }
}
