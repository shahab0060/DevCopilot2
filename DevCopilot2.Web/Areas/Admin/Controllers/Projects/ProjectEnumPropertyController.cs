using ClosedXML.Excel;
using DevCopilot2.Core.Exporters;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Languages;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Web.PresentationMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Text;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Projects
{
    [PermissionChecker("ProjectEnumPropertyManagement")]
    public class ProjectEnumPropertyController : BaseAdminController<ProjectEnumPropertyListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<ProjectEnumPropertyController> _localizer;
        private readonly IProjectService _projectService;
        private readonly ISiteService _siteService;
        public ProjectEnumPropertyController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<ProjectEnumPropertyController> localizer,
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
        public async Task<IActionResult> Index(FilterProjectEnumPropertiesDto filter)
        {

            return View(await _projectService.FilterProjectEnumProperties(filter));
        }

        #endregion

        #region detail

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {

            ProjectEnumPropertyListDto? projectEnumPropertyInformation = await _projectService.GetSingleProjectEnumPropertyInformation(id);
            if (projectEnumPropertyInformation is null) return NotFound();
            await GetViewDatas(projectEnumPropertyInformation.ProjectEnumId);
            return View(projectEnumPropertyInformation);
        }

        #endregion

        #region create

        [HttpGet]
        public async Task<IActionResult> Create(int projectEnumId)
        {
            await GetViewDatas(projectEnumId);
            CreateProjectEnumPropertyDto create = new CreateProjectEnumPropertyDto()
            {
                ProjectEnumId = projectEnumId,
            };
            return View(create);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectEnumPropertyDto create)
        {

            if (!ModelState.IsValid)
            {
                await GetViewDatas(create.ProjectEnumId);
                return View(create);
            }
            BaseChangeEntityResult result = await _projectService.CreateProjectEnumProperty(create);

            #region handling different types

            switch (result)
            {

                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectEnumProperty")} {_sharedLocalizer.GetString("Created Successfully")}";
                        return RedirectToAction("Index", "ProjectEnumProperty", new { Area = "Admin", projectEnumId = create.ProjectEnumId, });
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

            await GetViewDatas(create.ProjectEnumId);
            return View(create);
        }

        #endregion

        #region update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            UpdateProjectEnumPropertyDto? projectEnumPropertyInformation = await _projectService.GetProjectEnumPropertyInformation(id);
            if (projectEnumPropertyInformation is null) return NotFound();
            await GetViewDatas(projectEnumPropertyInformation.ProjectEnumId);
            return View(projectEnumPropertyInformation);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProjectEnumPropertyDto update)
        {

            if (!ModelState.IsValid)
            {
                await GetViewDatas(update.ProjectEnumId);
                return View(update);
            }
            BaseChangeEntityResult result = await _projectService.UpdateProjectEnumProperty(update);

            #region handling different types

            switch (result)
            {

                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectEnumProperty")} {_sharedLocalizer.GetString("Updated Successfully.")}";
                        return RedirectToAction("Index", "ProjectEnumProperty", new { Area = "Admin", projectEnumId = update.ProjectEnumId, });
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

            await GetViewDatas(update.ProjectEnumId);
            return View(update);
        }

        #endregion

        #region view datas

        async Task GetViewDatas(int projectEnumId)
        {
            ProjectEnumListDto projectEnumInformation = await _projectService
                .GetSingleProjectEnumInformation(projectEnumId) ?? new ProjectEnumListDto();
            await GetLanguagesViewData(projectEnumInformation.ProjectId ?? 0);
        }

        async Task GetLanguagesViewData(int projectId)
        => ViewData["Languages"] = (await _siteService
        .GetLanguagesAsCombo(new FilterLanguagesDto()
        {
            ProjectId = projectId,
        }))
        .ToSelectListItem();

        #endregion

        #region delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ProjectEnumPropertyListDto? projectEnumPropertyInformation = await _projectService.GetSingleProjectEnumPropertyInformation(id);
            if (projectEnumPropertyInformation is null) return NotFound();
            BaseChangeEntityResult result = await _projectService.DeleteProjectEnumProperty(id);
            switch (result)
            {
                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectEnumProperty")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
                        return RedirectToAction("Index", "ProjectEnumProperty", new { Area = "Admin", projectEnumId = projectEnumPropertyInformation.ProjectEnumId, });
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
                return RedirectToAction("Index", "ProjectEnumProperty", new { Area = "Admin" });
            }
            await _projectService.DeleteProjectEnumProperty(ids);
            TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectEnumProperties")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
            return RedirectToAction("Index", "ProjectEnumProperty", new { Area = "Admin" });
        }

        #endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterProjectEnumPropertiesDto filter)
        {
            List<ProjectEnumPropertyListDto> result = (await _projectService.FilterProjectEnumProperties(filter)).ProjectEnumProperties;
            string title = $"{_sharedEntitiesLocalizer.GetString("ProjectEnumProperties")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<ProjectEnumPropertyListDto> excelExporter = new ExcelExporter<ProjectEnumPropertyListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("ProjectEnumEnglishName")}", 2, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("ProjectEnumId")}", 3, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("Name")}", 4, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("Order")}", 5, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws, (rowIndex - 3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectEnumEnglishName, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectEnumId.ToString("#,0"), 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Name, 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Order.ToString("#,0"), 5, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterProjectEnumPropertiesDto filter)
        {
            List<ProjectEnumPropertyListDto> result = (await _projectService.FilterProjectEnumProperties(filter)).ProjectEnumProperties;
            string title = $"{_sharedEntitiesLocalizer.GetString("ProjectEnumProperties")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectEnumEnglishName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectEnumId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Name")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Order")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.ProjectEnumEnglishName,

item.ProjectEnumId.ToString("#,0"),

item.Name,

item.Order.ToString("#,0"));
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
