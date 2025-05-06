using System;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Templates;
using DevCopilot2.Domain.Attributes;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.DTOs.Paging;

namespace DevCopilot2.Domain.DTOs.Templates
{

    public class FilterTemplatesDto : BaseFilterDto
    {
        #region properties

		public List<TemplateListDto> Templates { get; set; } = new List<TemplateListDto>();

        public SortTemplateType? SortProperty { get; set; }

                [Display(Name = "ProjectId")]
        public int? ProjectId { get; set; }
        [Display(Name = "AuthorId")]
        public long? AuthorId { get; set; }

        #endregion

        #region methods

        public FilterTemplatesDto  SetTemplates(List<TemplateListDto> templates)
		{
			this.Templates = templates;
			return this;
		}

		public FilterTemplatesDto  SetPaging(BasePaging paging)
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

    public class TemplateListDto : BaseListDto<int>
    {

        [Display(Name="Title")]
        public string Title { get; set; } = null!;

        [Display(Name="ProjectId")]
        public string ProjectTitle { get; set; } = null!;

        [Display(Name="ProjectId")]
        public int? ProjectId { get; set; } 

        [Display(Name="ListViewHtml")]
        public string ListViewHtml { get; set; } = null!;

        [Display(Name="ListFirstThCode")]
        public string ListFirstThCode { get; set; } = null!;

        [Display(Name="ListOtherThCodes")]
        public string ListOtherThCodes { get; set; } = null!;

        [Display(Name="ListBoolTdHtml")]
        public string ListBoolTdHtml { get; set; } = null!;

        [Display(Name="ListTextTdHtml")]
        public string ListTextTdHtml { get; set; } = null!;

        [Display(Name="ListImageTdHtml")]
        public string ListImageTdHtml { get; set; } = null!;

        [Display(Name="ListPriceTdHtml")]
        public string ListPriceTdHtml { get; set; } = null!;

        [Display(Name="ListDefaultTdCode")]
        public string ListDefaultTdCode { get; set; } = null!;

        [Display(Name="ListViewCardHtml")]
        public string ListViewCardHtml { get; set; } = null!;

        [Display(Name="CreatePageHtml")]
        public string CreatePageHtml { get; set; } = null!;

        [Display(Name="CheckBoxInputCode")]
        public string CheckBoxInputCode { get; set; } = null!;

        [Display(Name="FileInputCode")]
        public string FileInputCode { get; set; } = null!;

        [Display(Name="TextInputHtml")]
        public string TextInputHtml { get; set; } = null!;

        [Display(Name="TextEditorInputHtml")]
        public string TextEditorInputHtml { get; set; } = null!;

        [Display(Name="IntegerInputHtml")]
        public string IntegerInputHtml { get; set; } = null!;

        [Display(Name="SelectInputHtml")]
        public string SelectInputHtml { get; set; } = null!;

        [Display(Name="AuthorId")]
        public string AuthorPhoneNumber { get; set; } = null!;

        [Display(Name="AuthorId")]
        public long AuthorId { get; set; } 

        [Display(Name="SingleImageHtml")]
        public string SingleImageHtml { get; set; } = null!;

        [Display(Name="ColorPickerInputCode")]
        public string ColorPickerInputCode { get; set; } = null!;

        [Display(Name="BreadCrumbCode")]
        public string BreadCrumbCode { get; set; } = null!;

        [Display(Name="AnchorTagCode")]
        public string AnchorTagCode { get; set; } = null!;

        [Display(Name="SubmitBtnCode")]
        public string SubmitBtnCode { get; set; } = null!;

    }

    public class BaseUpsertTemplateDto
    {
        [Display(Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string Title { get; set; } = null!;

        [Display(Name = "ProjectId")]

        public int? ProjectId { get; set; } 

        [Display(Name = "ListViewHtml")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string ListViewHtml { get; set; } = null!;

        [Display(Name = "ListFirstThCode")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string ListFirstThCode { get; set; } = null!;

        [Display(Name = "ListOtherThCodes")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string ListOtherThCodes { get; set; } = null!;

        [Display(Name = "ListBoolTdHtml")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string ListBoolTdHtml { get; set; } = null!;

        [Display(Name = "ListTextTdHtml")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string ListTextTdHtml { get; set; } = null!;

        [Display(Name = "ListImageTdHtml")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string ListImageTdHtml { get; set; } = null!;

        [Display(Name = "ListPriceTdHtml")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string ListPriceTdHtml { get; set; } = null!;

        [Display(Name = "ListDefaultTdCode")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string ListDefaultTdCode { get; set; } = null!;

        [Display(Name = "ListViewCardHtml")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string ListViewCardHtml { get; set; } = null!;

        [Display(Name = "CreatePageHtml")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string CreatePageHtml { get; set; } = null!;

        [Display(Name = "CheckBoxInputCode")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string CheckBoxInputCode { get; set; } = null!;

        [Display(Name = "FileInputCode")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string FileInputCode { get; set; } = null!;

        [Display(Name = "TextInputHtml")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string TextInputHtml { get; set; } = null!;

        [Display(Name = "TextEditorInputHtml")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string TextEditorInputHtml { get; set; } = null!;

        [Display(Name = "IntegerInputHtml")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string IntegerInputHtml { get; set; } = null!;

        [Display(Name = "SelectInputHtml")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string SelectInputHtml { get; set; } = null!;

        [Display(Name = "AuthorId")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public long AuthorId { get; set; } = 0;

        [Display(Name = "SingleImageHtml")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string SingleImageHtml { get; set; } = null!;

        [Display(Name = "ColorPickerInputCode")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string ColorPickerInputCode { get; set; } = null!;

        [Display(Name = "BreadCrumbCode")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string BreadCrumbCode { get; set; } = null!;

        [Display(Name = "AnchorTagCode")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string AnchorTagCode { get; set; } = null!;

        [Display(Name = "SubmitBtnCode")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public string SubmitBtnCode { get; set; } = null!;

    }

    public class CreateTemplateDto: BaseUpsertTemplateDto
    {

    }

    public class UpdateTemplateDto: BaseUpsertTemplateDto
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]

        public int Id { get; set; } = 0;

    }

}
