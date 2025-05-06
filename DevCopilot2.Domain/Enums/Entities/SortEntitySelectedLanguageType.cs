using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Entities
{
    public enum SortEntitySelectedLanguageType
    {

        [Display(Name = "EntityPluralName")]
        EntityPluralName,

        [Display(Name = "EntityId")]
        EntityId,

        [Display(Name = "LanguageName")]
        LanguageName,

        [Display(Name = "LanguageId")]
        LanguageId,

        [Display(Name = "SingularTitle")]
        SingularTitle,

        [Display(Name = "PluralTitle")]
        PluralTitle,
    }
}
