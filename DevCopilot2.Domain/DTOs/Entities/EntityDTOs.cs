using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Entities;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.Enums.DataTypes;
using DevCopilot2.Domain.DTOs.Properties;

namespace DevCopilot2.Domain.DTOs.Entities
{

    public class FilterEntitiesDto : BaseFilterDto
    {
        #region properties

		public List<EntityListDto> Entities { get; set; } = new List<EntityListDto>();

        public SortEntityType? SortProperty { get; set; }

                [Display(Name = "InheritedEntityId")]
        public int? InheritedEntityId { get; set; }
        [Display(Name = "AuthorId")]
        public long? AuthorId { get; set; }
        [Display(Name = "ProjectId")]
        public int? ProjectId { get; set; }
        [Display(Name = "IdType")]
        public IdTypeEnum? IdType { get; set; }

        #endregion

        #region methods

        public FilterEntitiesDto  SetEntities(List<EntityListDto> entities)
		{
			this.Entities = entities;
			return this;
		}

		public FilterEntitiesDto  SetPaging(BasePaging paging)
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


    public class BaseEntityListDto : BaseListDto<int>
    {

        [Display(Name = "SingularName")]
        public string SingularName { get; set; } = null!;

        [Display(Name = "PluralName")]
        public string PluralName { get; set; } = null!;

        [Display(Name = "FolderName")]
        public string FolderName { get; set; } = null!;

        [Display(Name = "InheritedEntityId")]
        public string InheritedEntityPluralName { get; set; } = null!;

        [Display(Name = "InheritedEntityId")]
        public int? InheritedEntityId { get; set; }

        [Display(Name = "IdType")]
        public IdTypeEnum IdType { get; set; }

        [Display(Name = "ServiceName")]
        public string ServiceName { get; set; } = null!;

        [Display(Name = "AuthorId")]
        public string AuthorPhoneNumber { get; set; } = null!;

        [Display(Name = "AuthorId")]
        public long? AuthorId { get; set; }

        [Display(Name = "ProjectId")]
        public string ProjectTitle { get; set; } = null!;

        [Display(Name = "ProjectId")]
        public int ProjectId { get; set; }

        [Display(Name = "IsExcluded")]
        public bool IsExcluded { get; set; }

        [Display(Name = "AddToMenu")]
        public bool AddToMenu { get; set; }

    }

    public class EntityListDto : BaseEntityListDto
    {
        [Display(Name="PropertiesList")]
        public List<PropertyListDto> PropertiesList { get; set; } = new List<PropertyListDto>();

        [Display(Name="EntitySelectedProjectAreasList")]
        public List<EntitySelectedProjectAreaListDto> EntitySelectedProjectAreasList { get; set; } = new List<EntitySelectedProjectAreaListDto>();

        [Display(Name="EntitySelectedLanguagesList")]
        public List<EntitySelectedLanguageListDto> EntitySelectedLanguagesList { get; set; } = new List<EntitySelectedLanguageListDto>();

    }

    public class BaseUpsertEntityDto
    {
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

        public long? AuthorId { get; set; } = 0;

        [Display(Name = "ProjectId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int ProjectId { get; set; } 

        [Display(Name = "IsExcluded")]

        public bool IsExcluded { get; set; } 

        [Display(Name = "AddToMenu")]

        public bool AddToMenu { get; set; } 

    }

    public class CreateEntityDto: BaseUpsertEntityDto
    {
        [Display(Name = "PropertiesList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<CreatePropertyDto> PropertiesList { get; set; } =new List<CreatePropertyDto>();

        [Display(Name = "EntitySelectedProjectAreasList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<CreateEntitySelectedProjectAreaDto> EntitySelectedProjectAreasList { get; set; } =new List<CreateEntitySelectedProjectAreaDto>();

        [Display(Name = "EntitySelectedLanguagesList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<CreateEntitySelectedLanguageDto> EntitySelectedLanguagesList { get; set; } =new List<CreateEntitySelectedLanguageDto>();

    }

    public class UpdateEntityDto: BaseUpsertEntityDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

        [Display(Name = "PropertiesList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<UpdatePropertyDto> PropertiesList { get; set; } =new List<UpdatePropertyDto>();

        [Display(Name = "EntitySelectedProjectAreasList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<UpdateEntitySelectedProjectAreaDto> EntitySelectedProjectAreasList { get; set; } =new List<UpdateEntitySelectedProjectAreaDto>();

        [Display(Name = "EntitySelectedLanguagesList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<UpdateEntitySelectedLanguageDto> EntitySelectedLanguagesList { get; set; } =new List<UpdateEntitySelectedLanguageDto>();

    }

}
