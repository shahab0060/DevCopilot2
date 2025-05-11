using ClosedXML.Excel;
using DevCopilot2.Core.Exporters;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Core.Utils;
using DevCopilot2.Domain.DTOs.Languages;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Templates;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Domain.Enums.Projects;
using DevCopilot2.Web.PresentationExtensions;
using DevCopilot2.Web.PresentationMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Projects
{
    [PermissionChecker("ProjectManagement")]
    public class ProjectController : BaseAdminController<ProjectListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<ProjectController> _localizer;
        private readonly IProjectService _projectService;
        private readonly ITemplateService _templateService;
        private readonly ISiteService _siteService;
        private readonly IWebHostEnvironment _environment;
        public ProjectController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<ProjectController> localizer,
                           IProjectService projectService,
                           ITemplateService templateService,
                           ISiteService siteService,
                           IWebHostEnvironment environment
                                      )
        {
            this._sharedLocalizer = sharedLocalizer;
            this._sharedEntitiesLocalizer = sharedEntitiesLocalizer;
            this._localizer = localizer;
            this._projectService = projectService;
            this._templateService = templateService;
            this._siteService = siteService;
            this._environment = environment;
        }

        #endregion

        #region index

        [HttpGet]
        public async Task<IActionResult> Index(FilterProjectsDto filter)
        {

            return View(await _projectService.FilterProjects(filter));
        }

        #endregion

        #region detail

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {

            ProjectListDto? projectInformation = await _projectService.GetSingleProjectInformation(id);
            if (
            projectInformation is null) return NotFound();

            await GetViewDatas();
            return View(
            projectInformation);
        }

        #endregion

        #region create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetViewDatas();
            CreateProjectDto create = new CreateProjectDto()
            {

            };
            return View(create);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectDto create)
        {
            if (_environment.IsProduction())
                create.Location = $"{PathExtension.BaseSetPath}/files/{create.EnglishName}";

            create.AuthorId = User.GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                await GetViewDatas();
                return View(create);
            }
            ChangeProjectResult result = await _projectService.CreateProject(create);

            #region handling different types

            switch (result)
            {

                case ChangeProjectResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Project")} {_sharedLocalizer.GetString("Created Successfully")}";
                        return RedirectToAction("Index", "Project", new { Area = "Admin", });
                    }

                case ChangeProjectResult.NotFound:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                    }

                case ChangeProjectResult.EnglishNameExists:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Project")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("EnglishName")} {create.EnglishName}";
                        break;
                    }

                case ChangeProjectResult.TitleExists:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Project")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Title")} {create.Title}";
                        break;
                    }

                case ChangeProjectResult.LocationExists:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Project")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Location")} {create.Location}";
                        break;
                    }

            }

            #endregion

            await GetViewDatas();
            return View(create);
        }

        #endregion

        #region update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            UpdateProjectDto? projectInformation = await _projectService.GetProjectInformation(id);
            if (projectInformation is null) return NotFound();
            await GetViewDatas();
            return View(projectInformation);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProjectDto update)
        {
            if (_environment.IsProduction())
                update.Location = $"{PathExtension.BaseSetPath}/files/{update.EnglishName}";
            update.AuthorId = User.GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                await GetViewDatas();
                return View(update);
            }
            ChangeProjectResult result = await _projectService.UpdateProject(update);

            #region handling different types

            switch (result)
            {

                case ChangeProjectResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Project")} {_sharedLocalizer.GetString("Updated Successfully.")}";
                        return RedirectToAction("Index", "Project", new { Area = "Admin", });
                    }

                case ChangeProjectResult.NotFound:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                    }

                case ChangeProjectResult.EnglishNameExists:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Project")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("EnglishName")} {update.EnglishName}";
                        break;
                    }

                case ChangeProjectResult.TitleExists:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Project")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Title")} {update.Title}";
                        break;
                    }

                case ChangeProjectResult.LocationExists:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Project")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Location")} {update.Location}";
                        break;
                    }

            }

            #endregion

            await GetViewDatas();
            return View(update);
        }

        #endregion

        #region view datas

        async Task GetViewDatas()
        {
            await GetTemplatesViewData();
            await GetLanguagesViewData();
        }

        async Task GetTemplatesViewData()
        => ViewData["Templates"] = (await _templateService
        .GetTemplatesAsCombo(new FilterTemplatesDto()))
        .ToSelectListItem();

        async Task GetLanguagesViewData()
        => ViewData["Languages"] = (await _siteService
        .GetLanguagesAsCombo(new FilterLanguagesDto()))
        .ToSelectListItem();

        #endregion

        #region delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ProjectListDto? projectInformation = await _projectService.GetSingleProjectInformation(id);
            if (projectInformation is null) return NotFound();
            BaseChangeEntityResult result = await _projectService.DeleteProject(id);
            switch (result)
            {
                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Project")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
                        return RedirectToAction("Index", "Project", new { Area = "Admin", });
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
                return RedirectToAction("Index", "Project", new { Area = "Admin" });
            }
            await _projectService.DeleteProject(ids);
            TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Projects")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
            return RedirectToAction("Index", "Project", new { Area = "Admin" });
        }

        #endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterProjectsDto filter)
        {
            List<ProjectListDto> result = (await _projectService.FilterProjects(filter)).Projects;
            string title = $"{_sharedEntitiesLocalizer.GetString("Projects")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<ProjectListDto> excelExporter = new ExcelExporter<ProjectListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("EnglishName")}", 2, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("Title")}", 3, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("Location")}", 4, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("Architecture")}", 5, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("AuthorPhoneNumber")}", 6, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("AuthorId")}", 7, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("ReactProjectLocation")}", 8, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws, (rowIndex - 3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.EnglishName, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Title, 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Location, 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Architecture.GetEnumName(), 5, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.AuthorPhoneNumber, 6, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.AuthorId.ToString(), 7, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ReactProjectLocation?.ToString(), 8, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterProjectsDto filter)
        {
            List<ProjectListDto> result = (await _projectService.FilterProjects(filter)).Projects;
            string title = $"{_sharedEntitiesLocalizer.GetString("Projects")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("EnglishName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Title")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Location")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Architecture")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("AuthorPhoneNumber")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("AuthorId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ReactProjectLocation")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.EnglishName,

item.Title,

item.Location,

item.Architecture.GetEnumName(),

item.AuthorPhoneNumber,

item.AuthorId.ToString(),

item.ReactProjectLocation?.ToString());
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
