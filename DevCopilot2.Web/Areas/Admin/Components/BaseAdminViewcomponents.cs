using DevCopilot2.Core.Extensions.AdvanceExtensions.AdminMenuItemsData;
using DevCopilot2.Core.Services.Classes;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Permissions;
using DevCopilot2.Domain.DTOs.SiteSettings;
using DevCopilot2.Domain.DTOs.Users;
using DevCopilot2.Domain.Entities.Permissions;
using DevCopilot2.Web.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;

namespace DevCopilot2.Web.Areas.Admin.Components
{

    #region admin navbar

    public class AdminNavbarViewComponent : ViewComponent
    {
        #region constructor

        private readonly IUserService _userService;
        private readonly ISiteService _siteService;
        public AdminNavbarViewComponent(IUserService userService, ISiteService siteService)
        {
            this._userService = userService;
            this._siteService = siteService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserListDto UserInformation = await _userService.GetSingleUserInformation(User.GetCurrentUserId()) ?? new UserListDto();
            ViewData["SiteSettigs"] = await _siteService.GetSingleSiteSettingInformation(1) ?? new SiteSettingListDto();
            return View("AdminNavbar", UserInformation);
        }
    }

    #endregion

    #region admin horizental menu

    public class AdminHorizentalMenuViewComponent : ViewComponent
    {
        #region constructor

        private readonly IUserService _userService;
        private readonly ISiteService _siteService;
        public AdminHorizentalMenuViewComponent(
            IUserService userService, ISiteService siteService)
        {
            this._userService = userService;
            this._siteService = siteService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = User.GetCurrentUserId();
            List<PermissionListDto>? userPermissions =
                await _userService.FilterUserPermissions(userId);
            var menuItems = userPermissions.GetMenuItems();

            ViewData["IsSuperAdmin"] = (await _userService.GetSingleUserInformation(userId)).IsSuperAdmin;
            ViewData["SiteSettigs"] = await _siteService.GetSingleSiteSettingInformation(1) ?? new SiteSettingListDto();
            return View("AdminHorizentalMenu", menuItems);
        }
    }

    #endregion

    #region admin horizental menu item

    public class AdminHorizentalMenuItemViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(MenuItemDto menuItem)
        {
            return View("AdminHorizentalMenuItem", menuItem);
        }
    }

    #endregion

    #region admin Vertical menu

    public class AdminVerticalMenuViewComponent : ViewComponent
    {
        #region constructor

        private readonly IUserService _userService;
        private readonly ISiteService _siteService;
        public AdminVerticalMenuViewComponent(IUserService userService, 
            ISiteService siteService)
        {
            this._userService = userService;
            this._siteService = siteService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = User.GetCurrentUserId();
            List<PermissionListDto>? userPermissions =
                await _userService.FilterUserPermissions(userId);


            var menuItems = userPermissions.GetMenuItems();

            ViewData["IsSuperAdmin"] = (await _userService.GetSingleUserInformation(userId)).IsSuperAdmin;
            return View("AdminVerticalMenu", menuItems);
        }
    }

    #endregion

    #region admin Vertical menu item

    public class AdminVerticalMenuItemViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(MenuItemDto menuItem)
        {
            return View("AdminVerticalMenuItem", menuItem);
        }
    }

    #endregion

    #region admin footer

    public class AdminFooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AdminFooter");
        }
    }

    #endregion

    #region admin meta tags and title

    public class AdminMetaTagsAndTitleViewComponent : ViewComponent
    {
        #region constructor

        private readonly ISiteService _siteService;
        public AdminMetaTagsAndTitleViewComponent(ISiteService siteService)
        {
            this._siteService = siteService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SiteSettingListDto? siteSettings = await _siteService
                .GetSingleSiteSettingInformation(1) ??
                new SiteSettingListDto();
            return View("AdminMetaTagsAndTitle", siteSettings);
        }
    }

    #endregion

    #region admin modals

    public class AdminModalsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AdminModals");
        }
    }

    #endregion

    #region admin settings panel

    public class AdminSettingsPanelViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AdminSettingsPanel");
        }
    }

    #endregion
}
