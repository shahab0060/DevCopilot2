using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Users;
using DevCopilot2.Domain.Enums.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DevCopilot2.Web.Areas.Admin.Controllers.Generators
{
    public class GeneratorController : BaseAdminController<UserListDto>
    {
        #region constructor

        private readonly IProjectService _projectService;
        private readonly IEntityService _entityService;
        private readonly IGeneratorService _generatorService;
        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IStringLocalizer<EntitiesSharedResources> _sharedEntitiesLocalizer;
        private readonly IStringLocalizer<GeneratorController> _localizer;
        public GeneratorController(
            IProjectService projectService,
            IEntityService entityService,
            IGeneratorService generatorService,
            IStringLocalizer<SharedResources> sharedLocalizer,
            IStringLocalizer<EntitiesSharedResources> sharedEntitiesLocalizer,
            IStringLocalizer<GeneratorController> localizer)
        {
            this._projectService = projectService;
            this._generatorService = generatorService;
            this._sharedLocalizer = sharedLocalizer;
            this._sharedEntitiesLocalizer = sharedEntitiesLocalizer;
            this._localizer = localizer;
            this._entityService = entityService;
        }

        #endregion

        #region generate clean architecutre

        [HttpGet]
        public async Task<IActionResult> GenerateEntity(int entityId)
        {
            EntityListDto? entityInformation = await _entityService
                           .GetSingleEntityInformation(entityId);
            if (entityInformation is null)
            {
                TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                return NotFound();
            }
            GenerateEntityDto generate = new GenerateEntityDto();
            generate = generate.SetFullControl();
            generate.EntityId = entityId;
            generate.ProjectId = entityInformation.ProjectId;
            return View(generate);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateEntity(GenerateEntityDto generate)
        {
            EntityListDto? entityInformation = await _entityService
                           .GetSingleEntityInformation(generate.EntityId);
            if (entityInformation is null)
            {
                TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
                return NotFound();
            }
            GenerateCleanArchitectureResultDto result = await _generatorService.GenerateCleanArchitecture(generate);

            TempData[SuccessMessage] = $"{result.EntityResult.CreatedCount} {(result.EntityResult.CreatedCount > 1 ? _sharedEntitiesLocalizer.GetString("Entities") : _sharedEntitiesLocalizer.GetString("Entity"))} {_sharedLocalizer.GetString("Created Successfully")}";
            return RedirectToAction("Index", "Entity", new { projectId = entityInformation.ProjectId });


        }

        #endregion

        #region generate clean architecture solutions

        [HttpGet]
        public async Task<IActionResult> GenerateCleanArchitectureSolution(int projectId)
        {
            BaseChangeEntityResult result = await _generatorService.GenerateCleanArchitecutreSolution(projectId);

            #region return results

            switch (result)
            {
                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"{_localizer.GetString("Solution Has Been Created Successfully!")}";
                        return RedirectToAction("Index", "Project");
                    }
            }

            #endregion

            TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("Invalid Request.")}";
            return NotFound();
        }

        #endregion

        #region generate react js base solution

        [HttpGet]
        public async Task<IActionResult> GenerateReactJsBaseSolution(int projectId)
        {
            BaseChangeEntityResult result = await _generatorService.GenerateReactJsSolution(projectId);

            #region return results

            switch (result)
            {
                case BaseChangeEntityResult.Success:
                    {
                        TempData[SuccessMessage] = $"React JS solution has been created successfully";
                        return RedirectToAction("Index", "Project");
                    }
            }

            #endregion

            return NotFound();
        }

        #endregion

        #region generate project files

        [HttpGet]
        public IActionResult GenerateProjectFiles(int projectId)
        {
            GenerateProjectDto generateProject = new GenerateProjectDto();
            generateProject = generateProject.SetFullControl();
            generateProject.ProjectId = projectId;
            return View(generateProject);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateProjectFiles(GenerateProjectDto generate)
        {
            List<GenerateCleanArchitectureResultDto> result = await _generatorService.GenerateCleanArchitecture(generate);
            TempData["SuccessMessage"] = $"{result.Sum(a => a.EntityResult.CreatedCount)} " +
                $"{(result.Sum(a => a.EntityResult.CreatedCount) > 1 ? _sharedEntitiesLocalizer.GetString("Entities").Value : _sharedEntitiesLocalizer.GetString("Entity").Value)} " +
                $"{_sharedLocalizer.GetString("Created Successfully").Value}";
            return RedirectToAction("Index", "Project", new { area = "Admin" });

        }

        #endregion
    }
}
