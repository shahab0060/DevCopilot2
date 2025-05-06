using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Roles;
using DevCopilot2.Domain.Entities.Roles;

namespace DevCopilot2.Core.Mappers.Roles
{
    public static class RoleSelectedPermissionMappers
    {
      public static RoleSelectedPermissionListDto ToDto(this RoleSelectedPermission roleSelectedPermission)
		=> new RoleSelectedPermissionListDto()
		{
			Id = roleSelectedPermission.Id,
			LatestEditDate = roleSelectedPermission.LatestEditDate,
			CreateDate = roleSelectedPermission.CreateDate,
			EditCounts = roleSelectedPermission.EditCounts,
            RoleId = roleSelectedPermission.RoleId,
                     RoleTitle = roleSelectedPermission.Role.Title,
PermissionId = roleSelectedPermission.PermissionId,
                     PermissionTitle = roleSelectedPermission.Permission.Title,

		};

      public static UpdateRoleSelectedPermissionDto ToUpdateDto(this RoleSelectedPermission roleSelectedPermission)
   => new UpdateRoleSelectedPermissionDto()
   {
         Id = roleSelectedPermission.Id,

   };

      public static ComboDto ToCombo(this RoleSelectedPermission roleSelectedPermission)
			=> new ComboDto()
			{Title = roleSelectedPermission.RoleId.ToString(),Value = roleSelectedPermission.Id.ToString()};

      public static IQueryable<RoleSelectedPermissionListDto> ToDto(this IQueryable<RoleSelectedPermission> RoleSelectedPermissions)
			=> RoleSelectedPermissions.Select(a => a.ToDto());

      public static IQueryable<ComboDto> ToCombo(this IQueryable<RoleSelectedPermission> RoleSelectedPermissions)
			=> RoleSelectedPermissions.Select(a => a.ToCombo());

      public static async Task<RoleSelectedPermission> ToModel(this CreateRoleSelectedPermissionDto create)
				=> new RoleSelectedPermission()
				{

					RoleId = create.RoleId,

					PermissionId = create.PermissionId,
				};

      public static async Task<RoleSelectedPermission> ToModel(this RoleSelectedPermission roleSelectedPermission, UpdateRoleSelectedPermissionDto update)
		{

			return roleSelectedPermission;
		}
    }
}
