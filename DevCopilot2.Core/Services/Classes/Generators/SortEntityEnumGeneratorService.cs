using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Properties;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class SortEntityEnumGeneratorService : ISortEntityEnumGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public SortEntityEnumGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public CreateFileResultDto Generate(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            GenerateFileDto generateFile = new GenerateFileDto()
            {
                FileNameWithExtension = $"{entity.GetSortEntityEnumName()}.cs",
                Override = generate.OverrideFiles,
                Path = $@"{entity.Project.Location}\{entity.Project.EnglishName}.Domain\Enums\{entity.Entity.FolderName}"
            };
            generateFile.Code = $@"{GetUsings(entity)}
namespace {GetNameSpace(entity)}
{{
    public enum {entity.GetSortEntityEnumName()}
    {{
        {GetEnumProperteisCode(entity)}
    }}
}}
";
            return _baseGeneratorService.GenerateFile(generateFile);
        }

        string GetUsings(EntityFullInformationDto entity)
        {
            List<string> usings = [.. _baseGeneratorService.GetBaseUsings()];
            usings.Add($"System.ComponentModel.DataAnnotations");
            return usings.GetUsings(GetNameSpace(entity));
        }

        public string GetNameSpace(EntityFullInformationDto entity)
          => $@"{entity.Project.EnglishName}.Domain.Enums.{entity.Entity.FolderName}";

        private string GetEnumProperteisCode(EntityFullInformationDto entity)
        {
            List<PropertyListDto> properties = entity.GetEntityListDtoProperties();
            List<PropertyListDto> relationProperties = entity.GetMiddleRelationListDtoProperties();
            properties.AddRange(relationProperties);
            return string.Join("\n", properties.ConvertAll(GetSingleEnumPropertyCode));
        }

        private string GetSingleEnumPropertyCode(PropertyListDto property)
        {
            return $@"    
        [Display(Name = ""{property.Name}"")]
        {property.Name},";
        }
    }
}
