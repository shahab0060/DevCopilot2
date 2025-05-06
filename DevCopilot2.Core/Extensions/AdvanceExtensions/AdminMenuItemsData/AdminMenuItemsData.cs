using System;
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
                    Title = "Project",
                    IconName = "project",
                    VerticalIconName = "project",
                    ControllerName = "Project",
                    ActionName = "Index",
                    PluralTitle = "Projects",
                    FolderName = "ProjectsManagement",
                    UserHasPermission = userPermissions.Any(a=>a.KeyName=="ProjectsManagement"),

                    InnerItems = new List<MenuItemDto>()
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
                    Title = "ProjectArea",
                    IconName = "projectArea",
                    VerticalIconName = "projectarea",
                    ControllerName = "ProjectArea",
                    ActionName = "Index",
                    PluralTitle = "ProjectAreas",
                    FolderName = "ProjectAreasManagement",
                    UserHasPermission = userPermissions.Any(a=>a.KeyName=="ProjectAreasManagement"),

                },
                    },
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
                    Title = "SiteSetting",
                    IconName = "siteSetting",
                    VerticalIconName = "sitesetting",
                    ControllerName = "SiteSetting",
                    ActionName = "Index",
                    PluralTitle = "SiteSettings",
                    FolderName = "SiteSettingsManagement",
                    UserHasPermission = userPermissions.Any(a=>a.KeyName=="SiteSettingsManagement"),

                },

                new MenuItemDto()
                {
                    Title = "GeneralSetting",
                    IconName = "generalSetting",
                    VerticalIconName = "generalsetting",
                    ControllerName = "GeneralSetting",
                    ActionName = "Index",
                    PluralTitle = "GeneralSettings",
                    FolderName = "GeneralSettingsManagement",
                    UserHasPermission = userPermissions.Any(a=>a.KeyName=="GeneralSettingsManagement"),

                },
            };

    }
}
