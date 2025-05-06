using System;
using System.Text;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Web.PresentationExtensions;
using DevCopilot2.Web.PresentationMappers;
using DevCopilot2.Domain.Enums.Properties;
using DevCopilot2.Domain.DTOs.Properties;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ClosedXML.Excel;
using DevCopilot2.Core.Exporters;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Languages;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Properties
{
	//[PermissionChecker("PropertyManagement")]
    public class PropertyController : BaseAdminController<PropertyListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<PropertyController> _localizer;
        private readonly IEntityService _entityService;
        private readonly IProjectService _projectService;
        private readonly ISiteService _siteService;
        public PropertyController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<PropertyController> localizer,
                           IEntityService entityService,
                           IProjectService projectService,
                           ISiteService siteService 
                                      )
        {
            this._sharedLocalizer = sharedLocalizer;
            this._sharedEntitiesLocalizer = sharedEntitiesLocalizer;
            this._localizer = localizer;
            this._entityService = entityService;
            this._projectService = projectService;
            this._siteService = siteService;
        }

        #endregion

        #region index

        [HttpGet]
        public async Task<IActionResult> Index(FilterPropertiesDto filter)
        {

            return View(await _entityService.FilterProperties(filter));
        }

        #endregion

        #region detail

		[HttpGet]
		public async Task<IActionResult>Detail(int id)
		{

            PropertyListDto? propertyInformation = await _entityService.GetSinglePropertyInformation(id);
			if (
            propertyInformation is null)return NotFound();

            await GetViewDatas();
            return View(
            propertyInformation);	
		}

		#endregion

        #region create

		[HttpGet]
		public async Task<IActionResult> Create(int entityId)
		{
            await GetViewDatas();
            CreatePropertyDto create = new CreatePropertyDto()
            {
                EntityId = entityId, 
            };
            return View(create);
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreatePropertyDto create)
		{

            create.AuthorId = User.GetCurrentUserId();

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(create);
            }
			BaseChangeEntityResult result = await _entityService.CreateProperty(create);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Property")} {_sharedLocalizer.GetString("Created Successfully")}";
					return RedirectToAction("Index", "Property", new { Area = "Admin",entityId=create.EntityId, });
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
			UpdatePropertyDto? propertyInformation = await _entityService.GetPropertyInformation(id);
			if (propertyInformation is null) return NotFound();
			await GetViewDatas();
            return View(propertyInformation);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdatePropertyDto update)
		{

            update.AuthorId = User.GetCurrentUserId();

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(update);
            }
			BaseChangeEntityResult result = await _entityService.UpdateProperty(update);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Property")} {_sharedLocalizer.GetString("Updated Successfully.")}";
					return RedirectToAction("Index", "Property", new { Area = "Admin",entityId=update.EntityId, });
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
            await GetProjectEnumsViewData();
            await GetEntitiesViewData();
            await GetLanguagesViewData();
        }

        async Task GetProjectEnumsViewData()
        => ViewData["ProjectEnums"] = (await _projectService
        .GetProjectEnumsAsCombo(new FilterProjectEnumsDto()))
        .ToSelectListItem(false,true);

        async Task GetEntitiesViewData()
        => ViewData["Entities"] = (await _entityService
        .GetEntitiesAsCombo(new FilterEntitiesDto()))
        .ToSelectListItem();

        async Task GetLanguagesViewData()
        => ViewData["Languages"] = (await _siteService
        .GetLanguagesAsCombo(new FilterLanguagesDto()))
        .ToSelectListItem();

        #endregion

        #region delete

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
            PropertyListDto? propertyInformation = await _entityService.GetSinglePropertyInformation(id);
            if (propertyInformation is null) return NotFound();
			BaseChangeEntityResult result = await _entityService.DeleteProperty(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Property")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
					return RedirectToAction("Index", "Property", new { Area = "Admin",entityId=propertyInformation.EntityId, });
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
				return RedirectToAction("Index", "Property", new { Area = "Admin" });
			}
			await _entityService.DeleteProperty(ids);
			TempData[SuccessMessage] =$"{_sharedEntitiesLocalizer.GetString("Properties")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
			return RedirectToAction("Index", "Property", new { Area = "Admin" });
		}

		#endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterPropertiesDto filter)
        {
            List<PropertyListDto> result = (await _entityService.FilterProperties(filter)).Properties;
            string title = $"{_sharedEntitiesLocalizer.GetString("Properties")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<PropertyListDto> excelExporter = new ExcelExporter<PropertyListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("Name")}", 2, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("DataType")}", 3, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("MaxLength")}", 4, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("MinLength")}", 5, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("RangeFrom")}", 6, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("RangeTo")}", 7, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("IsRequired")}", 8, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ProjectEnumEnglishName")}", 9, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ProjectEnumId")}", 10, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("DataAnnotationDataType")}", 11, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("IsUnique")}", 12, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("IsUpdatable")}", 13, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ShowInList")}", 14, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("IsFilterContain")}", 15, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("IsFilterEqual")}", 16, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("Order")}", 17, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("UseEditor")}", 18, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("EntityPluralName")}", 19, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("EntityId")}", 20, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("AuthorPhoneNumber")}", 21, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("AuthorId")}", 22, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ForceMapperCode")}", 23, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ExcludeFromListDto")}", 24, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Name, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.DataType.GetEnumName(), 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.MaxLength?.ToString(), 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.MinLength?.ToString(), 5, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.RangeFrom?.ToString(), 6, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.RangeTo?.ToString(), 7, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.IsRequired.ConvertBoolToText(), 8, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectEnumEnglishName, 9, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectEnumId?.ToString(), 10, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.DataAnnotationDataType.GetEnumName(), 11, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.IsUnique.ConvertBoolToText(), 12, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.IsUpdatable.ConvertBoolToText(), 13, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ShowInList.ConvertBoolToText(), 14, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.IsFilterContain.ConvertBoolToText(), 15, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.IsFilterEqual.ConvertBoolToText(), 16, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Order?.ToString(), 17, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.UseEditor.ConvertBoolToText(), 18, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.EntityPluralName, 19, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.EntityId.ToString("#,0"), 20, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.AuthorPhoneNumber, 21, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.AuthorId.ToString(), 22, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ForceMapperCode?.ToString(), 23, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ExcludeFromListDto.ConvertBoolToText(), 24, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterPropertiesDto filter)
        {
            List<PropertyListDto> result = (await _entityService.FilterProperties(filter)).Properties;
            string title = $"{_sharedEntitiesLocalizer.GetString("Properties")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Name")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("DataType")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("MaxLength")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("MinLength")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("RangeFrom")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("RangeTo")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("IsRequired")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectEnumEnglishName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectEnumId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("DataAnnotationDataType")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("IsUnique")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("IsUpdatable")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ShowInList")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("IsFilterContain")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("IsFilterEqual")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Order")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("UseEditor")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("EntityPluralName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("EntityId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("AuthorPhoneNumber")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("AuthorId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ForceMapperCode")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ExcludeFromListDto")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.Name,

item.DataType.GetEnumName(),

item.MaxLength?.ToString(),

item.MinLength?.ToString(),

item.RangeFrom?.ToString(),

item.RangeTo?.ToString(),

item.IsRequired.ConvertBoolToText(),

item.ProjectEnumEnglishName,

item.ProjectEnumId?.ToString(),

item.DataAnnotationDataType.GetEnumName(),

item.IsUnique.ConvertBoolToText(),

item.IsUpdatable.ConvertBoolToText(),

item.ShowInList.ConvertBoolToText(),

item.IsFilterContain.ConvertBoolToText(),

item.IsFilterEqual.ConvertBoolToText(),

item.Order?.ToString(),

item.UseEditor.ConvertBoolToText(),

item.EntityPluralName,

item.EntityId.ToString("#,0"),

item.AuthorPhoneNumber,

item.AuthorId.ToString(),

item.ForceMapperCode?.ToString(),

item.ExcludeFromListDto.ConvertBoolToText() );
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
