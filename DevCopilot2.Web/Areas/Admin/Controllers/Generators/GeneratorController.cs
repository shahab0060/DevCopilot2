using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Users;
using DevCopilot2.Domain.Enums.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.IO.Compression;

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

        #region generate entity

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

            TempData[SuccessMessage] = $"{result.GetAllResults().CreatedCount} {(result.EntityResult.CreatedCount > 1 ? _sharedLocalizer.GetString("Files") : _sharedLocalizer.GetString("File"))} {_sharedLocalizer.GetString("Created Successfully")}";
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
            TempData["SuccessMessage"] = $"{result.Sum(a => a.GetAllResults().CreatedCount)} " +
                $"{(result.Sum(a => a.GetAllResults().CreatedCount) > 1 ? _sharedLocalizer.GetString("Files").Value : _sharedLocalizer.GetString("File").Value)} " +
                $"{_sharedLocalizer.GetString("Created Successfully").Value}";
            return RedirectToAction("Index", "Project", new { area = "Admin" });

        }

        #endregion

        #region download zip

        [HttpGet("download-zip")]
        public async Task<IActionResult> DownloadProjectAsZip(int projectId)
        {
            ProjectListDto? projectInformation = await _projectService
                .GetSingleProjectInformation(projectId);

            if (projectInformation is null) return NotFound();

            if (!Directory.Exists(projectInformation.Location))
            {
                TempData[ErrorMessage] = $"{_sharedLocalizer.GetString("File Not Found. Please First Generate Files")}";
                return NotFound();
            }

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    AddFilesToZip(archive, projectInformation.Location, "");
                }

                memoryStream.Position = 0; // Reset stream position for download
                TempData[SuccessMessage] = $"{_sharedLocalizer.GetString("Download Started Successfully")}";
                return File(memoryStream.ToArray(), "application/zip", $"{projectInformation.EnglishName}.zip");
            }
        }

        private void AddFilesToZip(ZipArchive archive, string sourceDir, string entryNamePrefix)
        {
            foreach (var filePath in Directory.GetFiles(sourceDir))
            {
                var entryName = Path.Combine(entryNamePrefix, Path.GetFileName(filePath));
                var entry = archive.CreateEntry(entryName, CompressionLevel.Fastest);
                using (var entryStream = entry.Open())
                using (var fileStream = System.IO.File.OpenRead(filePath))
                {
                    fileStream.CopyTo(entryStream);
                }
            }

            foreach (var subDir in Directory.GetDirectories(sourceDir))
            {
                var subDirName = Path.Combine(entryNamePrefix, Path.GetFileName(subDir));
                AddFilesToZip(archive, subDir, subDirName);
            }
        }

        #endregion
    }
}
