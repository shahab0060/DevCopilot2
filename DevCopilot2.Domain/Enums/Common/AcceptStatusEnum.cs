
using System;

using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Common
{

    public enum AcceptStatus
    {

        [Display(Name = "در انتظار")]
        Pending = 0,

        [Display(Name = "تایید شده")]
        Accepted = 1,

        [Display(Name = "رد شده")]
        Rejected = 2,
    }

}
