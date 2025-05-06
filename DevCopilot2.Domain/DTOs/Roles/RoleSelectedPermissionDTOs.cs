
using System;

using DevCopilot2.Domain.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.Attributes;
namespace DevCopilot2.Domain.DTOs.Roles
{
    public class FilterRoleSelectedPermissionsDto : BaseFilterDto
{
        #region Properties

		public List<RoleSelectedPermissionListDto> RoleSelectedPermissions { get; set; }

        public long? RoleId { get; set; }
public long? PermissionId { get; set; }

        #endregion

        #region methods

        public FilterRoleSelectedPermissionsDto  SetRoleSelectedPermissions(List<RoleSelectedPermissionListDto> RoleSelectedPermissions)
		{
			this.RoleSelectedPermissions = RoleSelectedPermissions;
			return this;
		}

		public FilterRoleSelectedPermissionsDto  SetPaging(BasePaging paging)
		{
			PageId = paging.PageId;
			AllEntitiesCount = paging.AllEntitiesCount;
			StartPage = paging.StartPage;
			EndPage = paging.EndPage;
			HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
			TakeEntity = paging.TakeEntity;
			SkipEntity = paging.SkipEntity;
			PageCount = paging.PageCount;
			return this;
		}

		#endregion
}
    public class RoleSelectedPermissionListDto : BaseListDto<long>
    {

        [Display(Name="نقش")]
        public long RoleId { get; set; }

        [Display(Name="نقش")]
        public string RoleTitle { get; set; }

        [Display(Name="دسترسی")]
        public long PermissionId { get; set; }

        [Display(Name="دسترسی")]
        public string PermissionTitle { get; set; }    }
    public class BaseUpsertRoleSelectedPermissionDto
    {
            }    
    public class CreateRoleSelectedPermissionDto : BaseUpsertRoleSelectedPermissionDto
    {

        [Display(Name = "نقش")]
        [Required(ErrorMessage = "{0} اجباری است")]

        public long RoleId { get; set; }

        [Display(Name = "دسترسی")]
        [Required(ErrorMessage = "{0} اجباری است")]

[Range(1, long.MaxValue, ErrorMessage = "لطفا {0} را وارد کنید")]

        public long PermissionId { get; set; }

    }
    public class UpdateRoleSelectedPermissionDto : BaseUpsertRoleSelectedPermissionDto
    {
        public long Id { get; set; }

            }
}
