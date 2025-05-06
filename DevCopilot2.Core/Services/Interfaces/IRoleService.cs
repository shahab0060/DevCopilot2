
using System;

using DevCopilot2.Domain.DTOs.Roles;

using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Domain.DTOs.Common;
namespace DevCopilot2.Core.Services.Interfaces
{
    public interface IRoleService : IService
    {

        #region user selected roles

		Task<FilterUserSelectedRolesDto> FilterUserSelectedRoles(FilterUserSelectedRolesDto filter);
        Task<List<ComboDto>> GetUserSelectedRolesAsCombo(FilterUserSelectedRolesDto filter);
		Task<UserSelectedRoleListDto?> GetSingleUserSelectedRoleInformation(long userSelectedRoleId);
		Task<UpdateUserSelectedRoleDto?> GetUserSelectedRoleInformation(long userSelectedRoleId);
		Task<BaseChangeEntityResult> CreateUserSelectedRole(CreateUserSelectedRoleDto create);
		Task<BaseChangeEntityResult> UpdateUserSelectedRole(UpdateUserSelectedRoleDto update);
		Task<BaseChangeEntityResult> DeleteUserSelectedRole(long userSelectedRoleId);
		Task DeleteUserSelectedRole(List<long> userSelectedRolesId);

        #endregion

        #region role selected permissions

		Task<FilterRoleSelectedPermissionsDto> FilterRoleSelectedPermissions(FilterRoleSelectedPermissionsDto filter);
        Task<List<ComboDto>> GetRoleSelectedPermissionsAsCombo(FilterRoleSelectedPermissionsDto filter);
		Task<RoleSelectedPermissionListDto?> GetSingleRoleSelectedPermissionInformation(long roleSelectedPermissionId);
		Task<UpdateRoleSelectedPermissionDto?> GetRoleSelectedPermissionInformation(long roleSelectedPermissionId);
		Task<BaseChangeEntityResult> CreateRoleSelectedPermission(CreateRoleSelectedPermissionDto create);
		Task<BaseChangeEntityResult> UpdateRoleSelectedPermission(UpdateRoleSelectedPermissionDto update);
		Task<BaseChangeEntityResult> DeleteRoleSelectedPermission(long roleSelectedPermissionId);
		Task DeleteRoleSelectedPermission(List<long> roleSelectedPermissionsId);

        #endregion

        #region roles

		Task<FilterRolesDto> FilterRoles(FilterRolesDto filter);
        Task<List<ComboDto>> GetRolesAsCombo(FilterRolesDto filter);
		Task<RoleListDto?> GetSingleRoleInformation(long roleId);
		Task<UpdateRoleDto?> GetRoleInformation(long roleId);
		Task<BaseChangeEntityResult> CreateRole(CreateRoleDto create);
		Task<BaseChangeEntityResult> UpdateRole(UpdateRoleDto update);
		Task<BaseChangeEntityResult> DeleteRole(long roleId);
		Task DeleteRole(List<long> rolesId);

        #endregion

    }
}
