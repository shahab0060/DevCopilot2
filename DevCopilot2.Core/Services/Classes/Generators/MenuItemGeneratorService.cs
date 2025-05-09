using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class MenuGeneratorService : IMenuGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public MenuGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public CreateFileResultDto Generate(GenerateEntityDto generate, List<EntityFullInformationDto> entities)
        {
            EntityFullInformationDto? firstEntity = entities.FirstOrDefault();
            if (firstEntity is null) return new CreateFileResultDto();
            GenerateFileDto generateFile = new GenerateFileDto()
            {
                FileNameWithExtension = $"AdminMenuItemsData.cs",
                Override = generate.OverrideFiles,
                Path = GetPath(firstEntity)
            };
            generateFile.Code = $@"{GetUsings(firstEntity)}

namespace {GetNameSpace(firstEntity)}
{{
    public static class AdminMenuItemsData
    {{
{GetMethodCode(entities)}
    }}
}}
";
            return _baseGeneratorService.GenerateFile(generateFile);
        }

        string GetUsings(EntityFullInformationDto entity)
        {
            List<string> usings = [.. _baseGeneratorService.GetBaseUsings()];
            usings.Add($"{entity.Project.EnglishName}.Domain.DTOs.Common");
            usings.Add($"{entity.Project.EnglishName}.Domain.DTOs.Permissions");
            return usings.GetUsings(GetNameSpace(entity));
        }

        private string GetMethodCode(List<EntityFullInformationDto> entities)
        {
            var groupedEntities = entities
                .Where(a => a.Entity.AddToMenu)
                .OrderByDescending(a => a.Relations.Count)
              .GroupBy(a => a.Entity.FolderName);
            List<MenuItemDto> menuItems = new List<MenuItemDto>();
            foreach (var group in groupedEntities)
            {
                EntityFullInformationDto firstEntity = group.First();
                MenuItemDto menuItem = firstEntity.GetMenuItem();
                if (group.Count() > 1)
                {
                    menuItem.InnerItems = new List<MenuItemDto>();
                    foreach (EntityFullInformationDto entity in group)
                    {
                        menuItem.InnerItems.Add(entity.GetMenuItem());
                    }
                }
                menuItems.Add(menuItem);
            }
            string menuItemsCode = string.Join("\n",
                menuItems
                .ConvertAll(GetSingleMenuItem));
            return $@"

        public static List<MenuItemDto> GetMenuItems(
            this List<PermissionListDto> userPermissions)
            => new List<MenuItemDto>()
            {{
                {menuItemsCode}
            }};
";
        }

        public string GetSingleMenuItem(MenuItemDto a)
        {
            string? innerItemsCode = null;
            if (a.InnerItems is not null)
                innerItemsCode = string.Join("\n",
                    a.InnerItems.ConvertAll(GetSingleMenuItem));
            return $@"
                new MenuItemDto()
                {{
                    Title = ""{a.Title}"",
                    IconName = ""{a.IconName}"",
                    VerticalIconName = ""{a.ControllerName.ToLower()}"",
                    ControllerName = ""{a.ControllerName}"",
                    ActionName = ""Index"",
                    PluralTitle = ""{a.PluralTitle}"",
                    FolderName = ""{a.FolderName}"",
                    UserHasPermission = userPermissions.Any(a=>a.KeyName==""{a.FolderName}""),
                    {(innerItemsCode == null ? "" : $@"
                    InnerItems = new List<MenuItemDto>()
                    {{
                        {innerItemsCode}
                    }},")}
                }},";
        }


        string GetNameSpace(EntityFullInformationDto entity)
          => $@"{entity.Project.EnglishName}.Core.Extensions.AdvanceExtensions.AdminMenuItemsData";

        string GetPath(EntityFullInformationDto entity)
            => $@"{entity.Project.Location}\{entity.Project.EnglishName}.Core\Extensions\AdvanceExtensions\AdminMenuItemsData";

    }
}
