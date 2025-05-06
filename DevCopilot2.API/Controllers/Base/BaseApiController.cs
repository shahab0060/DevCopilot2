using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Permissions;
using DevCopilot2.Domain.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevCopilot2.Api.Controllers.Base
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "default")]

    public class BaseApiController : ControllerBase
    {
        //protected IActionResult ReturnErrors(ModelStateDictionary modelState)
        //{

        //    var errors = modelState.Values.SelectMany(v => v.Errors)
        //                                  .Select(e => e.ErrorMessage)
        //                                  .ToList();

        //    return BadRequest(new { Errors = errors });
        //}
        protected IActionResult ReturnErrors(ModelStateDictionary modelState)
        {

            var errors = modelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .FirstOrDefault();
            return BadRequest(errors);
        }

        protected async Task BadRequestAsync(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .FirstOrDefault();

            HttpContext.Response.StatusCode = 400;
            HttpContext.Response.ContentType = "application/json";
            var json = System.Text.Json.JsonSerializer.Serialize(errors);
            await HttpContext.Response.WriteAsync(json);
        }
        protected IActionResult CustomBadRequest(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .FirstOrDefault();
            return StatusCode(400, errors);
        }
        protected IActionResult ReturnError(string errorMessage)
            => BadRequest(errorMessage);
    }

    #region permission checker

    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        private string _permissionName;
        private readonly string _accessDeniedRedirectResult;

        public PermissionCheckerAttribute(string permissionName)
        {
            _permissionName = permissionName;
            _accessDeniedRedirectResult = "/404";
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));
            //_context = (AryaTradingPlatformDbContext)context.HttpContext.RequestServices.GetService(typeof(AryaTradingPlatformDbContext));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var data = context.HttpContext.User.Claims.SingleOrDefault(s => s.Type == "uid");
                int userId = Convert.ToInt32(data.Value);
                UserListDto? userInformation = _userService.GetSingleUserInformation(userId).Result;
                if (userInformation is null)
                {
                    context.Result = new RedirectResult(_accessDeniedRedirectResult);
                    return;
                }
                if (userInformation.IsSuperAdmin) return;
                List<PermissionListDto> userPermissions = _permissionService.FilterUserPermissions(userId).Result;
                if (string.IsNullOrEmpty(_permissionName))
                {
                    if (userPermissions.Any()) return;
                    context.Result = new RedirectResult(_accessDeniedRedirectResult);
                    return;
                }
                if (userPermissions.Any()) return;
                context.Result = new RedirectResult(_accessDeniedRedirectResult);
                return;
            }
            context.Result = new RedirectResult(_accessDeniedRedirectResult);
            return;
        }

        #endregion

    }
}
