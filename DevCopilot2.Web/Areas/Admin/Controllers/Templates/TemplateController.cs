using System;
using System.Text;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Web.PresentationExtensions;
using DevCopilot2.Web.PresentationMappers;
using DevCopilot2.Domain.Enums.Templates;
using DevCopilot2.Domain.DTOs.Templates;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ClosedXML.Excel;
using DevCopilot2.Core.Exporters;
using DevCopilot2.Domain.DTOs.Projects;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Templates
{
	//[PermissionChecker("TemplateManagement")]
    public class TemplateController : BaseAdminController<TemplateListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<TemplateController> _localizer;
        private readonly ITemplateService _templateService;
        private readonly IProjectService _projectService;
        public TemplateController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<TemplateController> localizer,
                           ITemplateService templateService,
                           IProjectService projectService 
                                      )
        {
            this._sharedLocalizer = sharedLocalizer;
            this._sharedEntitiesLocalizer = sharedEntitiesLocalizer;
            this._localizer = localizer;
            this._templateService = templateService;
            this._projectService = projectService;
        }

        #endregion

        #region index

        [HttpGet]
        public async Task<IActionResult> Index(FilterTemplatesDto filter)
        {

            return View(await _templateService.FilterTemplates(filter));
        }

        #endregion

        #region detail

		[HttpGet]
		public async Task<IActionResult>Detail(int id)
		{

            TemplateListDto? templateInformation = await _templateService.GetSingleTemplateInformation(id);
			if (
            templateInformation is null)return NotFound();

            await GetViewDatas();
            return View(
            templateInformation);	
		}

		#endregion

        #region create

		[HttpGet]
		public async Task<IActionResult> Create()
		{
            await GetViewDatas();
            CreateTemplateDto create = new CreateTemplateDto()
            {

            };
            return View(create);
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreateTemplateDto create)
		{

            create.AuthorId = User.GetCurrentUserId();

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(create);
            }
			BaseChangeEntityResult result = await _templateService.CreateTemplate(create);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Template")} {_sharedLocalizer.GetString("Created Successfully")}";
					return RedirectToAction("Index", "Template", new { Area = "Admin", });
				}

                case BaseChangeEntityResult.NotFound:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                }

				case BaseChangeEntityResult.Exists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Template")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Title")} {create.Title}";
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
			UpdateTemplateDto? templateInformation = await _templateService.GetTemplateInformation(id);
			if (templateInformation is null) return NotFound();
			await GetViewDatas();
            return View(templateInformation);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateTemplateDto update)
		{

            update.AuthorId = User.GetCurrentUserId();

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(update);
            }
			BaseChangeEntityResult result = await _templateService.UpdateTemplate(update);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Template")} {_sharedLocalizer.GetString("Updated Successfully.")}";
					return RedirectToAction("Index", "Template", new { Area = "Admin", });
				}

                case BaseChangeEntityResult.NotFound:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                }

				case BaseChangeEntityResult.Exists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Template")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Title")} {update.Title}";
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
        }

        async Task GetProjectsViewData()
        => ViewData["Projects"] = (await _projectService
        .GetProjectsAsCombo(new FilterProjectsDto()))
        .ToSelectListItem(false,true);

        #endregion

        #region delete

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
            TemplateListDto? templateInformation = await _templateService.GetSingleTemplateInformation(id);
            if (templateInformation is null) return NotFound();
			BaseChangeEntityResult result = await _templateService.DeleteTemplate(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Template")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
					return RedirectToAction("Index", "Template", new { Area = "Admin", });
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
				return RedirectToAction("Index", "Template", new { Area = "Admin" });
			}
			await _templateService.DeleteTemplate(ids);
			TempData[SuccessMessage] =$"{_sharedEntitiesLocalizer.GetString("Templates")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
			return RedirectToAction("Index", "Template", new { Area = "Admin" });
		}

		#endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterTemplatesDto filter)
        {
            List<TemplateListDto> result = (await _templateService.FilterTemplates(filter)).Templates;
            string title = $"{_sharedEntitiesLocalizer.GetString("Templates")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<TemplateListDto> excelExporter = new ExcelExporter<TemplateListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("Title")}", 2, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ProjectTitle")}", 3, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ProjectId")}", 4, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ListViewHtml")}", 5, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ListFirstThCode")}", 6, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ListOtherThCodes")}", 7, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ListBoolTdHtml")}", 8, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ListTextTdHtml")}", 9, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ListImageTdHtml")}", 10, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ListPriceTdHtml")}", 11, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ListDefaultTdCode")}", 12, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ListViewCardHtml")}", 13, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("CreatePageHtml")}", 14, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("CheckBoxInputCode")}", 15, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("FileInputCode")}", 16, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("TextInputHtml")}", 17, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("TextEditorInputHtml")}", 18, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("IntegerInputHtml")}", 19, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("SelectInputHtml")}", 20, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("AuthorPhoneNumber")}", 21, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("AuthorId")}", 22, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("SingleImageHtml")}", 23, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ColorPickerInputCode")}", 24, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("BreadCrumbCode")}", 25, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("AnchorTagCode")}", 26, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("SubmitBtnCode")}", 27, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Title, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectTitle, 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectId?.ToString(), 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ListViewHtml, 5, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ListFirstThCode, 6, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ListOtherThCodes, 7, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ListBoolTdHtml, 8, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ListTextTdHtml, 9, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ListImageTdHtml, 10, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ListPriceTdHtml, 11, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ListDefaultTdCode, 12, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ListViewCardHtml, 13, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.CreatePageHtml, 14, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.CheckBoxInputCode, 15, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.FileInputCode, 16, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.TextInputHtml, 17, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.TextEditorInputHtml, 18, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.IntegerInputHtml, 19, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.SelectInputHtml, 20, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.AuthorPhoneNumber, 21, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.AuthorId.ToString(), 22, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.SingleImageHtml, 23, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ColorPickerInputCode, 24, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.BreadCrumbCode, 25, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.AnchorTagCode, 26, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.SubmitBtnCode, 27, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterTemplatesDto filter)
        {
            List<TemplateListDto> result = (await _templateService.FilterTemplates(filter)).Templates;
            string title = $"{_sharedEntitiesLocalizer.GetString("Templates")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Title")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectTitle")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ListViewHtml")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ListFirstThCode")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ListOtherThCodes")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ListBoolTdHtml")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ListTextTdHtml")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ListImageTdHtml")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ListPriceTdHtml")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ListDefaultTdCode")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ListViewCardHtml")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("CreatePageHtml")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("CheckBoxInputCode")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("FileInputCode")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("TextInputHtml")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("TextEditorInputHtml")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("IntegerInputHtml")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("SelectInputHtml")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("AuthorPhoneNumber")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("AuthorId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("SingleImageHtml")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ColorPickerInputCode")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("BreadCrumbCode")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("AnchorTagCode")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("SubmitBtnCode")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.Title,

item.ProjectTitle,

item.ProjectId?.ToString(),

item.ListViewHtml,

item.ListFirstThCode,

item.ListOtherThCodes,

item.ListBoolTdHtml,

item.ListTextTdHtml,

item.ListImageTdHtml,

item.ListPriceTdHtml,

item.ListDefaultTdCode,

item.ListViewCardHtml,

item.CreatePageHtml,

item.CheckBoxInputCode,

item.FileInputCode,

item.TextInputHtml,

item.TextEditorInputHtml,

item.IntegerInputHtml,

item.SelectInputHtml,

item.AuthorPhoneNumber,

item.AuthorId.ToString(),

item.SingleImageHtml,

item.ColorPickerInputCode,

item.BreadCrumbCode,

item.AnchorTagCode,

item.SubmitBtnCode );
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
