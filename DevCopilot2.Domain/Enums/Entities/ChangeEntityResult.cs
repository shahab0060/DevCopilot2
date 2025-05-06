using System;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.Enums.Entities
{
    public enum ChangeEntityResult
    {
        NotFound,
        Success,

        [Display(Name = "SingularName")]
        SingularNameExists,

        [Display(Name = "PluralName")]
        PluralNameExists,
    }
}
