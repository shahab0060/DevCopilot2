using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Projects;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class EnumResourceGeneratorService : IEnumResourceGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public EnumResourceGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public List<CreateFileResultDto> Generate(List<ProjectEnumListDto> projectEnums, ProjectListDto project)
        {
            List<CreateFileResultDto> results = new List<CreateFileResultDto>();
            foreach (var projectEnum in projectEnums)
            {
                results.AddRange(Generate(projectEnum, project));
            }
            return results;
        }

        public List<CreateFileResultDto> Generate(ProjectEnumListDto projectEnum, ProjectListDto project)
        {
            List<CreateFileResultDto> results = new List<CreateFileResultDto>();
            List<GenerateFileDto> files = GetFiles(projectEnum, project);
            foreach (var file in files)
            {
                results.Add(_baseGeneratorService.GenerateFile(file));
            }
            return results;
        }

        string GetPath(ProjectEnumListDto projectEnum, ProjectListDto project)
 => $@"{project.Location}\{project.EnglishName}.Domain\Resources\Enums\{projectEnum.FolderName}";

        string GetNameSpace(ProjectEnumListDto projectEnum, ProjectListDto project)
=> $@"{project.EnglishName}.Domain.Resources.Enums.{projectEnum.FolderName}";

        List<GenerateFileDto> GetFiles(ProjectEnumListDto projectEnum, ProjectListDto project)
        {
            List<GenerateFileDto> files = new List<GenerateFileDto>();
            foreach (var language in project.ProjectSelectedLanguagesList)
            {
                string enumResourceName = $"{projectEnum.EnglishName}Resources";
                files.Add(enumResourceName.GetDesignerFile(GetNameSpace(projectEnum, project), GetPath(projectEnum, project), projectEnum
                    .ProjectEnumPropertiesList
                    .Select(a => a.Name)
                    .ToList()));
                files.Add(GetSingleFile(projectEnum, project, language));
            }
            return files;
        }

        GenerateFileDto GetSingleFile(ProjectEnumListDto projectEnum, ProjectListDto project, ProjectSelectedLanguageListDto language)
        {
            return new GenerateFileDto()
            {
                Override = true,
                Path = GetPath(projectEnum, project),
                FileNameWithExtension = $"{projectEnum.EnglishName}Resources.resx",
                Code = GetSingleFileCode(projectEnum, language)
            };
        }

        string GetSingleFileCode(ProjectEnumListDto projectEnum, ProjectSelectedLanguageListDto language)
        => projectEnum
                .ProjectEnumPropertiesList
                .GetResources(language);
    }
}
