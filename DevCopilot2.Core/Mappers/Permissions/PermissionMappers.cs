using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Permissions;
using DevCopilot2.Domain.Entities.Permissions;

namespace DevCopilot2.Core.Mappers.Permissions
{
    public static class PermissionMappers
    {
        public static PermissionListDto ToDto(this Permission permission)
          => new PermissionListDto()
          {
              Id = permission.Id,
              LatestEditDate = permission.LatestEditDate,
              CreateDate = permission.CreateDate,
              EditCounts = permission.EditCounts,
              Title = permission.Title,

              KeyName = permission.KeyName,

          };

        public static UpdatePermissionDto ToUpdateDto(this Permission permission)
     => new UpdatePermissionDto()
     {
         Id = permission.Id,

         Title = permission.Title,

         KeyName = permission.KeyName,

     };

        public static ComboDto ToCombo(this Permission permission)
              => new ComboDto()
              { Title = permission.Title, Value = permission.Id.ToString() };

        public static IQueryable<PermissionListDto> ToDto(this IQueryable<Permission> Permissions)
            => Permissions.Select(a => a.ToDto());

        public static IQueryable<ComboDto> ToCombo(this IQueryable<Permission> Permissions)
              => Permissions.Select(a => a.ToCombo());

        public static async Task<Permission> ToModel(this CreatePermissionDto create)
                  => new Permission()
                  {

                      Title = create.Title.ToTitle()!,

                      KeyName = create.KeyName.ToTitle()!,
                  };

        public static async Task<Permission> ToModel(this Permission permission, UpdatePermissionDto update)
        {

            permission.Title = update.Title.ToTitle()!;

            permission.KeyName = update.KeyName.ToTitle()!;
            return permission;
        }
    }
}
