using DevCopilot2.Domain.DTOs.Common;
using Microsoft.Extensions.Configuration;

namespace DevCopilot2.Core.Extensions.BasicExtensions
{
    public static class AppSettingsExtensions
    {
        public static FtpServerDto GetFtpServer()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration.GetSection("FtpSettings").Get<FtpServerDto>();
        }
        public static DlHostInformationDto GetDlHost()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration.GetSection("DlHost").Get<DlHostInformationDto>();
        }
    }
}
