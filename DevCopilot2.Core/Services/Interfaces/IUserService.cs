using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Permissions;
using DevCopilot2.Domain.DTOs.Users;
using DevCopilot2.Domain.Enums.Common;
namespace DevCopilot2.Core.Services.Interfaces
{
    public interface IUserService : IService
    {
        #region users

		Task<FilterUsersDto> FilterUsers(FilterUsersDto filter);
        Task<List<PermissionListDto>> FilterUserPermissions(long userId);
        Task<List<ComboDto>> GetUsersAsCombo(FilterUsersDto filter);
		Task<UserListDto?> GetSingleUserInformation(long userId);
		Task<UserListDto?> GetSingleUserInformation(string phoneNumber);
        Task<UpdateUserDto?> GetUserInformation(long userId);
        Task<BaseChangeEntityResult> CreateUser(CreateUserDto create);
        Task<BaseChangeEntityResult> UpdateUser(UpdateUserDto update);
        Task<BaseChangeEntityResult> DeleteUser(long userId);
		Task DeleteUser(List<long> usersId);

        #endregion

    }
}
