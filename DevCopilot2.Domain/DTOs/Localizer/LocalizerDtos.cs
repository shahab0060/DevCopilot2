namespace DevCopilot2.Domain.DTOs.Localizer
{
    public class ListViewLocalizersDto
    {
        public string MetaTitle { get; set; }

        public string Title { get; set; }
     
        public string AddNew { get; set; }

    }

    public class SharedLocalizerDto
    {
        public string All { get; set; } = "All";

        public string Search { get; set; } = "Search";

        public string FromDate { get; set; } = "From Date";

        public string ToDate { get; set; } = "To Date";

        public string ExportExcel { get; set; } = "Export Excel";

        public string CreateDate { get; set; } = "Create Date";

        public string LatestEditDate { get; set; } = "Latest Edit Date";

        public string EditCount { get; set; } = "Edit Count";

        public string Operation { get; set; } = "Operation";

        public string Detail { get; set; } = "Detail";

        public string Update { get; set; } = "Update";

        public string Delete { get; set; } = "Delete";

        public string NoItemsFound { get; set; } = "No Items Found";
    }
}
