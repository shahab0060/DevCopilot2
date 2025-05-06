using DevCopilot2.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.DTOs.Users
{

    public class LoginRegisterUserDto
    {
        [Display(Name = "شماره تلفن")]
        [PersianPhoneNumber(ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PhoneNumber { get; set; }
    }

    public class LoginUserByPhoneNumberOtpCode
    {
        [Display(Name = "شماره تلفن")]
        [PersianPhoneNumber(ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PhoneNumber { get; set; }

        [Display(Name = "کد ورود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(4, ErrorMessage = "{0} باید {1} رقم باشد")]
        [MaxLength(4, ErrorMessage = "{0} باید {1} رقم باشد")]
        public string OtpCode { get; set; }
    }

    public class LoginByPhoneNumberAndPasswordDto
    {
        [Display(Name = "شماره تلفن")]
        [PersianPhoneNumber(ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PhoneNumber { get; set; }

        [Display(Name = "رمز عبور")]
        [MinLength(8, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password, ErrorMessage = "{0} معتبر نمی باشد")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
