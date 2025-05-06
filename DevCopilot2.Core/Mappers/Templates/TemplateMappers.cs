using System;
using DevCopilot2.Domain.Entities.Templates;
using DevCopilot2.Domain.DTOs.Templates;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;

namespace DevCopilot2.Core.Mappers.Templates
{
    public static class TemplateMappers
    {
        #region to dto

        public static IQueryable<TemplateListDto>ToDto(this IQueryable<Template> query)
                    => query.Select(template => new TemplateListDto()
                    {

                        Id = template.Id,
                        LatestEditDate = template.LatestEditDate,
                        CreateDate = template.CreateDate,
                        EditCounts = template.EditCounts,

                        Title = template.Title,
                        ProjectTitle = template.Project.Title,
                        ProjectId = template.ProjectId,
                        ListViewHtml = template.ListViewHtml,
                        ListFirstThCode = template.ListFirstThCode,
                        ListOtherThCodes = template.ListOtherThCodes,
                        ListBoolTdHtml = template.ListBoolTdHtml,
                        ListTextTdHtml = template.ListTextTdHtml,
                        ListImageTdHtml = template.ListImageTdHtml,
                        ListPriceTdHtml = template.ListPriceTdHtml,
                        ListDefaultTdCode = template.ListDefaultTdCode,
                        ListViewCardHtml = template.ListViewCardHtml,
                        CreatePageHtml = template.CreatePageHtml,
                        CheckBoxInputCode = template.CheckBoxInputCode,
                        FileInputCode = template.FileInputCode,
                        TextInputHtml = template.TextInputHtml,
                        TextEditorInputHtml = template.TextEditorInputHtml,
                        IntegerInputHtml = template.IntegerInputHtml,
                        SelectInputHtml = template.SelectInputHtml,
                        AuthorPhoneNumber = template.Author.PhoneNumber,
                        AuthorId = template.AuthorId,
                        SingleImageHtml = template.SingleImageHtml,
                        ColorPickerInputCode = template.ColorPickerInputCode,
                        BreadCrumbCode = template.BreadCrumbCode,
                        AnchorTagCode = template.AnchorTagCode,
                        SubmitBtnCode = template.SubmitBtnCode,

                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateTemplateDto>ToUpdateDto(this IQueryable<Template> query)
                    => query.Select(template => new UpdateTemplateDto()
                    {

                        Id = template.Id,

                        Title = template.Title,
                        ProjectId = template.ProjectId,
                        ListViewHtml = template.ListViewHtml,
                        ListFirstThCode = template.ListFirstThCode,
                        ListOtherThCodes = template.ListOtherThCodes,
                        ListBoolTdHtml = template.ListBoolTdHtml,
                        ListTextTdHtml = template.ListTextTdHtml,
                        ListImageTdHtml = template.ListImageTdHtml,
                        ListPriceTdHtml = template.ListPriceTdHtml,
                        ListDefaultTdCode = template.ListDefaultTdCode,
                        ListViewCardHtml = template.ListViewCardHtml,
                        CreatePageHtml = template.CreatePageHtml,
                        CheckBoxInputCode = template.CheckBoxInputCode,
                        FileInputCode = template.FileInputCode,
                        TextInputHtml = template.TextInputHtml,
                        TextEditorInputHtml = template.TextEditorInputHtml,
                        IntegerInputHtml = template.IntegerInputHtml,
                        SelectInputHtml = template.SelectInputHtml,
                        AuthorId = template.AuthorId,
                        SingleImageHtml = template.SingleImageHtml,
                        ColorPickerInputCode = template.ColorPickerInputCode,
                        BreadCrumbCode = template.BreadCrumbCode,
                        AnchorTagCode = template.AnchorTagCode,
                        SubmitBtnCode = template.SubmitBtnCode,

                    });

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<Template> query)
			    => query.Select(template => new ComboDto()
			{
            Title = template.Title,
            Value = template.Id.ToString()
            });

        #endregion

        #region to create model

        public static Template ToModel(this CreateTemplateDto create)
				=> new Template()
				{
                    Title = create.Title.ToTitle()!,
                    ProjectId = create.ProjectId > 0 ? create.ProjectId: null,
                    ListViewHtml = create.ListViewHtml.SanitizeText()!,
                    ListFirstThCode = create.ListFirstThCode.SanitizeText()!,
                    ListOtherThCodes = create.ListOtherThCodes.SanitizeText()!,
                    ListBoolTdHtml = create.ListBoolTdHtml.SanitizeText()!,
                    ListTextTdHtml = create.ListTextTdHtml.SanitizeText()!,
                    ListImageTdHtml = create.ListImageTdHtml.SanitizeText()!,
                    ListPriceTdHtml = create.ListPriceTdHtml.SanitizeText()!,
                    ListDefaultTdCode = create.ListDefaultTdCode.SanitizeText()!,
                    ListViewCardHtml = create.ListViewCardHtml.SanitizeText()!,
                    CreatePageHtml = create.CreatePageHtml.SanitizeText()!,
                    CheckBoxInputCode = create.CheckBoxInputCode.SanitizeText()!,
                    FileInputCode = create.FileInputCode.SanitizeText()!,
                    TextInputHtml = create.TextInputHtml.SanitizeText()!,
                    TextEditorInputHtml = create.TextEditorInputHtml.SanitizeText()!,
                    IntegerInputHtml = create.IntegerInputHtml.SanitizeText()!,
                    SelectInputHtml = create.SelectInputHtml.SanitizeText()!,
                    AuthorId = create.AuthorId,
                    SingleImageHtml = create.SingleImageHtml.SanitizeText()!,
                    ColorPickerInputCode = create.ColorPickerInputCode.SanitizeText()!,
                    BreadCrumbCode = create.BreadCrumbCode.SanitizeText()!,
                    AnchorTagCode = create.AnchorTagCode.SanitizeText()!,
                    SubmitBtnCode = create.SubmitBtnCode.SanitizeText()!,
				};

        #endregion

        #region to update model

        public static Template ToModel(this Template template, UpdateTemplateDto update)
        {
            template.Title = update.Title.ToTitle()!;
            template.ProjectId = update.ProjectId > 0 ? update.ProjectId: null;
            template.ListViewHtml = update.ListViewHtml.SanitizeText()!;
            template.ListFirstThCode = update.ListFirstThCode.SanitizeText()!;
            template.ListOtherThCodes = update.ListOtherThCodes.SanitizeText()!;
            template.ListBoolTdHtml = update.ListBoolTdHtml.SanitizeText()!;
            template.ListTextTdHtml = update.ListTextTdHtml.SanitizeText()!;
            template.ListImageTdHtml = update.ListImageTdHtml.SanitizeText()!;
            template.ListPriceTdHtml = update.ListPriceTdHtml.SanitizeText()!;
            template.ListDefaultTdCode = update.ListDefaultTdCode.SanitizeText()!;
            template.ListViewCardHtml = update.ListViewCardHtml.SanitizeText()!;
            template.CreatePageHtml = update.CreatePageHtml.SanitizeText()!;
            template.CheckBoxInputCode = update.CheckBoxInputCode.SanitizeText()!;
            template.FileInputCode = update.FileInputCode.SanitizeText()!;
            template.TextInputHtml = update.TextInputHtml.SanitizeText()!;
            template.TextEditorInputHtml = update.TextEditorInputHtml.SanitizeText()!;
            template.IntegerInputHtml = update.IntegerInputHtml.SanitizeText()!;
            template.SelectInputHtml = update.SelectInputHtml.SanitizeText()!;
            template.AuthorId = update.AuthorId;
            template.SingleImageHtml = update.SingleImageHtml.SanitizeText()!;
            template.ColorPickerInputCode = update.ColorPickerInputCode.SanitizeText()!;
            template.BreadCrumbCode = update.BreadCrumbCode.SanitizeText()!;
            template.AnchorTagCode = update.AnchorTagCode.SanitizeText()!;
            template.SubmitBtnCode = update.SubmitBtnCode.SanitizeText()!;
            return template;
        }

        #endregion

    }
}
