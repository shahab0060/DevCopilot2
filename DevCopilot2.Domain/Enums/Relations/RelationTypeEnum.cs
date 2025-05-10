using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Relations;

namespace DevCopilot2.Domain.Enums.Relations
{
    public enum RelationTypeEnum
    {
        [Display(ResourceType = typeof(RelationTypeEnumResources), Name = nameof(RelationTypeEnumResources.OneToMany))]
        OneToMany = 0,
        [Display(ResourceType = typeof(RelationTypeEnumResources), Name = nameof(RelationTypeEnumResources.OneToOne))]
        OneToOne = 1,
    }
}
