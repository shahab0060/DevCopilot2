using DevCopilot2.Domain.Resources.Enums.Relations;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.Enums.Relations
{
    public enum RelationTypeEnum
    {
        [Display(ResourceType = typeof(RelationTypeEnumResources), Name = nameof(RelationTypeEnumResources.OneToMany))]
        OneToMany,
        [Display(ResourceType = typeof(RelationTypeEnumResources), Name = nameof(RelationTypeEnumResources.OneToOne))]
        OneToOne,
    }
}
