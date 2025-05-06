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
using DevCopilot2.Domain.DTOs.Properties;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Entities
{
	//[PermissionChecker("EntitySelectedProjectAreaSelectedFilterManagement")]
    public class EntitySelectedProjectAreaSelectedFilterController : BaseAdminController<EntitySelectedProjectAreaSelectedFilterListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<EntitySelectedProjectAreaSelectedFilterController> _localizer;
        private readonly IEntityService _entityService;
        public EntitySelectedProjectAreaSelectedFilterController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<EntitySelectedProjectAreaSelectedFilterController> localizer,
                           IEntityService entityService 
                                      )
        {
            this._sharedLocalizer = sharedLocalizer;
            this._sharedEntitiesLocalizer = sharedEntitiesLocalizer;
            this._localizer = localizer;
            this._entityService = entityService;
        }

        #endregion

        #region index

        [HttpGet]
        public async Task<IActionResult> Index(FilterEntitySelectedProjectAreaSelectedFiltersDto filter)
        {

            return View(await _entityService.FilterEntitySelectedProjectAreaSelectedFilters(filter));
        }

        #endregion

        #region detail

		[HttpGet]
		public async Task<IActionResult>Detail(int id)
		{

            EntitySelectedProjectAreaSelectedFilterListDto? entitySelectedProjectAreaSelectedFilterInformation = await _entityService.GetSingleEntitySelectedProjectAreaSelectedFilterInformation(id);
			if (
            entitySelectedProjectAreaSelectedFilterInformation is null)return NotFound();

            await GetViewDatas();
            return View(
            entitySelectedProjectAreaSelectedFilterInformation);	
		}

		#endregion

        #region create

		[HttpGet]
		public async Task<IActionResult> Create(int entitySelectedProjectAreaId)
		{
            await GetViewDatas();
            CreateEntitySelectedProjectAreaSelectedFilterDto create = new CreateEntitySelectedProjectAreaSelectedFilterDto()
            {
                EntitySelectedProjectAreaId = entitySelectedProjectAreaId, 
            };
            return View(create);
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreateEntitySelectedProjectAreaSelectedFilterDto create)
		{

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(create);
            }
			BaseChangeEntityResult result = await _entityService.CreateEntitySelectedProjectAreaSelectedFilter(create);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedProjectAreaSelectedFilter")} {_sharedLocalizer.GetString("Created Successfully")}";
					return RedirectToAction("Index", "EntitySelectedProjectAreaSelectedFilter", new { Area = "Admin",entitySelectedProjectAreaId=create.EntitySelectedProjectAreaId, });
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
			UpdateEntitySelectedProjectAreaSelectedFilterDto? entitySelectedProjectAreaSelectedFilterInformation = await _entityService.GetEntitySelectedProjectAreaSelectedFilterInformation(id);
			if (entitySelectedProjectAreaSelectedFilterInformation is null) return NotFound();
			await GetViewDatas();
            return View(entitySelectedProjectAreaSelectedFilterInformation);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateEntitySelectedProjectAreaSelectedFilterDto update)
		{

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(update);
            }
			BaseChangeEntityResult result = await _entityService.UpdateEntitySelectedProjectAreaSelectedFilter(update);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedProjectAreaSelectedFilter")} {_sharedLocalizer.GetString("Updated Successfully.")}";
					return RedirectToAction("Index", "EntitySelectedProjectAreaSelectedFilter", new { Area = "Admin",entitySelectedProjectAreaId=update.EntitySelectedProjectAreaId, });
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
            await GetPropertiesViewData();
        }

        async Task GetPropertiesViewData()
        => ViewData["Properties"] = (await _entityService
        .GetPropertiesAsCombo(new FilterPropertiesDto()))
        .ToSelectListItem();

        #endregion

        #region delete

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
            EntitySelectedProjectAreaSelectedFilterListDto? entitySelectedProjectAreaSelectedFilterInformation = await _entityService.GetSingleEntitySelectedProjectAreaSelectedFilterInformation(id);
            if (entitySelectedProjectAreaSelectedFilterInformation is null) return NotFound();
			BaseChangeEntityResult result = await _entityService.DeleteEntitySelectedProjectAreaSelectedFilter(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedProjectAreaSelectedFilter")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
					return RedirectToAction("Index", "EntitySelectedProjectAreaSelectedFilter", new { Area = "Admin",entitySelectedProjectAreaId=entitySelectedProjectAreaSelectedFilterInformation.EntitySelectedProjectAreaId, });
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
				return RedirectToAction("Index", "EntitySelectedProjectAreaSelectedFilter", new { Area = "Admin" });
			}
			await _entityService.DeleteEntitySelectedProjectAreaSelectedFilter(ids);
			TempData[SuccessMessage] =$"{_sharedEntitiesLocalizer.GetString("EntitySelectedProjectAreaSelectedFilters")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
			return RedirectToAction("Index", "EntitySelectedProjectAreaSelectedFilter", new { Area = "Admin" });
		}

		#endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterEntitySelectedProjectAreaSelectedFiltersDto filter)
        {
            List<EntitySelectedProjectAreaSelectedFilterListDto> result = (await _entityService.FilterEntitySelectedProjectAreaSelectedFilters(filter)).EntitySelectedProjectAreaSelectedFilters;
            string title = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedProjectAreaSelectedFilters")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<EntitySelectedProjectAreaSelectedFilterListDto> excelExporter = new ExcelExporter<EntitySelectedProjectAreaSelectedFilterListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("EntitySelectedProjectAreaHasWeb")}", 2, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("EntitySelectedProjectAreaId")}", 3, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("PropertyName")}", 4, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("PropertyId")}", 5, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("Value")}", 6, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.EntitySelectedProjectAreaHasWeb.ConvertBoolToText(), 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.EntitySelectedProjectAreaId.ToString("#,0"), 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.PropertyName, 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.PropertyId.ToString("#,0"), 5, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Value, 6, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterEntitySelectedProjectAreaSelectedFiltersDto filter)
        {
            List<EntitySelectedProjectAreaSelectedFilterListDto> result = (await _entityService.FilterEntitySelectedProjectAreaSelectedFilters(filter)).EntitySelectedProjectAreaSelectedFilters;
            string title = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedProjectAreaSelectedFilters")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("EntitySelectedProjectAreaHasWeb")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("EntitySelectedProjectAreaId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("PropertyName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("PropertyId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Value")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.EntitySelectedProjectAreaHasWeb.ConvertBoolToText(),

item.EntitySelectedProjectAreaId.ToString("#,0"),

item.PropertyName,

item.PropertyId.ToString("#,0"),

item.Value );
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
