using DevCopilot2.Domain.DTOs.Account;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Entities.Users;

namespace DevCopilot2.Core.Mappers.Account
{
    public static class AccountMappers
    {
        public static IQueryable<UserClaimsListDto> ToClaims(this IQueryable<User> users)
    => users
    .Select(u => new UserClaimsListDto()
    {
        UserId = u.Id,
        IsSuperAdmin = u.IsSuperAdmin,
        Roles = u.UserSelectedRoles
        .Select(r => new ComboDto()
        {
            Title = r.Role.Title,
            Value = r.RoleId.ToString()
        }).ToList(),
        Permissions = u.UserSelectedRoles
        .SelectMany(r => r.Role
        .RoleSelectedPermissions.Select(p => new ComboDto()
        {
            Title = p.Permission.Title,
            Value = p.Permission.KeyName
        })).ToList()
    });

    }
}
