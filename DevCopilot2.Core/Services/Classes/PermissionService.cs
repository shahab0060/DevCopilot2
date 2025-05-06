
using System;

using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.IRepository;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces;
using DevCopilot2.Core.Security;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.DTOs.Permissions;
using DevCopilot2.Domain.Entities.Permissions;
using DevCopilot2.Core.Mappers.Permissions;

using DevCopilot2.Domain.Enums.Permissions;
using DevCopilot2.Domain.Entities.Roles;

namespace DevCopilot2.Core.Services.Classes
{
    public class PermissionService : IPermissionService
    {
        #region constructor

        private readonly ICrudRepository<Permission, long> _permissionRepository;
        private readonly ICrudRepository<UserSelectedRole, long> _userSelectedRoleRepository;
        public PermissionService(ICrudRepository<Permission, long> permissionRepository,
            ICrudRepository<UserSelectedRole, long> userSelectedRoleRepository)
        {
            this._permissionRepository = permissionRepository;
            this._userSelectedRoleRepository = userSelectedRoleRepository;
        }

        #endregion

        #region permissions
        IQueryable<Permission> GetPermissionsWithFilterAndSort(FilterPermissionsDto filter)
        {
            IQueryable<Permission> query = _permissionRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.Title, $"%{filter.Search}%") ||
EF.Functions.Like(q.KeyName, $"%{filter.Search}%"));


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
        public async Task<FilterPermissionsDto> FilterPermissions(FilterPermissionsDto filter)
        {
            IQueryable<Permission> query = GetPermissionsWithFilterAndSort(filter);

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

            filter.Permissions = await query.Paging(pager).ToDto().ToListAsync();

            return filter.SetPaging(pager);
        }



        public async Task<PermissionListDto?> GetSinglePermissionInformation(long permissionId)
            => await _permissionRepository
            .GetQueryable()
            .Where(a => a.Id == permissionId)

            .Select(a => a.ToDto())
            .FirstOrDefaultAsync();
        public async Task<List<ComboDto>> GetPermissionsAsCombo(FilterPermissionsDto filter)
            => await GetPermissionsWithFilterAndSort(filter)
            .Select(a => a.ToCombo())
            .ToListAsync();
        public async Task<ChangePermissionResult> CreatePermission(CreatePermissionDto create)
        {

            #region validate unique properties

            if (await _permissionRepository
                .GetQueryable()
                .AnyAsync(a => a.Title == create.Title.ToTitle()!))
                return ChangePermissionResult.TitleExists;

            if (await _permissionRepository
                .GetQueryable()
                .AnyAsync(a => a.KeyName == create.KeyName.ToTitle()!))
                return ChangePermissionResult.KeyNameExists;

            #endregion

            Permission permission = await create.ToModel();
            await _permissionRepository.Add(permission);
            await _permissionRepository.SaveChanges();

            return ChangePermissionResult.Success;
        }
        public async Task<ChangePermissionResult> UpdatePermission(UpdatePermissionDto update)
        {

            #region validate unique properties

            if (await _permissionRepository
                .GetQueryable()
                .AnyAsync(a => a.Title == update.Title.ToTitle()!
                    && a.Id != update.Id))
                return ChangePermissionResult.TitleExists;

            if (await _permissionRepository
                .GetQueryable()
                .AnyAsync(a => a.KeyName == update.KeyName.ToTitle()!
                    && a.Id != update.Id))
                return ChangePermissionResult.KeyNameExists;

            #endregion

            Permission? permission = await _permissionRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)

                .FirstOrDefaultAsync();
            if (permission is null) return ChangePermissionResult.NotFound;

            permission = await permission.ToModel(update);
            _permissionRepository.Update(permission);

            await _permissionRepository.SaveChanges();

            return ChangePermissionResult.Success;
        }
        public async Task<UpdatePermissionDto?> GetPermissionInformation(long permissionId)
        {
            Permission? permission = await _permissionRepository
                .GetQueryable()

                .FirstOrDefaultAsync(a => a.Id == permissionId);
            if (permission is null) return null;
            return permission.ToUpdateDto();
        }
        public async Task<BaseChangeEntityResult> DeletePermission(long permissionId)
        {
            Permission? permission = await _permissionRepository.GetAsTracking(permissionId);
            if (permission is null) return BaseChangeEntityResult.NotFound;

            _permissionRepository.SoftDelete(permission);

            await _permissionRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeletePermission(List<long> permissionIds)
        {
            foreach (int permissionId in permissionIds)
            {
                Permission? permission = await _permissionRepository.GetAsTracking(permissionId);
                if (permission is not null)
                    _permissionRepository.SoftDelete(permission);
            }
            await _permissionRepository.SaveChanges();
        }
        public async Task<List<PermissionListDto>> FilterUserPermissions(int userId)
          => await _userSelectedRoleRepository
          .GetQueryable()
          .Where(a => a.UserId == userId)
          .Include(a => a.Role)
          .ThenInclude(a => a.RoleSelectedPermissions)
          .ThenInclude(a => a.Permission)
          .SelectMany(a => a.Role
          .RoleSelectedPermissions
          .Select(a => a.Permission))
          .AsQueryable()
          .ToDto()
          .ToListAsync();

        #endregion

    }
}
