using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.GeneralSettings;
using DevCopilot2.Domain.Enums.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Text;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace DevCopilot2.Web.Areas.Admin.Controllers.GeneralSettings
{
    [PermissionChecker("GeneralSettingManagement")]
    public class GeneralSettingController : BaseAdminController<GeneralSettingListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<GeneralSettingController> _localizer;
        private readonly ISiteService _siteService;
        public GeneralSettingController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<GeneralSettingController> localizer,
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
        public async Task<IActionResult> Update(int id)
        {
            UpdateGeneralSettingDto? generalSettingInformation = await _siteService.GetGeneralSettingInformation(id);
            if (generalSettingInformation is null) return NotFound();

            return View(generalSettingInformation);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateGeneralSettingDto update)
        {

            if (!ModelState.IsValid)
            {

                return View(update);
            }
            BaseChangeEntityResult result = await _siteService.UpdateGeneralSetting(update);

            #region handling different types

            switch (result)
            {

                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("GeneralSetting")} {_sharedLocalizer.GetString("Updated Successfully.")}";
                        return RedirectToAction("Update", "GeneralSetting", new { Area = "Admin", id = update.Id });
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
