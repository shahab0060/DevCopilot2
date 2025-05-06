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
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Languages;
using DevCopilot2.Domain.DTOs.Properties;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Entities
{
	//[PermissionChecker("EntityManagement")]
    public class EntityController : BaseAdminController<EntityListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<EntityController> _localizer;
        private readonly IEntityService _entityService;
        private readonly IProjectService _projectService;
        private readonly ISiteService _siteService;
        public EntityController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<EntityController> localizer,
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
        public async Task<IActionResult> Index(FilterEntitiesDto filter)
        {

            return View(await _entityService.FilterEntities(filter));
        }

        #endregion

        #region detail

		[HttpGet]
		public async Task<IActionResult>Detail(int id)
		{

            EntityListDto? entityInformation = await _entityService.GetSingleEntityInformation(id);
			if (
            entityInformation is null)return NotFound();

            await GetViewDatas();
            return View(
            entityInformation);	
		}

		#endregion

        #region create

		[HttpGet]
		public async Task<IActionResult> Create()
		{
            await GetViewDatas();
            CreateEntityDto create = new CreateEntityDto()
            {

            };
            return View(create);
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreateEntityDto create)
		{

            create.AuthorId = User.GetCurrentUserId();

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(create);
            }
			ChangeEntityResult result = await _entityService.CreateEntity(create);

            #region handling different types

            switch (result)
			{

				case ChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Entity")} {_sharedLocalizer.GetString("Created Successfully")}";
                        return RedirectToAction("Index", "Entity", new { Area = "Admin", });
                    }

				case ChangeEntityResult.NotFound:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                    }

				case ChangeEntityResult.SingularNameExists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Entity")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("SingularName")} {create.SingularName}";
                        break;
                }

				case ChangeEntityResult.PluralNameExists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Entity")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("PluralName")} {create.PluralName}";
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
			UpdateEntityDto? entityInformation = await _entityService.GetEntityInformation(id);
			if (entityInformation is null) return NotFound();
			await GetViewDatas();
            return View(entityInformation);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateEntityDto update)
		{

            update.AuthorId = User.GetCurrentUserId();

			if (!ModelState.IsValid)
			{
                await GetViewDatas();
                return View(update);
            }
			ChangeEntityResult result = await _entityService.UpdateEntity(update);

            #region handling different types

            switch (result)
			{

				case ChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Entity")} {_sharedLocalizer.GetString("Updated Successfully.")}";
                        return RedirectToAction("Index", "Entity", new { Area = "Admin", });
                    }

				case ChangeEntityResult.NotFound:
                    {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                        return NotFound();
                    }

				case ChangeEntityResult.SingularNameExists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Entity")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("SingularName")} {update.SingularName}";
                        break;
                }

				case ChangeEntityResult.PluralNameExists:
                {
                        TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("A")} {_sharedEntitiesLocalizer.GetString("Entity")} {_sharedLocalizer.GetString("Exists With This")} {_localizer.GetString("PluralName")} {update.PluralName}";
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
            await GetProjectsViewData();
            await GetProjectEnumsViewData();
            await GetLanguagesViewData();
            await GetProjectAreasViewData();
            await GetPropertiesViewData();
        }

        async Task GetEntitiesViewData()
        => ViewData["Entities"] = (await _entityService
        .GetEntitiesAsCombo(new FilterEntitiesDto()))
        .ToSelectListItem(false,true);

        async Task GetProjectsViewData()
        => ViewData["Projects"] = (await _projectService
        .GetProjectsAsCombo(new FilterProjectsDto()))
        .ToSelectListItem();

        async Task GetProjectEnumsViewData()
        => ViewData["ProjectEnums"] = (await _projectService
        .GetProjectEnumsAsCombo(new FilterProjectEnumsDto()))
        .ToSelectListItem(false,true);

        async Task GetLanguagesViewData()
        => ViewData["Languages"] = (await _siteService
        .GetLanguagesAsCombo(new FilterLanguagesDto()))
        .ToSelectListItem();

        async Task GetProjectAreasViewData()
        => ViewData["ProjectAreas"] = (await _projectService
        .GetProjectAreasAsCombo(new FilterProjectAreasDto()))
        .ToSelectListItem();

        async Task GetPropertiesViewData()
        => ViewData["Properties"] = (await _entityService
        .GetPropertiesAsCombo(new FilterPropertiesDto()))
        .ToSelectListItem();

        #endregion

        #region delete

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
            EntityListDto? entityInformation = await _entityService.GetSingleEntityInformation(id);
            if (entityInformation is null) return NotFound();
			BaseChangeEntityResult result = await _entityService.DeleteEntity(id);
			switch (result)
			{
				case BaseChangeEntityResult.Success:
					{
						TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("Entity")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
					return RedirectToAction("Index", "Entity", new { Area = "Admin", });
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
				return RedirectToAction("Index", "Entity", new { Area = "Admin" });
			}
			await _entityService.DeleteEntity(ids);
			TempData[SuccessMessage] =$"{_sharedEntitiesLocalizer.GetString("Entities")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
			return RedirectToAction("Index", "Entity", new { Area = "Admin" });
		}

		#endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterEntitiesDto filter)
        {
            List<EntityListDto> result = (await _entityService.FilterEntities(filter)).Entities;
            string title = $"{_sharedEntitiesLocalizer.GetString("Entities")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<EntityListDto> excelExporter = new ExcelExporter<EntityListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("SingularName")}", 2, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("PluralName")}", 3, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("FolderName")}", 4, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("InheritedEntityPluralName")}", 5, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("InheritedEntityId")}", 6, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("IdType")}", 7, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ServiceName")}", 8, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("AuthorPhoneNumber")}", 9, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("AuthorId")}", 10, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ProjectTitle")}", 11, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("ProjectId")}", 12, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("IsExcluded")}", 13, 3);
ws = excelExporter.AddColumn(ws,$"{_localizer.GetString("AddToMenu")}", 14, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.SingularName, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.PluralName, 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.FolderName, 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.InheritedEntityPluralName, 5, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.InheritedEntityId?.ToString(), 6, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.IdType.GetEnumName(), 7, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ServiceName, 8, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.AuthorPhoneNumber, 9, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.AuthorId?.ToString(), 10, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectTitle, 11, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectId.ToString("#,0"), 12, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.IsExcluded.ConvertBoolToText(), 13, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.AddToMenu.ConvertBoolToText(), 14, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterEntitiesDto filter)
        {
            List<EntityListDto> result = (await _entityService.FilterEntities(filter)).Entities;
            string title = $"{_sharedEntitiesLocalizer.GetString("Entities")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("SingularName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("PluralName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("FolderName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("InheritedEntityPluralName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("InheritedEntityId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("IdType")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ServiceName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("AuthorPhoneNumber")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("AuthorId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectTitle")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("IsExcluded")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("AddToMenu")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.SingularName,

item.PluralName,

item.FolderName,

item.InheritedEntityPluralName,

item.InheritedEntityId?.ToString(),

item.IdType.GetEnumName(),

item.ServiceName,

item.AuthorPhoneNumber,

item.AuthorId?.ToString(),

item.ProjectTitle,

item.ProjectId.ToString("#,0"),

item.IsExcluded.ConvertBoolToText(),

item.AddToMenu.ConvertBoolToText() );
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
