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

namespace DevCopilot2.Web.Areas.Admin.Controllers.Projects
{
	//[PermissionChecker("ProjectEnumManagement")]
    public class ProjectEnumController : BaseAdminController<ProjectEnumListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<ProjectEnumController> _localizer;
        private readonly IProjectService _projectService;
        private readonly ISiteService _siteService;
        public ProjectEnumController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<ProjectEnumController> localizer,
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
        public async Task<IActionResult> Index(FilterProjectEnumsDto filter)
        {

            return View(await _projectService.FilterProjectEnums(filter));
        }

        #endregion

        #region detail

		[HttpGet]
		public async Task<IActionResult>Detail(int id)
		{

            ProjectEnumListDto? projectEnumInformation = await _projectService.GetSingleProjectEnumInformation(id);
			if (
            projectEnumInformation is null)return NotFound();

            await GetViewDatas();
            return View(
            projectEnumInformation);	
		}

		#endregion

        #region create

		[HttpGet]
		public async Task<IActionResult> Create()
		{
            await GetViewDatas();
            CreateProjectEnumDto create = new CreateProjectEnumDto()
            {

            };
            return View(create);
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreateProjectEnumDto create)
		{

            create.AuthorId = User.GetCurrentUserId();

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(create);
            }
			BaseChangeEntityResult result = await _projectService.CreateProjectEnum(create);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectEnum")} {_sharedLocalizer.GetString("Created Successfully")}";
					return RedirectToAction("Index", "ProjectEnum", new { Area = "Admin", });
				}

                case BaseChangeEntityResult.NotFound:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                }

				case BaseChangeEntityResult.Exists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("ProjectEnum")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("EnglishName")} {create.EnglishName}";
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
			UpdateProjectEnumDto? projectEnumInformation = await _projectService.GetProjectEnumInformation(id);
			if (projectEnumInformation is null) return NotFound();
			await GetViewDatas();
            return View(projectEnumInformation);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateProjectEnumDto update)
		{

            update.AuthorId = User.GetCurrentUserId();

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(update);
            }
			BaseChangeEntityResult result = await _projectService.UpdateProjectEnum(update);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectEnum")} {_sharedLocalizer.GetString("Updated Successfully.")}";
					return RedirectToAction("Index", "ProjectEnum", new { Area = "Admin", });
				}

                case BaseChangeEntityResult.NotFound:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                }

				case BaseChangeEntityResult.Exists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("ProjectEnum")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("EnglishName")} {update.EnglishName}";
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
            await GetProjectsViewData();
            await GetLanguagesViewData();
        }

        async Task GetProjectsViewData()
        => ViewData["Projects"] = (await _projectService
        .GetProjectsAsCombo(new FilterProjectsDto()))
        .ToSelectListItem(false,true);

        async Task GetLanguagesViewData()
        => ViewData["Languages"] = (await _siteService
        .GetLanguagesAsCombo(new FilterLanguagesDto()))
        .ToSelectListItem();

        #endregion

        #region delete

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
            ProjectEnumListDto? projectEnumInformation = await _projectService.GetSingleProjectEnumInformation(id);
            if (projectEnumInformation is null) return NotFound();
			BaseChangeEntityResult result = await _projectService.DeleteProjectEnum(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectEnum")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
					return RedirectToAction("Index", "ProjectEnum", new { Area = "Admin", });
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
				return RedirectToAction("Index", "ProjectEnum", new { Area = "Admin" });
			}
			await _projectService.DeleteProjectEnum(ids);
			TempData[SuccessMessage] =$"{_sharedEntitiesLocalizer.GetString("ProjectEnums")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
			return RedirectToAction("Index", "ProjectEnum", new { Area = "Admin" });
		}

		#endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterProjectEnumsDto filter)
        {
            List<ProjectEnumListDto> result = (await _projectService.FilterProjectEnums(filter)).ProjectEnums;
            string title = $"{_sharedEntitiesLocalizer.GetString("ProjectEnums")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<ProjectEnumListDto> excelExporter = new ExcelExporter<ProjectEnumListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ProjectTitle")}", 2, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ProjectId")}", 3, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("EnglishName")}", 4, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("FolderName")}", 5, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("AuthorPhoneNumber")}", 6, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("AuthorId")}", 7, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectTitle, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectId?.ToString(), 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.EnglishName, 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.FolderName, 5, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.AuthorPhoneNumber, 6, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.AuthorId.ToString(), 7, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterProjectEnumsDto filter)
        {
            List<ProjectEnumListDto> result = (await _projectService.FilterProjectEnums(filter)).ProjectEnums;
            string title = $"{_sharedEntitiesLocalizer.GetString("ProjectEnums")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectTitle")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("EnglishName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("FolderName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("AuthorPhoneNumber")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("AuthorId")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.ProjectTitle,

item.ProjectId?.ToString(),

item.EnglishName,

item.FolderName,

item.AuthorPhoneNumber,

item.AuthorId.ToString() );
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
