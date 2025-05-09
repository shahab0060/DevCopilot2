using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Domain.DTOs.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCopilot2.Domain.Enums.Common;

namespace DevCopilot2.Core.Services.Interfaces.Generators
{
    public interface IGeneratorService : IService
    {
        Task<GenerateCleanArchitectureResultDto> GenerateCleanArchitecture(GenerateEntityDto generate);
        Task<List<GenerateCleanArchitectureResultDto>> GenerateCleanArchitecture(GenerateProjectDto generate);
        Task<List<CreateFileResultDto>> GenerateProjectEnums(int projectId, bool generateResources);
        Task<BaseChangeEntityResult> GenerateCleanArchitecutreSolution(int projectId);
        Task<BaseChangeEntityResult> GenerateReactJsSolution(int projectId);
    }
}
