using System;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Resources.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Entities.Projects;
using System.ComponentModel.DataAnnotations.Schema;
using DevCopilot2.Domain.Entities.Users;

namespace DevCopilot2.Domain.Entities.Templates
{
    public partial class Template : EntityId<int>
    {

        #region properties

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

        public long AuthorId { get; set; } 

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

        #endregion

        #region relations

        public virtual ICollection<ProjectArea> ProjectAreas { get; set; } = new List<ProjectArea>();

        [ForeignKey(nameof(ProjectId))]
        public virtual Project? Project { get; set; } = null!;

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; } = null!;

        #endregion
    }
}
