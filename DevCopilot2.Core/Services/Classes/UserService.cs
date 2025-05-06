
using System;
using DevCopilot2.Domain.Entities.Roles;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.IRepository;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Core.Security;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.DTOs.Users;
using DevCopilot2.Domain.Entities.Users;
using DevCopilot2.Core.Mappers.Users;
using DevCopilot2.Domain.Enums.User;
using DevCopilot2.Domain.DTOs.Permissions;
using DevCopilot2.Core.Mappers.Permissions;

namespace DevCopilot2.Core.Services.Classes
{
    public class UserService : IUserService
    {
        #region constructor

        private readonly ISmsService _smsService;
        private readonly ICrudRepository<User, long> _userRepository;
        private readonly ICrudRepository<UserSelectedRole, long> _userSelectedRoleRepository;
        private readonly ICrudRepository<Role, long> _roleRepository;
        public UserService(
           ISmsService smsService, ICrudRepository<User, long> userRepository, ICrudRepository<Role, long> roleRepository,
           ICrudRepository<UserSelectedRole, long> userSelectedRoleRepository)
        {
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
            this._userSelectedRoleRepository = userSelectedRoleRepository;
            this._smsService = smsService;
        }

        #endregion

        #region users
        IQueryable<User> GetUsersWithFilterAndSort(FilterUsersDto filter)
        {
            IQueryable<User> query = _userRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.FirstName, $"%{filter.Search}%") ||
EF.Functions.Like(q.LastName, $"%{filter.Search}%") ||
EF.Functions.Like(q.PhoneNumber, $"%{filter.Search}%"));
            if (filter.IsSuperAdmin is not null)
                query = query.Where(q => q.IsSuperAdmin == filter.IsSuperAdmin);

            if (filter.PhoneNumber is not null)
                query = query.Where(q => q.PhoneNumber == filter.PhoneNumber);

            #endregion

            #region base sort

            query = filter.BaseSortEntityType switch
            {
                BaseSortEntityType.Default => query,
                BaseSortEntityType.Newest => query.OrderByDescending(q => q.CreateDate),
                BaseSortEntityType.LatestUpdate => query.OrderByDescending(q => q.LatestEditDate),
                _ => query
            };

            query = filter.SortType switch
            {
                SortType.Ascending => query.Reverse(),
                SortType.Descending => query,
                _ => query
            };

            #endregion

            return query;
        }
        public async Task<FilterUsersDto> FilterUsers(FilterUsersDto filter)
        {
            IQueryable<User> query = GetUsersWithFilterAndSort(filter);

            query = query

                .Include(a => a.UserSelectedRoles)
                ;

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

            filter.Users = await query.Paging(pager).ToDto().ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<PermissionListDto>> FilterUserPermissions(long userId)
            => await _userSelectedRoleRepository
            .GetQueryable()
            .Where(a => a.UserId == userId)
            .Include(a => a.Role)
            .ThenInclude(a => a.RoleSelectedPermissions)
            .ThenInclude(a => a.Permission)
            .SelectMany(a => a.Role
            .RoleSelectedPermissions
            .Select(a => a.Permission.ToDto()))
            .ToListAsync();

        public async Task<UserListDto?> GetSingleUserInformation(long userId)
            => await _userRepository
            .GetQueryable()
            .Where(a => a.Id == userId)

                .Include(a => a.UserSelectedRoles)
            .Select(a => a.ToDto())
            .FirstOrDefaultAsync();

        public async Task<UserListDto?> GetSingleUserInformation(string phoneNumber)
           => await _userRepository
           .GetQueryable()
           .Where(a => a.PhoneNumber == phoneNumber)
               .Include(a => a.UserSelectedRoles)
           .Select(a => a.ToDto())
           .FirstOrDefaultAsync();

        public async Task<List<ComboDto>> GetUsersAsCombo(FilterUsersDto filter)
            => await GetUsersWithFilterAndSort(filter)
            .Select(a => a.ToCombo())
            .ToListAsync();



        public async Task<BaseChangeEntityResult> CreateUser(CreateUserDto create)
        {

            #region validate unique properties

            if (await _userRepository
                .GetQueryable()
                .AnyAsync(a => a.PhoneNumber == create.PhoneNumber))
                return BaseChangeEntityResult.Exists;

            #endregion

            User user = await create.ToModel();
            await _userRepository.Add(user);
            await _userRepository.SaveChanges();

            #region add relations

            #region add roles

            if (create.RoleIds is not null && create.RoleIds.Any())
            {
                foreach (var role in create.RoleIds)
                {
                    if (!await _roleRepository.GetQueryable()
                        .AnyAsync(a => a.Id == role))
                        continue;
                    UserSelectedRole userSelectedRole = new UserSelectedRole()
                    {
                        UserId = user.Id,
                        RoleId = role,
                    };
                    await _userSelectedRoleRepository.Add(userSelectedRole);
                }

            }

            #endregion

            await _userRepository.SaveChanges();

            #endregion

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> UpdateUser(UpdateUserDto update)
        {

            #region validate unique properties

            if (await _userRepository
                .GetQueryable()
                .AnyAsync(a => a.PhoneNumber == update.PhoneNumber
                    && a.Id != update.Id))
                return BaseChangeEntityResult.Exists;

            #endregion

            User? user = await _userRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)

                .Include(a => a.UserSelectedRoles)
                .FirstOrDefaultAsync();
            if (user is null) return BaseChangeEntityResult.NotFound;

            user = await user.ToModel(update);
            _userRepository.Update(user);

            #region remove relations

            #region remove roles

            if (user.UserSelectedRoles is not null && user.UserSelectedRoles.Any())
            {
                foreach (var role in user.UserSelectedRoles)
                {
                    _userSelectedRoleRepository.Delete(role);
                }

            }

            #endregion

            #endregion

            #region add relations

            #region add roles

            if (update.RoleIds is not null && update.RoleIds.Any())
            {
                foreach (var role in update.RoleIds)
                {
                    if (!await _roleRepository.GetQueryable()
                        .AnyAsync(a => a.Id == role))
                        continue;
                    UserSelectedRole userSelectedRole = new UserSelectedRole()
                    {
                        UserId = user.Id,
                        RoleId = role,
                    };
                    await _userSelectedRoleRepository.Add(userSelectedRole);
                }

            }

            #endregion

            #endregion

            await _userRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdateUserDto?> GetUserInformation(long userId)
        {
            User? user = await _userRepository
                .GetQueryable()

                .Include(a => a.UserSelectedRoles)
                .FirstOrDefaultAsync(a => a.Id == userId);
            if (user is null) return null;
            return user.ToUpdateDto();
        }

        public async Task<BaseChangeEntityResult> DeleteUser(long userId)
        {
            User? user = await _userRepository.GetAsTracking(userId);
            if (user is null) return BaseChangeEntityResult.NotFound;

            _userRepository.SoftDelete(user);

            await _userRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteUser(List<long> userIds)
        {
            foreach (int userId in userIds)
            {
                User? user = await _userRepository.GetAsTracking(userId);
                if (user is not null)
                    _userRepository.SoftDelete(user);
            }
            await _userRepository.SaveChanges();
        }

        #endregion

    }
}
