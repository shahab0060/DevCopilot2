namespace DevCopilot2.Domain.DTOs.Generators
{
    public class CreateFileResultDto
    {
        public string Name { get; set; } = null!;

        public int CreatedCount { get; set; }

        public int FailedCount { get; set; }

        public List<string> FailedErrors { get; set; } = new List<string>();

    }

    public class GenerateCleanArchitectureResultDto
    {
        public CreateFileResultDto EntityResult { get; set; } = new CreateFileResultDto();
        public CreateFileResultDto DtoResult { get; set; } = new CreateFileResultDto();
        public CreateFileResultDto PropertyResult { get; set; } = new CreateFileResultDto();
        public CreateFileResultDto MediaInformationResult { get; set; } = new CreateFileResultDto();
        public CreateFileResultDto MapperResult { get; set; } = new CreateFileResultDto();
        public CreateFileResultDto IServiceResult { get; set; } = new CreateFileResultDto();
        public CreateFileResultDto ServiceResult { get; set; } = new CreateFileResultDto();
        public List<CreateFileResultDto> ControllerResults { get; set; } = new List<CreateFileResultDto>();
        public List<CreateFileResultDto> SharedResourcesResults { get; set; } = new List<CreateFileResultDto>();
        public List<CreateFileResultDto> DtoResourcesResults { get; set; } = new List<CreateFileResultDto>();
        public List<CreateFileResultDto> ControllerResourcesResults { get; set; } = new List<CreateFileResultDto>();
        public List<CreateFileResultDto> ViewResourcesResults { get; set; } = new List<CreateFileResultDto>();
        public CreateFileResultDto ApiControllerResult { get; set; } = new CreateFileResultDto();
        public CreateFileResultDto EntityRelationResult { get; set; } = new CreateFileResultDto();
        public List<CreateFileResultDto> EnumsResult { get; set; } = new List<CreateFileResultDto>();
        public List<CreateFileResultDto> ViewsResults { get; set; } = new List<CreateFileResultDto>();

        public CreateFileResultDto GetAllResults()
        {
            CreateFileResultDto result = new CreateFileResultDto();
            result = Add(result, EntityResult);
            result = Add(result, DtoResult);
            result = Add(result, PropertyResult);
            result = Add(result, MediaInformationResult);
            result = Add(result, MapperResult);
            result = Add(result, IServiceResult);
            result = Add(result, ServiceResult);
            result = Add(result, ControllerResults);
            result = Add(result, SharedResourcesResults);
            result = Add(result, DtoResourcesResults);
            result = Add(result, ControllerResourcesResults);
            result = Add(result, ViewResourcesResults);
            result = Add(result, ApiControllerResult);
            result = Add(result, EntityRelationResult);
            result = Add(result, EnumsResult);
            result = Add(result, ViewsResults);
            return result;
        }

        CreateFileResultDto Add(CreateFileResultDto mainResult, CreateFileResultDto newResult)
        {
            mainResult.CreatedCount += newResult.CreatedCount;
            mainResult.FailedCount += newResult.FailedCount;
            mainResult.FailedErrors.AddRange(newResult.FailedErrors);
            return mainResult;
        }

        CreateFileResultDto Add(CreateFileResultDto mainResult, List<CreateFileResultDto> newResults)
        {
            foreach (var newResult in newResults)
            {
                mainResult = Add(mainResult, newResult);
            }
            return mainResult;
        }
    }
}
