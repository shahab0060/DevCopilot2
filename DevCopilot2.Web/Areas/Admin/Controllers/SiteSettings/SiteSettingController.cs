using System;
using System.Text;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Web.PresentationExtensions;
using DevCopilot2.Web.PresentationMappers;
using DevCopilot2.Domain.Enums.SiteSettings;
using DevCopilot2.Domain.DTOs.SiteSettings;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ClosedXML.Excel;
using DevCopilot2.Core.Exporters;

namespace DevCopilot2.Web.Areas.Admin.Controllers.SiteSettings
{
    [PermissionChecker("SiteSettingManagement")]
    public class SiteSettingController : BaseAdminController<SiteSettingListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<SiteSettingController> _localizer;
        private readonly ISiteService _siteService;
        public SiteSettingController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<SiteSettingController> localizer,
                           ISiteService siteService
                                      )
        {
            this._sharedLocalizer = sharedLocalizer;
            this._sharedEntitiesLocalizer = sharedEntitiesLocalizer;
            this._localizer = localizer;
            this._siteService = siteService;
        }

        #endregion

        #region update

        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            UpdateSiteSettingDto? siteSettingInformation = await _siteService.GetSiteSettingInformation(id);
            if (siteSettingInformation is null) return NotFound();

            return View(siteSettingInformation);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateSiteSettingDto update)
        {

            if (!ModelState.IsValid)
            {

                return View(update);
            }
            BaseChangeEntityResult result = await _siteService.UpdateSiteSetting(update);

            #region handling different types

            switch (result)
            {

                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("SiteSetting")} {_sharedLocalizer.GetString("Updated Successfully.")}";
                        return RedirectToAction("Update", "SiteSetting", new { Area = "Admin", id = update.Id });
                    }

                case BaseChangeEntityResult.NotFound:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                    }

                case BaseChangeEntityResult.Exists:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Item Exists.")}";
                        break;
                    }

            }

            #endregion

            return View(update);
        }

        #endregion

    }
}
