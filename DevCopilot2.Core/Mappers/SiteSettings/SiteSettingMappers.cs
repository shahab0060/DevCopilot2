using System;
using DevCopilot2.Domain.Entities.SiteSettings;
using DevCopilot2.Domain.DTOs.SiteSettings;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;
using DevCopilot2.Core.MediasInformation.SiteSettings;

namespace DevCopilot2.Core.Mappers.SiteSettings
{
    public static class SiteSettingMappers
    {
        #region to dto

        public static IQueryable<SiteSettingListDto>ToDto(this IQueryable<SiteSetting> query)
                    => query.Select(siteSetting => new SiteSettingListDto()
                    {

                        Id = siteSetting.Id,
                        LatestEditDate = siteSetting.LatestEditDate,
                        CreateDate = siteSetting.CreateDate,
                        EditCounts = siteSetting.EditCounts,

                        SMSApiKey = siteSetting.SMSApiKey,
                        SMSTemplateName = siteSetting.SMSTemplateName,
                        FavIconName = siteSetting.FavIconName,
                        SiteName = siteSetting.SiteName,
                        LogoImageName = siteSetting.LogoImageName,

                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateSiteSettingDto>ToUpdateDto(this IQueryable<SiteSetting> query)
                    => query.Select(siteSetting => new UpdateSiteSettingDto()
                    {

                        Id = siteSetting.Id,

                        SMSApiKey = siteSetting.SMSApiKey,
                        SMSTemplateName = siteSetting.SMSTemplateName,
                        FavIconName = siteSetting.FavIconName,
                        SiteName = siteSetting.SiteName,
                        LogoImageName = siteSetting.LogoImageName,

                    });

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<SiteSetting> query)
			    => query.Select(siteSetting => new ComboDto()
			{
            Title = siteSetting.SiteName,
            Value = siteSetting.Id.ToString()
            });

        #endregion

        #region to create model

        public static async Task<SiteSetting> ToModel(this CreateSiteSettingDto create)
				=> new SiteSetting()
				{
                    SMSApiKey = create.SMSApiKey.SanitizeText()!,
                    SMSTemplateName = create.SMSTemplateName.SanitizeText()!,
                    FavIconName = await create.FavIconNameFile.UploadLocalImageAsync(
                          SiteSettingMediaInformation.FavIconNameMediasInformation,
                          Guid.NewGuid().ToString(), 0, null) ??"",
                    SiteName = create.SiteName.ToTitle()!,
                    LogoImageName = await create.LogoImageNameFile.UploadLocalImageAsync(
                          SiteSettingMediaInformation.LogoImageNameMediasInformation,
                          Guid.NewGuid().ToString(), 0, null) ??"",
				};

        #endregion

        #region to update model

        public static async Task<SiteSetting> ToModel(this SiteSetting siteSetting, UpdateSiteSettingDto update)
        {
            siteSetting.SMSApiKey = update.SMSApiKey.SanitizeText()!;
            siteSetting.SMSTemplateName = update.SMSTemplateName.SanitizeText()!;
            if(update.FavIconNameFile is not null)
                                              siteSetting.FavIconName = await update.FavIconNameFile.UploadLocalImageAsync(
                          SiteSettingMediaInformation.FavIconNameMediasInformation,
                          Guid.NewGuid().ToString(), siteSetting.EditCounts+1, siteSetting.FavIconName) ??"";
            siteSetting.SiteName = update.SiteName.ToTitle()!;
            if(update.LogoImageNameFile is not null)
                                              siteSetting.LogoImageName = await update.LogoImageNameFile.UploadLocalImageAsync(
                          SiteSettingMediaInformation.LogoImageNameMediasInformation,
                          Guid.NewGuid().ToString(), siteSetting.EditCounts+1, siteSetting.LogoImageName) ??"";
            return siteSetting;
        }

        #endregion

    }
}
