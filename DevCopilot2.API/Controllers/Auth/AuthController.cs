using DevCopilot2.Api.Controllers.Base;
using DevCopilot2.API.Helpers;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.DataLayer.Context;
using DevCopilot2.Domain.DTOs.Account;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Domain.Enums.User;
using DevCopilot2.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevCopilot2.API.Controllers.Auth;

public class AuthController : BaseApiController
{

    #region constructor

    private readonly IPermissionService _permissionService;
    private readonly AppConfig _config;
    private readonly IUserService _userService;
    private readonly IAccountService _accountService;
    private readonly DevCopilot2DbContext _dbContext;
    private readonly ISmsService _smsService;

    public AuthController(IConfiguration config
        , IPermissionService permissionService
        , DevCopilot2DbContext dbContext
        , IUserService userService
        , ISmsService smsService,
        IAccountService accountService
        )
    {
        _config = config.Get<AppConfig>()!;
        _permissionService = permissionService;
        _dbContext = dbContext;
        _userService = userService;
        _accountService = accountService;
        _smsService = smsService;
    }

    #endregion

    #region login

    [AllowAnonymous]
    [HttpPost("/login")]
    public async Task<IActionResult> Login(ApiLoginDto request)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        UserClaimsListDto? userClaims = await _accountService.GetUserClaims(request);
        if (userClaims is null) return ReturnError("نام کاربری یا رمز عبور اشتباه است");

        var claims = new List<Claim>
            {
                new Claim("uid", userClaims.UserId.ToString())
            };

        foreach (var role in userClaims.Roles)
        {
            claims.Add(new Claim("role", role.Title));
        }

        foreach (var permission in userClaims.Permissions)
        {
            claims.Add(new Claim("permission", permission.Value));
        }

        var token = new JwtResponseDto
        {
            AccessToken = JwtHelper.GenerateJwtToken(claims, _config.DefaultJwtSettings),
            AccessTokenExpires = DateTimeOffset.UtcNow.AddMinutes(_config.DefaultJwtSettings.TokenExpiryInMinutes).ToUnixTimeSeconds(),
            User = (await _userService.GetSingleUserInformation(userClaims.UserId))!
        };
        return Ok(token);
    }

    #endregion

    #region get claim principal

    [Authorize]
    [HttpGet("/claims")]
    public async Task<IActionResult> GetClaimPrincipal()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirstValue("uid");
        if (string.IsNullOrWhiteSpace(userId))
            return NotFound("Token is not valid");

        UserClaimsListDto? userClaims = await _accountService.GetUserClaims(userId);
        if (userClaims is null)
            return NotFound("Token is not valid");
        return Ok(userClaims);
    }

    #endregion

}
