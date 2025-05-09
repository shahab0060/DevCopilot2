using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Projects;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class ControllerResourceGeneratorService : IControllerResourceGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public ControllerResourceGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public List<CreateFileResultDto> Generate(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            List<CreateFileResultDto> results = new List<CreateFileResultDto>();
            List<GenerateFileDto> files = GetResources(generate, entity);
            foreach (var file in files)
            {
                results.Add(_baseGeneratorService.GenerateFile(file));
            }
            return results;
        }
        string GetFilePath(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto selectedArea)
        {
            string projectName = $"{entity.Project.EnglishName}.Web";
            if (selectedArea.ProjectAreaTitle.ToLower() == "site")
                return $@"{entity.Project.Location}\{projectName}\Resources\Controllers\{entity.Entity.SingularName}";
            return $@"{entity.Project.Location}\{projectName}\Resources\Areas\{selectedArea.ProjectAreaTitle}\Controllers\{entity.Entity.SingularName}";
        }
        List<GenerateFileDto> GetResources(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            List<GenerateFileDto> results = new List<GenerateFileDto>();
            foreach (var area in entity.Entity.EntitySelectedProjectAreasList)
            {
                foreach (var language in entity.Project.ProjectSelectedLanguagesList)
                {
                    results.Add(GetResourceFile(generate, entity, language, area));
                }
            }
            return results;
        }

        GenerateFileDto GetResourceFile(GenerateEntityDto generate, EntityFullInformationDto entity,
            ProjectSelectedLanguageListDto language, EntitySelectedProjectAreaListDto area)
        => new GenerateFileDto()
        {
            Override = generate.OverrideFiles,
            Path = GetFilePath(entity, area),
            FileNameWithExtension = $"{entity.Entity.SingularName}Controller.{language.LanguageCulture}.resx",
            Code = GetResourceContent(entity, language)
        };

        string GetResourceContent(EntityFullInformationDto entity, ProjectSelectedLanguageListDto language)
        {
            string propertiesContent = entity
                .GetUniqueProperties()
                .GetResources(language);
            return $@"{propertiesContent}";
        }
    }
}
