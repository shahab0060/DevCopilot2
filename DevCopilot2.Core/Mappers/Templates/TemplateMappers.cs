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
                    ListViewHtml = create.ListViewHtml,
                    ListFirstThCode = create.ListFirstThCode,
                    ListOtherThCodes = create.ListOtherThCodes,
                    ListBoolTdHtml = create.ListBoolTdHtml,
                    ListTextTdHtml = create.ListTextTdHtml,
                    ListImageTdHtml = create.ListImageTdHtml,
                    ListPriceTdHtml = create.ListPriceTdHtml,
                    ListDefaultTdCode = create.ListDefaultTdCode,
                    ListViewCardHtml = create.ListViewCardHtml,
                    CreatePageHtml = create.CreatePageHtml,
                    CheckBoxInputCode = create.CheckBoxInputCode,
                    FileInputCode = create.FileInputCode,
                    TextInputHtml = create.TextInputHtml,
                    TextEditorInputHtml = create.TextEditorInputHtml,
                    IntegerInputHtml = create.IntegerInputHtml,
                    SelectInputHtml = create.SelectInputHtml,
                    AuthorId = create.AuthorId,
                    SingleImageHtml = create.SingleImageHtml,
                    ColorPickerInputCode = create.ColorPickerInputCode,
                    BreadCrumbCode = create.BreadCrumbCode,
                    AnchorTagCode = create.AnchorTagCode,
                    SubmitBtnCode = create.SubmitBtnCode,
				};

        #endregion

        #region to update model

        public static Template ToModel(this Template template, UpdateTemplateDto update)
        {
            template.Title = update.Title.ToTitle()!;
            template.ProjectId = update.ProjectId > 0 ? update.ProjectId: null;
            template.ListViewHtml = update.ListViewHtml;
            template.ListFirstThCode = update.ListFirstThCode;
            template.ListOtherThCodes = update.ListOtherThCodes;
            template.ListBoolTdHtml = update.ListBoolTdHtml;
            template.ListTextTdHtml = update.ListTextTdHtml;
            template.ListImageTdHtml = update.ListImageTdHtml;
            template.ListPriceTdHtml = update.ListPriceTdHtml;
            template.ListDefaultTdCode = update.ListDefaultTdCode;
            template.ListViewCardHtml = update.ListViewCardHtml;
            template.CreatePageHtml = update.CreatePageHtml;
            template.CheckBoxInputCode = update.CheckBoxInputCode;
            template.FileInputCode = update.FileInputCode;
            template.TextInputHtml = update.TextInputHtml;
            template.TextEditorInputHtml = update.TextEditorInputHtml;
            template.IntegerInputHtml = update.IntegerInputHtml;
            template.SelectInputHtml = update.SelectInputHtml;
            template.AuthorId = update.AuthorId;
            template.SingleImageHtml = update.SingleImageHtml;
            template.ColorPickerInputCode = update.ColorPickerInputCode;
            template.BreadCrumbCode = update.BreadCrumbCode;
            template.AnchorTagCode = update.AnchorTagCode;
            template.SubmitBtnCode = update.SubmitBtnCode;
            return template;
        }

        #endregion

    }
}
