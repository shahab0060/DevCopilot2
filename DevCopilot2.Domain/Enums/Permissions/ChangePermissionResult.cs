
using System;

using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Permissions
{

    public enum ChangePermissionResult
    {
        NotFound,
        Success,

        [Display(Name = "عنوان")]
        TitleExists,

        [Display(Name = "نام انگلیسی")]
        KeyNameExists,
    }

}
