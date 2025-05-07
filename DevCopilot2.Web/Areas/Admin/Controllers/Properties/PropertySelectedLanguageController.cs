using ClosedXML.Excel;
using DevCopilot2.Core.Exporters;
using DevCopilot2.Core.Services.Classes;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Languages;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Web.PresentationMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Text;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Properties
{
    [PermissionChecker("PropertySelectedLanguageManagement")]
    public class PropertySelectedLanguageController : BaseAdminController<PropertySelectedLanguageListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<PropertySelectedLanguageController> _localizer;
        private readonly IEntityService _entityService;
        private readonly ISiteService _siteService;
        public PropertySelectedLanguageController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<PropertySelectedLanguageController> localizer,
                           IEntityService entityService,
                           ISiteService siteService
                                      )
        {
            this._sharedLocalizer = sharedLocalizer;
            this._sharedEntitiesLocalizer = sharedEntitiesLocalizer;
            this._localizer = localizer;
            this._entityService = entityService;
            this._siteService = siteService;
        }

        #endregion

        #region index

        [HttpGet]
        public async Task<IActionResult> Index(FilterPropertySelectedLanguagesDto filter)
        {

            return View(await _entityService.FilterPropertySelectedLanguages(filter));
        }

        #endregion

        #region detail

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {

            PropertySelectedLanguageListDto? propertySelectedLanguageInformation = await _entityService.GetSinglePropertySelectedLanguageInformation(id);
            if (
            propertySelectedLanguageInformation is null) return NotFound();

            await GetViewDatas(propertySelectedLanguageInformation.PropertyId);
            return View(propertySelectedLanguageInformation);
        }

        #endregion

        #region create

        [HttpGet]
        public async Task<IActionResult> Create(int propertyId)
        {
            await GetViewDatas(propertyId, true);
            CreatePropertySelectedLanguageDto create = new CreatePropertySelectedLanguageDto()
            {
                PropertyId = propertyId,
            };
            return View(create);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertySelectedLanguageDto create)
        {

            if (!ModelState.IsValid)
            {
                await GetViewDatas(create.PropertyId, true);
                return View(create);
            }
            BaseChangeEntityResult result = await _entityService.CreatePropertySelectedLanguage(create);

            #region handling different types

            switch (result)
            {

                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("PropertySelectedLanguage")} {_sharedLocalizer.GetString("Created Successfully")}";
                        return RedirectToAction("Index", "PropertySelectedLanguage", new { Area = "Admin", propertyId = create.PropertyId, });
                    }

                case BaseChangeEntityResult.NotFound:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                    }

                case BaseChangeEntityResult.Exists:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("PropertySelectedLanguage")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Title")} {create.Title}";
                        break;
                    }

            }

            #endregion

            await GetViewDatas(create.PropertyId, true);
            return View(create);
        }

        #endregion

        #region update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            UpdatePropertySelectedLanguageDto? propertySelectedLanguageInformation = await _entityService.GetPropertySelectedLanguageInformation(id);
            if (propertySelectedLanguageInformation is null) return NotFound();
            await GetViewDatas(propertySelectedLanguageInformation.PropertyId, true);
            return View(propertySelectedLanguageInformation);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePropertySelectedLanguageDto update)
        {

            if (!ModelState.IsValid)
            {
                await GetViewDatas(update.PropertyId, true);
                return View(update);
            }
            BaseChangeEntityResult result = await _entityService.UpdatePropertySelectedLanguage(update);

            #region handling different types

            switch (result)
            {

                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("PropertySelectedLanguage")} {_sharedLocalizer.GetString("Updated Successfully.")}";
                        return RedirectToAction("Index", "PropertySelectedLanguage", new { Area = "Admin", propertyId = update.PropertyId, });
                    }

                case BaseChangeEntityResult.NotFound:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                    }

                case BaseChangeEntityResult.Exists:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("PropertySelectedLanguage")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Title")} {update.Title}";
                        break;
                    }

            }

            #endregion

            await GetViewDatas(update.PropertyId, true);
            return View(update);
        }

        #endregion

        #region view datas

        async Task GetViewDatas(int propertyId, bool showNotUsedLanguagesInProject = false)
        {
            await GetLanguagesViewData(propertyId, showNotUsedLanguagesInProject);
        }

        async Task GetLanguagesViewData(int propertyId, bool showNotUsedLanguagesInProject = false)
        {
            PropertyListDto propertyInformation = await _entityService
                .GetSinglePropertyInformation(propertyId) ?? new PropertyListDto();
            ViewData["Languages"] = (await _siteService
                                    .GetLanguagesAsCombo(new FilterLanguagesDto()
                                    {
                                        ProjectId = propertyInformation.ProjectId,
                                        ExcludePropertyId = showNotUsedLanguagesInProject ? propertyId : 0
                                    })).ToSelectListItem();
        }

        #endregion

        #region delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            PropertySelectedLanguageListDto? propertySelectedLanguageInformation = await _entityService.GetSinglePropertySelectedLanguageInformation(id);
            if (propertySelectedLanguageInformation is null) return NotFound();
            BaseChangeEntityResult result = await _entityService.DeletePropertySelectedLanguage(id);
            switch (result)
            {
                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("PropertySelectedLanguage")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
                        return RedirectToAction("Index", "PropertySelectedLanguage", new { Area = "Admin", propertyId = propertySelectedLanguageInformation.PropertyId, });
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
                return RedirectToAction("Index", "PropertySelectedLanguage", new { Area = "Admin" });
            }
            await _entityService.DeletePropertySelectedLanguage(ids);
            TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("PropertySelectedLanguages")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
            return RedirectToAction("Index", "PropertySelectedLanguage", new { Area = "Admin" });
        }

        #endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterPropertySelectedLanguagesDto filter)
        {
            List<PropertySelectedLanguageListDto> result = (await _entityService.FilterPropertySelectedLanguages(filter)).PropertySelectedLanguages;
            string title = $"{_sharedEntitiesLocalizer.GetString("PropertySelectedLanguages")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<PropertySelectedLanguageListDto> excelExporter = new ExcelExporter<PropertySelectedLanguageListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("PropertyName")}", 2, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("PropertyId")}", 3, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("LanguageName")}", 4, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("LanguageId")}", 5, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("Title")}", 6, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws, (rowIndex - 3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.PropertyName, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.PropertyId.ToString("#,0"), 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.LanguageName, 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.LanguageId.ToString("#,0"), 5, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterPropertySelectedLanguagesDto filter)
        {
            List<PropertySelectedLanguageListDto> result = (await _entityService.FilterPropertySelectedLanguages(filter)).PropertySelectedLanguages;
            string title = $"{_sharedEntitiesLocalizer.GetString("PropertySelectedLanguages")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("PropertyName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("PropertyId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("LanguageName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("LanguageId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Title")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.PropertyName,

item.PropertyId.ToString("#,0"),

item.LanguageName,

item.LanguageId.ToString("#,0"),

item.Title);
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
