namespace DevCopilot2.Domain.DTOs.Common
{
    public class ComboDto
    {
        public string Title { get; set; }

        public string Value { get; set; }
    }

    public class CategorizedComboDto
    {
        public required string Title { get; set; }

        public required string Value { get; set; }

        public required short CategoryId { get; set; }
        public required string Category { get; set; }
    }
}
