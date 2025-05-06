using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.Entities.GeneralSettings
{
    public partial class GeneralSetting : EntityId<int>
    {

        #region properties

        [Display(Name = "DefaultSolutionName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string DefaultSolutionName { get; set; } = null!;

        [Display(Name = "DefaultSolutionLocation")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string DefaultSolutionLocation { get; set; } = null!;

        [Display(Name = "DefaultReactJsSolutionName")]

        public string? DefaultReactJsSolutionName { get; set; } = null!;

        [Display(Name = "DefaultReactSolutionLocation")]

        public string? DefaultReactSolutionLocation { get; set; } = null!;

        #endregion

    }
}
