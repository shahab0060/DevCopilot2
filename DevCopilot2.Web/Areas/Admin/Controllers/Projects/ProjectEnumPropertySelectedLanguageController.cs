using System;
using System.Text;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Web.PresentationExtensions;
using DevCopilot2.Web.PresentationMappers;
using DevCopilot2.Domain.Enums.Projects;
using DevCopilot2.Domain.DTOs.Projects;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ClosedXML.Excel;
using DevCopilot2.Core.Exporters;
using DevCopilot2.Domain.DTOs.Languages;
using DevCopilot2.Domain.Entities.Common;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Projects
{
    [PermissionChecker("ProjectEnumPropertySelectedLanguageManagement")]
    public class ProjectEnumPropertySelectedLanguageController : BaseAdminController<ProjectEnumPropertySelectedLanguageListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<ProjectEnumPropertySelectedLanguageController> _localizer;
        private readonly IProjectService _projectService;
        private readonly ISiteService _siteService;
        public ProjectEnumPropertySelectedLanguageController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<ProjectEnumPropertySelectedLanguageController> localizer,
                           IProjectService projectService,
                           ISiteService siteService
                                      )
        {
            this._sharedLocalizer = sharedLocalizer;
            this._sharedEntitiesLocalizer = sharedEntitiesLocalizer;
            this._localizer = localizer;
            this._projectService = projectService;
            this._siteService = siteService;
        }

        #endregion

        #region index

        [HttpGet]
        public async Task<IActionResult> Index(FilterProjectEnumPropertySelectedLanguagesDto filter)
        {

            return View(await _projectService.FilterProjectEnumPropertySelectedLanguages(filter));
        }

        #endregion

        #region detail

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {

            ProjectEnumPropertySelectedLanguageListDto? projectEnumPropertySelectedLanguageInformation = await _projectService.GetSingleProjectEnumPropertySelectedLanguageInformation(id);
            if (projectEnumPropertySelectedLanguageInformation is null) return NotFound();

            await GetViewDatas(projectEnumPropertySelectedLanguageInformation.ProjectEnumPropertyId);
            return View(projectEnumPropertySelectedLanguageInformation);
        }

        #endregion

        #region create

        [HttpGet]
        public async Task<IActionResult> Create(int projectEnumPropertyId)
        {
            await GetViewDatas(projectEnumPropertyId, true);
            CreateProjectEnumPropertySelectedLanguageDto create = new CreateProjectEnumPropertySelectedLanguageDto()
            {
                ProjectEnumPropertyId = projectEnumPropertyId,
            };
            return View(create);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectEnumPropertySelectedLanguageDto create)
        {

            if (!ModelState.IsValid)
            {
                await GetViewDatas(create.ProjectEnumPropertyId, true);
                return View(create);
            }
            BaseChangeEntityResult result = await _projectService.CreateProjectEnumPropertySelectedLanguage(create);

            #region handling different types

            switch (result)
            {

                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectEnumPropertySelectedLanguage")} {_sharedLocalizer.GetString("Created Successfully")}";
                        return RedirectToAction("Index", "ProjectEnumPropertySelectedLanguage", new { Area = "Admin", projectEnumPropertyId = create.ProjectEnumPropertyId, });
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

            await GetViewDatas(create.ProjectEnumPropertyId, true);
            return View(create);
        }

        #endregion

        #region update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            UpdateProjectEnumPropertySelectedLanguageDto? projectEnumPropertySelectedLanguageInformation = await _projectService.GetProjectEnumPropertySelectedLanguageInformation(id);
            if (projectEnumPropertySelectedLanguageInformation is null) return NotFound();
            await GetViewDatas(id, true);
            return View(projectEnumPropertySelectedLanguageInformation);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProjectEnumPropertySelectedLanguageDto update)
        {

            if (!ModelState.IsValid)
            {
                await GetViewDatas(update.ProjectEnumPropertyId, true);
                return View(update);
            }
            BaseChangeEntityResult result = await _projectService.UpdateProjectEnumPropertySelectedLanguage(update);

            #region handling different types

            switch (result)
            {

                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectEnumPropertySelectedLanguage")} {_sharedLocalizer.GetString("Updated Successfully.")}";
                        return RedirectToAction("Index", "ProjectEnumPropertySelectedLanguage", new { Area = "Admin", projectEnumPropertyId = update.ProjectEnumPropertyId, });
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

            await GetViewDatas(update.ProjectEnumPropertyId, true);
            return View(update);
        }

        #endregion

        #region view datas

        async Task GetViewDatas(int projectEnumPropertyId, bool showNotUsedLanguagesInProject = false)
        {
            await GetLanguagesViewData(projectEnumPropertyId, showNotUsedLanguagesInProject);
        }

        async Task GetLanguagesViewData(int projectEnumPropertyId, bool showNotUsedLanguagesInProject = false)
        {
            ProjectEnumPropertyListDto enumPropertyInformation = await _projectService
                .GetSingleProjectEnumPropertyInformation(projectEnumPropertyId) ?? new ProjectEnumPropertyListDto();
            ViewData["Languages"] = (await _siteService
        .GetLanguagesAsCombo(new FilterLanguagesDto()
        {
            ProjectId = enumPropertyInformation.ProjectId,
            ExcludeProjectEnumPropertyId = showNotUsedLanguagesInProject ? projectEnumPropertyId : 0
        }))
        .ToSelectListItem();
        }

        #endregion

        #region delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ProjectEnumPropertySelectedLanguageListDto? projectEnumPropertySelectedLanguageInformation = await _projectService.GetSingleProjectEnumPropertySelectedLanguageInformation(id);
            if (projectEnumPropertySelectedLanguageInformation is null) return NotFound();
            BaseChangeEntityResult result = await _projectService.DeleteProjectEnumPropertySelectedLanguage(id);
            switch (result)
            {
                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectEnumPropertySelectedLanguage")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
                        return RedirectToAction("Index", "ProjectEnumPropertySelectedLanguage", new { Area = "Admin", projectEnumPropertyId = projectEnumPropertySelectedLanguageInformation.ProjectEnumPropertyId, });
                    }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRange(List<int> ids)
        {
            if (!ids.Distinct().Any())
            {
                TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Please AtLeast Choose One Item.")}";
                return RedirectToAction("Index", "ProjectEnumPropertySelectedLanguage", new { Area = "Admin" });
            }
            await _projectService.DeleteProjectEnumPropertySelectedLanguage(ids);
            TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectEnumPropertySelectedLanguages")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
            return RedirectToAction("Index", "ProjectEnumPropertySelectedLanguage", new { Area = "Admin" });
        }

        #endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterProjectEnumPropertySelectedLanguagesDto filter)
        {
            List<ProjectEnumPropertySelectedLanguageListDto> result = (await _projectService.FilterProjectEnumPropertySelectedLanguages(filter)).ProjectEnumPropertySelectedLanguages;
            string title = $"{_sharedEntitiesLocalizer.GetString("ProjectEnumPropertySelectedLanguages")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<ProjectEnumPropertySelectedLanguageListDto> excelExporter = new ExcelExporter<ProjectEnumPropertySelectedLanguageListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("LanguageName")}", 2, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("LanguageId")}", 3, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("ProjectEnumPropertyName")}", 4, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("ProjectEnumPropertyId")}", 5, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("Title")}", 6, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws, (rowIndex - 3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.LanguageName, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.LanguageId.ToString("#,0"), 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectEnumPropertyName, 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectEnumPropertyId.ToString("#,0"), 5, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Title, 6, rowIndex);

                    rowIndex++;
                }
                ws.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    wbook.SaveAs(stream);
                    stream.Position = 0;
                    return ReturnExcel(stream, title);
                }
            }
        }

        #endregion

        #region export pdf

        [HttpGet]
        public async Task<IActionResult> ExportPdf(FilterProjectEnumPropertySelectedLanguagesDto filter)
        {
            List<ProjectEnumPropertySelectedLanguageListDto> result = (await _projectService.FilterProjectEnumPropertySelectedLanguages(filter)).ProjectEnumPropertySelectedLanguages;
            string title = $"{_sharedEntitiesLocalizer.GetString("ProjectEnumPropertySelectedLanguages")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("LanguageName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("LanguageId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectEnumPropertyName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectEnumPropertyId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Title")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.LanguageName,

item.LanguageId.ToString("#,0"),

item.ProjectEnumPropertyName,

item.ProjectEnumPropertyId.ToString("#,0"),

item.Title);
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
