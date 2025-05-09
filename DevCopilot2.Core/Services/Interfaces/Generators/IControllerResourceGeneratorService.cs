using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;

namespace DevCopilot2.Core.Services.Interfaces.Generators
{
    public interface IControllerResourceGeneratorService : IService
    {
        List<CreateFileResultDto> Generate(GenerateEntityDto generate, EntityFullInformationDto entity);
    }
}
