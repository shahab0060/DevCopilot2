using System;
using System.Text;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Web.PresentationExtensions;
using DevCopilot2.Web.PresentationMappers;
using DevCopilot2.Domain.Enums.Entities;
using DevCopilot2.Domain.DTOs.Entities;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ClosedXML.Excel;
using DevCopilot2.Core.Exporters;
using DevCopilot2.Domain.DTOs.Languages;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Entities
{
	//[PermissionChecker("EntitySelectedLanguageManagement")]
    public class EntitySelectedLanguageController : BaseAdminController<EntitySelectedLanguageListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<EntitySelectedLanguageController> _localizer;
        private readonly IEntityService _entityService;
        private readonly ISiteService _siteService;
        public EntitySelectedLanguageController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<EntitySelectedLanguageController> localizer,
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
        public async Task<IActionResult> Index(FilterEntitySelectedLanguagesDto filter)
        {

            return View(await _entityService.FilterEntitySelectedLanguages(filter));
        }

        #endregion

        #region detail

		[HttpGet]
		public async Task<IActionResult>Detail(int id)
		{

            EntitySelectedLanguageListDto? entitySelectedLanguageInformation = await _entityService.GetSingleEntitySelectedLanguageInformation(id);
			if (
            entitySelectedLanguageInformation is null)return NotFound();

            await GetViewDatas();
            return View(
            entitySelectedLanguageInformation);	
		}

		#endregion

        #region create

		[HttpGet]
		public async Task<IActionResult> Create(int entityId)
		{
            await GetViewDatas();
            CreateEntitySelectedLanguageDto create = new CreateEntitySelectedLanguageDto()
            {
                EntityId = entityId, 
            };
            return View(create);
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreateEntitySelectedLanguageDto create)
		{

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(create);
            }
			BaseChangeEntityResult result = await _entityService.CreateEntitySelectedLanguage(create);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedLanguage")} {_sharedLocalizer.GetString("Created Successfully")}";
					return RedirectToAction("Index", "EntitySelectedLanguage", new { Area = "Admin",entityId=create.EntityId, });
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
			UpdateEntitySelectedLanguageDto? entitySelectedLanguageInformation = await _entityService.GetEntitySelectedLanguageInformation(id);
			if (entitySelectedLanguageInformation is null) return NotFound();
			await GetViewDatas();
            return View(entitySelectedLanguageInformation);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateEntitySelectedLanguageDto update)
		{

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(update);
            }
			BaseChangeEntityResult result = await _entityService.UpdateEntitySelectedLanguage(update);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedLanguage")} {_sharedLocalizer.GetString("Updated Successfully.")}";
					return RedirectToAction("Index", "EntitySelectedLanguage", new { Area = "Admin",entityId=update.EntityId, });
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
            EntitySelectedLanguageListDto? entitySelectedLanguageInformation = await _entityService.GetSingleEntitySelectedLanguageInformation(id);
            if (entitySelectedLanguageInformation is null) return NotFound();
			BaseChangeEntityResult result = await _entityService.DeleteEntitySelectedLanguage(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedLanguage")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
					return RedirectToAction("Index", "EntitySelectedLanguage", new { Area = "Admin",entityId=entitySelectedLanguageInformation.EntityId, });
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
				return RedirectToAction("Index", "EntitySelectedLanguage", new { Area = "Admin" });
			}
			await _entityService.DeleteEntitySelectedLanguage(ids);
			TempData[SuccessMessage] =$"{_sharedEntitiesLocalizer.GetString("EntitySelectedLanguages")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
			return RedirectToAction("Index", "EntitySelectedLanguage", new { Area = "Admin" });
		}

		#endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterEntitySelectedLanguagesDto filter)
        {
            List<EntitySelectedLanguageListDto> result = (await _entityService.FilterEntitySelectedLanguages(filter)).EntitySelectedLanguages;
            string title = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedLanguages")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<EntitySelectedLanguageListDto> excelExporter = new ExcelExporter<EntitySelectedLanguageListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("EntityPluralName")}", 2, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("EntityId")}", 3, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("LanguageName")}", 4, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("LanguageId")}", 5, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("SingularTitle")}", 6, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("PluralTitle")}", 7, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.EntityPluralName, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.EntityId.ToString("#,0"), 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.LanguageName, 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.LanguageId.ToString("#,0"), 5, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.SingularTitle, 6, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.PluralTitle, 7, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterEntitySelectedLanguagesDto filter)
        {
            List<EntitySelectedLanguageListDto> result = (await _entityService.FilterEntitySelectedLanguages(filter)).EntitySelectedLanguages;
            string title = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedLanguages")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("EntityPluralName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("EntityId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("LanguageName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("LanguageId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("SingularTitle")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("PluralTitle")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.EntityPluralName,

item.EntityId.ToString("#,0"),

item.LanguageName,

item.LanguageId.ToString("#,0"),

item.SingularTitle,

item.PluralTitle );
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
