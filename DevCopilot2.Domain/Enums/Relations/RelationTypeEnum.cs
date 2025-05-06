using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Relations;

namespace DevCopilot2.Domain.Enums.Relations
{
    public enum RelationTypeEnum
    {
        [Display(ResourceType = typeof(RelationTypeEnumResources), Name = nameof(RelationTypeEnumResources.RelationType))]
        RelationType = 1,
    }
}
