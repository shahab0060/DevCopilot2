using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Domain.Entities.Users;

namespace DevCopilot2.Core.Utils
{
    public static class PathExtension
    {
        private static string dlHostBaseAddress = "";
        static PathExtension()
        {
            dlHostBaseAddress = AppSettingsExtensions.GetDlHost().BaseAddress;
        }
        public static string UserNoPhoto => $"{BaseGetPath}shared/user.png";
        public static string BackArrow => $"{BaseGetPath}shared/back-arrow.png";
        public static string UploadPhoto => $"{BaseGetPath}shared/upload.jpg";

        #region base paths

        //public static string BaseGetPath => $"{dlHostBaseAddress}/medias/";
       // public static string BaseSetPath => $"/medias/";
        public static string BaseGetPath => $"/medias/";
        public static string BaseSetPath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/medias/");


        #endregion

        #region document image

        public static string OriginalDocumentImage => $"{PathExtension.BaseGetPath}images/document/original/";
        public static string OriginalDocumentImageServer =>
             $"{PathExtension.BaseSetPath}images/document/original/";

        #endregion

        #region Domain Address

        public static string DomainAddress = "https://localhost:44367";
        //public static string DomainAddress = "https://Test3.com";

        #endregion
    }
    public static class FileAcceptableFormats
    {
        public static string Image = "image/png, image/jpeg, image/jpg";
        public static string VideoAvailableFormat = "video/mp4, video/webm";
        public static string ThreeDAvailableFormat = ".gltf, .glb";
    }
}
