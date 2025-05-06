using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.GeneralSettings
{
    public enum SortGeneralSettingType
    {

        [Display(Name = "DefaultSolutionName")]
        DefaultSolutionName,

        [Display(Name = "DefaultSolutionLocation")]
        DefaultSolutionLocation,

        [Display(Name = "DefaultReactJsSolutionName")]
        DefaultReactJsSolutionName,

        [Display(Name = "DefaultReactSolutionLocation")]
        DefaultReactSolutionLocation,
    }
}
