using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Entities.Properties;
using System.ComponentModel.DataAnnotations.Schema;
using DevCopilot2.Domain.Entities.Users;
using DevCopilot2.Domain.Entities.Projects;
using DevCopilot2.Domain.Enums.DataTypes;

namespace DevCopilot2.Domain.Entities.Entities
{
    public partial class Entity : EntityId<int>
    {

        #region properties

        [Display(Name = "SingularName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string SingularName { get; set; } = null!;

        [Display(Name = "PluralName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string PluralName { get; set; } = null!;

        [Display(Name = "FolderName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string FolderName { get; set; } = null!;

        [Display(Name = "InheritedEntityId")]

        public int? InheritedEntityId { get; set; } 

        [Display(Name = "IdType")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public IdTypeEnum IdType { get; set; } 

        [Display(Name = "ServiceName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string ServiceName { get; set; } = null!;

        [Display(Name = "AuthorId")]

        public long? AuthorId { get; set; } 

        [Display(Name = "ProjectId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int ProjectId { get; set; } 

        [Display(Name = "IsExcluded")]

        public bool IsExcluded { get; set; } 

        [Display(Name = "AddToMenu")]

        public bool AddToMenu { get; set; } 

        #endregion

        #region relations

        public virtual ICollection<Entity> Entities { get; set; } = new List<Entity>();

        public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

        [InverseProperty("SecondaryEntity")]
        public virtual ICollection<EntityRelation>? SecondaryEntityList { get; set; } = new List<EntityRelation>();

        [InverseProperty("MiddleEntity")]
        public virtual ICollection<EntityRelation>? MiddleEntityList { get; set; } = new List<EntityRelation>();

        public virtual ICollection<EntitySelectedProjectArea> EntitySelectedProjectAreas { get; set; } = new List<EntitySelectedProjectArea>();

        public virtual ICollection<EntitySelectedLanguage> EntitySelectedLanguages { get; set; } = new List<EntitySelectedLanguage>();

        [ForeignKey(nameof(InheritedEntityId))]
        public virtual Entity? InheritedEntity { get; set; } = null!;

        [ForeignKey(nameof(AuthorId))]
        public virtual User? Author { get; set; } = null!;

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; } = null!;

        #endregion
    }
}
