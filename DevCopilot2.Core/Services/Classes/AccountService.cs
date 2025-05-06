using DevCopilot2.Core.Mappers.Account;
using DevCopilot2.Core.Mappers.Permissions;
using DevCopilot2.Core.Mappers.Users;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Account;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Users;
using DevCopilot2.Domain.Entities.Permissions;
using DevCopilot2.Domain.Entities.Users;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Domain.Enums.User;
using DevCopilot2.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCopilot2.Core.Services.Classes
{
    public class AccountService : IAccountService
    {
        #region constructor

        private readonly ICrudRepository<User, long> _userRepository;
        private readonly ICrudRepository<Permission, long> _permissionRepository;
        private readonly IUserService _userService;
        private readonly ISmsService _smsService;
        public AccountService(ICrudRepository<User, long> userRepository,
            IUserService userService,
            ICrudRepository<Permission, long> permissionRepository,
            ISmsService smsService)
        {
            this._userRepository = userRepository;
            this._permissionRepository = permissionRepository;
            this._userService = userService;
            this._smsService = smsService;
        }

        #endregion

        #region account

        public async Task LoginRegisterUser
            (LoginRegisterUserDto loginRegister)
        {
            if (!await _userRepository
                .GetQueryable()
                .AnyAsync(a => a.PhoneNumber == loginRegister.PhoneNumber))
            {
                await _userService.CreateUser(new CreateUserDto()
                { PhoneNumber = loginRegister.PhoneNumber });
            }
            await SendUserPhoneNumberOtpCode(loginRegister.PhoneNumber);
        }

        public async Task<LoginUserResult> LoginByPhoneNumberOtpCode
            (LoginUserByPhoneNumberOtpCode login)
        {
            User? user = await _userRepository
                .GetQueryable()
                .Where(a => a.PhoneNumber == login.PhoneNumber)
                .FirstOrDefaultAsync();
            if (user is null) return LoginUserResult.NotFound;
            if (user.PhoneNumberActivationCode != login.OtpCode) return LoginUserResult.IncorrectCode;
            if (user.PhoneNumberActivationCodeExpireTime < DateTime.Now) return LoginUserResult.CodeExpired;
            return LoginUserResult.Success;
        }

        public async Task<bool> LoginByPhoneNumberAndPassword
            (LoginByPhoneNumberAndPasswordDto login)
            => await _userRepository
            .GetQueryable()
            .AnyAsync(a => a.PhoneNumber == login.PhoneNumber &&
                        a.Password == login.Password);



        public async Task<BaseChangeEntityResult> SendUserPhoneNumberOtpCode(string phoneNumber)
        {
            User? user = await _userRepository
                .GetQueryable()
                .Where(a => a.PhoneNumber == phoneNumber)
                .FirstOrDefaultAsync();
            if (user is null) return BaseChangeEntityResult.NotFound;
            if (user.PhoneNumberActivationCodeExpireTime > DateTime.Now)
                return BaseChangeEntityResult.Exists;
            user = await user.SendPhoneNumberOtpCode();
            await _smsService.SendAuthorizeSms(new LoginUserByPhoneNumberOtpCode()
            {
                PhoneNumber = user.PhoneNumber,
                OtpCode = user.PhoneNumberActivationCode
            });
            _userRepository.Update(user);
            await _userRepository.SaveChanges();
            return BaseChangeEntityResult.Success;
        }


        #endregion

        #region user claims

        public async Task<UserClaimsListDto?> GetUserClaims(long userId)
        => await _userRepository
                   .GetQueryable()
                   .Where(a => a.Id == userId)
                  .ToClaims()
                   .FirstOrDefaultAsync();

        public async Task<UserClaimsListDto?> GetUserClaims(string userName)
      => await _userRepository
                 .GetQueryable()
                 .Where(a => a.PhoneNumber.ToLower() == userName.ToLower())
                .ToClaims()
                 .FirstOrDefaultAsync();

        public async Task<UserClaimsListDto?> GetUserClaims(ApiLoginDto login)
             => await _userRepository
              .GetQueryable()
              .Where(a => (a.PhoneNumber == login.NormalizedUsername || a.PhoneNumber == login.NormalizedUsername) &&
              //PasswordHelper.VerifyHash(login.Password, a.Password))
              (a.Password == login.Password || (a.PhoneNumberActivationCode == login.Password && a.PhoneNumberActivationCodeExpireTime >= DateTime.Now)))
             .ToClaims()
              .FirstOrDefaultAsync();

        #endregion
    }
}
