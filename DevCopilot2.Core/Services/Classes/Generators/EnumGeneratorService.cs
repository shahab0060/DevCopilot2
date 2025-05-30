using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Projects;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class EnumGeneratorService : IEnumGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public EnumGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public List<CreateFileResultDto> Generate(ProjectListDto project, List<ProjectEnumListDto> projectEnums)
        {
            List<CreateFileResultDto> results = new List<CreateFileResultDto>();
            foreach (var projectEnum in projectEnums)
            {
                results.Add(Generate(project, projectEnum));
            }
            return results;
        }


        public CreateFileResultDto Generate(ProjectListDto project, ProjectEnumListDto projectEnum)
        {
            GenerateFileDto generateFile = new GenerateFileDto()
            {
                FileNameWithExtension = $"{projectEnum.EnglishName}.cs",
                Override = true,
                Path = $@"{project.Location}\{project.EnglishName}.Domain\Enums\{projectEnum.FolderName}"
            };
            generateFile.Code = $@"{GetUsings(project, projectEnum)}

namespace {GetNameSpace(project, projectEnum)}
{{
    public enum {projectEnum.EnglishName}
    {{
{GetEnumProperteisCode(projectEnum)}
    }}
}}
";
            return _baseGeneratorService.GenerateFile(generateFile);
        }

        string GetUsings(ProjectListDto project, ProjectEnumListDto projectEnum)
        {
            List<string> usings = [.. _baseGeneratorService.GetBaseUsings()];
            usings.Add($"System.ComponentModel.DataAnnotations");
            usings.Add($"{project.EnglishName}.Domain.Resources.Enums.{projectEnum.FolderName}");
            return usings.GetUsings(GetNameSpace(project, projectEnum));
        }

        public string GetNameSpace(ProjectListDto project, ProjectEnumListDto projectEnum)
          => $@"{project.EnglishName}.Domain.Enums.{projectEnum.FolderName}";


        private string GetEnumProperteisCode(ProjectEnumListDto projectEnum)
        => string.Join("\n",
            projectEnum
            .ProjectEnumPropertiesList
            .Select((a, index) => GetSingleEnumPropertyCode(a, projectEnum, index)));

        private string GetSingleEnumPropertyCode(ProjectEnumPropertyListDto property, ProjectEnumListDto projectEnum, int index)
        //    => $@"
        //[Display(Name = ""{property.Title}"")]
        //{property.Name} = {property.Order},";
        {
            if (projectEnum.ProjectId == 10006) return GetSingleWihtoutResourceEnumPropertyCode(property, index);
            return $@"        [Display(ResourceType = typeof({projectEnum.GetResourceName()}), Name = nameof({projectEnum.GetResourceName()}.{property.Name}))]
            {property.Name} = {index},";
        }

        private string GetSingleWihtoutResourceEnumPropertyCode(ProjectEnumPropertyListDto property, int index)
        {
            return $@"        [Display(Name = ""{property.Name}"")]
            {property.Name} = {index},";
        }
    }
}
