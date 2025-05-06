using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Permissions;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Domain.Enums.Permissions;
namespace DevCopilot2.Core.Services.Interfaces
{
    public interface IPermissionService : IService
    {

        #region permissions

		Task<FilterPermissionsDto> FilterPermissions(FilterPermissionsDto filter);
        Task<List<PermissionListDto>> FilterUserPermissions(int userId);
        Task<List<ComboDto>> GetPermissionsAsCombo(FilterPermissionsDto filter);
		Task<PermissionListDto?> GetSinglePermissionInformation(long permissionId);
		Task<UpdatePermissionDto?> GetPermissionInformation(long permissionId);
		Task<ChangePermissionResult> CreatePermission(CreatePermissionDto create);
		Task<ChangePermissionResult> UpdatePermission(UpdatePermissionDto update);
		Task<BaseChangeEntityResult> DeletePermission(long permissionId);
		Task DeletePermission(List<long> permissionsId);

        #endregion

    }
}
