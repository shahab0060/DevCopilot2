using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Mappers.Generate;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.GeneralSettings;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.Enums.Common;
using System.IO.Compression;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class GeneratorService : IGeneratorService
    {
        #region constructor

        private readonly IProjectService _projectService;
        private readonly IEntityGeneratorService _entityGeneratorService;
        private readonly IBaseGeneratorService _baseGeneratorService;
        private readonly IDbContextGeneratorService _dbContextGeneratorService;
        private readonly IDtoGeneratorService _dtoGeneratorService;
        private readonly IMediaInformationGeneratorService _mediaInformationGeneratorService;
        private readonly IMapperGeneratorService _mapperGeneratorService;
        private readonly IServiceInterfaceGeneratorService _serviceInterfaceGeneratorService;
        private readonly IServiceGeneratorService _serviceGeneratorService;
        private readonly IControllerGeneratorService _controllerGeneratorService;
        private readonly IApiControllerGeneratorService _apiControllerGeneratorService;
        private readonly ISharedResourceGeneratorService _sharedResourceGeneratorService;
        private readonly IDtoResourceGeneratorService _dtoResourceGeneratorService;
        private readonly IControllerResourceGeneratorService _controllerResourceGeneratorService;
        private readonly IViewResourceGeneratorService _viewResourceGeneratorService;
        private readonly IChangeEntityEnumGeneratorService _changeEntityEnumGeneratorService;
        private readonly ISortEntityEnumGeneratorService _sortEntityEnumGeneratorService;
        private readonly IEnumGeneratorService _enumGeneratorService;
        private readonly IEnumResourceGeneratorService _enumResourceGeneratorService;
        private readonly IViewGeneratorService _viewGeneratorService;
        private readonly IMenuGeneratorService _menuGeneratorService;
        private readonly ISiteService _siteService;

        public GeneratorService(IEntityGeneratorService entityGeneratorService,
            IProjectService projectService,
            IBaseGeneratorService baseGeneratorService,
            IDbContextGeneratorService dbContextGeneratorService,
            IDtoGeneratorService dtoGeneratorService,
            IMediaInformationGeneratorService mediaInformationGeneratorService,
            IMapperGeneratorService mapperGeneratorService,
            IServiceInterfaceGeneratorService serviceInterfaceGeneratorService,
            IServiceGeneratorService serviceGeneratorService,
            IControllerGeneratorService controllerGeneratorService,
            IApiControllerGeneratorService apiControllerGeneratorService,
            ISharedResourceGeneratorService sharedResourceGeneratorService,
            IDtoResourceGeneratorService dtoResourceGeneratorService,
            IControllerResourceGeneratorService controllerResourceGeneratorService,
            IViewResourceGeneratorService viewResourceGeneratorService,
            IChangeEntityEnumGeneratorService changeEntityEnumGeneratorService,
            ISortEntityEnumGeneratorService sortEntityEnumGeneratorService,
            IEnumGeneratorService enumGeneratorService,
            IEnumResourceGeneratorService enumResourceGeneratorService,
            IViewGeneratorService viewGeneratorService,
            IMenuGeneratorService menuGeneratorService,
            ISiteService siteService
            )
        {
            this._entityGeneratorService = entityGeneratorService;
            this._baseGeneratorService = baseGeneratorService;
            _dbContextGeneratorService = dbContextGeneratorService;
            _dtoGeneratorService = dtoGeneratorService;
            _mediaInformationGeneratorService = mediaInformationGeneratorService;
            this._mapperGeneratorService = mapperGeneratorService;
            this._serviceInterfaceGeneratorService = serviceInterfaceGeneratorService;
            this._serviceGeneratorService = serviceGeneratorService;
            this._controllerGeneratorService = controllerGeneratorService;
            _apiControllerGeneratorService = apiControllerGeneratorService;
            this._sharedResourceGeneratorService = sharedResourceGeneratorService;
            _dtoResourceGeneratorService = dtoResourceGeneratorService;
            _controllerResourceGeneratorService = controllerResourceGeneratorService;
            _viewResourceGeneratorService = viewResourceGeneratorService;
            this._changeEntityEnumGeneratorService = changeEntityEnumGeneratorService;
            this._sortEntityEnumGeneratorService = sortEntityEnumGeneratorService;
            this._enumGeneratorService = enumGeneratorService;
            this._projectService = projectService;
            this._enumResourceGeneratorService = enumResourceGeneratorService;
            this._viewGeneratorService = viewGeneratorService;
            this._menuGeneratorService = menuGeneratorService;
            this._siteService = siteService;
        }

        #endregion

        public async Task<List<GenerateCleanArchitectureResultDto>> GenerateCleanArchitecture(GenerateProjectDto generate)
        {
            List<GenerateCleanArchitectureResultDto> results = new List<GenerateCleanArchitectureResultDto>();
            List<EntityFullInformationDto> entities = await _baseGeneratorService.GetEntitiesFullInformation(generate.ProjectId);
            List<EntityFullInformationDto> distinctServiceEntities = entities
                .DistinctBy(a => a.Entity.ServiceName)
                .ToList();
            GenerateEntityDto generateEntityDto = generate.ToGenerate();
            generateEntityDto.GenerateIServices = false;
            generateEntityDto.GenerateServices = false;
            generateEntityDto.GenerateEnums = false;
            generateEntityDto.GenerateMenus = false;
            foreach (var entity in entities)
            {
                var res = await GenerateCleanArchitecture(generateEntityDto, entity);
                results.Add(res);
            }
            generateEntityDto.GenerateIServices = generate.GenerateIServices;
            generateEntityDto.GenerateServices = generate.GenerateServices;
            generateEntityDto.GenerateEnums = generate.GenerateEnums;
            generateEntityDto.GenerateMenus = generate.GenerateMenus;

            if (generate.GenerateEnums)
            {
                GenerateCleanArchitectureResultDto result = new GenerateCleanArchitectureResultDto();
                result.EnumsResult.AddRange((await GenerateProjectEnums(generate.ProjectId, generate.GenerateResources)));
            }
            if (generate.GenerateMenus)
                GenerateAdminMenus(generateEntityDto, entities);
            if (generate.GenerateIServices || generate.GenerateServices)
            {
                foreach (var singleDistinctServiceEntity in distinctServiceEntities)
                {
                    GenerateCleanArchitectureResultDto generateServicesResult = new GenerateCleanArchitectureResultDto();
                    List<EntityFullInformationDto> allEntitiesWithThisService = entities
                        .Where(a => a.Entity.ServiceName == singleDistinctServiceEntity.Entity.ServiceName)
                        .ToList();
                    if (generate.GenerateIServices)
                        generateServicesResult.IServiceResult = _serviceInterfaceGeneratorService.Generate(generateEntityDto, allEntitiesWithThisService);
                    if (generate.GenerateServices)
                        generateServicesResult.ServiceResult = _serviceGeneratorService.Generate(generateEntityDto, allEntitiesWithThisService);

                    results.Add(generateServicesResult);
                }
            }
            if (generate.GenerateSharedResources)
            {
                GenerateCleanArchitectureResultDto result = new GenerateCleanArchitectureResultDto();
                result.SharedResourcesResults = _sharedResourceGeneratorService.Generate(generateEntityDto, entities);
                results.Add(result);
            }

            return results;
        }

        public async Task<GenerateCleanArchitectureResultDto> GenerateCleanArchitecture(GenerateEntityDto generate)
        {
            GenerateCleanArchitectureResultDto generateCleanArchitectureResult = new GenerateCleanArchitectureResultDto();

            EntityFullInformationDto? entityFullInformation = await _baseGeneratorService.GetEntityFullInformation(generate.EntityId);
            if (entityFullInformation is null)
            {
                generateCleanArchitectureResult.EntityResult = new CreateFileResultDto()
                {
                    FailedCount = 1,
                    Name = $"entity with id : {generate.EntityId}",
                    FailedErrors = new List<string>()
                    {
                    "Entity Not Found in the database"
                    }
                };
                return generateCleanArchitectureResult;
            }

            return await GenerateCleanArchitecture(generate, entityFullInformation);
        }

        private async Task<GenerateCleanArchitectureResultDto> GenerateCleanArchitecture(GenerateEntityDto generate, EntityFullInformationDto entityFullInformation)
        {
            GenerateCleanArchitectureResultDto generateCleanArchitectureResult = new GenerateCleanArchitectureResultDto();
            if (generate.GenerateEntities)
            {
                generateCleanArchitectureResult.EntityResult = _entityGeneratorService.Generate(generate, entityFullInformation);
                generateCleanArchitectureResult.EnumsResult.Add(_changeEntityEnumGeneratorService.Generate(generate, entityFullInformation));
                generateCleanArchitectureResult.EnumsResult.Add(_sortEntityEnumGeneratorService.Generate(generate, entityFullInformation));
            }
            if (generate.AddToDbContext)
                _dbContextGeneratorService.AddEntityDbSet(entityFullInformation);
            if (generate.GenerateMenus)
                GenerateAdminMenus(generate, entityFullInformation);
            if (generate.GenerateDtos)
                generateCleanArchitectureResult.DtoResult = _dtoGeneratorService.Generate(generate, entityFullInformation);
            if (generate.GenerateMediasInformation)
                generateCleanArchitectureResult.MediaInformationResult = _mediaInformationGeneratorService.Generate(generate, entityFullInformation);
            if (generate.GenerateMappers)
                generateCleanArchitectureResult.MapperResult = _mapperGeneratorService.Generate(generate, entityFullInformation);
            if (generate.GenerateIServices || generate.GenerateServices)
            {
                List<EntityFullInformationDto> allEntitiesInThisService = await _baseGeneratorService.GetEntitiesFullInformation(entityFullInformation.Entity.ServiceName, entityFullInformation.Entity.ProjectId);
                if (generate.GenerateIServices)
                {
                    generateCleanArchitectureResult.IServiceResult = _serviceInterfaceGeneratorService.Generate(generate, allEntitiesInThisService);
                }
                if (generate.GenerateServices)
                    generateCleanArchitectureResult.ServiceResult = _serviceGeneratorService.Generate(generate, allEntitiesInThisService);
            }
            if (generate.GenerateWebControllers)
                generateCleanArchitectureResult.ControllerResults = _controllerGeneratorService.Generate(generate, entityFullInformation);
            if (generate.GenerateApiControllers)
                generateCleanArchitectureResult.ApiControllerResult = _apiControllerGeneratorService.Generate(generate, entityFullInformation);
            if (generate.GenerateResources)
                generateCleanArchitectureResult.DtoResourcesResults = _dtoResourceGeneratorService.Generate(generate, entityFullInformation);
            if (generate.GenerateControllerResources)
                generateCleanArchitectureResult.ControllerResourcesResults = _controllerResourceGeneratorService.Generate(generate, entityFullInformation);
            if (generate.GenerateViewResources)
                generateCleanArchitectureResult.ViewResourcesResults = _viewResourceGeneratorService.Generate(generate, entityFullInformation);
            if (generate.GenerateEnums)
                generateCleanArchitectureResult.EnumsResult.AddRange((await GenerateProjectEnums(entityFullInformation.Project.Id, generate.GenerateResources)));
            if (generate.GenerateWebViews)
                generateCleanArchitectureResult.ViewsResults.AddRange(_viewGeneratorService.Generate(generate, entityFullInformation));
            return generateCleanArchitectureResult;
        }

        public async Task<List<CreateFileResultDto>> GenerateProjectEnums(int projectId, bool generateResources)
        {
            ProjectListDto? project = await _projectService.GetSingleProjectInformation(projectId);
            if (project is null) return new List<CreateFileResultDto>();
            FilterProjectEnumsDto filter = new FilterProjectEnumsDto()
            {
                ProjectId = projectId
            };
            FilterProjectEnumsDto result = await _projectService.FilterProjectEnums(filter);
            List<CreateFileResultDto> results = _enumGeneratorService.Generate(project, result.ProjectEnums);
            if (generateResources)
                results.AddRange(_enumResourceGeneratorService.Generate(result.ProjectEnums, project));
            return results;
        }

        async Task<CreateFileResultDto> GenerateAdminMenus(GenerateEntityDto generate)
        {
            EntityFullInformationDto? entity = await _baseGeneratorService.GetEntityFullInformation(generate.EntityId);
            if (entity is null) return new CreateFileResultDto();
            return GenerateAdminMenus(generate, entity);
        }

        CreateFileResultDto GenerateAdminMenus(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            List<EntityFullInformationDto> entities = new List<EntityFullInformationDto>() { entity };
            return GenerateAdminMenus(generate, entities);
        }

        CreateFileResultDto GenerateAdminMenus(GenerateEntityDto generate, List<EntityFullInformationDto> entities)
        => _menuGeneratorService.Generate(generate, entities);

        public async Task<BaseChangeEntityResult> GenerateCleanArchitecutreSolution(int projectId)
        {
            ProjectListDto? projectInformation = await _projectService.GetSingleProjectInformation(projectId);
            if (projectInformation is null) return BaseChangeEntityResult.NotFound;
            GeneralSettingListDto? generalSettingInformation = await _siteService.GetSingleGeneralSettingInformation(1);
            if (generalSettingInformation is null) return BaseChangeEntityResult.NotFound;
            return CloneBaseFiles(generalSettingInformation.DefaultSolutionLocation, projectInformation.Location, generalSettingInformation.DefaultSolutionName, projectInformation.EnglishName);
        }

        public async Task<BaseChangeEntityResult> GenerateReactJsSolution(int projectId)
        {
            ProjectListDto? projectInformation = await _projectService.GetSingleProjectInformation(projectId);
            if (projectInformation is null) return BaseChangeEntityResult.NotFound;
            GeneralSettingListDto? generalSettingInformation = await _siteService.GetSingleGeneralSettingInformation(1);
            if (generalSettingInformation is null) return BaseChangeEntityResult.NotFound;
            if (string.IsNullOrEmpty(projectInformation.ReactProjectLocation)) return BaseChangeEntityResult.NotFound;
            if (string.IsNullOrEmpty(generalSettingInformation.DefaultReactSolutionLocation) ||
                string.IsNullOrEmpty(generalSettingInformation.DefaultReactJsSolutionName)) return BaseChangeEntityResult.NotFound;
            return CloneBaseFiles(generalSettingInformation.DefaultReactSolutionLocation, projectInformation.ReactProjectLocation, generalSettingInformation.DefaultReactJsSolutionName, projectInformation.EnglishName);
        }

        BaseChangeEntityResult CloneBaseFiles(string fromLocation, string targetLocation, string oldName, string newName)
        {
            if (!Directory.Exists(targetLocation))
                Directory.CreateDirectory(targetLocation);
            if (!File.Exists(fromLocation))
                return BaseChangeEntityResult.NotFound;
            string fileName = Path.GetFileName(fromLocation);
            string copyLocation = Path.Combine(targetLocation, fileName);
            File.Copy(fromLocation, copyLocation, true);

            using (ZipArchive archive = ZipFile.OpenRead(fromLocation))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string destinationPath = Path.Combine(targetLocation, entry.FullName);

                    // Ensure the destination directory for the entry exists
                    string destinationDirPath = Path.GetDirectoryName(destinationPath);
                    if (!Directory.Exists(destinationDirPath))
                        Directory.CreateDirectory(destinationDirPath);

                    // Extract the entry
                    if (entry.Name != "")
                        entry.ExtractToFile(destinationPath, true);
                }
            }
            copyLocation.DeleteFile();

            targetLocation.RenameFiles(oldName, newName);
            targetLocation.RenameFolders(oldName, newName);
            targetLocation.ReplaceTextInFiles(oldName, newName);
            return BaseChangeEntityResult.Success;
        }


    }
}
