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

namespace DevCopilot2.Web.Areas.Admin.Controllers.Entities
{
	//[PermissionChecker("EntityRelationManagement")]
    public class EntityRelationController : BaseAdminController<EntityRelationListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<EntityRelationController> _localizer;
        private readonly IEntityService _entityService;
        public EntityRelationController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<EntityRelationController> localizer,
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
        public async Task<IActionResult> Index(FilterEntityRelationsDto filter)
        {

            return View(await _entityService.FilterEntityRelations(filter));
        }

        #endregion

        #region detail

		[HttpGet]
		public async Task<IActionResult>Detail(int id)
		{

            EntityRelationListDto? entityRelationInformation = await _entityService.GetSingleEntityRelationInformation(id);
			if (
            entityRelationInformation is null)return NotFound();

            await GetViewDatas();
            return View(
            entityRelationInformation);	
		}

		#endregion

        #region create

		[HttpGet]
		public async Task<IActionResult> Create(int primaryPropertyId)
		{
            await GetViewDatas();
            CreateEntityRelationDto create = new CreateEntityRelationDto()
            {
                PrimaryPropertyId = primaryPropertyId, 
            };
            return View(create);
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreateEntityRelationDto create)
		{

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(create);
            }
			BaseChangeEntityResult result = await _entityService.CreateEntityRelation(create);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("EntityRelation")} {_sharedLocalizer.GetString("Created Successfully")}";
					return RedirectToAction("Index", "EntityRelation", new { Area = "Admin",primaryPropertyId=create.PrimaryPropertyId, });
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
			UpdateEntityRelationDto? entityRelationInformation = await _entityService.GetEntityRelationInformation(id);
			if (entityRelationInformation is null) return NotFound();
			await GetViewDatas();
            return View(entityRelationInformation);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateEntityRelationDto update)
		{

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(update);
            }
			BaseChangeEntityResult result = await _entityService.UpdateEntityRelation(update);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("EntityRelation")} {_sharedLocalizer.GetString("Updated Successfully.")}";
					return RedirectToAction("Index", "EntityRelation", new { Area = "Admin",primaryPropertyId=update.PrimaryPropertyId, });
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
            await GetEntitiesViewData();
        }

        async Task GetEntitiesViewData()
        => ViewData["Entities"] = (await _entityService
        .GetEntitiesAsCombo(new FilterEntitiesDto()))
        .ToSelectListItem();

        #endregion

        #region delete

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
            EntityRelationListDto? entityRelationInformation = await _entityService.GetSingleEntityRelationInformation(id);
            if (entityRelationInformation is null) return NotFound();
			BaseChangeEntityResult result = await _entityService.DeleteEntityRelation(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("EntityRelation")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
					return RedirectToAction("Index", "EntityRelation", new { Area = "Admin",primaryPropertyId=entityRelationInformation.PrimaryPropertyId, });
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
				return RedirectToAction("Index", "EntityRelation", new { Area = "Admin" });
			}
			await _entityService.DeleteEntityRelation(ids);
			TempData[SuccessMessage] =$"{_sharedEntitiesLocalizer.GetString("EntityRelations")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
			return RedirectToAction("Index", "EntityRelation", new { Area = "Admin" });
		}

		#endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterEntityRelationsDto filter)
        {
            List<EntityRelationListDto> result = (await _entityService.FilterEntityRelations(filter)).EntityRelations;
            string title = $"{_sharedEntitiesLocalizer.GetString("EntityRelations")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<EntityRelationListDto> excelExporter = new ExcelExporter<EntityRelationListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("PrimaryPropertyName")}", 2, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("PrimaryPropertyId")}", 3, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("SecondaryEntityPluralName")}", 4, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("SecondaryEntityId")}", 5, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("MiddleEntityPluralName")}", 6, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("MiddleEntityId")}", 7, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("RelationType")}", 8, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("InputType")}", 9, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("FillingType")}", 10, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("FillingCode")}", 11, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.PrimaryPropertyName, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.PrimaryPropertyId.ToString("#,0"), 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.SecondaryEntityPluralName, 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.SecondaryEntityId.ToString("#,0"), 5, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.MiddleEntityPluralName, 6, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.MiddleEntityId?.ToString(), 7, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.RelationType.GetEnumName(), 8, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.InputType.GetEnumName(), 9, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.FillingType.GetEnumName(), 10, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.FillingCode?.ToString(), 11, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterEntityRelationsDto filter)
        {
            List<EntityRelationListDto> result = (await _entityService.FilterEntityRelations(filter)).EntityRelations;
            string title = $"{_sharedEntitiesLocalizer.GetString("EntityRelations")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("PrimaryPropertyName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("PrimaryPropertyId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("SecondaryEntityPluralName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("SecondaryEntityId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("MiddleEntityPluralName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("MiddleEntityId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("RelationType")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("InputType")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("FillingType")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("FillingCode")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.PrimaryPropertyName,

item.PrimaryPropertyId.ToString("#,0"),

item.SecondaryEntityPluralName,

item.SecondaryEntityId.ToString("#,0"),

item.MiddleEntityPluralName,

item.MiddleEntityId?.ToString(),

item.RelationType.GetEnumName(),

item.InputType.GetEnumName(),

item.FillingType.GetEnumName(),

item.FillingCode?.ToString() );
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
