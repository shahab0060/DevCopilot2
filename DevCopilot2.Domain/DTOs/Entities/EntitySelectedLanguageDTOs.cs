using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Entities;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;

namespace DevCopilot2.Domain.DTOs.Entities
{

    public class FilterEntitySelectedLanguagesDto : BaseFilterDto
    {
        #region properties

		public List<EntitySelectedLanguageListDto> EntitySelectedLanguages { get; set; } = new List<EntitySelectedLanguageListDto>();

        public SortEntitySelectedLanguageType? SortProperty { get; set; }

                [Display(Name = "EntityId")]
        public int? EntityId { get; set; }
        [Display(Name = "LanguageId")]
        public int? LanguageId { get; set; }

        #endregion

        #region methods

        public FilterEntitySelectedLanguagesDto  SetEntitySelectedLanguages(List<EntitySelectedLanguageListDto> entitySelectedLanguages)
		{
			this.EntitySelectedLanguages = entitySelectedLanguages;
			return this;
		}

		public FilterEntitySelectedLanguagesDto  SetPaging(BasePaging paging)
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

    public class EntitySelectedLanguageListDto : BaseListDto<int>
    {

        [Display(Name="EntityId")]
        public string EntityPluralName { get; set; } = null!;

        [Display(Name="EntityId")]
        public int EntityId { get; set; } 

        [Display(Name="LanguageId")]
        public string LanguageName { get; set; } = null!;

        [Display(Name="LanguageId")]
        public int LanguageId { get; set; } 

        [Display(Name="SingularTitle")]
        public string SingularTitle { get; set; } = null!;

        [Display(Name="PluralTitle")]
        public string PluralTitle { get; set; } = null!;

    }

    public class BaseUpsertEntitySelectedLanguageDto
    {
        [Display(Name = "EntityId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int EntityId { get; set; } = 0;

        [Display(Name = "LanguageId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int LanguageId { get; set; } 

        [Display(Name = "SingularTitle")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(250, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string SingularTitle { get; set; } = null!;

        [Display(Name = "PluralTitle")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [MaxLength(250, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]

        public string PluralTitle { get; set; } = null!;

    }

    public class CreateEntitySelectedLanguageDto: BaseUpsertEntitySelectedLanguageDto
    {

    }

    public class UpdateEntitySelectedLanguageDto: BaseUpsertEntitySelectedLanguageDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

    }

}
