
using System;
using DevCopilot2.Domain.Entities.Users;
using DevCopilot2.Domain.Entities.Permissions;

using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.IRepository;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Core.Security;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.DTOs.Roles;
using DevCopilot2.Domain.Entities.Roles;
using DevCopilot2.Core.Mappers.Roles;

namespace DevCopilot2.Core.Services.Classes
{
    public class RoleService : IRoleService
    {
        #region constructor

        private readonly ICrudRepository<UserSelectedRole, long> _userSelectedRoleRepository;

        private readonly ICrudRepository<RoleSelectedPermission, long> _roleSelectedPermissionRepository;
        private readonly ICrudRepository<Role, long> _roleRepository;
        private readonly ICrudRepository<Permission, long> _permissionRepository;
        private readonly ICrudRepository<User, long> _userRepository;
        public RoleService(ICrudRepository<UserSelectedRole, long> userSelectedRoleRepository, ICrudRepository<RoleSelectedPermission, long> roleSelectedPermissionRepository, ICrudRepository<Role, long> roleRepository, ICrudRepository<Permission, long> permissionRepository, ICrudRepository<User, long> userRepository)
        {
            this._userSelectedRoleRepository = userSelectedRoleRepository;
            this._roleSelectedPermissionRepository = roleSelectedPermissionRepository;
            this._roleRepository = roleRepository;
            this._permissionRepository = permissionRepository;
            this._userRepository = userRepository;
        }

        #endregion

        #region user selected roles
        IQueryable<UserSelectedRole> GetUserSelectedRolesWithFilterAndSort(FilterUserSelectedRolesDto filter)
        {
            IQueryable<UserSelectedRole> query = _userSelectedRoleRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (filter.UserId > 0)
                query = query.Where(q => q.UserId == filter.UserId);

if (filter.RoleId > 0)
                query = query.Where(q => q.RoleId == filter.RoleId);

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
		public async Task<FilterUserSelectedRolesDto> FilterUserSelectedRoles(FilterUserSelectedRolesDto filter)
        {
            IQueryable<UserSelectedRole> query = GetUserSelectedRolesWithFilterAndSort(filter);

            query = query

                .Include(a => a.User)
.Include(a => a.Role)

                ;

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

            filter.UserSelectedRoles = await query.Paging(pager).ToDto().ToListAsync();

            return filter.SetPaging(pager);
        }
        public async Task<UserSelectedRoleListDto?> GetSingleUserSelectedRoleInformation(long userSelectedRoleId)
            => await _userSelectedRoleRepository
            .GetQueryable()
            .Where(a => a.Id == userSelectedRoleId)

                .Include(a => a.User)
.Include(a => a.Role)

            .Select(a => a.ToDto())
            .FirstOrDefaultAsync();        
        public async Task<List<ComboDto>> GetUserSelectedRolesAsCombo(FilterUserSelectedRolesDto filter)
            => await GetUserSelectedRolesWithFilterAndSort(filter)
            .Select(a => a.ToCombo())
            .ToListAsync();
        public async Task<BaseChangeEntityResult> CreateUserSelectedRole(CreateUserSelectedRoleDto create)
        {

            #region validate relation ids

            if (!await _userRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.UserId))
                return BaseChangeEntityResult.NotFound;

            if (!await _roleRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.RoleId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            UserSelectedRole userSelectedRole = await create.ToModel();
            await _userSelectedRoleRepository.Add(userSelectedRole);
            await _userSelectedRoleRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> UpdateUserSelectedRole(UpdateUserSelectedRoleDto update)
        {

            UserSelectedRole? userSelectedRole = await _userSelectedRoleRepository
                .GetQueryable()
                .Where(a=>a.Id==update.Id)

                .FirstOrDefaultAsync();
            if (userSelectedRole is null) return BaseChangeEntityResult.NotFound;

            userSelectedRole = await userSelectedRole.ToModel(update);
            _userSelectedRoleRepository.Update(userSelectedRole);

            await _userSelectedRoleRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdateUserSelectedRoleDto?> GetUserSelectedRoleInformation(long userSelectedRoleId)
        {
            UserSelectedRole? userSelectedRole = await _userSelectedRoleRepository
                .GetQueryable()

                .FirstOrDefaultAsync(a => a.Id == userSelectedRoleId);
            if (userSelectedRole is null) return null;
            return userSelectedRole.ToUpdateDto();
        }
        public async Task<BaseChangeEntityResult> DeleteUserSelectedRole(long userSelectedRoleId)
        {
            UserSelectedRole? userSelectedRole = await _userSelectedRoleRepository.GetAsTracking(userSelectedRoleId);
            if (userSelectedRole is null) return BaseChangeEntityResult.NotFound;

            _userSelectedRoleRepository.SoftDelete(userSelectedRole);

            await _userSelectedRoleRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteUserSelectedRole(List<long> userSelectedRoleIds)
        {
            foreach (int userSelectedRoleId in userSelectedRoleIds)
            { 
                UserSelectedRole? userSelectedRole = await _userSelectedRoleRepository.GetAsTracking(userSelectedRoleId);
                if (userSelectedRole is not null)
                    _userSelectedRoleRepository.SoftDelete(userSelectedRole);
            }
            await _userSelectedRoleRepository.SaveChanges();
        }

        #endregion

        #region role selected permissions
        IQueryable<RoleSelectedPermission> GetRoleSelectedPermissionsWithFilterAndSort(FilterRoleSelectedPermissionsDto filter)
        {
            IQueryable<RoleSelectedPermission> query = _roleSelectedPermissionRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (filter.RoleId > 0)
                query = query.Where(q => q.RoleId == filter.RoleId);

if (filter.PermissionId > 0)
                query = query.Where(q => q.PermissionId == filter.PermissionId);

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
		public async Task<FilterRoleSelectedPermissionsDto> FilterRoleSelectedPermissions(FilterRoleSelectedPermissionsDto filter)
        {
            IQueryable<RoleSelectedPermission> query = GetRoleSelectedPermissionsWithFilterAndSort(filter);

            query = query

                .Include(a => a.Role)
.Include(a => a.Permission)

                ;

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

            filter.RoleSelectedPermissions = await query.Paging(pager).ToDto().ToListAsync();

            return filter.SetPaging(pager);
        }
        public async Task<RoleSelectedPermissionListDto?> GetSingleRoleSelectedPermissionInformation(long roleSelectedPermissionId)
            => await _roleSelectedPermissionRepository
            .GetQueryable()
            .Where(a => a.Id == roleSelectedPermissionId)

                .Include(a => a.Role)
.Include(a => a.Permission)

            .Select(a => a.ToDto())
            .FirstOrDefaultAsync();        
        public async Task<List<ComboDto>> GetRoleSelectedPermissionsAsCombo(FilterRoleSelectedPermissionsDto filter)
            => await GetRoleSelectedPermissionsWithFilterAndSort(filter)
            .Select(a => a.ToCombo())
            .ToListAsync();
        public async Task<BaseChangeEntityResult> CreateRoleSelectedPermission(CreateRoleSelectedPermissionDto create)
        {

            #region validate relation ids

            if (!await _roleRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.RoleId))
                return BaseChangeEntityResult.NotFound;

            if (!await _permissionRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.PermissionId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            RoleSelectedPermission roleSelectedPermission = await create.ToModel();
            await _roleSelectedPermissionRepository.Add(roleSelectedPermission);
            await _roleSelectedPermissionRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> UpdateRoleSelectedPermission(UpdateRoleSelectedPermissionDto update)
        {

            RoleSelectedPermission? roleSelectedPermission = await _roleSelectedPermissionRepository
                .GetQueryable()
                .Where(a=>a.Id==update.Id)

                .FirstOrDefaultAsync();
            if (roleSelectedPermission is null) return BaseChangeEntityResult.NotFound;

            roleSelectedPermission = await roleSelectedPermission.ToModel(update);
            _roleSelectedPermissionRepository.Update(roleSelectedPermission);

            await _roleSelectedPermissionRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdateRoleSelectedPermissionDto?> GetRoleSelectedPermissionInformation(long roleSelectedPermissionId)
        {
            RoleSelectedPermission? roleSelectedPermission = await _roleSelectedPermissionRepository
                .GetQueryable()

                .FirstOrDefaultAsync(a => a.Id == roleSelectedPermissionId);
            if (roleSelectedPermission is null) return null;
            return roleSelectedPermission.ToUpdateDto();
        }
        public async Task<BaseChangeEntityResult> DeleteRoleSelectedPermission(long roleSelectedPermissionId)
        {
            RoleSelectedPermission? roleSelectedPermission = await _roleSelectedPermissionRepository.GetAsTracking(roleSelectedPermissionId);
            if (roleSelectedPermission is null) return BaseChangeEntityResult.NotFound;

            _roleSelectedPermissionRepository.SoftDelete(roleSelectedPermission);

            await _roleSelectedPermissionRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteRoleSelectedPermission(List<long> roleSelectedPermissionIds)
        {
            foreach (int roleSelectedPermissionId in roleSelectedPermissionIds)
            { 
                RoleSelectedPermission? roleSelectedPermission = await _roleSelectedPermissionRepository.GetAsTracking(roleSelectedPermissionId);
                if (roleSelectedPermission is not null)
                    _roleSelectedPermissionRepository.SoftDelete(roleSelectedPermission);
            }
            await _roleSelectedPermissionRepository.SaveChanges();
        }

        #endregion

        #region roles
        IQueryable<Role> GetRolesWithFilterAndSort(FilterRolesDto filter)
        {
            IQueryable<Role> query = _roleRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.Title, $"%{filter.Search}%") );

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
		public async Task<FilterRolesDto> FilterRoles(FilterRolesDto filter)
        {
            IQueryable<Role> query = GetRolesWithFilterAndSort(filter);

            query = query

                .Include(a => a.RoleSelectedPermissions)
                ;

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

            filter.Roles = await query.Paging(pager).ToDto().ToListAsync();

            return filter.SetPaging(pager);
        }
        public async Task<RoleListDto?> GetSingleRoleInformation(long roleId)
            => await _roleRepository
            .GetQueryable()
            .Where(a => a.Id == roleId)

                .Include(a => a.RoleSelectedPermissions)
            .Select(a => a.ToDto())
            .FirstOrDefaultAsync();        
        public async Task<List<ComboDto>> GetRolesAsCombo(FilterRolesDto filter)
            => await GetRolesWithFilterAndSort(filter)
            .Select(a => a.ToCombo())
            .ToListAsync();
        public async Task<BaseChangeEntityResult> CreateRole(CreateRoleDto create)
        {

            #region validate unique properties

            if (await _roleRepository
                .GetQueryable()
                .AnyAsync(a => a.Title == create.Title.ToTitle()! ))
                return BaseChangeEntityResult.Exists;

            #endregion

            Role role = await create.ToModel();
            await _roleRepository.Add(role);
            await _roleRepository.SaveChanges();

            #region add relations

            #region add permissions

            if (create.PermissionIds is not null && create.PermissionIds.Any())
            {
                foreach (var permission in create.PermissionIds)
                {
                    if (!await _permissionRepository.GetQueryable()
                        .AnyAsync(a => a.Id == permission))
                        continue;
                    RoleSelectedPermission roleSelectedPermission = new RoleSelectedPermission()
                    {
                        RoleId = role.Id,
                        PermissionId = permission,
                    };
                    await _roleSelectedPermissionRepository.Add(roleSelectedPermission);
                }

            }

            #endregion

            await _roleRepository.SaveChanges();

            #endregion

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> UpdateRole(UpdateRoleDto update)
        {

            #region validate unique properties

            if (await _roleRepository
                .GetQueryable()
                .AnyAsync(a => a.Title == update.Title.ToTitle()!  
                    && a.Id!=update.Id))
                return BaseChangeEntityResult.Exists;

            #endregion

            Role? role = await _roleRepository
                .GetQueryable()
                .Where(a=>a.Id==update.Id)

                .Include(a => a.RoleSelectedPermissions)
                .FirstOrDefaultAsync();
            if (role is null) return BaseChangeEntityResult.NotFound;

            role = await role.ToModel(update);
            _roleRepository.Update(role);

            #region remove relations

            #region remove permissions

            if (role.RoleSelectedPermissions is not null && role.RoleSelectedPermissions.Any())
            {
                foreach (var permission in role.RoleSelectedPermissions)
                {
                    _roleSelectedPermissionRepository.Delete(permission);
                }

            }

            #endregion

            #endregion

            #region add relations

            #region add permissions

            if (update.PermissionIds is not null && update.PermissionIds.Any())
            {
                foreach (var permission in update.PermissionIds)
                {
                    if (!await _permissionRepository.GetQueryable()
                        .AnyAsync(a => a.Id == permission))
                        continue;
                    RoleSelectedPermission roleSelectedPermission = new RoleSelectedPermission()
                    {
                        RoleId = role.Id,
                        PermissionId = permission,
                    };
                    await _roleSelectedPermissionRepository.Add(roleSelectedPermission);
                }

            }

            #endregion

            #endregion

            await _roleRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdateRoleDto?> GetRoleInformation(long roleId)
        {
            Role? role = await _roleRepository
                .GetQueryable()

                .Include(a => a.RoleSelectedPermissions)
                .FirstOrDefaultAsync(a => a.Id == roleId);
            if (role is null) return null;
            return role.ToUpdateDto();
        }
        public async Task<BaseChangeEntityResult> DeleteRole(long roleId)
        {
            Role? role = await _roleRepository.GetAsTracking(roleId);
            if (role is null) return BaseChangeEntityResult.NotFound;

            _roleRepository.SoftDelete(role);

            await _roleRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteRole(List<long> roleIds)
        {
            foreach (int roleId in roleIds)
            { 
                Role? role = await _roleRepository.GetAsTracking(roleId);
                if (role is not null)
                    _roleRepository.SoftDelete(role);
            }
            await _roleRepository.SaveChanges();
        }

        #endregion

    }
}
