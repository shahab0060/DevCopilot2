using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.SiteSettings;
using DevCopilot2.Domain.DTOs.Users;
using Kavenegar.Models;

namespace DevCopilot2.Core.Services.Classes
{
    public class SmsService : ISmsService
    {
        #region constructor

        private readonly ISiteService _siteService;
        public SmsService(ISiteService siteService)
        {
            this._siteService = siteService;
        }

        #endregion

        public async Task SendAuthorizeSms(LoginUserByPhoneNumberOtpCode login)
        {
            try
            {
                SiteSettingListDto siteSettings = await _siteService
                    .GetSingleSiteSettingInformation(1) ?? new SiteSettingListDto();
                var api = new Kavenegar.KavenegarApi
                    (siteSettings.SMSApiKey);
                SendResult res = api.VerifyLookup(receptor: login.PhoneNumber, token:
                    login.OtpCode, template: siteSettings.SMSTemplateName);
                //return BaseChangeEntityResult.Success;

            }
            catch
            {
                //return BaseChangeEntityResult.NotFound;
            }
        }

      
    }
}
