using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.Enums.DataTypes;
using DevCopilot2.Domain.Enums.Properties;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.DTOs.Properties
{

    public class FilterPropertiesDto : BaseFilterDto
    {
        #region properties

		public List<PropertyListDto> Properties { get; set; } = new List<PropertyListDto>();

        public SortPropertyType? SortProperty { get; set; }

                [Display(Name = "ProjectEnumId")]
        public int? ProjectEnumId { get; set; }
        [Display(Name = "EntityId")]
        public int? EntityId { get; set; }

        [Display(Name = "ProjectId")]
        public int? ProjectId { get; set; }

        #endregion

        #region methods

        public FilterPropertiesDto  SetProperties(List<PropertyListDto> properties)
		{
			this.Properties = properties;
			return this;
		}

		public FilterPropertiesDto  SetPaging(BasePaging paging)
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

    public class PropertyListDto : BaseListDto<int>
    {
        public string? ForceDataTypeCode { get; set; }

        public string? InitializationCode { get; set; }

        public ProjectEnumListDto? ProjectEnum { get; set; }

        [Display(Name="Name")]
        public string Name { get; set; } = null!;

        [Display(Name = "NameInDb")]
        public string NameInDb { get; set; } = null!;

        [Display(Name="DataType")]
        public DataTypeEnum DataType { get; set; } 

        [Display(Name="MaxLength")]
        public int? MaxLength { get; set; } 

        [Display(Name="MinLength")]
        public int? MinLength { get; set; } 

        [Display(Name="RangeFrom")]
        public int? RangeFrom { get; set; } 

        [Display(Name="RangeTo")]
        public int? RangeTo { get; set; } 

        [Display(Name="IsRequired")]
        public bool IsRequired { get; set; } 

        [Display(Name="ProjectEnumId")]
        public string ProjectEnumEnglishName { get; set; } = null!;

        [Display(Name="ProjectEnumId")]
        public int? ProjectEnumId { get; set; } 

        [Display(Name="DataAnnotationDataType")]
        public DataAnnotationsDataType DataAnnotationDataType { get; set; } 

        [Display(Name="IsUnique")]
        public bool IsUnique { get; set; } 

        [Display(Name="IsUpdatable")]
        public bool IsUpdatable { get; set; } 

        [Display(Name="ShowInList")]
        public bool ShowInList { get; set; } 

        [Display(Name="IsFilterContain")]
        public bool IsFilterContain { get; set; } 

        [Display(Name="IsFilterEqual")]
        public bool IsFilterEqual { get; set; } 

        [Display(Name="Order")]
        public int? Order { get; set; } 

        [Display(Name="UseEditor")]
        public bool UseEditor { get; set; } 

        [Display(Name="EntityId")]
        public string EntityPluralName { get; set; } = null!;

        [Display(Name = "EntitySingularName")]
        public string EntityTitle { get; set; } = null!;

        [Display(Name = "AspFor")]
        public string? AspFor { get; set; } = null!;

        [Display(Name="EntityId")]
        public int EntityId { get; set; } 

        [Display(Name="ForceMapperCode")]
        public string? ForceMapperCode { get; set; } = null!;

        [Display(Name="ExcludeFromListDto")]
        public bool ExcludeFromListDto { get; set; }

        [Display(Name = "ProjectId")]
        public int ProjectId { get; set; }

        public string? ClassNames { get; set; }
        public string? AdditionalAttributes { get; set; }

        [Display(Name="EntityRelationsList")]
        public List<EntityRelationListDto> EntityRelationsList { get; set; } = new List<EntityRelationListDto>();

        [Display(Name = "FirstEntityRelation")]
        public EntityRelationListDto? EntityRelation => EntityRelationsList.FirstOrDefault();

        [Display(Name="PropertyImageResizeInformationList")]
        public List<PropertyImageResizeInformationListDto> PropertyImageResizeInformationList { get; set; } = new List<PropertyImageResizeInformationListDto>();

        [Display(Name="PropertySelectedLanguagesList")]
        public List<PropertySelectedLanguageListDto> PropertySelectedLanguagesList { get; set; } = new List<PropertySelectedLanguageListDto>();

        [Display(Name = "EntitySelectedProjectAreaSelectedFilters")]
        public List<EntitySelectedProjectAreaSelectedFilterListDto> EntitySelectedProjectAreaSelectedFilters { get; set; } = new List<EntitySelectedProjectAreaSelectedFilterListDto>();

    }

    public class BaseUpsertPropertyDto
    {
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

        public bool IsUpdatable { get; set; } = true;

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

        public int EntityId { get; set; } = 0;

        [Display(Name = "ForceMapperCode")]

        public string? ForceMapperCode { get; set; } = null!;

        [Display(Name = "ExcludeFromListDto")]

        public bool ExcludeFromListDto { get; set; } 

    }

    public class CreatePropertyDto: BaseUpsertPropertyDto
    {
        [Display(Name = "EntityRelationsList")]
       // [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        //[NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<CreateEntityRelationDto> EntityRelationsList { get; set; } =new List<CreateEntityRelationDto>();

        [Display(Name = "PropertyImageResizeInformationList")]
      //  [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
       // [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<CreatePropertyImageResizeInformationDto> PropertyImageResizeInformationList { get; set; } =new List<CreatePropertyImageResizeInformationDto>();

        [Display(Name = "PropertySelectedLanguagesList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<CreatePropertySelectedLanguageDto> PropertySelectedLanguagesList { get; set; } =new List<CreatePropertySelectedLanguageDto>();

    }

    public class UpdatePropertyDto: BaseUpsertPropertyDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

        [Display(Name = "EntityRelationsList")]
       // [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        //[NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<UpdateEntityRelationDto> EntityRelationsList { get; set; } =new List<UpdateEntityRelationDto>();

        [Display(Name = "PropertyImageResizeInformationList")]
      //  [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        //[NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<UpdatePropertyImageResizeInformationDto> PropertyImageResizeInformationList { get; set; } =new List<UpdatePropertyImageResizeInformationDto>();

        [Display(Name = "PropertySelectedLanguagesList")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<UpdatePropertySelectedLanguageDto> PropertySelectedLanguagesList { get; set; } =new List<UpdatePropertySelectedLanguageDto>();

    }

}
