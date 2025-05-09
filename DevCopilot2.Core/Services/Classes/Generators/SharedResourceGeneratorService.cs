using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Projects;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class SharedResourceGeneratorService : ISharedResourceGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public SharedResourceGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public List<CreateFileResultDto> Generate(GenerateEntityDto generate, List<EntityFullInformationDto> entities)
        {
            List<CreateFileResultDto> results = new List<CreateFileResultDto>();
            EntityFullInformationDto? firstEntity = entities.FirstOrDefault()!;
            List<GenerateFileDto> sharedEntitiesResourcesFiles = GetEntitiesResourceFiles(entities, generate);
            List<GenerateFileDto> sharedResourcesFiles = GetSharedResourceFiles(firstEntity);
            List<GenerateFileDto> cSharpFiles = GetCSharpFiles(firstEntity);
            List<GenerateFileDto> baseListDtoFiles = GetBaseListDtoFiles(entities);
            List<GenerateFileDto> generateFiles = [
                ..sharedEntitiesResourcesFiles,
                ..sharedResourcesFiles,
                ..cSharpFiles,
                ..baseListDtoFiles
                ];
            foreach (GenerateFileDto generateFile in generateFiles)
            {
                results.Add(_baseGeneratorService.GenerateFile(generateFile));
            }
            return results;
        }

        public string GetResourcePath(EntityFullInformationDto entity)
        {
            string projectName = $"{entity.Project.EnglishName}.Web";
            return $@"{entity.Project.Location}\{projectName}\Resources";
        }
        public string GetCSharpFilePath(EntityFullInformationDto entity)
        {
            string projectName = $"{entity.Project.EnglishName}.Web";
            return $@"{entity.Project.Location}\{projectName}";
        }

        public string GetBaseListDtoPath(EntityFullInformationDto entity)
        {
            string projectName = $@"{entity.Project.EnglishName}.Domain\Resources\DTOs\Common";
            return $@"{entity.Project.Location}\{projectName}";
        }

        public List<GenerateFileDto> GetCSharpFiles(EntityFullInformationDto entity)
        {
            List<GenerateFileDto> files =
            [
                GetCSharpFile(entity,"EntitiesSharedResources"),
                GetCSharpFile(entity,"SharedResources"),
            ];
            return files;
        }

        #region shared entities resource


        List<GenerateFileDto> GetEntitiesResourceFiles(List<EntityFullInformationDto> entities, GenerateEntityDto generate)
        {
            EntityFullInformationDto? firstEntity = entities.FirstOrDefault();
            if (firstEntity is null) return new List<GenerateFileDto>();
            List<GenerateFileDto> files = new List<GenerateFileDto>();
            foreach (var selectedLanguage in firstEntity.Project.ProjectSelectedLanguagesList)
            {
                files.Add(new GenerateFileDto()
                {
                    FileNameWithExtension = GetEntitiesResourceFileName(selectedLanguage),
                    Code = GetEntitiesResourceContent(entities, selectedLanguage),
                    Override = generate.OverrideFiles,
                    Path = GetResourcePath(firstEntity)
                });
            }
            return files;
        }

        string GetEntitiesResourceFileName(ProjectSelectedLanguageListDto language)
        => $@"EntitiesSharedResources.{language.LanguageCulture}.resx";


        #endregion

        string GetEntitiesResourceContent(List<EntityFullInformationDto> entities, ProjectSelectedLanguageListDto language)
        {
            List<ResourceListDto> resources =
                entities
                .SelectMany(a => new List<ResourceListDto>()
                {
                    new ResourceListDto()
                    {
                        Name = a.Entity.SingularName,
                        Value = a.Entity.EntitySelectedLanguagesList
                                .Where(b=>b.LanguageId==language.LanguageId)
                                .Select(b=>b.SingularTitle)
                                .FirstOrDefault()??"",
                    },
                    new ResourceListDto()
                    {
                        Name = a.Entity.PluralName,
                        Value = a.Entity.EntitySelectedLanguagesList
                                .Where(b=>b.LanguageId==language.LanguageId)
                                .Select(b=>b.PluralTitle)
                                .FirstOrDefault()??"",
                    }
                }).ToList();
            return $@"{resources.GetResources()}";
        }



        List<GenerateFileDto> GetSharedResourceFiles(EntityFullInformationDto entity)
        {
            List<GenerateFileDto> files = new List<GenerateFileDto>();
            foreach (var selectedLanguage in entity.Project.ProjectSelectedLanguagesList)
            {
                files.Add(new GenerateFileDto()
                {
                    FileNameWithExtension = GetSharedResourceFileName(selectedLanguage),
                    Code = GetSharedResourceContent(),
                    Override = false,
                    Path = GetResourcePath(entity)
                });
            }
            return files;
        }

        string GetSharedResourceFileName(ProjectSelectedLanguageListDto language)
        => $@"SharedResources.{language.LanguageCulture}.resx";


        string GetSharedResourceContent()
        {
            return $@"<?xml version=""1.0"" encoding=""utf-8""?>
<root>
{ResourceExtensionMethods.GetHeadersAndComments()}

</root>";
        }

        string GetBaseListDtoResourceContent()
        {
            return $@"<?xml version=""1.0"" encoding=""utf-8""?>
<root>
{ResourceExtensionMethods.GetHeadersAndComments()}

</root>";
        }

        List<GenerateFileDto> GetBaseListDtoFiles(List<EntityFullInformationDto> entities)
        {
            EntityFullInformationDto? firstEntity = entities.FirstOrDefault();
            if (firstEntity is null) return new List<GenerateFileDto>();
            string name = "BaseListDtoResources";
            string nameSpace = $"{firstEntity.Project.EnglishName}.Domain.Resources.DTOs.Common";
            List<GenerateFileDto> files = [name.GetDesignerFile(nameSpace, GetBaseListDtoPath(firstEntity), new List<string>())];
            foreach (ProjectSelectedLanguageListDto selectedLanguage in firstEntity.Project.ProjectSelectedLanguagesList)
            {
                files.Add(new GenerateFileDto()
                {
                    FileNameWithExtension = $"{name}.{selectedLanguage.LanguageCulture}.resx",
                    Code = GetBaseListDtoResourceContent(),
                    Path = GetBaseListDtoPath(firstEntity),
                    Override = false
                });
            }
            return files;
        }


        GenerateFileDto GetCSharpFile(EntityFullInformationDto entity, string name)
        {
            return new GenerateFileDto()
            {
                FileNameWithExtension = $"{name}.cs",
                Code = GetCSharpFileContent(entity, name),
                Override = false,
                Path = GetCSharpFilePath(entity)
            };
        }

        string GetCSharpFileContent(EntityFullInformationDto entity, string name)
        => $@"namespace {entity.Project.EnglishName}.Web
{{
    public class {name}
    {{
    }}
}}
";

    }
}
