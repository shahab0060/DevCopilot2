using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.Enums.DataTypes;
using DevCopilot2.Domain.Enums.Entities;
using DevCopilot2.Domain.Enums.Relations;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.DTOs.Entities
{

    public class FilterEntityRelationsDto : BaseFilterDto
    {
        #region properties

		public List<EntityRelationListDto> EntityRelations { get; set; } = new List<EntityRelationListDto>();

        public SortEntityRelationType? SortProperty { get; set; }

                [Display(Name = "PrimaryPropertyId")]
        public int? PrimaryPropertyId { get; set; }
        [Display(Name = "SecondaryEntityId")]
        public int? SecondaryEntityId { get; set; }
        [Display(Name = "MiddleEntityId")]
        public int? MiddleEntityId { get; set; }

        #endregion

        #region methods

        public FilterEntityRelationsDto  SetEntityRelations(List<EntityRelationListDto> entityRelations)
		{
			this.EntityRelations = entityRelations;
			return this;
		}

		public FilterEntityRelationsDto  SetPaging(BasePaging paging)
		{
			PageId = paging.PageId;
			AllEntitiesCount = paging.AllEntitiesCount;
			StartPage = paging.StartPage;
			EndPage = paging.EndPage;
			HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
			TakeEntity = paging.TakeEntity;
			SkipEntity = paging.SkipEntity;
			PageCount = paging.PageCount;
			return this;
		}

		#endregion
    }

    public class EntityRelationListDto : BaseListDto<int>
    {

        [Display(Name = "PrimaryPropertyId")]
        public long PrimaryPropertyId { get; set; }

        [Display(Name = "PrimaryPropertyId")]
        public string PrimaryPropertyTitle { get; set; } = null!;

        [Display(Name = "PrimaryPropertyDataType")]
        public DataTypeEnum PrimaryPropertyDataType { get; set; }

        public string PrimaryPropertyEntityTitle { get; set; } = null!;
        public string PrimaryPropertyEntityPluralTitle { get; set; } = null!;
        public long PrimaryPropertyEntityId { get; set; }
        public string PrimaryPropertyEntityFolderName { get; set; } = null!;

        [Display(Name="SecondaryEntityId")]
        public string SecondaryEntityPluralName { get; set; } = null!;

        [Display(Name="SecondaryEntityId")]
        public int SecondaryEntityId { get; set; }

        [Display(Name = "MiddleEntityServiceName")]
        public string MiddleEntityServiceName { get; set; } = null!;

        [Display(Name="MiddleEntityId")]
        public string MiddleEntityPluralName { get; set; } = null!;

        [Display(Name = "MiddleEntityFolderName")]
        public string MiddleEntityFolderName { get; set; } = null!;

        [Display(Name = "MiddleEntitySingularTitle")]
        public string MiddleEntityTitle { get; set; } = null!;

        [Display(Name="MiddleEntityId")]
        public int? MiddleEntityId { get; set; }

        [Display(Name = "ProjectId")]
        public int ProjectId { get; set; }

        [Display(Name="RelationType")]
        public RelationTypeEnum RelationType { get; set; } 

        [Display(Name="InputType")]
        public InputTypeEnum InputType { get; set; } 

        [Display(Name="FillingType")]
        public FillingTypeEnum FillingType { get; set; } 

        [Display(Name="FillingCode")]
        public string? FillingCode { get; set; } = null!;

        public EntityListDto SecondaryEntity { get; set; } = new EntityListDto();

        [Display(Name = "SecondaryEntityTitleProperty")]
        public PropertyListDto SecondaryEntityTitleProperty { get; set; } = new PropertyListDto();
    }

    public class BaseUpsertEntityRelationDto
    {
        [Display(Name = "PrimaryPropertyId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int PrimaryPropertyId { get; set; } = 0;

        [Display(Name = "SecondaryEntityId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int SecondaryEntityId { get; set; } 

        [Display(Name = "MiddleEntityId")]

        public int? MiddleEntityId { get; set; } 

        [Display(Name = "RelationType")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public RelationTypeEnum RelationType { get; set; } 

        [Display(Name = "InputType")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public InputTypeEnum InputType { get; set; } 

        [Display(Name = "FillingType")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public FillingTypeEnum FillingType { get; set; } 

        [Display(Name = "FillingCode")]

        public string? FillingCode { get; set; } = null!;

    }

    public class CreateEntityRelationDto: BaseUpsertEntityRelationDto
    {

    }

    public class UpdateEntityRelationDto: BaseUpsertEntityRelationDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

    }

}
