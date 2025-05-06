using DevCopilot2.Domain.DTOs.Users;

namespace DevCopilot2.Core.Services.Interfaces
{
    public interface ISmsService : IService
    {
        Task SendAuthorizeSms(LoginUserByPhoneNumberOtpCode login);
    }
}
