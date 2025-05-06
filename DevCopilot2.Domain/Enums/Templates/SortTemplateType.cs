using System;
using System.ComponentModel.DataAnnotations;
namespace DevCopilot2.Domain.Enums.Templates
{
    public enum SortTemplateType
    {

        [Display(Name = "Title")]
        Title,

        [Display(Name = "ProjectTitle")]
        ProjectTitle,

        [Display(Name = "ProjectId")]
        ProjectId,

        [Display(Name = "ListViewHtml")]
        ListViewHtml,

        [Display(Name = "ListFirstThCode")]
        ListFirstThCode,

        [Display(Name = "ListOtherThCodes")]
        ListOtherThCodes,

        [Display(Name = "ListBoolTdHtml")]
        ListBoolTdHtml,

        [Display(Name = "ListTextTdHtml")]
        ListTextTdHtml,

        [Display(Name = "ListImageTdHtml")]
        ListImageTdHtml,

        [Display(Name = "ListPriceTdHtml")]
        ListPriceTdHtml,

        [Display(Name = "ListDefaultTdCode")]
        ListDefaultTdCode,

        [Display(Name = "ListViewCardHtml")]
        ListViewCardHtml,

        [Display(Name = "CreatePageHtml")]
        CreatePageHtml,

        [Display(Name = "CheckBoxInputCode")]
        CheckBoxInputCode,

        [Display(Name = "FileInputCode")]
        FileInputCode,

        [Display(Name = "TextInputHtml")]
        TextInputHtml,

        [Display(Name = "TextEditorInputHtml")]
        TextEditorInputHtml,

        [Display(Name = "IntegerInputHtml")]
        IntegerInputHtml,

        [Display(Name = "SelectInputHtml")]
        SelectInputHtml,

        [Display(Name = "AuthorPhoneNumber")]
        AuthorPhoneNumber,

        [Display(Name = "AuthorId")]
        AuthorId,

        [Display(Name = "SingleImageHtml")]
        SingleImageHtml,

        [Display(Name = "ColorPickerInputCode")]
        ColorPickerInputCode,

        [Display(Name = "BreadCrumbCode")]
        BreadCrumbCode,

        [Display(Name = "AnchorTagCode")]
        AnchorTagCode,

        [Display(Name = "SubmitBtnCode")]
        SubmitBtnCode,
    }
}
