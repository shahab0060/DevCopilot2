using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Entities;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.DTOs.Templates;

namespace DevCopilot2.Domain.DTOs.Entities
{

    public class FilterEntitySelectedProjectAreasDto : BaseFilterDto
    {
        #region properties

		public List<EntitySelectedProjectAreaListDto> EntitySelectedProjectAreas { get; set; } = new List<EntitySelectedProjectAreaListDto>();

        public SortEntitySelectedProjectAreaType? SortProperty { get; set; }

                [Display(Name = "EntityId")]
        public int? EntityId { get; set; }
        [Display(Name = "ProjectAreaId")]
        public int? ProjectAreaId { get; set; }

        #endregion

        #region methods

        public FilterEntitySelectedProjectAreasDto  SetEntitySelectedProjectAreas(List<EntitySelectedProjectAreaListDto> entitySelectedProjectAreas)
		{
			this.EntitySelectedProjectAreas = entitySelectedProjectAreas;
			return this;
		}

		public FilterEntitySelectedProjectAreasDto  SetPaging(BasePaging paging)
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

    public class EntitySelectedProjectAreaListDto : BaseListDto<int>
    {

        [Display(Name="EntityId")]
        public string EntityPluralName { get; set; } = null!;

        [Display(Name="EntityId")]
        public int EntityId { get; set; } 

        [Display(Name="ProjectAreaId")]
        public string ProjectAreaTitle { get; set; } = null!;

        [Display(Name="ProjectAreaId")]
        public int ProjectAreaId { get; set; } 

        [Display(Name="HasIndex")]
        public bool HasIndex { get; set; } 

        [Display(Name="HasCreate")]
        public bool HasCreate { get; set; } 

        [Display(Name="HasUpdate")]
        public bool HasUpdate { get; set; } 

        [Display(Name="HasDelete")]
        public bool HasDelete { get; set; } 

        [Display(Name="HasApi")]
        public bool HasApi { get; set; } 

        [Display(Name="HasWeb")]
        public bool HasWeb { get; set; }

        [Display(Name = "ProjectId")]
        public int ProjectId { get; set; }

        [Display(Name="EntitySelectedProjectAreaSelectedFiltersList")]
        public List<EntitySelectedProjectAreaSelectedFilterListDto> EntitySelectedProjectAreaSelectedFiltersList { get; set; } = new List<EntitySelectedProjectAreaSelectedFilterListDto>();

        public TemplateListDto Template { get; set; } = new TemplateListDto();
    }

    public class BaseUpsertEntitySelectedProjectAreaDto
    {
        [Display(Name = "EntityId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int EntityId { get; set; } = 0;

        [Display(Name = "ProjectAreaId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int ProjectAreaId { get; set; } 

        [Display(Name = "HasIndex")]

        public bool HasIndex { get; set; } 

        [Display(Name = "HasCreate")]

        public bool HasCreate { get; set; } 

        [Display(Name = "HasUpdate")]

        public bool HasUpdate { get; set; } 

        [Display(Name = "HasDelete")]

        public bool HasDelete { get; set; } 

        [Display(Name = "HasApi")]

        public bool HasApi { get; set; } 

        [Display(Name = "HasWeb")]

        public bool HasWeb { get; set; } 

    }

    public class CreateEntitySelectedProjectAreaDto: BaseUpsertEntitySelectedProjectAreaDto
    {
        [Display(Name = "EntitySelectedProjectAreaSelectedFiltersList")]
     //   [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
      //  [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<CreateEntitySelectedProjectAreaSelectedFilterDto> EntitySelectedProjectAreaSelectedFiltersList { get; set; } =new List<CreateEntitySelectedProjectAreaSelectedFilterDto>();

    }

    public class UpdateEntitySelectedProjectAreaDto: BaseUpsertEntitySelectedProjectAreaDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

        [Display(Name = "EntitySelectedProjectAreaSelectedFiltersList")]
      //  [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
     //   [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public List<UpdateEntitySelectedProjectAreaSelectedFilterDto> EntitySelectedProjectAreaSelectedFiltersList { get; set; } =new List<UpdateEntitySelectedProjectAreaSelectedFilterDto>();

    }

}
