using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Projects;

namespace DevCopilot2.Core.Services.Interfaces.Generators
{
    public interface IEnumResourceGeneratorService : IService
    {
        List<CreateFileResultDto> Generate(ProjectEnumListDto projectEnum, ProjectListDto project);
        List<CreateFileResultDto> Generate(List<ProjectEnumListDto> projectEnums, ProjectListDto project);
    }
}
