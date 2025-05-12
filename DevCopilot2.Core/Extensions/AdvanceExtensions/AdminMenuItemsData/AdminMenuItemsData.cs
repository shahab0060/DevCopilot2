using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Permissions;

namespace DevCopilot2.Core.Extensions.AdvanceExtensions.AdminMenuItemsData
{
    public static class AdminMenuItemsData
    {

        public static List<MenuItemDto> GetMenuItems(
            this List<PermissionListDto> userPermissions)
            => new List<MenuItemDto>()
            {

                new MenuItemDto()
                {
                    Title = "Project",
                    IconName = "project",
                    VerticalIconName = "project",
                    ControllerName = "Project",
                    ActionName = "Index",
                    PluralTitle = "Projects",
                    FolderName = "ProjectsManagement",
                    UserHasPermission = userPermissions.Any(a=>a.KeyName=="ProjectsManagement"),
                },

                new MenuItemDto()
                {
                    Title = "Entity",
                    IconName = "entity",
                    VerticalIconName = "entity",
                    ControllerName = "Entity",
                    ActionName = "Index",
                    PluralTitle = "Entities",
                    FolderName = "EntitiesManagement",
                    UserHasPermission = userPermissions.Any(a=>a.KeyName=="EntitiesManagement"),

                },


                new MenuItemDto()
                {
                    Title = "ProjectEnum",
                    IconName = "projectEnum",
                    VerticalIconName = "projectenum",
                    ControllerName = "ProjectEnum",
                    ActionName = "Index",
                    PluralTitle = "ProjectEnums",
                    FolderName = "ProjectEnumsManagement",
                    UserHasPermission = userPermissions.Any(a=>a.KeyName=="ProjectEnumsManagement"),

                },

                new MenuItemDto()
                {
                    Title = "Language",
                    IconName = "language",
                    VerticalIconName = "language",
                    ControllerName = "Language",
                    ActionName = "Index",
                    PluralTitle = "Languages",
                    FolderName = "LanguagesManagement",
                    UserHasPermission = userPermissions.Any(a=>a.KeyName=="LanguagesManagement"),

                },

                new MenuItemDto()
                {
                    Title = "Template",
                    IconName = "template",
                    VerticalIconName = "template",
                    ControllerName = "Template",
                    ActionName = "Index",
                    PluralTitle = "Templates",
                    FolderName = "TemplatesManagement",
                    UserHasPermission = userPermissions.Any(a=>a.KeyName=="TemplatesManagement"),

                },
                new MenuItemDto()
                {
                    Title = "Import Data",
                    IconName = "project",
                    VerticalIconName = "project",
                    PluralTitle = "Import Data",
                    FolderName = "Import",
                    UserHasPermission = userPermissions.Any(a=>a.KeyName=="Import"),
                    UseSharedLocalizer = true,
                    InnerItems = new List<MenuItemDto>()
                    {
                        new MenuItemDto()
                        {
                            Title = "Import Enums",
                            IconName = "project",
                            VerticalIconName = "project",
                            PluralTitle = "Import Enums From Physical Location",
                            FolderName = "ImportProjectEnums",
                            ControllerName = "ProjectEnumImport",
                            ActionName = "Index",
                            UseSharedLocalizer = true,
                            UserHasPermission = userPermissions.Any(a=>a.KeyName=="Import") && userPermissions.Any(a=>a.KeyName=="ProjectEnumsManagement"),
                        }
                    }
                },

                new MenuItemDto()
                {
                    Title = "SiteSetting",
                    IconName = "siteSetting",
                    VerticalIconName = "sitesetting",
                    ControllerName = "SiteSetting",
                    ActionName = "Update",
                    PluralTitle = "SiteSettings",
                    FolderName = "SiteSettingsManagement",
                    UserHasPermission = userPermissions.Any(a=>a.KeyName=="SiteSettingsManagement"),
                    RouteData = new Dictionary<string, string>{{"id","1"}}
                },

                new MenuItemDto()
                {
                    Title = "GeneralSetting",
                    IconName = "generalSetting",
                    VerticalIconName = "generalsetting",
                    ControllerName = "GeneralSetting",
                    ActionName = "Update",
                    PluralTitle = "GeneralSettings",
                    FolderName = "GeneralSettingsManagement",
                    UserHasPermission = userPermissions.Any(a=>a.KeyName=="GeneralSettingsManagement"),
                    RouteData = new Dictionary<string, string>{{"id","1"}}
                },
            };
    }
}
