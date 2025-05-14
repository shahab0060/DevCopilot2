using DevCopilot2.Domain.Resources.DTOs.Common;
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

        [Display(Name = "DataSource")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        public string DataSource { get; set; }

        [Display(Name = "DbName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        public string DbName { get; set; }

        [Display(Name = "UserName")]
        //[Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        public string? UserName { get; set; }

        [Display(Name = "Password")]
        //[Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "MultipleActiveResultsSet")]
        public bool MultipleActiveResultsSet { get; set; }

        [Display(Name = "TrustServerCertificate")]
        public bool TrustServerCertificate { get; set; }
    }
}
