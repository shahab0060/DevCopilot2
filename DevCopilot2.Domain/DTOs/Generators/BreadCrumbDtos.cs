namespace DevCopilot2.Domain.DTOs.Generators
{
    public class BreadCrumbListDto
    {
        public string Title { get; set; } = null!;

        public string AspArea { get; set; } = null!;

        public string AspController { get; set; } = null!;

        public string AspAction { get; set; } = null!;

        public string RoutingProperties { get; set; } = null!;

        public string BreadCrumbHtmlCode { get; set; } = null!;
    }
}
