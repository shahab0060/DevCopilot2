using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Entities;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;

namespace DevCopilot2.Domain.DTOs.Entities
{

    public class FilterEntitySelectedProjectAreaSelectedFiltersDto : BaseFilterDto
    {
        #region properties

        public List<EntitySelectedProjectAreaSelectedFilterListDto> EntitySelectedProjectAreaSelectedFilters { get; set; } = new List<EntitySelectedProjectAreaSelectedFilterListDto>();

        public SortEntitySelectedProjectAreaSelectedFilterType? SortProperty { get; set; }

        [Display(Name = "EntitySelectedProjectAreaId")]
        public int? EntitySelectedProjectAreaId { get; set; }
        [Display(Name = "PropertyId")]
        public int? PropertyId { get; set; }

        #endregion

        #region methods

        public FilterEntitySelectedProjectAreaSelectedFiltersDto SetEntitySelectedProjectAreaSelectedFilters(List<EntitySelectedProjectAreaSelectedFilterListDto> entitySelectedProjectAreaSelectedFilters)
        {
            this.EntitySelectedProjectAreaSelectedFilters = entitySelectedProjectAreaSelectedFilters;
            return this;
        }

        public FilterEntitySelectedProjectAreaSelectedFiltersDto SetPaging(BasePaging paging)
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

    public class EntitySelectedProjectAreaSelectedFilterListDto : BaseListDto<int>
    {

        [Display(Name = "EntitySelectedProjectAreaId")]
        public bool EntitySelectedProjectAreaHasWeb { get; set; }

        [Display(Name = "EntitySelectedProjectAreaId")]
        public int EntitySelectedProjectAreaId { get; set; }

        [Display(Name = "PropertyId")]
        public string PropertyName { get; set; } = null!;

        [Display(Name = "PropertyId")]
        public int PropertyId { get; set; }

        [Display(Name = "EntityId")]
        public int EntityId { get; set; }

        [Display(Name = "Value")]
        public string Value { get; set; } = null!;

        [Display(Name = "EntitySelectedProjectAreaName")]
        public string EntitySelectedProjectAreaName { get; set; } = null!;

    }

    public class BaseUpsertEntitySelectedProjectAreaSelectedFilterDto
    {
        [Display(Name = "EntitySelectedProjectAreaId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int EntitySelectedProjectAreaId { get; set; } = 0;

        [Display(Name = "PropertyId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int PropertyId { get; set; }

        [Display(Name = "Value")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Value { get; set; } = null!;

    }

    public class CreateEntitySelectedProjectAreaSelectedFilterDto : BaseUpsertEntitySelectedProjectAreaSelectedFilterDto
    {

    }

    public class UpdateEntitySelectedProjectAreaSelectedFilterDto : BaseUpsertEntitySelectedProjectAreaSelectedFilterDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

    }

}
