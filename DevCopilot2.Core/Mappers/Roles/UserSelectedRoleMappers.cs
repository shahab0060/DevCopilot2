using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Roles;
using DevCopilot2.Domain.Entities.Roles;

namespace DevCopilot2.Core.Mappers.Roles
{
    public static class UserSelectedRoleMappers
    {
      public static UserSelectedRoleListDto ToDto(this UserSelectedRole userSelectedRole)
		=> new UserSelectedRoleListDto()
		{
			Id = userSelectedRole.Id,
			LatestEditDate = userSelectedRole.LatestEditDate,
			CreateDate = userSelectedRole.CreateDate,
			EditCounts = userSelectedRole.EditCounts,
            UserId = userSelectedRole.UserId,
                     UserTitle = userSelectedRole.User.FirstName,
RoleId = userSelectedRole.RoleId,
                     RoleTitle = userSelectedRole.Role.Title,

		};

      public static UpdateUserSelectedRoleDto ToUpdateDto(this UserSelectedRole userSelectedRole)
   => new UpdateUserSelectedRoleDto()
   {
         Id = userSelectedRole.Id,

   };

      public static ComboDto ToCombo(this UserSelectedRole userSelectedRole)
			=> new ComboDto()
			{Title = userSelectedRole.UserId.ToString(),Value = userSelectedRole.Id.ToString()};

      public static IQueryable<UserSelectedRoleListDto> ToDto(this IQueryable<UserSelectedRole> UserSelectedRoles)
			=> UserSelectedRoles.Select(a => a.ToDto());

      public static IQueryable<ComboDto> ToCombo(this IQueryable<UserSelectedRole> UserSelectedRoles)
			=> UserSelectedRoles.Select(a => a.ToCombo());

      public static async Task<UserSelectedRole> ToModel(this CreateUserSelectedRoleDto create)
				=> new UserSelectedRole()
				{

					UserId = create.UserId,

					RoleId = create.RoleId,
				};

      public static async Task<UserSelectedRole> ToModel(this UserSelectedRole userSelectedRole, UpdateUserSelectedRoleDto update)
		{

			return userSelectedRole;
		}
    }
}
