using DevCopilot2.Core.Extensions.AdvanceExtensions.AdminMenuItemsData;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Permissions;
using DevCopilot2.Web.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;

namespace DevCopilot2.Web.Areas.Admin.Controllers
{
    //[PermissionChecker("")]
    public class HomeController : BaseAdminController<HomeController>
    {
        #region constructor

        private readonly ISiteService _siteService;
        private readonly IUserService _userService;
        public HomeController(ISiteService siteService, IUserService userService)
        {
            this._siteService = siteService;
            _userService = userService;
        }

        #endregion

        #region index

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.GetCurrentUserId();

            bool isSuperAdmin = (await _userService.GetSingleUserInformation(userId)).IsSuperAdmin;

            //if (isSuperAdmin)
            // return RedirectToAction("Index", "Statistics", new { Area = "Admin" });

            List<PermissionListDto>? userPermissions =
                await _userService.FilterUserPermissions(userId);

            List<MenuItemDto> menuItems = userPermissions.GetMenuItems();
            var firstMenuItem = menuItems
                .Where(a => a.ControllerName.ToLower() != "home")
                .FirstOrDefault();

            if (firstMenuItem is not null)
                return RedirectToAction(firstMenuItem.ActionName,
                    firstMenuItem.ControllerName, new { Area = "Admin" });

            return View();
        }

        #endregion

        #region icons

        public IActionResult Icons()
        => View();

        #endregion

    }
}
