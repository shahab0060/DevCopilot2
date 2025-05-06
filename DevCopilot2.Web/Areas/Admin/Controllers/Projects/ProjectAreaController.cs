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
using DevCopilot2.Domain.DTOs.Templates;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Projects
{
	//[PermissionChecker("ProjectAreaManagement")]
    public class ProjectAreaController : BaseAdminController<ProjectAreaListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<ProjectAreaController> _localizer;
        private readonly IProjectService _projectService;
        private readonly ITemplateService _templateService;
        public ProjectAreaController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<ProjectAreaController> localizer,
                           IProjectService projectService,
                           ITemplateService templateService 
                                      )
        {
            this._sharedLocalizer = sharedLocalizer;
            this._sharedEntitiesLocalizer = sharedEntitiesLocalizer;
            this._localizer = localizer;
            this._projectService = projectService;
            this._templateService = templateService;
        }

        #endregion

        #region index

        [HttpGet]
        public async Task<IActionResult> Index(FilterProjectAreasDto filter)
        {

            return View(await _projectService.FilterProjectAreas(filter));
        }

        #endregion

        #region detail

		[HttpGet]
		public async Task<IActionResult>Detail(int id)
		{

            ProjectAreaListDto? projectAreaInformation = await _projectService.GetSingleProjectAreaInformation(id);
			if (
            projectAreaInformation is null)return NotFound();

            await GetViewDatas();
            return View(
            projectAreaInformation);	
		}

		#endregion

        #region create

		[HttpGet]
		public async Task<IActionResult> Create(int projectId)
		{
            await GetViewDatas();
            CreateProjectAreaDto create = new CreateProjectAreaDto()
            {
                ProjectId = projectId, 
            };
            return View(create);
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreateProjectAreaDto create)
		{

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(create);
            }
			ChangeProjectAreaResult result = await _projectService.CreateProjectArea(create);

            #region handling different types

            switch (result)
			{

				case ChangeProjectAreaResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectArea")} {_sharedLocalizer.GetString("Created Successfully")}";
                        return RedirectToAction("Index", "ProjectArea", new { Area = "Admin",projectId=create.ProjectId, });
                    }

				case ChangeProjectAreaResult.NotFound:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                    }

				case ChangeProjectAreaResult.EnglishNameExists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("ProjectArea")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("EnglishName")} {create.EnglishName}";
                        break;
                }

				case ChangeProjectAreaResult.TitleExists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("ProjectArea")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Title")} {create.Title}";
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
			UpdateProjectAreaDto? projectAreaInformation = await _projectService.GetProjectAreaInformation(id);
			if (projectAreaInformation is null) return NotFound();
			await GetViewDatas();
            return View(projectAreaInformation);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateProjectAreaDto update)
		{

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(update);
            }
			ChangeProjectAreaResult result = await _projectService.UpdateProjectArea(update);

            #region handling different types

            switch (result)
			{

				case ChangeProjectAreaResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectArea")} {_sharedLocalizer.GetString("Updated Successfully.")}";
                        return RedirectToAction("Index", "ProjectArea", new { Area = "Admin",projectId=update.ProjectId, });
                    }

				case ChangeProjectAreaResult.NotFound:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                    }

				case ChangeProjectAreaResult.EnglishNameExists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("ProjectArea")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("EnglishName")} {update.EnglishName}";
                        break;
                }

				case ChangeProjectAreaResult.TitleExists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("ProjectArea")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Title")} {update.Title}";
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
        }

        async Task GetTemplatesViewData()
        => ViewData["Templates"] = (await _templateService
        .GetTemplatesAsCombo(new FilterTemplatesDto()))
        .ToSelectListItem();

        #endregion

        #region delete

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
            ProjectAreaListDto? projectAreaInformation = await _projectService.GetSingleProjectAreaInformation(id);
            if (projectAreaInformation is null) return NotFound();
			BaseChangeEntityResult result = await _projectService.DeleteProjectArea(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectArea")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
					return RedirectToAction("Index", "ProjectArea", new { Area = "Admin",projectId=projectAreaInformation.ProjectId, });
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
				return RedirectToAction("Index", "ProjectArea", new { Area = "Admin" });
			}
			await _projectService.DeleteProjectArea(ids);
			TempData[SuccessMessage] =$"{_sharedEntitiesLocalizer.GetString("ProjectAreas")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
			return RedirectToAction("Index", "ProjectArea", new { Area = "Admin" });
		}

		#endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterProjectAreasDto filter)
        {
            List<ProjectAreaListDto> result = (await _projectService.FilterProjectAreas(filter)).ProjectAreas;
            string title = $"{_sharedEntitiesLocalizer.GetString("ProjectAreas")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<ProjectAreaListDto> excelExporter = new ExcelExporter<ProjectAreaListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ProjectTitle")}", 2, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ProjectId")}", 3, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("EnglishName")}", 4, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("Title")}", 5, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("TemplateTitle")}", 6, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("TemplateId")}", 7, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectTitle, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectId.ToString("#,0"), 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.EnglishName, 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Title, 5, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.TemplateTitle, 6, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.TemplateId.ToString("#,0"), 7, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterProjectAreasDto filter)
        {
            List<ProjectAreaListDto> result = (await _projectService.FilterProjectAreas(filter)).ProjectAreas;
            string title = $"{_sharedEntitiesLocalizer.GetString("ProjectAreas")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectTitle")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("EnglishName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Title")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("TemplateTitle")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("TemplateId")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.ProjectTitle,

item.ProjectId.ToString("#,0"),

item.EnglishName,

item.Title,

item.TemplateTitle,

item.TemplateId.ToString("#,0") );
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
