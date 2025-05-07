using ClosedXML.Excel;
using DevCopilot2.Core.Exporters;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Languages;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Web.PresentationMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Text;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Projects
{
    [PermissionChecker("ProjectSelectedLanguageManagement")]
    public class ProjectSelectedLanguageController : BaseAdminController<ProjectSelectedLanguageListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<ProjectSelectedLanguageController> _localizer;
        private readonly IProjectService _projectService;
        private readonly ISiteService _siteService;
        public ProjectSelectedLanguageController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<ProjectSelectedLanguageController> localizer,
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
        public async Task<IActionResult> Index(FilterProjectSelectedLanguagesDto filter)
        {

            return View(await _projectService.FilterProjectSelectedLanguages(filter));
        }

        #endregion

        #region detail

		[HttpGet]
		public async Task<IActionResult>Detail(int id)
		{

            ProjectSelectedLanguageListDto? projectSelectedLanguageInformation = await _projectService.GetSingleProjectSelectedLanguageInformation(id);
			if (
            projectSelectedLanguageInformation is null)return NotFound();

            await GetViewDatas();
            return View(
            projectSelectedLanguageInformation);	
		}

		#endregion

        #region create

		[HttpGet]
		public async Task<IActionResult> Create(int projectId)
		{
            await GetViewDatas();
            CreateProjectSelectedLanguageDto create = new CreateProjectSelectedLanguageDto()
            {
                ProjectId = projectId, 
            };
            return View(create);
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreateProjectSelectedLanguageDto create)
		{

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(create);
            }
			BaseChangeEntityResult result = await _projectService.CreateProjectSelectedLanguage(create);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectSelectedLanguage")} {_sharedLocalizer.GetString("Created Successfully")}";
					return RedirectToAction("Index", "ProjectSelectedLanguage", new { Area = "Admin",projectId=create.ProjectId, });
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

            await GetViewDatas();
			return View(create);
		}

		#endregion

        #region update

		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			UpdateProjectSelectedLanguageDto? projectSelectedLanguageInformation = await _projectService.GetProjectSelectedLanguageInformation(id);
			if (projectSelectedLanguageInformation is null) return NotFound();
			await GetViewDatas();
            return View(projectSelectedLanguageInformation);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateProjectSelectedLanguageDto update)
		{

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(update);
            }
			BaseChangeEntityResult result = await _projectService.UpdateProjectSelectedLanguage(update);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectSelectedLanguage")} {_sharedLocalizer.GetString("Updated Successfully.")}";
					return RedirectToAction("Index", "ProjectSelectedLanguage", new { Area = "Admin",projectId=update.ProjectId, });
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

            await GetViewDatas();
			return View(update);
		}

		#endregion

        #region view datas

        async Task GetViewDatas()
        {
            await GetLanguagesViewData();
        }

        async Task GetLanguagesViewData()
        => ViewData["Languages"] = (await _siteService
        .GetLanguagesAsCombo(new FilterLanguagesDto()))
        .ToSelectListItem();

        #endregion

        #region delete

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
            ProjectSelectedLanguageListDto? projectSelectedLanguageInformation = await _projectService.GetSingleProjectSelectedLanguageInformation(id);
            if (projectSelectedLanguageInformation is null) return NotFound();
			BaseChangeEntityResult result = await _projectService.DeleteProjectSelectedLanguage(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("ProjectSelectedLanguage")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
					return RedirectToAction("Index", "ProjectSelectedLanguage", new { Area = "Admin",projectId=projectSelectedLanguageInformation.ProjectId, });
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
				return RedirectToAction("Index", "ProjectSelectedLanguage", new { Area = "Admin" });
			}
			await _projectService.DeleteProjectSelectedLanguage(ids);
			TempData[SuccessMessage] =$"{_sharedEntitiesLocalizer.GetString("ProjectSelectedLanguages")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
			return RedirectToAction("Index", "ProjectSelectedLanguage", new { Area = "Admin" });
		}

		#endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterProjectSelectedLanguagesDto filter)
        {
            List<ProjectSelectedLanguageListDto> result = (await _projectService.FilterProjectSelectedLanguages(filter)).ProjectSelectedLanguages;
            string title = $"{_sharedEntitiesLocalizer.GetString("ProjectSelectedLanguages")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<ProjectSelectedLanguageListDto> excelExporter = new ExcelExporter<ProjectSelectedLanguageListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ProjectTitle")}", 2, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ProjectId")}", 3, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("LanguageName")}", 4, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("LanguageId")}", 5, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectTitle, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectId.ToString("#,0"), 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.LanguageName, 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.LanguageId.ToString("#,0"), 5, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterProjectSelectedLanguagesDto filter)
        {
            List<ProjectSelectedLanguageListDto> result = (await _projectService.FilterProjectSelectedLanguages(filter)).ProjectSelectedLanguages;
            string title = $"{_sharedEntitiesLocalizer.GetString("ProjectSelectedLanguages")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectTitle")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("LanguageName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("LanguageId")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.ProjectTitle,

item.ProjectId.ToString("#,0"),

item.LanguageName,

item.LanguageId.ToString("#,0") );
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
