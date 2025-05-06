
using System;
using DevCopilot2.Core.Utils;
using DevCopilot2.Domain.DTOs.Common;
namespace DevCopilot2.Core.MediasInformation.Users
{
    public static class UserMediaInformation
    {

        #region image name medias

        public static MediaInformationDto OriginalImage = new MediaInformationDto()
        {
            GetAddress = $"{PathExtension.BaseGetPath}images/users/images/",
            SetAddress = $"{PathExtension.BaseSetPath}images/users/images/",
            Height = null,
            Width = null
        };

        public static MediaInformationDto DesktopImage = new MediaInformationDto()
        {
            GetAddress = $"{PathExtension.BaseGetPath}images/users/desktop/",
            SetAddress = $"{PathExtension.BaseSetPath}images/users/desktop/",
            Height = 250,
            Width = 250
        };

        public static List<MediaInformationDto> ImageMediasInformation = new List<MediaInformationDto>()
                      {
                          OriginalImage ,
                          DesktopImage
                      };

        #endregion

    }
}
