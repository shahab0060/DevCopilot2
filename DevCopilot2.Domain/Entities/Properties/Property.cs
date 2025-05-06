using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Entities.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using DevCopilot2.Domain.Entities.Projects;
using DevCopilot2.Domain.Entities.Users;
using DevCopilot2.Domain.Enums.DataTypes;

namespace DevCopilot2.Domain.Entities.Properties
{
    public partial class Property : EntityId<int>
    {

        #region properties

        [Display(Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Name { get; set; } = null!;

        [Display(Name = "DataType")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public DataTypeEnum DataType { get; set; } 

        [Display(Name = "MaxLength")]

        public int? MaxLength { get; set; } 

        [Display(Name = "MinLength")]

        public int? MinLength { get; set; } 

        [Display(Name = "RangeFrom")]

        public int? RangeFrom { get; set; } 

        [Display(Name = "RangeTo")]

        public int? RangeTo { get; set; } 

        [Display(Name = "IsRequired")]

        public bool IsRequired { get; set; } 

        [Display(Name = "ProjectEnumId")]

        public int? ProjectEnumId { get; set; } 

        [Display(Name = "DataAnnotationDataType")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public DataAnnotationsDataType DataAnnotationDataType { get; set; } 

        [Display(Name = "IsUnique")]

        public bool IsUnique { get; set; } 

        [Display(Name = "IsUpdatable")]

        public bool IsUpdatable { get; set; } 

        [Display(Name = "ShowInList")]

        public bool ShowInList { get; set; } 

        [Display(Name = "IsFilterContain")]

        public bool IsFilterContain { get; set; } 

        [Display(Name = "IsFilterEqual")]

        public bool IsFilterEqual { get; set; } 

        [Display(Name = "Order")]

        public int? Order { get; set; } 

        [Display(Name = "UseEditor")]

        public bool UseEditor { get; set; } 

        [Display(Name = "EntityId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int EntityId { get; set; } 

        [Display(Name = "AuthorId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public long AuthorId { get; set; } 

        [Display(Name = "ForceMapperCode")]

        public string? ForceMapperCode { get; set; } = null!;

        [Display(Name = "ExcludeFromListDto")]

        public bool ExcludeFromListDto { get; set; } 

        #endregion

        #region relations

        public virtual ICollection<EntityRelation> EntityRelations { get; set; } = new List<EntityRelation>();

        public virtual ICollection<PropertyImageResizeInformation> PropertyImageResizeInformation { get; set; } = new List<PropertyImageResizeInformation>();

        public virtual ICollection<EntitySelectedProjectAreaSelectedFilter> EntitySelectedProjectAreaSelectedFilters { get; set; } = new List<EntitySelectedProjectAreaSelectedFilter>();

        public virtual ICollection<PropertySelectedLanguage> PropertySelectedLanguages { get; set; } = new List<PropertySelectedLanguage>();

        [ForeignKey(nameof(ProjectEnumId))]
        public virtual ProjectEnum? ProjectEnum { get; set; } = null!;

        [ForeignKey(nameof(EntityId))]
        public virtual Entity Entity { get; set; } = null!;

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; } = null!;

        #endregion
    }
}
