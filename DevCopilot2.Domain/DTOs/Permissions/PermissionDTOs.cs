
using System;

using DevCopilot2.Domain.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.Attributes;
namespace DevCopilot2.Domain.DTOs.Permissions
{
    public class FilterPermissionsDto : BaseFilterDto
{
        #region Properties

		public List<PermissionListDto> Permissions { get; set; }


        #endregion

        #region methods

        public FilterPermissionsDto  SetPermissions(List<PermissionListDto> Permissions)
		{
			this.Permissions = Permissions;
			return this;
		}

		public FilterPermissionsDto  SetPaging(BasePaging paging)
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
    public class PermissionListDto : BaseListDto<long>
    {

        [Display(Name="عنوان")]
        public string Title { get; set; }

        [Display(Name="نام انگلیسی")]
        public string KeyName { get; set; }

            }
    public class BaseUpsertPermissionDto
    {

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} اجباری است")]

        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]

        public string Title { get; set; }

        [Display(Name = "نام انگلیسی")]
        [Required(ErrorMessage = "{0} اجباری است")]

        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]

        public string KeyName { get; set; }

    }    
    public class CreatePermissionDto : BaseUpsertPermissionDto
    {
            }
    public class UpdatePermissionDto : BaseUpsertPermissionDto
    {
        public long Id { get; set; }

            }
}
