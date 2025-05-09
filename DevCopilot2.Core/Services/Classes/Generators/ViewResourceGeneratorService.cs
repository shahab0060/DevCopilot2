using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Projects;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class ViewResourceGeneratorService : IViewResourceGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public ViewResourceGeneratorService(IBaseGeneratorService baseGeneratorService)
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

        List<GenerateFileDto> GetResources(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            List<GenerateFileDto> results = new List<GenerateFileDto>();
            foreach (var area in entity.Entity.EntitySelectedProjectAreasList)
            {
                results.AddRange(GetSingleAreaResources(generate, entity, area));
            }
            return results;
        }

        List<GenerateFileDto> GetSingleAreaResources(GenerateEntityDto generate, EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            List<GenerateFileDto> files = new List<GenerateFileDto>();
            foreach (var selectedLanguage in entity.Project.ProjectSelectedLanguagesList)
            {
                if (area.HasIndex)
                {
                    files.Add(GetIndex(entity, generate, selectedLanguage, area));
                    files.Add(GetDetail(entity, generate, selectedLanguage, area));
                }
                if (area.HasCreate)
                    files.Add(GetCreate(entity, generate, selectedLanguage, area));
                if (area.HasUpdate)
                    files.Add(GetUpdate(entity, generate, selectedLanguage, area));
            }
            return files;
        }



        string GetSubLocation(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto selectedArea)
        {
            string projectName = $"{entity.Project.EnglishName}.Web";
            if (selectedArea.ProjectAreaTitle.ToLower() == "site")
                return $@"{entity.Project.Location}\{projectName}\Resources\Views\{entity.Entity.SingularName}";
            return $@"{entity.Project.Location}\{projectName}\Resources\Areas\{selectedArea.ProjectAreaTitle}\Views\{entity.Entity.SingularName}";
        }

        #region index

        GenerateFileDto GetIndex(EntityFullInformationDto entity, GenerateEntityDto generate,
            ProjectSelectedLanguageListDto language, EntitySelectedProjectAreaListDto area)
        {
            return new GenerateFileDto()
            {
                FileNameWithExtension = $"Index.{language.LanguageCulture}.resx",
                Code = GetIndexContent(entity, language),
                Override = generate.OverrideFiles,
                Path = GetSubLocation(entity, area)
            };
        }
        string GetIndexContent(EntityFullInformationDto entity, ProjectSelectedLanguageListDto language)
        {
            string propertiesContent = entity
                .GetEntityListDtoProperties()
                .GetResources(language);
            //string secondaryRelationEntitiesContent = string.Join("\n", relationSecondaryEntities
            //    .ConvertAll(a => a.GetSingleEntityResourceContent(language)));
            return $@"{propertiesContent}";
        }

        #endregion

        #region create

        GenerateFileDto GetCreate(EntityFullInformationDto entity, GenerateEntityDto generate,
            ProjectSelectedLanguageListDto language, EntitySelectedProjectAreaListDto area)
        {
            return new GenerateFileDto()
            {
                FileNameWithExtension = $"Create.{language.LanguageCulture}.resx",
                Code = GetCreateContent(entity, language),
                Override = generate.OverrideFiles,
                Path = GetSubLocation(entity, area)
            };
        }
        string GetCreateContent(EntityFullInformationDto entity, ProjectSelectedLanguageListDto language)
        {
            string propertiesContent = entity
                .Properties
                .GetResources(language);
            //string secondaryRelationEntitiesContent = string.Join("\n", relationSecondaryEntities
            //    .ConvertAll(a => a.GetSingleEntityResourceContent(language)));
            return $@"{propertiesContent}";
        }

        #endregion

        #region update

        GenerateFileDto GetUpdate(EntityFullInformationDto entity, GenerateEntityDto generate,
            ProjectSelectedLanguageListDto language, EntitySelectedProjectAreaListDto area)
        {
            return new GenerateFileDto()
            {
                FileNameWithExtension = $"Update.{language.LanguageCulture}.resx",
                Code = GetUpdateContent(entity, language),
                Override = generate.OverrideFiles,
                Path = GetSubLocation(entity, area)
            };
        }
        string GetUpdateContent(EntityFullInformationDto entity, ProjectSelectedLanguageListDto language)
        {
            string propertiesContent = entity
                .GetUpdateDtoAllProperties()
                .GetResources(language);
            //string secondaryRelationEntitiesContent = string.Join("\n", relationSecondaryEntities
            //    .ConvertAll(a => a.GetSingleEntityResourceContent(language)));
            return $@"{propertiesContent}";
        }

        #endregion

        #region detail

        GenerateFileDto GetDetail(EntityFullInformationDto entity, GenerateEntityDto generate,
            ProjectSelectedLanguageListDto language, EntitySelectedProjectAreaListDto area)
        {
            return new GenerateFileDto()
            {
                FileNameWithExtension = $"Detail.{language.LanguageCulture}.resx",
                Code = GetDetailContent(entity, language),
                Override = generate.OverrideFiles,
                Path = GetSubLocation(entity, area)
            };
        }
        string GetDetailContent(EntityFullInformationDto entity, ProjectSelectedLanguageListDto language)
        {
            string propertiesContent = entity
                .GetAllEntitiesListDtoProperties()
                .GetResources(language);
            //string secondaryRelationEntitiesContent = string.Join("\n", relationSecondaryEntities
            //    .ConvertAll(a => a.GetSingleEntityResourceContent(language)));
            return $@"{propertiesContent}";
        }

        #endregion
    }
}
