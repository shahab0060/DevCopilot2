using DevCopilot2.Core.Services.Classes;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Imports;
using DevCopilot2.Domain.DTOs.Languages;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.DTOs.Users;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Domain.Enums.Entities;
using DevCopilot2.Web.Areas.Admin.Controllers.Entities;
using DevCopilot2.Web.PresentationExtensions;
using DevCopilot2.Web.PresentationMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Imports
{
    public class EntityDbImportController : BaseAdminController<UserListDto>
    {
        #region constructor

        private readonly IEntityService _entityService;
        private readonly IProjectService _projectService;
        private readonly ISiteService _siteService;
        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        public EntityDbImportController(IEntityService entityService, IProjectService projectService, IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           ISiteService siteService,
                           IStringLocalizer<EntityController> localizer)
        {
            this._entityService = entityService;
            this._projectService = projectService;
            this._sharedLocalizer = sharedLocalizer;
            this._siteService = siteService;
            this._sharedEntitiesLocalizer = sharedEntitiesLocalizer;
        }

        #endregion

        #region select project

        [HttpGet]
        public async Task<IActionResult> SelectProject()
        {
            await GetProjectsViewDatas();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SelectProject(ImportEntitiesFromDbDto import)
        {
            if (import is null || import.ProjectId is not > 0)
            {
                TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Please AtLeast Choose One Item.")}";
                await GetProjectsViewDatas();
                return View(import);
            }
            return RedirectToAction("Index", "EntityDbImport", new { area = "Admin", projectId = import.ProjectId });
        }

        #endregion

        #region index

        public async Task<IActionResult> Index(int projectId)
        {
            await GetProjectRelatedViewDatas(projectId);
            ImportEntitiesFromDbDto import = new ImportEntitiesFromDbDto()
            {
                ProjectId = projectId
            };
            return View(import);
        }


        [HttpPost]
        public async Task<IActionResult> Index(ImportEntitiesFromDbDto import)
        {
            if (!ModelState.IsValid)
            {
                await GetProjectRelatedViewDatas(import.ProjectId);
                return View(import);
            }

            List<CreateEntityDto>? createEntities = _entityService.GetCreateEntitiesFromDb(import);

            if (createEntities is null)
            {
                TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Failed To Connect To The DataBase!")}";
                await GetProjectRelatedViewDatas(import.ProjectId);
                return View(import);
            }
            import.EntitiesList = createEntities;

            HttpContext.Session.SetString("CreateEntitiesByImport", JsonConvert.SerializeObject(import));

            return RedirectToAction("SelectEntities", "EntityDbImport");
        }

        #endregion

        #region Select Entities

        public async Task<IActionResult> SelectEntities()
        {
            try
            {
                // Retrieve object from session
                string sessionData = HttpContext.Session.GetString("CreateEntitiesByImport");
                if (string.IsNullOrEmpty(sessionData))
                {
                    return RedirectToAction("Index", "Project");
                }

                ImportEntitiesFromDbDto import =
                    JsonConvert.DeserializeObject<ImportEntitiesFromDbDto>(sessionData);

                await GetProjectRelatedViewDatas(import.ProjectId);
                return View(import);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Project");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SelectEntities(ImportEntitiesFromDbDto import)
        {
            int authorId = User.GetCurrentUserId();
            for (int i = 0; i < import.EntitiesList.Count; i++)
            {
                import.EntitiesList[i].AuthorId = authorId;
            }
            if (!ModelState.IsValid)
            {
                await GetProjectRelatedViewDatas(import.ProjectId);
                var errors = ModelState.Where(ms => ms.Value.Errors.Any())
                           .ToDictionary(ms => ms.Key, ms => ms.Value.Errors.Select(e => e.ErrorMessage).ToList());
                return View(import);
            }

            List<ChangeEntityResult> results = await _entityService.CreateEntities(import.EntitiesList);

            // HttpContext.Session.SetString("CreateEntitiesByImportResults", JsonConvert.SerializeObject(results));
            // return RedirectToAction("ShowResults", "EntityDbImport");
            //return RedirectToAction("ShowResults", "ProjectEnumImport");
            int successCount = results.Count(a => a == ChangeEntityResult.Success);
            TempData["SuccessMessage"] = $"{successCount} " +
                $"{(successCount > 1 ? _sharedEntitiesLocalizer.GetString("Entities") : _sharedEntitiesLocalizer.GetString("Entity"))} " +
                $"{_sharedLocalizer.GetString("Created Successfully")}";
            return RedirectToAction("Index", "Entity", new { Area = "Admin", projectId = import.ProjectId });
        }

        #endregion

        #region show results

        public IActionResult ShowResults()
        {
            try
            {
                // Retrieve object from session
                string sessionData = HttpContext.Session.GetString("CreateEntitiesByImportResults");
                if (string.IsNullOrEmpty(sessionData))
                {
                    return RedirectToAction("Index", "Project");
                }

                List<BaseChangeEntityResult> results =
                    JsonConvert.DeserializeObject<List<BaseChangeEntityResult>>(sessionData);
                return View(results);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Project");
            }
        }

        #endregion

        #region view datas

        async Task GetProjectAreasViewDatas(int projectId)
        => ViewData["ProjectAreas"] = (await _projectService
            .GetProjectAreasAsCombo(new FilterProjectAreasDto() { ProjectId = projectId })).ToSelectListItem();

        async Task GetProjectsViewDatas()
        => ViewData["Projects"] = (await _projectService
            .GetProjectsAsCombo(new FilterProjectsDto())).ToSelectListItem();

        async Task GetEntitiesViewData(int projectId)
       => ViewData["Entities"] = (await _entityService
           .GetEntitiesAsCombo(new FilterEntitiesDto() { ProjectId = projectId }))
            .ToSelectListItem(false, true);

        async Task GetProjectEnumsViewData(int projectId)
        => ViewData["ProjectEnums"] = (await _projectService
        .GetProjectEnumsAsCombo(new FilterProjectEnumsDto() { ProjectId = projectId }))
        .ToSelectListItem(false, true);

        async Task GetLanguagesViewData(int projectId)
        {
            ViewData["LanguagesList"] = (await _siteService
        .FilterLanguages(new FilterLanguagesDto() { ProjectId = projectId }))
        .Languages;
            ViewData["Languages"] = (await _siteService
        .GetLanguagesAsCombo(new FilterLanguagesDto() { ProjectId = projectId }))
        .ToSelectListItem();
        }

        async Task GetPropertiesViewData(int projectId)
       => ViewData["Properties"] = (await _entityService
       .GetPropertiesAsCombo(new FilterPropertiesDto() { ProjectId = projectId }))
       .ToSelectListItem();

        async Task GetProjectRelatedViewDatas(int projectId)
        {
            await GetEntitiesViewData(projectId);
            await GetProjectEnumsViewData(projectId);
            await GetLanguagesViewData(projectId);
            await GetProjectAreasViewDatas(projectId);
            await GetPropertiesViewData(projectId);
        }

        #endregion
    }
}
