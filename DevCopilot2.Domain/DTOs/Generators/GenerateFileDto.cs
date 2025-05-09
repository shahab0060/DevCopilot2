namespace DevCopilot2.Domain.DTOs.Generators
{
    public class GenerateFileDto
    {
        public string Path { get; set; } = null!;
        public string FileNameWithExtension { get; set; } = null!;
        public string Code { get; set; } = null!;
        public bool Override { get; set; } = true;

    }
}
