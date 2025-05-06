using System.ComponentModel;

namespace DevCopilot2.Domain.DTOs.Common
{
    public class FtpServerDto
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
    }

    public class ChangeFtpFile
    {
        public string FileName { get; set; }

        public List<MediaInformationDto> Medias { get; set; }
    }

    public class UploadFtpDto
    {
        public MediaInformationDto Media { protected get; set; }

        public string FileName { protected get; set; }

        public MemoryStream Stream { get; set; }

        public string FilePath { get; protected set; }

        public void SetPath()
        {
            this.FilePath = Path.Combine(Media.SetAddress, FileName);
        }
    }
}
