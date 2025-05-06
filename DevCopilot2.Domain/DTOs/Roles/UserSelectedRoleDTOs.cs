
using System;

using DevCopilot2.Domain.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.Attributes;
namespace DevCopilot2.Domain.DTOs.Roles
{
    public class FilterUserSelectedRolesDto : BaseFilterDto
{
        #region Properties

		public List<UserSelectedRoleListDto> UserSelectedRoles { get; set; }

        public long? UserId { get; set; }
public long? RoleId { get; set; }

        #endregion

        #region methods

        public FilterUserSelectedRolesDto  SetUserSelectedRoles(List<UserSelectedRoleListDto> UserSelectedRoles)
		{
			this.UserSelectedRoles = UserSelectedRoles;
			return this;
		}

		public FilterUserSelectedRolesDto  SetPaging(BasePaging paging)
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
    public class UserSelectedRoleListDto : BaseListDto<long>
    {

        [Display(Name="کاربر")]
        public long UserId { get; set; }

        [Display(Name="کاربر")]
        public string? UserTitle { get; set; }

        [Display(Name="نقش")]
        public long RoleId { get; set; }

        [Display(Name="نقش")]
        public string RoleTitle { get; set; }    }
    public class BaseUpsertUserSelectedRoleDto
    {
            }    
    public class CreateUserSelectedRoleDto : BaseUpsertUserSelectedRoleDto
    {

        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "{0} اجباری است")]

        public long UserId { get; set; }

        [Display(Name = "نقش")]
        [Required(ErrorMessage = "{0} اجباری است")]

[Range(1, long.MaxValue, ErrorMessage = "لطفا {0} را وارد کنید")]

        public long RoleId { get; set; }

    }
    public class UpdateUserSelectedRoleDto : BaseUpsertUserSelectedRoleDto
    {
        public long Id { get; set; }

            }
}
