using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Entities.Properties;
using DevCopilot2.Domain.Entities.Users;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevCopilot2.Domain.Entities.Projects
{
    public partial class ProjectEnum : EntityId<int>
    {

        #region properties

        [Display(Name = "ProjectId")]

        public int? ProjectId { get; set; } 

        [Display(Name = "EnglishName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string EnglishName { get; set; } = null!;

        [Display(Name = "FolderName")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string FolderName { get; set; } = null!;

        [Display(Name = "AuthorId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public long AuthorId { get; set; } 

        #endregion

        #region relations

        public virtual ICollection<ProjectEnumProperty> ProjectEnumProperties { get; set; } = new List<ProjectEnumProperty>();

        public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

        [ForeignKey(nameof(ProjectId))]
        public virtual Project? Project { get; set; } = null!;

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; } = null!;

        #endregion
    }
}
