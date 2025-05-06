using DevCopilot2.Core.Extensions.AdvanceExtensions.Users;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Users;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Domain.Enums.User;
using DevCopilot2.Web.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevCopilot2.Web.Controllers
{
    public class AccountController : BaseSiteController
    {
        #region constructor

        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        public AccountController(IUserService userService, IAccountService accountService)
        {
            this._userService = userService;
            _accountService = accountService;
        }

        #endregion

        #region Log Out

        [Microsoft.AspNetCore.Mvc.HttpGet("log-out")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("sign-out")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            TempData[SuccessMessage] = "شما با موفقیت از حساب خود خارج شدید";
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region login

        async Task Login(UserListDto user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name,user.PhoneNumber),
                new(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.Now.AddDays(31)
            };
            await HttpContext.SignInAsync(principal, properties);
        }

        #endregion

        #region login by password

        [HttpGet("sign-in")]
        public async Task<IActionResult> PhoneNumberAndPasswordSignIn(string? returnUrl)
        {
            return View(new LoginByPhoneNumberAndPasswordDto()
            {
                ReturnUrl = returnUrl,
                Password = "12345678",
                PhoneNumber = "09121234567"
            });
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> PhoneNumberAndPasswordSignIn(LoginByPhoneNumberAndPasswordDto login)
        {
            if (!ModelState.IsValid)
                return View(login);
            bool result = await _accountService
                .LoginByPhoneNumberAndPassword(login);
            if (result)
            {
                UserListDto userInformation = await _userService
                    .GetSingleUserInformation(login.PhoneNumber) ?? new UserListDto();
                await Login(userInformation);
                TempData[SuccessMessage] = "ورود شما با موفقیت انجام شد";
                if (string.IsNullOrEmpty(login.ReturnUrl))
                    return RedirectToAction("Index", "Home");
                return Redirect(login.ReturnUrl);
            }
            TempData[ErrorMessage] = "کاربری با مشخصات وارد شده یافت نشد";
            return View(login);
        }

        #endregion

        #region login register

        [HttpPost]
        public async Task<IActionResult> LoginRegister(LoginRegisterUserDto login)
        {
            if (!ModelState.IsValid)
                return GetModelStateValidationErrorMessageJson(ModelState);
            await _accountService.LoginRegisterUser(login);
            return JsonResponseStatusType.Success.SendStatus("کد ورود برای شما ارسال  شد");
        }

        #endregion

        #region login by phonenumber otp

        [HttpPost]
        public async Task<IActionResult> PhoneNumberOtpLogin(LoginUserByPhoneNumberOtpCode login)
        {
            if (!ModelState.IsValid)
                return GetModelStateValidationErrorMessageJson(ModelState);
            LoginUserResult result = await _accountService.LoginByPhoneNumberOtpCode(login);
            switch (result)
            {
                case LoginUserResult.Success:
                    {
                        UserListDto user = await _userService
                            .GetSingleUserInformation(login.PhoneNumber) ?? new UserListDto();
                        string name = user.GetName("کاربر");
                        string message = $"{name} عزیز! ورود شما با موفقیت انجام شد";
                        await Login(user);
                        return JsonResponseStatusType.Success.SendStatus(message);
                    }
                case LoginUserResult.NotFound:
                    break;
                case LoginUserResult.IncorrectCode:
                    return JsonResponseStatusType.Danger.SendStatus("کد وارد شده اشتباه است", result);
                case LoginUserResult.CodeExpired:
                    {
                        await _accountService.SendUserPhoneNumberOtpCode(login.PhoneNumber);
                        return JsonResponseStatusType.Warning
                            .SendStatus("کد وارد شده منقضی شده است. کد جدید ارسال شد", result);
                    }
            }
            return JsonResponseStatusType.Danger.SendStatus("مشکلی در انجام عملیات پیش آمد!", result);
        }

        #endregion

        #region resend phone number otp

        [HttpPost]
        public async Task<IActionResult> ResendPhoneNumberOtpCode(LoginRegisterUserDto login)
        {
            if (!ModelState.IsValid)
                return GetModelStateValidationErrorMessageJson(ModelState);
            BaseChangeEntityResult result = await _accountService.SendUserPhoneNumberOtpCode(login.PhoneNumber);
            if (result == BaseChangeEntityResult.Success)
                return JsonResponseStatusType.Success.SendStatus("کد با موفقیت ارسال شد");
            return JsonResponseStatusType.Danger.SendStatus("مشکلی در ارسال کد پیش آمد");
        }

        #endregion

    }
}
