using DevCopilot2.Domain.DTOs.Account;
using DevCopilot2.Domain.DTOs.Users;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Domain.Enums.User;

namespace DevCopilot2.Core.Services.Interfaces
{
    public interface IAccountService
    {
        #region account

        Task LoginRegisterUser(LoginRegisterUserDto loginRegister);
        Task<LoginUserResult> LoginByPhoneNumberOtpCode(LoginUserByPhoneNumberOtpCode login);
        Task<bool> LoginByPhoneNumberAndPassword(LoginByPhoneNumberAndPasswordDto login);
        Task<BaseChangeEntityResult> SendUserPhoneNumberOtpCode(string phoneNumber);

        #endregion

        #region user claims

        Task<UserClaimsListDto?> GetUserClaims(long userId);
        Task<UserClaimsListDto?> GetUserClaims(string userName);
        Task<UserClaimsListDto?> GetUserClaims(ApiLoginDto login);

        #endregion
    }
}
