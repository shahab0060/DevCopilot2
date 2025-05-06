using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Properties;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;

namespace DevCopilot2.Domain.DTOs.Properties
{

    public class FilterPropertyImageResizeInformationDto : BaseFilterDto
    {
        #region properties

		public List<PropertyImageResizeInformationListDto> PropertyImageResizeInformation { get; set; } = new List<PropertyImageResizeInformationListDto>();

        public SortPropertyImageResizeInformationType? SortProperty { get; set; }

                [Display(Name = "PropertyId")]
        public int? PropertyId { get; set; }

        #endregion

        #region methods

        public FilterPropertyImageResizeInformationDto  SetPropertyImageResizeInformation(List<PropertyImageResizeInformationListDto> propertyImageResizeInformation)
		{
			this.PropertyImageResizeInformation = propertyImageResizeInformation;
			return this;
		}

		public FilterPropertyImageResizeInformationDto  SetPaging(BasePaging paging)
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

    public class PropertyImageResizeInformationListDto : BaseListDto<int>
    {

        [Display(Name="PropertyId")]
        public string PropertyName { get; set; } = null!;

        [Display(Name="PropertyId")]
        public int PropertyId { get; set; } 

        [Display(Name="Name")]
        public string Name { get; set; } = null!;

        [Display(Name="Width")]
        public int Width { get; set; } 

        [Display(Name="Height")]
        public int Height { get; set; } 

    }

    public class BaseUpsertPropertyImageResizeInformationDto
    {
        [Display(Name = "PropertyId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int PropertyId { get; set; } = 0;

        [Display(Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Name { get; set; } = null!;

        [Display(Name = "Width")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [Range(1, 10000, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RangeErrorMessage))]

        public int Width { get; set; } 

        [Display(Name = "Height")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]
        [Range(1, 10000, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RangeErrorMessage))]

        public int Height { get; set; } 

    }

    public class CreatePropertyImageResizeInformationDto: BaseUpsertPropertyImageResizeInformationDto
    {

    }

    public class UpdatePropertyImageResizeInformationDto: BaseUpsertPropertyImageResizeInformationDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

    }

}
