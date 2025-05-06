using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.SiteSettings
{
    public enum SortSiteSettingType
    {

        [Display(Name = "SMSApiKey")]
        SMSApiKey,

        [Display(Name = "SMSTemplateName")]
        SMSTemplateName,

        [Display(Name = "FavIconName")]
        FavIconName,

        [Display(Name = "SiteName")]
        SiteName,

        [Display(Name = "LogoImageName")]
        LogoImageName,
    }
}
