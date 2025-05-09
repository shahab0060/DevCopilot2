using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Properties;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class ChangeEntityEnumGeneratorService : IChangeEntityEnumGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public ChangeEntityEnumGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public CreateFileResultDto Generate(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            if (entity.CountUniqueProperties() < 2) return new CreateFileResultDto();
            GenerateFileDto generateFile = new GenerateFileDto()
            {
                FileNameWithExtension = $"{entity.GetChangeEntityEnumName()}.cs",
                Override = generate.OverrideFiles,
                Path = $@"{entity.Project.Location}\{entity.Project.EnglishName}.Domain\Enums\{entity.Entity.FolderName}"
            };
            generateFile.Code = $@"{GetUsings(entity)}

namespace {GetNameSpace(entity)}
{{
    public enum {entity.GetChangeEntityEnumName()}
    {{
        NotFound,
        Success,

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
       => string.Join("\n",
           entity
           .GetUniqueProperties()
           .ToList()
           .ConvertAll(GetSingleEnumPropertyCode));

        private string GetSingleEnumPropertyCode(PropertyListDto property)
            => $@"    
        [Display(Name = ""{property.Name}"")]
        {property.Name}Exists,";

    }
}