using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCopilot2.Domain.DTOs.Account
{
    public class JwtResponseDto
    {
        public string AccessToken { get; set; }

        public long AccessTokenExpires { get; set; }

        public UserListDto User { get; set; } = new UserListDto();
    }
    public class ApiLoginDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        //[StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
        public bool IsPersistent { get; set; }
        public string NormalizedUsername => Username.Normalize().ToLower();

    }

    public class ApiSignupDto
    {
        [Required]
        [MaxLength(250)]
        public string Username { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        //[StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [PersianPhoneNumber(ErrorMessage = "{0} معتبر نمی باشد")]
        public string PhoneNumber { get; set; } = null!;
    }

    public class ApiForgetPasswordDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        //here username stands for both username and phoneNumber
        //the reason for not getting two props is in front end code we do not want to get two inputs from the user.


        //[Required]
        //public string? PhoneNumber { get; set; }
    }

    public class ApiResetPasswordDto
    {
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Otp { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
    }

    public class UserClaimsListDto
    {
        public long UserId { get; set; }

        public bool IsSuperAdmin { get; set; }

        public List<ComboDto> Roles { get; set; } = new List<ComboDto>();

        public List<ComboDto> Permissions { get; set; } = new List<ComboDto>();
    }

    public class GetUserClaimsListDto
    {
        public List<string> TargetClaims { get; set; } = new List<string>();

        public int UserId { get; set; } = 0;
    }
}
