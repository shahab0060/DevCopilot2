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
    }
}
