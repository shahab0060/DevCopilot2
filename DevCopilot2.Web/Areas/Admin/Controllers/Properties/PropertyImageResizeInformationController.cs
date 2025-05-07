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

namespace DevCopilot2.Web.Areas.Admin.Controllers.Properties
{
	[PermissionChecker("PropertyImageResizeInformationManagement")]
    public class PropertyImageResizeInformationController : BaseAdminController<PropertyImageResizeInformationListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<PropertyImageResizeInformationController> _localizer;
        private readonly IEntityService _entityService;
        public PropertyImageResizeInformationController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<PropertyImageResizeInformationController> localizer,
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
        public async Task<IActionResult> Index(FilterPropertyImageResizeInformationDto filter)
        {

            return View(await _entityService.FilterPropertyImageResizeInformation(filter));
        }

        #endregion

        #region detail

		[HttpGet]
		public async Task<IActionResult>Detail(int id)
		{

            PropertyImageResizeInformationListDto? propertyImageResizeInformationInformation = await _entityService.GetSinglePropertyImageResizeInformationInformation(id);
			if (
            propertyImageResizeInformationInformation is null)return NotFound();

            return View(
            propertyImageResizeInformationInformation);	
		}

		#endregion

        #region create

		[HttpGet]
		public IActionResult Create(int propertyId)
		{

            CreatePropertyImageResizeInformationDto create = new CreatePropertyImageResizeInformationDto()
            {
                PropertyId = propertyId, 
            };
            return View(create);
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreatePropertyImageResizeInformationDto create)
		{

			if (!ModelState.IsValid)
			{

                return View(create);
            }
			BaseChangeEntityResult result = await _entityService.CreatePropertyImageResizeInformation(create);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("PropertyImageResizeInformation")} {_sharedLocalizer.GetString("Created Successfully")}";
					return RedirectToAction("Index", "PropertyImageResizeInformation", new { Area = "Admin",propertyId=create.PropertyId, });
				}

                case BaseChangeEntityResult.NotFound:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                }

				case BaseChangeEntityResult.Exists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("PropertyImageResizeInformation")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Name")} {create.Name}";
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
			UpdatePropertyImageResizeInformationDto? propertyImageResizeInformationInformation = await _entityService.GetPropertyImageResizeInformationInformation(id);
			if (propertyImageResizeInformationInformation is null) return NotFound();

            return View(propertyImageResizeInformationInformation);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdatePropertyImageResizeInformationDto update)
		{

			if (!ModelState.IsValid)
			{

                return View(update);
            }
			BaseChangeEntityResult result = await _entityService.UpdatePropertyImageResizeInformation(update);

            #region handling different types

            switch (result)
			{

				case BaseChangeEntityResult.Success:
				{
					TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("PropertyImageResizeInformation")} {_sharedLocalizer.GetString("Updated Successfully.")}";
					return RedirectToAction("Index", "PropertyImageResizeInformation", new { Area = "Admin",propertyId=update.PropertyId, });
				}

                case BaseChangeEntityResult.NotFound:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                }

				case BaseChangeEntityResult.Exists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("PropertyImageResizeInformation")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("Name")} {update.Name}";
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
            PropertyImageResizeInformationListDto? propertyImageResizeInformationInformation = await _entityService.GetSinglePropertyImageResizeInformationInformation(id);
            if (propertyImageResizeInformationInformation is null) return NotFound();
			BaseChangeEntityResult result = await _entityService.DeletePropertyImageResizeInformation(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("PropertyImageResizeInformation")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
					return RedirectToAction("Index", "PropertyImageResizeInformation", new { Area = "Admin",propertyId=propertyImageResizeInformationInformation.PropertyId, });
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
				return RedirectToAction("Index", "PropertyImageResizeInformation", new { Area = "Admin" });
			}
			await _entityService.DeletePropertyImageResizeInformation(ids);
			TempData[SuccessMessage] =$"{_sharedEntitiesLocalizer.GetString("PropertyImageResizeInformation")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
			return RedirectToAction("Index", "PropertyImageResizeInformation", new { Area = "Admin" });
		}

		#endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterPropertyImageResizeInformationDto filter)
        {
            List<PropertyImageResizeInformationListDto> result = (await _entityService.FilterPropertyImageResizeInformation(filter)).PropertyImageResizeInformation;
            string title = $"{_sharedEntitiesLocalizer.GetString("PropertyImageResizeInformation")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<PropertyImageResizeInformationListDto> excelExporter = new ExcelExporter<PropertyImageResizeInformationListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("PropertyName")}", 2, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("PropertyId")}", 3, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("Name")}", 4, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("Width")}", 5, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("Height")}", 6, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.PropertyName, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.PropertyId.ToString("#,0"), 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Name, 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Width.ToString("#,0"), 5, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.Height.ToString("#,0"), 6, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterPropertyImageResizeInformationDto filter)
        {
            List<PropertyImageResizeInformationListDto> result = (await _entityService.FilterPropertyImageResizeInformation(filter)).PropertyImageResizeInformation;
            string title = $"{_sharedEntitiesLocalizer.GetString("PropertyImageResizeInformation")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("PropertyName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("PropertyId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Name")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Width")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("Height")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.PropertyName,

item.PropertyId.ToString("#,0"),

item.Name,

item.Width.ToString("#,0"),

item.Height.ToString("#,0") );
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
