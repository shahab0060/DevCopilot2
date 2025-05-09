using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.DTOs.Data
{
    public class ConnectionStringListDto
    {
        public ConnectionStringListDto()
        {
            this.MultipleActiveResultsSet = true;
            this.TrustServerCertificate = true;
        }

        [Display(Name = "Data Source")]
        [Required(ErrorMessage = "{0} اجباری است")]
        public string DataSource { get; set; }

        [Display(Name = "نام بانک")]
        [Required(ErrorMessage = "{0} اجباری است")]
        public string DbName { get; set; }

        [Display(Name = "نام کاربری")]
        //[Required(ErrorMessage = "{0} اجباری است")]
        public string? UserName { get; set; }

        [Display(Name = "رمز عبور")]
        //[Required(ErrorMessage = "{0} اجباری است")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool MultipleActiveResultsSet { get; set; }

        public bool TrustServerCertificate { get; set; }
    }
}
