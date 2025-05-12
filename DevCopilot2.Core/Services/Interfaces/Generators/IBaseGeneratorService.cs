using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.Enums.Common;

namespace DevCopilot2.Core.Services.Interfaces.Generators
{
    public interface IBaseGeneratorService : IService
    {
        Task<EntityFullInformationDto?> GetEntityFullInformation(long entityid);
        Task<List<EntityFullInformationDto>> GetEntitiesFullInformation(long projectId);
        Task<List<EntityFullInformationDto>> GetEntitiesFullInformation(string serviceName,long projectId);
        CreateFileResultDto GenerateFile(GenerateFileDto generate);
        List<string> GetBaseUsings();
        void FormatFile(string fileLocation);
        BaseChangeEntityResult AddRegionAndCode(string fileLocation, string regionName, string code,string regionNameToFind);
        BaseChangeEntityResult AppendUsing(string fileLocation, string newUsing);
    }
}
