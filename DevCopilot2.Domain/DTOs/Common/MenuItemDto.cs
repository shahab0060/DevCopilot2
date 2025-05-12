namespace DevCopilot2.Domain.DTOs.Common
{
    public class MenuItemDto
    {
        public MenuItemDto()
        {
            Enabled = true;
        }
        public string ControllerName { get; set; }

        public string FolderName { get; set; }

        public string Title { get; set; }

        public bool HasCrud { get; set; } = true;

        public int Index { get; set; }
        public string PluralTitle { get; set; }

        public string ActionName { get; set; }

        public bool UserHasPermission { get; set; }

        public string IconName { get; set; }

        public string VerticalIconName { get; set; }

        public bool Enabled { get; set; }

        public bool UseSharedLocalizer { get; set; }

        public List<MenuItemDto>? InnerItems { get; set; } = null;

        public Dictionary<string, string> RouteData { get; set; }

    }

}
