namespace DevCopilot2.Domain.DTOs.Generators
{
    public class ConstructorListDto
    {
        public string ClassName { get; set; } = null!;

        public List<DependencyInjectionListDto> DependencyInjections { get; set; } = new List<DependencyInjectionListDto>();
    }

    public class DependencyInjectionListDto
    {
        public string Name { get; set; } = null!;

        public string FileName { get; set; } = null!;
    }
}
