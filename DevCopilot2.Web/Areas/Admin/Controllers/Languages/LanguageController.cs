using System;
using System.Text;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Web.PresentationExtensions;
using DevCopilot2.Web.PresentationMappers;
using DevCopilot2.Domain.Enums.Languages;
using DevCopilot2.Domain.DTOs.Languages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ClosedXML.Excel;
using DevCopilot2.Core.Exporters;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Languages
{
	//[PermissionChecker("LanguageManagement")]
    public class LanguageController : BaseAdminController<LanguageListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<LanguageController> _localizer;
        private readonly ISiteService _siteService;
        public LanguageController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<LanguageController> localizer,
                           ISiteService siteService 
                                      )
        {
            this._sharedLocalizer = sharedLocalizer;
            this._sharedEntitiesLocalizer = sharedEntitiesLocalizer;
            this._localizer = localizer;
            this._siteService = siteService;
        }

        #endregion

        #region index

        [HttpGet]
        public async Task<IActionResult> Index(FilterLanguagesDto filter)
        {

            return View(await _siteService.FilterLanguages(filter));
        }

        #endregion

        #region detail

		[HttpGet]
		public async Task<IActionResult>Detail(int id)
		{

            LanguageListDto? languageInformation = await _siteService.GetSingleLanguageInformation(id);
			if (
            languageInformation is null)return NotFound();

            return View(
            languageInformation);	
		}

		#endregion

        #region create

		[HttpGet]
		public IActionResult Create()
		{

            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreateLanguageDto create)
		{

			if (!ModelState.IsValid)
			{

                return View(create);
            }
			BaseChangeEntityResult result = await _siteService.CreateLanguage(create);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Language")} {_sharedLocalizer.GetString("Created Successfully")}";
					return RedirectToAction("Index", "Language", new { Area = "Admin", });
				}

                case BaseChangeEntityResult.NotFound:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                }

				case BaseChangeEntityResult.Exists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Language")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Name")} {create.Name}";
                        break;
                }

			}

            #endregion

			return View(create);
		}

		#endregion

        #region update

		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			UpdateLanguageDto? languageInformation = await _siteService.GetLanguageInformation(id);
			if (languageInformation is null) return NotFound();

            return View(languageInformation);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateLanguageDto update)
		{

			if (!ModelState.IsValid)
			{

                return View(update);
            }
			BaseChangeEntityResult result = await _siteService.UpdateLanguage(update);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Language")} {_sharedLocalizer.GetString("Updated Successfully.")}";
					return RedirectToAction("Index", "Language", new { Area = "Admin", });
				}

                case BaseChangeEntityResult.NotFound:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                }

				case BaseChangeEntityResult.Exists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Language")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Name")} {update.Name}";
                        break;
                }

			}

            #endregion

			return View(update);
		}

		#endregion

        #region delete

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
            LanguageListDto? languageInformation = await _siteService.GetSingleLanguageInformation(id);
            if (languageInformation is null) return NotFound();
			BaseChangeEntityResult result = await _siteService.DeleteLanguage(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Language")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
					return RedirectToAction("Index", "Language", new { Area = "Admin", });
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
				return RedirectToAction("Index", "Language", new { Area = "Admin" });
			}
			await _siteService.DeleteLanguage(ids);
			TempData[SuccessMessage] =$"{_sharedEntitiesLocalizer.GetString("Languages")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
			return RedirectToAction("Index", "Language", new { Area = "Admin" });
		}

		#endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterLanguagesDto filter)
        {
            List<LanguageListDto> result = (await _siteService.FilterLanguages(filter)).Languages;
            string title = $"{_sharedEntitiesLocalizer.GetString("Languages")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<LanguageListDto> excelExporter = new ExcelExporter<LanguageListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("Name")}", 2, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("Culture")}", 3, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("DefaultPluralSuffix")}", 4, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Name, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Culture, 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.DefaultPluralSuffix, 4, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterLanguagesDto filter)
        {
            List<LanguageListDto> result = (await _siteService.FilterLanguages(filter)).Languages;
            string title = $"{_sharedEntitiesLocalizer.GetString("Languages")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Name")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Culture")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("DefaultPluralSuffix")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.Name,

item.Culture,

item.DefaultPluralSuffix );
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
