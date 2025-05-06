
using System;

using DevCopilot2.Domain.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.Attributes;
namespace DevCopilot2.Domain.DTOs.Roles
{
    public class FilterRolesDto : BaseFilterDto
{
        #region Properties

		public List<RoleListDto> Roles { get; set; }

        #endregion

        #region methods

        public FilterRolesDto  SetRoles(List<RoleListDto> Roles)
		{
			this.Roles = Roles;
			return this;
		}

		public FilterRolesDto  SetPaging(BasePaging paging)
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
    public class RoleListDto : BaseListDto<long>
    {

        [Display(Name="عنوان")]
        public string Title { get; set; }

        [Display(Name="دسترسی ها")]
        public List<long> PermissionIds { get; set; }

            }
    public class BaseUpsertRoleDto
    {

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} اجباری است")]

        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]

        public string Title { get; set; }

        [Display(Name = "دسترسی ها")]
        [Required(ErrorMessage = "{0} اجباری است")]

        public List<long> PermissionIds { get; set; }

    }    
    public class CreateRoleDto : BaseUpsertRoleDto
    {
            }
    public class UpdateRoleDto : BaseUpsertRoleDto
    {
        public long Id { get; set; }

            }
}
