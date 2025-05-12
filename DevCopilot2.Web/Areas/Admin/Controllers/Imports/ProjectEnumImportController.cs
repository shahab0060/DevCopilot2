using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Imports;
using DevCopilot2.Domain.DTOs.Languages;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Users;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Web.PresentationExtensions;
using DevCopilot2.Web.PresentationMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Imports
{
    public class ProjectEnumImportController : BaseAdminController<UserListDto>
    {
        #region constructor

        private readonly IProjectService _projectService;
        private readonly ISiteService _siteService;
        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        public ProjectEnumImportController(IProjectService projectService, ISiteService siteService,
            IStringLocalizer<SharedResources> sharedLocalizer, IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer)
        {
            this._projectService = projectService;
            this._siteService = siteService;
            this._sharedLocalizer = sharedLocalizer;
            this._sharedEntitiesLocalizer = sharedEntitiesLocalizer;
        }

        #endregion

        #region index

        public async Task<IActionResult> Index()
        {
            await GetProjects();
            return View(new ImportProjectEnumsFromLocationDto());
        }

        [HttpPost]
        public async Task<IActionResult> Index(ImportProjectEnumsFromLocationDto import)
        {
            import.AuthorId = User.GetCurrentUserId();
            import.ProjectEnumsList = await _projectService.GetCreateProjectEnumsFromLocation(import);
            if (!ModelState.IsValid)
            {
                await GetProjects();
                return View(import);
            }
            if (import.ProjectEnumsList is null || !import.ProjectEnumsList.Any())
            {
                TempData[ErrorMessage] = _sharedLocalizer.GetString("No Items Found!");
                await GetProjects();
                return View(import);
            }
            HttpContext.Session.SetString("CreateEnumsByImport", JsonConvert.SerializeObject(import));
            return RedirectToAction("SelectEnums", "ProjectEnumImport");
        }

        #endregion

        #region select enums

        [HttpGet]
        public async Task<IActionResult> SelectEnums()
        {
            try
            {
                // Retrieve object from session
                string sessionData = HttpContext.Session.GetString("CreateEnumsByImport");
                if (string.IsNullOrEmpty(sessionData))
                {
                    return RedirectToAction("Index", "ProjectEnumImport");
                }

                ImportProjectEnumsFromLocationDto import =
                    JsonConvert.DeserializeObject<ImportProjectEnumsFromLocationDto>(sessionData);

                await GetLanguages(import.ProjectId);
                return View(import);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "ProjectEnumImport");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SelectEnums(ImportProjectEnumsFromLocationDto import)
        {
            import.AuthorId = User.GetCurrentUserId();
            if (!ModelState.IsValid)
            {
                await GetLanguages(import.ProjectId);
                return View(import);
            };
            List<BaseChangeEntityResult> results = await _projectService.ImportProjectEnumsFromLocation(import);
            int successCount = results.Count(a => a == BaseChangeEntityResult.Success);
            //HttpContext.Session.SetString("CreateEnumsByImportResults", JsonConvert.SerializeObject(results));
            //return RedirectToAction("ShowResults", "ProjectEnumImport");
            TempData["SuccessMessage"] = $"{successCount} " +
                $"{(successCount > 1 ? _sharedEntitiesLocalizer.GetString("Enums") : _sharedEntitiesLocalizer.GetString("Enum"))} " +
                $"{_sharedLocalizer.GetString("Created Successfully")}";
            return RedirectToAction("Index", "ProjectEnum", new { Area = "Admin", projectId = import.ProjectId });
        }

        #endregion

        #region show results

        public IActionResult ShowResults()
        {
            try
            {
                // Retrieve object from session
                string sessionData = HttpContext.Session.GetString("CreateEnumsByImportResults");
                if (string.IsNullOrEmpty(sessionData))
                {
                    return RedirectToAction("Index", "ProjectEnumImport");
                }

                List<BaseChangeEntityResult> results =
                    JsonConvert.DeserializeObject<List<BaseChangeEntityResult>>(sessionData);
                return View(results);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "ProjectEnumImport");
            }
        }

        #endregion

        #region get view datas

        async Task GetProjects()
            => ViewData["Projects"] = (await _projectService
            .GetProjectsAsCombo(new FilterProjectsDto()))
            .ToSelectListItem(false, true);

        async Task GetLanguages(int? projectId)
            => ViewData["Languages"] = (await _siteService
            .GetLanguagesAsCombo(new FilterLanguagesDto() { ProjectId = projectId }))
            .ToSelectListItem();

        #endregion
    }
}
