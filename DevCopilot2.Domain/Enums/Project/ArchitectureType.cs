using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Project;

namespace DevCopilot2.Domain.Enums.Project
{
    public enum ArchitectureType
    {
        [Display(ResourceType = typeof(ArchitectureTypeResources), Name = nameof(ArchitectureTypeResources.Clean))]
        Clean = 0,
    }
}
