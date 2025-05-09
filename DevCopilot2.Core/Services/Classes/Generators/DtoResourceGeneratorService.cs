using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Properties;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class DtoResourceGeneratorService : IDtoResourceGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public DtoResourceGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public List<CreateFileResultDto> Generate(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            List<GenerateFileDto> generateFiles = [
                ..GetFilterFiles(entity,generate),
                ..GetListFiles(entity,generate),
                ..GetCreateFiles(entity,generate),
                ..GetUpdateFiles(entity,generate),
                ..GetListFiles(entity,generate),
                ];
            List<CreateFileResultDto> fileResults = new List<CreateFileResultDto>();
            foreach (GenerateFileDto generateFile in generateFiles)
            {
                fileResults.Add(_baseGeneratorService.GenerateFile(generateFile));
            }
            return fileResults;
        }
        string GetFilePath(EntityFullInformationDto entity)
       => $@"{entity.Project.Location}\{entity.Project.EnglishName}.Domain\Resources\DTOs\{entity.Entity.FolderName}";

        public string GetNameSpace(EntityFullInformationDto entity)
       => $@"{entity.Project.EnglishName}.Domain.DTOs.{entity.Entity.FolderName}";

        #region filter

        public List<GenerateFileDto> GetFilterFiles(EntityFullInformationDto entity, GenerateEntityDto generate)
        {
            List<GenerateFileDto> files = new List<GenerateFileDto>();
            List<PropertyListDto> properties = entity.GetFilterProperties();
            foreach (var selectedLanguage in entity.Project.ProjectSelectedLanguagesList)
            {
                files.Add(new GenerateFileDto()
                {
                    FileNameWithExtension = $"Filter{entity.Entity.PluralName}Dto.{selectedLanguage.LanguageCulture}.resx",
                    Code = properties.GetResources(selectedLanguage),
                    Override = generate.OverrideFiles,
                    Path = GetFilePath(entity)
                });
            }
            return files;
        }

        #endregion

        #region create

        public List<GenerateFileDto> GetCreateFiles(EntityFullInformationDto entity, GenerateEntityDto generate)
        {
            List<GenerateFileDto> files = new List<GenerateFileDto>();
            List<PropertyListDto> properties = entity.GetBaseUpsertDtoAllProperties();
            properties.AddRange(entity.GetCreateDtoAllProperties());
            properties = properties.Distinct().ToList();
            foreach (var selectedLanguage in entity.Project.ProjectSelectedLanguagesList)
            {
                files.Add(new GenerateFileDto()
                {
                    FileNameWithExtension = $"Create{entity.Entity.SingularName}Dto.{selectedLanguage.LanguageCulture}.resx",
                    Code = properties.GetResources(selectedLanguage),
                    Override = generate.OverrideFiles,
                    Path = GetFilePath(entity)
                });
            }
            return files;
        }

        #endregion

        #region update

        public List<GenerateFileDto> GetUpdateFiles(EntityFullInformationDto entity, GenerateEntityDto generate)
        {
            List<GenerateFileDto> files = new List<GenerateFileDto>();
            List<PropertyListDto> properties = entity.GetUpdateDtoAllProperties();
            properties.AddRange(entity.GetBaseUpsertDtoAllProperties());
            properties = properties.Distinct().ToList();
            foreach (var selectedLanguage in entity.Project.ProjectSelectedLanguagesList)
            {
                files.Add(new GenerateFileDto()
                {
                    FileNameWithExtension = $"Update{entity.Entity.SingularName}Dto.{selectedLanguage.LanguageCulture}.resx",
                    Code = properties.GetResources(selectedLanguage),
                    Override = generate.OverrideFiles,
                    Path = GetFilePath(entity)
                });
            }
            return files;
        }

        #endregion

        #region list

        public List<GenerateFileDto> GetListFiles(EntityFullInformationDto entity, GenerateEntityDto generate)
        {
            List<GenerateFileDto> files = new List<GenerateFileDto>();
            List<PropertyListDto> properties = entity.GetEntityListDtoProperties();
            foreach (var selectedLanguage in entity.Project.ProjectSelectedLanguagesList)
            {
                files.Add(new GenerateFileDto()
                {
                    FileNameWithExtension = $"{entity.Entity.SingularName}ListDto.{selectedLanguage.LanguageCulture}.resx",
                    Code = properties.GetResources(selectedLanguage),
                    Override = generate.OverrideFiles,
                    Path = GetFilePath(entity)
                });
            }
            return files;
        }

        #endregion
    }
}
