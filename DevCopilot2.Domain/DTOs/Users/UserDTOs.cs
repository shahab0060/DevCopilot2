
using System;

using DevCopilot2.Domain.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.Attributes;
using System.Xml.Linq;
namespace DevCopilot2.Domain.DTOs.Users
{
    public class FilterUsersDto : BaseFilterDto
    {
        #region Properties

        public List<UserListDto> Users { get; set; }

        public bool? IsSuperAdmin { get; set; }
        public string? PhoneNumber { get; set; }

        #endregion

        #region methods

        public FilterUsersDto SetUsers(List<UserListDto> Users)
        {
            this.Users = Users;
            return this;
        }

        public FilterUsersDto SetPaging(BasePaging paging)
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
    public class UserListDto : BaseListDto<long>
    {

        [Display(Name = "ادمین کل هست / نیست")]
        public bool IsSuperAdmin { get; set; }

        [Display(Name = "عکس")]
        public string? ImageName { get; set; }

        [Display(Name = "نام")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string? LastName { get; set; }

        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; }

        [Display(Name = "رمز عبور")]
        public string? Password { get; set; }

        [Display(Name = "نقش ها")]
        public List<long> RoleIds { get; set; }

    }
    public class BaseUpsertUserDto
    {

        [Display(Name = "ادمین کل هست / نیست")]

        public bool IsSuperAdmin { get; set; }

        [Display(Name = "نام")]

        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]

        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]

        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]

        public string? LastName { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0} اجباری است")]

        [PersianPhoneNumber(ErrorMessage = "{0} معتبر نمی باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "رمز عبور")]

        [MinLength(8, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]

        [DataType(DataType.Password, ErrorMessage = "{0} معتبر نمی باشد")]
        public string? Password { get; set; }

    }
    public class CreateUserDto : BaseUpsertUserDto
    {

        [Display(Name = "عکس")]

        public IFormFile? Image { get; set; }


        [Display(Name = "نقش ها")]
        [Required(ErrorMessage = "{0} اجباری است")]
        public List<long> RoleIds { get; set; }

    }
    public class UpdateUserDto : BaseUpsertUserDto
    {
        public long Id { get; set; }

        [Display(Name = "عکس")]

        public IFormFile? Image { get; set; }

        [Display(Name = "عکس")]

        public string? ImageName { get; set; }


        [Display(Name = "نقش ها")]
        public List<long>? RoleIds { get; set; }

    }

}
