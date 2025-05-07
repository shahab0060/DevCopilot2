using ClosedXML.Excel;
using DevCopilot2.Core.Exporters;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Web.PresentationMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Localization;
using System.Text;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Entities
{
    [PermissionChecker("EntitySelectedProjectAreaManagement")]
    public class EntitySelectedProjectAreaController : BaseAdminController<EntitySelectedProjectAreaListDto>
    {

        #region constructor

        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<EntitySelectedProjectAreaController> _localizer;
        private readonly IEntityService _entityService;
        private readonly IProjectService _projectService;
        public EntitySelectedProjectAreaController(
                           IStringLocalizer<SharedResources> sharedLocalizer,
                           IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
                           IStringLocalizer<EntitySelectedProjectAreaController> localizer,
                           IEntityService entityService,
                           IProjectService projectService
                                      )
        {
            this._sharedLocalizer = sharedLocalizer;
            this._sharedEntitiesLocalizer = sharedEntitiesLocalizer;
            this._localizer = localizer;
            this._entityService = entityService;
            this._projectService = projectService;
        }

        #endregion

        #region index

        [HttpGet]
        public async Task<IActionResult> Index(FilterEntitySelectedProjectAreasDto filter)
        {

            return View(await _entityService.FilterEntitySelectedProjectAreas(filter));
        }

        #endregion

        #region detail

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {

            EntitySelectedProjectAreaListDto? entitySelectedProjectAreaInformation = await _entityService.GetSingleEntitySelectedProjectAreaInformation(id);
            if (entitySelectedProjectAreaInformation is null) return NotFound();

            await GetViewDatas(entitySelectedProjectAreaInformation.ProjectId);
            return View(entitySelectedProjectAreaInformation);
        }

        #endregion

        #region create

        [HttpGet]
        public async Task<IActionResult> Create(int entityId)
        {
            EntityListDto? entity = await _entityService
                .GetSingleEntityInformation(entityId);
            if (entity is null) return NotFound();
            await GetViewDatas(entity.ProjectId);
            CreateEntitySelectedProjectAreaDto create = new CreateEntitySelectedProjectAreaDto()
            {
                EntityId = entityId,
            };
            return View(create);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEntitySelectedProjectAreaDto create)
        {
            EntityListDto? entity = await _entityService
                .GetSingleEntityInformation(create.EntityId);
            if (entity is null) return NotFound();
            if (!ModelState.IsValid)
            {
                await GetViewDatas(entity.ProjectId);
                return View(create);
            }
            BaseChangeEntityResult result = await _entityService.CreateEntitySelectedProjectArea(create);

            #region handling different types

            switch (result)
            {

                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedProjectArea")} {_sharedLocalizer.GetString("Created Successfully")}";
                        return RedirectToAction("Index", "EntitySelectedProjectArea", new { Area = "Admin", entityId = create.EntityId, });
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

            await GetViewDatas(entity.ProjectId);
            return View(create);
        }

        #endregion

        #region update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            UpdateEntitySelectedProjectAreaDto? entitySelectedProjectAreaInformation = await _entityService.GetEntitySelectedProjectAreaInformation(id);
            if (entitySelectedProjectAreaInformation is null) return NotFound();

            EntityListDto? entity = await _entityService
                            .GetSingleEntityInformation(entitySelectedProjectAreaInformation.EntityId);
            if (entity is null) return NotFound();
            await GetViewDatas(entity.ProjectId);
            return View(entitySelectedProjectAreaInformation);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEntitySelectedProjectAreaDto update)
        {
            EntityListDto? entity = await _entityService
                .GetSingleEntityInformation(update.EntityId);
            if (entity is null) return NotFound();
            if (!ModelState.IsValid)
            {
                await GetViewDatas(entity.ProjectId);
                return View(update);
            }
            BaseChangeEntityResult result = await _entityService.UpdateEntitySelectedProjectArea(update);

            #region handling different types

            switch (result)
            {

                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedProjectArea")} {_sharedLocalizer.GetString("Updated Successfully.")}";
                        return RedirectToAction("Index", "EntitySelectedProjectArea", new { Area = "Admin", entityId = update.EntityId, });
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

            await GetViewDatas(entity.ProjectId);
            return View(update);
        }

        #endregion

        #region view datas

        async Task GetViewDatas(int projectId)
        {
            await GetProjectAreasViewData(projectId);
            await GetPropertiesViewData(projectId);
        }

        async Task GetProjectAreasViewData(int projectId)
        => ViewData["ProjectAreas"] = (await _projectService
        .GetProjectAreasAsCombo(new FilterProjectAreasDto() { ProjectId = projectId }))
        .ToSelectListItem();

        async Task GetPropertiesViewData(int projectId)
        => ViewData["Properties"] = (await _entityService
        .GetPropertiesAsCombo(new FilterPropertiesDto() { ProjectId = projectId }))
        .ToSelectListItem();

        #endregion

        #region delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            EntitySelectedProjectAreaListDto? entitySelectedProjectAreaInformation = await _entityService.GetSingleEntitySelectedProjectAreaInformation(id);
            if (entitySelectedProjectAreaInformation is null) return NotFound();
            BaseChangeEntityResult result = await _entityService.DeleteEntitySelectedProjectArea(id);
            switch (result)
            {
                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedProjectArea")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
                        return RedirectToAction("Index", "EntitySelectedProjectArea", new { Area = "Admin", entityId = entitySelectedProjectAreaInformation.EntityId, });
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
                return RedirectToAction("Index", "EntitySelectedProjectArea", new { Area = "Admin" });
            }
            await _entityService.DeleteEntitySelectedProjectArea(ids);
            TempData[SuccessMessage] = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedProjectAreas")} {_sharedLocalizer.GetString("Deleted Successfully.")}";
            return RedirectToAction("Index", "EntitySelectedProjectArea", new { Area = "Admin" });
        }

        #endregion

        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(FilterEntitySelectedProjectAreasDto filter)
        {
            List<EntitySelectedProjectAreaListDto> result = (await _entityService.FilterEntitySelectedProjectAreas(filter)).EntitySelectedProjectAreas;
            string title = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedProjectAreas")}";
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                ExcelExporter<EntitySelectedProjectAreaListDto> excelExporter = new ExcelExporter<EntitySelectedProjectAreaListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $"{_sharedLocalizer.GetString("Row")}", 1, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("EntityPluralName")}", 2, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("EntityId")}", 3, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("ProjectAreaTitle")}", 4, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("ProjectAreaId")}", 5, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("HasIndex")}", 6, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("HasCreate")}", 7, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("HasUpdate")}", 8, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("HasDelete")}", 9, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("HasApi")}", 10, 3);
                ws = excelExporter.AddColumn(ws, $"{_localizer.GetString("HasWeb")}", 11, 3);

                int rowIndex = 4;
                foreach (var item in result)
                {
                    ws = excelExporter.AddColumn(ws, (rowIndex - 3).ToString(), 1, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.EntityPluralName, 2, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.EntityId.ToString("#,0"), 3, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectAreaTitle, 4, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.ProjectAreaId.ToString("#,0"), 5, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.HasIndex.ConvertBoolToText(), 6, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.HasCreate.ConvertBoolToText(), 7, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.HasUpdate.ConvertBoolToText(), 8, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.HasDelete.ConvertBoolToText(), 9, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.HasApi.ConvertBoolToText(), 10, rowIndex);

                    ws = excelExporter.AddColumn(ws, item.HasWeb.ConvertBoolToText(), 11, rowIndex);

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
        public async Task<IActionResult> ExportPdf(FilterEntitySelectedProjectAreasDto filter)
        {
            List<EntitySelectedProjectAreaListDto> result = (await _entityService.FilterEntitySelectedProjectAreas(filter)).EntitySelectedProjectAreas;
            string title = $"{_sharedEntitiesLocalizer.GetString("EntitySelectedProjectAreas")}";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($"{_sharedLocalizer.GetString("Row")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("EntityPluralName")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("EntityId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectAreaTitle")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("ProjectAreaId")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("HasIndex")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("HasCreate")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("HasUpdate")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("HasDelete")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("HasApi")}");

            dataTable = dataTable.AddHeader($"{_localizer.GetString("HasWeb")}");
            int index = 1;
            foreach (var item in result)
            {
                dataTable = dataTable.AddContentRow(index,
item.EntityPluralName,

item.EntityId.ToString("#,0"),

item.ProjectAreaTitle,

item.ProjectAreaId.ToString("#,0"),

item.HasIndex.ConvertBoolToText(),

item.HasCreate.ConvertBoolToText(),

item.HasUpdate.ConvertBoolToText(),

item.HasDelete.ConvertBoolToText(),

item.HasApi.ConvertBoolToText(),

item.HasWeb.ConvertBoolToText());
                index++;
            }

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }

        #endregion

    }
}
