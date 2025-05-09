using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Projects;

namespace DevCopilot2.Core.Services.Interfaces.Generators
{
    public interface IEnumGeneratorService : IService
    {
        CreateFileResultDto Generate(ProjectListDto entity,ProjectEnumListDto projectEnum);
        List<CreateFileResultDto> Generate(ProjectListDto entity,List<ProjectEnumListDto> projectEnum);
    }
}
