using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Roles;
using DevCopilot2.Domain.Entities.Roles;

namespace DevCopilot2.Core.Mappers.Roles
{
    public static class RoleMappers
    {
      public static RoleListDto ToDto(this Role role)
		=> new RoleListDto()
		{
			Id = role.Id,
			LatestEditDate = role.LatestEditDate,
			CreateDate = role.CreateDate,
			EditCounts = role.EditCounts,
            Title = role.Title,

         PermissionIds = role.RoleSelectedPermissions
         .Select(d => d.PermissionId)
         .ToList()
		};

      public static UpdateRoleDto ToUpdateDto(this Role role)
   => new UpdateRoleDto()
   {
         Id = role.Id,

         Title = role.Title,

         PermissionIds = role.RoleSelectedPermissions
         .Select(d => d.PermissionId)
         .ToList()
   };

      public static ComboDto ToCombo(this Role role)
			=> new ComboDto()
			{Title = role.Title,Value = role.Id.ToString()};

      public static IQueryable<RoleListDto> ToDto(this IQueryable<Role> Roles)
			=> Roles.Select(a => a.ToDto());

      public static IQueryable<ComboDto> ToCombo(this IQueryable<Role> Roles)
			=> Roles.Select(a => a.ToCombo());

      public static async Task<Role> ToModel(this CreateRoleDto create)
				=> new Role()
				{

					Title = create.Title.ToTitle()!,
				};

      public static async Task<Role> ToModel(this Role role, UpdateRoleDto update)
		{

					role.Title = update.Title.ToTitle()!;
			return role;
		}
    }
}
