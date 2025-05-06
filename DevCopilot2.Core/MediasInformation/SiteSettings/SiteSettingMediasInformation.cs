using System;
using DevCopilot2.Core.Utils;
using DevCopilot2.Domain.DTOs.Common;

namespace DevCopilot2.Core.MediasInformation.SiteSettings
{
    public static class SiteSettingMediaInformation
    {

        #region fav icon name medias

        public static MediaInformationDto OriginalFavIconName = new MediaInformationDto()
        {
            GetAddress = $"{PathExtension.BaseGetPath}images/site-settings/fav-icon-name/original/",
            SetAddress = $"{PathExtension.BaseSetPath}images/site-settings/fav-icon-name/original/",
            Height = null,
            Width = null
        };    

        public static List<MediaInformationDto> FavIconNameMediasInformation = new List<MediaInformationDto>()
                      {

                          OriginalFavIconName 
                      };

        #endregion

        #region logo image name medias

        public static MediaInformationDto OriginalLogoImageName = new MediaInformationDto()
        {
            GetAddress = $"{PathExtension.BaseGetPath}images/site-settings/logo-image-name/original/",
            SetAddress = $"{PathExtension.BaseSetPath}images/site-settings/logo-image-name/original/",
            Height = null,
            Width = null
        };    

        public static List<MediaInformationDto> LogoImageNameMediasInformation = new List<MediaInformationDto>()
                      {

                          OriginalLogoImageName 
                      };

        #endregion

    }
}
