using DevCopilot2.Domain.DTOs.Generators;

namespace DevCopilot2.Core.Services.Interfaces.Generators
{
    public interface IDbContextGeneratorService : IService
    {
        void AddEntityDbSet(EntityFullInformationDto entity);
    }
}
