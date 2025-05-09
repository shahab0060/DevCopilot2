using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.New.Enums.DTOs;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.DTOs.Templates;
using DevCopilot2.Domain.Enums.DataTypes;
using DevCopilot2.Domain.Enums.Relations;
using System.Text;
using System.Text.RegularExpressions;

namespace DevCopilot2.Core.Extensions.AdvanceExtensions.Generators
{
    public static class BaseViewGeneratorExtensionMethods
    {
        public static string GetIndexTitles(this string listViewHtml, EntityFullInformationDto entity)
            => listViewHtml.Replace("{title}", $@"@SharedEntitiesLocalizer.GetString(""{entity.Entity.PluralName}"")");

        public static string GetItemsCount(this string listViewHtml)
            => listViewHtml.Replace("{counts}", $@"@Model.AllEntitiesCount.ToString(""#,0"")");

        public static string GetCountText(this string listViewHtml, EntityFullInformationDto entity)
           => listViewHtml.Replace("{counts-text}", $@"@Localizer.GetString(""{entity.Entity.PluralName}"") SharedEntitiesLocalizer.GetString(""Count"")");


        public static string GetCreateBtnRouting(this string listViewHtml,
            EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            List<PropertyListDto> routingProperties = entity.GetRelationsInRouting();
            string routingPropertiesCode = string.Join("\n",
                routingProperties
                .ConvertAll(a => $@"asp-route-{a.Name.ToFirstCharLower()}=""@Model.{a.Name}"""));
            return listViewHtml.Replace("{create-btn-routing}", $@"
                            asp-area=""{area.ProjectAreaTitle}"" asp-controller=""{entity.Entity.SingularName}"" asp-action=""Create""
                            {routingPropertiesCode}");
        }

        public static string GetExcelBtnRouting(this string listViewHtml, EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
            => listViewHtml.Replace("{excel-export-btn-routing}", $@"
                            asp-area=""{area.ProjectAreaTitle}"" asp-controller=""{entity.Entity.SingularName}"" asp-action=""ExportExcel""");

        public static string GetPdfBtnRouting(this string listViewHtml, EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
            => listViewHtml.Replace("{pdf-export-btn-routing}", $@"
                            asp-area=""{area.ProjectAreaTitle}"" asp-controller=""{entity.Entity.SingularName}"" asp-action=""ExportPdf""");

        public static string GetCreateBtnText(this string listViewHtml, EntityFullInformationDto entity)
            => listViewHtml.Replace("{create-btn-text}", $@"@SharedLocalizer.GetString(""Create New"") @SharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"")");

        public static string GetFilterFormRouting(this string listViewHtml, EntityFullInformationDto entity,
            EntitySelectedProjectAreaListDto area)
        => listViewHtml.Replace("{filter-form-routing}", $@"
                            asp-area=""{area.ProjectAreaTitle}"" asp-controller=""{entity.Entity.SingularName}"" asp-action=""Index""");
        public static string GetFilterFormHiddenInputs(this string listViewHtml,
            EntityFullInformationDto entity)
        {
            return listViewHtml.Replace("{filter-form-hidden-inputs}", $@"
                <input type=""hidden"" asp-for=""@Model.PageId"" name=""PageId"" id=""PageId"" />");
        }

        public static string GetIndexCardHtml(this string listViewHtml, EntityFullInformationDto entity, TemplateListDto template)
        {
            return listViewHtml.Replace("{card-html}",
                $@"
                   @if (Model.{entity.Entity.PluralName}.Any())
                   {{
                    {template.ListViewCardHtml}
                   }}

                   else
                   {{
                    <div class=""alert alert-info"">@SharedLocalizer.GetString(""No Items Found!"")</div>
                   }}");
        }

        public static string GetIndexPaging(this string html, EntitySelectedProjectAreaListDto area)
        => html.Replace("{paging}", $@"<partial name=""_{area.ProjectAreaTitle}PagingPartial"" model=""@Model.GetCurrentPaging()"" />");


        private static string GetIndexPageRoutingTags(this EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            List<PropertyListDto> routingProperties = entity.GetRelationsInRouting();

            string routingCode = $@"asp-area=""{area.ProjectAreaTitle}""
                                  asp-controller=""{entity.Entity.SingularName}""
                                  asp-action=""Index""";
            PropertyListDto? firstRoutingProperty = routingProperties.FirstOrDefault();
            if (firstRoutingProperty != null)
            {

                routingCode += $@" 
                                asp-route-{firstRoutingProperty.Name.ToFirstCharLower()}Id=""@Model.{firstRoutingProperty.Name}""";

            }
            return routingCode;
        }

        private static string GetIndexPagePropertiesRoutingTags(this EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            List<PropertyListDto> routingProperties = entity.GetRelationsInRouting();
            PropertyListDto? firstRoutingProperty = routingProperties.FirstOrDefault();
            if (firstRoutingProperty != null)
                return $@" 
                                asp-route-{firstRoutingProperty.Name.ToFirstCharLower()}=""@Model.{firstRoutingProperty.Name}""";
            return "";
        }


        public static string SetBreadCrumbs(this string html, EntityFullInformationDto entity,
            EntitySelectedProjectAreaListDto area, TemplateListDto template, OperationTypeEnums operationType)
        {
            return html.Replace("{breadcrumbs}", entity.GetBreadCrumbs(area, template, operationType));
        }

        static string GetBreadCrumbs(this EntityFullInformationDto entity,
        EntitySelectedProjectAreaListDto area, TemplateListDto template, OperationTypeEnums operationType)
        {
            List<BreadCrumbListDto> breadCrumbs =
            [
                new BreadCrumbListDto()
                {
                    AspArea = area.ProjectAreaTitle,
                    Title = $@"@SharedLocalizer.GetString(""{area.ProjectAreaTitle}"")",
                    AspController = "Home",
                    AspAction = "Index",
                    BreadCrumbHtmlCode = template.BreadCrumbCode,
                },
            ];
            if (operationType != OperationTypeEnums.List)
                breadCrumbs.Add(new BreadCrumbListDto()
                {
                    AspArea = area.ProjectAreaTitle,
                    Title = $@"@SharedEntitiesLocalizer.GetString(""{entity.Entity.PluralName}"")",
                    AspController = entity.Entity.SingularName,
                    AspAction = "Index",
                    BreadCrumbHtmlCode = template.BreadCrumbCode,
                    RoutingProperties = entity.GetIndexPagePropertiesRoutingTags(area)
                });

            return breadCrumbs.GetBreadCrumbs();
        }

        static string GetBreadCrumbs(this List<BreadCrumbListDto> breadCrumbs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var breadCrumb in breadCrumbs)
            {
                sb.AppendLine(breadCrumb.GetSingleBreadCrumbCode());
            }
            return sb.ToString();
        }

        //private static string GetSingleBreadCrumbCode(this PropertyListDto property, EntitySelectedProjectAreaListDto area,
        //    string breadCrumbCode, string additionalTags = "")
        //    => breadCrumbCode
        //    .Replace("{href}", $@"asp-area=""{area.ProjectAreaTitle}""
        //                          asp-controller=""{property.EntityTitle}""
        //                          asp-action=""Index"" 
        //                          {additionalTags}")
        //    .Replace("{title}", $@"@SharedEntitiesLocalizer.GetString(""{property.EntityTitle}"")");

        //private static string GetSingleBreadCrumbCode(this EntityFullInformationDto entity, string routingTagsCode,
        //    string breadCrumbCode)
        //    => breadCrumbCode
        //    .Replace("{href}", $@"{routingTagsCode}")
        //    .Replace("{title}", $@"@SharedEntitiesLocalizer.GetString(""{entity.Entity.PluralName}"")");

        private static string GetSingleBreadCrumbCode(this BreadCrumbListDto breadCrumb)
            => breadCrumb.BreadCrumbHtmlCode
            .Replace("{href}", $@"asp-area=""{breadCrumb.AspArea}""
                                  asp-controller=""{breadCrumb.AspController}""
                                  asp-action=""{breadCrumb.AspAction}"" 
                                  {breadCrumb.RoutingProperties}")
            .Replace("{title}", $@"{breadCrumb.Title}");

        public static List<PropertyListDto> AddExtraInofrmationToTable(this List<PropertyListDto> properties)
        {
            if (properties.Count() < 5)
            {
                properties.AddRange(AddExtraInofrmationToTable());
            }
            return properties;
        }
        static List<PropertyListDto> AddExtraInofrmationToTable()
        {
            List<PropertyListDto> properties =
            [
                new PropertyListDto()
                {
                    Name = "CreateDate",
                    DataAnnotationDataType = DataAnnotationsDataType.PersianDate,
                    DataType = DataTypeEnum.DateTime,
                    Order = 5,
                },
                new PropertyListDto()
                {
                    Name = "LatestEditDate",
                    DataAnnotationDataType = DataAnnotationsDataType.PersianDate,
                    DataType = DataTypeEnum.DateTime,
                    Order = 6,
                },
                new PropertyListDto()
                {
                    Name = "EditCounts",
                    DataAnnotationDataType = DataAnnotationsDataType.Others,
                    DataType = DataTypeEnum.Int,
                    Order = 7,
                },
            ];
            return properties;
        }

        public static string GetTableHeadersHtml(this string listViewHtml, EntityFullInformationDto entity,
        TemplateListDto template)
        {
            List<PropertyListDto> propertiesInList = entity.GeIndexViewProperties();
            if (!propertiesInList.Any()) return listViewHtml;
            int propertiesCount = propertiesInList.Count();
            //propertiesInList = propertiesInList.AddExtraInofrmationToTable();
            var firstPropertyListDto = propertiesInList.First();
            var headersCode = string.Join("\n",
                propertiesInList.ConvertAll(a =>
                $@"{(a == firstPropertyListDto ? template.ListFirstThCode.GetSingleTh(a.NameInDb ?? a.Name, false) :
                template.ListOtherThCodes.GetSingleTh(a.NameInDb ?? a.Name, false))}"));
            propertiesInList = propertiesInList.AddExtraInofrmationToTable();
            if (propertiesInList.Count() != propertiesCount)
            {
                var extraHeadersCode = string.Join("\n",
                propertiesInList.Skip(propertiesCount)
                .ToList()
                .ConvertAll(a =>
                $@"{(a == firstPropertyListDto ? template.ListFirstThCode.GetSingleTh(a.NameInDb ?? a.Name, true) :
                template.ListOtherThCodes.GetSingleTh(a.NameInDb ?? a.Name, true))}"));
                headersCode += extraHeadersCode;
            }

            string operationThHtmlCode = template.ListOtherThCodes;
            //custom th code
            //            operationThHtmlCode = @"
            //<th class=""sort text-end align-middle pe-0 ps-4"" scope=""col"">{title}</th>";
            return listViewHtml.Replace("{table-headers}",
                $@"
                    <tr>
                    {headersCode}
                    {operationThHtmlCode.GetSingleTh("Operation", true)}
                    </tr>");
        }

        public static string GetTableBodyRows(this string listViewHtml, EntityFullInformationDto entity,
            TemplateListDto template, EntitySelectedProjectAreaListDto area)
        {
            List<PropertyListDto> propertiesInList = entity.GeIndexViewProperties();
            if (!propertiesInList.Any()) return listViewHtml;
            propertiesInList = propertiesInList.AddExtraInofrmationToTable();
            string tdsCode = string.Join("\n",
                propertiesInList
                .ConvertAll(a => GetSingleTd(a, entity, template)));
            return listViewHtml.Replace("{table-rows}", $@"

                            @foreach (
            {entity.Entity.SingularName}ListDto {entity.Entity.SingularName.ToFirstCharLower()} in Model.{entity.Entity.PluralName})
                            {{

                                <tr>
                            
                                {tdsCode}
                                
                                {entity.GetAdditionalTdsCode(area)}

                                </tr>
                            }}
            ");
        }

        private static string GetAdditionalTdsCode(this EntityFullInformationDto entity,
            EntitySelectedProjectAreaListDto area)
        {
            return $@"
                                    <td>
                                        {(area.HasUpdate ? $@"<a asp-area=""{area.ProjectAreaTitle}"" asp-controller=""{entity.Entity.SingularName}"" asp-action=""Update""
                                           asp-route-id=""@{entity.Entity.SingularName.ToFirstCharLower()}.Id"" class=""btn btn-sm btn-info me-1 mb-1 mb-md-0 update-btn"">@SharedLocalizer.GetString(""Update"")</a>" : "")}
                                         <a asp-area=""{area.ProjectAreaTitle}"" asp-controller=""{entity.Entity.SingularName}"" asp-action=""Detail""
                                           asp-route-id=""@{entity.Entity.SingularName.ToFirstCharLower()}.Id"" class=""btn btn-sm btn-blue detail-btn me-1 mb-1 mb-md-0"">@SharedLocalizer.GetString(""Detail"")</a>
                                        {(area.HasDelete ? $@"<a asp-area=""{area.ProjectAreaTitle}"" asp-controller=""{entity.Entity.SingularName}"" asp-action=""Delete""
                                           asp-route-id=""@{entity.Entity.SingularName.ToFirstCharLower()}.Id"" class=""btn btn-sm btn-danger me-1 mb-1 mb-md-0 delete-btn"">@SharedLocalizer.GetString(""Delete"")</a>" : "")}
                                        {entity.GetEntityRelationRoutingTdsCode(area)}
                                        </td>";
        }

        private static string GetEntityRelationRoutingTdsCode(this EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            string className = @"btn btn-sm btn-info me-1 mb-1 mb-md-0";
            // className = @"dropdown-item";
            return string.Join("\n",
                entity.Relations
                .ConvertAll(a => $@"<a asp-area=""{area.ProjectAreaTitle}"" asp-controller=""{a.PrimaryPropertyEntityTitle}"" asp-action=""Index""
                                           asp-route-{a.PrimaryPropertyTitle.ToFirstCharLower()}=""@{entity.Entity.SingularName.ToFirstCharLower()}.Id"" class=""{className} relation-btn"">@SharedEntitiesLocalizer.GetString(""{a.PrimaryPropertyEntityPluralTitle}"")</a>"));
        }


        private static string GetSingleTd(this PropertyListDto property, EntityFullInformationDto entity, TemplateListDto template)
        {
            string tdHtml = string.Empty;
            tdHtml = property.DataType switch
            {
                DataTypeEnum.String =>
                (property.DataAnnotationDataType == DataAnnotationsDataType.Image) ?
                template.ListImageTdHtml : template.ListTextTdHtml,
                DataTypeEnum.Bool => template.ListBoolTdHtml,
                DataTypeEnum.Int => (property.DataAnnotationDataType == DataAnnotationsDataType.Price) ?
                template.ListPriceTdHtml : template.ListTextTdHtml,
                _ => template.ListDefaultTdCode
            };

            string instanceName = $"@{entity.Entity.SingularName.ToFirstCharLower()}";
            return property.GetPropertyText(entity, tdHtml, instanceName);
        }

        public static string GetPropertyText(this PropertyListDto property, EntityFullInformationDto entity, string text
            , string instanceName)
        {
            if (property.DataAnnotationDataType == DataAnnotationsDataType.Image)
                return property.GetSingleImageCode(text, instanceName);
            if (property.DataType == DataTypeEnum.Bool)
                return text.Replace("{class}",
                    $@"({instanceName}.{property.Name}?""text-success"":""text-danger"")")
                    .Replace("{text}", $@"{instanceName}.{property.Name}.ConvertBoolToText()");
            return text.Replace("{text}",
                $@"{instanceName}.{property.Name}{property.GetPropertyShowingExtensionMethod()}");
        }

        public static string GetPropertyShowingExtensionMethod(this PropertyListDto property)
        {
            return property.DataType switch
            {
                DataTypeEnum.Int => (property.DataAnnotationDataType == DataAnnotationsDataType.Price) ?
                ".ToAmount()" :
                property.IsRequired ? @".ToString(""#,0"")" : ".ToString()",
                DataTypeEnum.Bool => $".ConvertBoolToText()",
                DataTypeEnum.Guid => $".ToString()",
                DataTypeEnum.Enum => $@".GetEnumName()",
                DataTypeEnum.Long => ".ToString()",
                DataTypeEnum.String => (property.IsRequired ? "" : ".ToString()"),
                DataTypeEnum.DateTime => (property.DataAnnotationDataType == DataAnnotationsDataType.PersianDate) ?
                ".ToShamsi()" : "",
                _ => "",
            };
        }

        private static string GetSingleTh(this string thCode, string title, bool isSharedLocalizer)
            => thCode.Replace("{title}", $@"@{(isSharedLocalizer ? "SharedLocalizer" : "Localizer")}.GetString(""{title}"")");


        public static string GetBaseFiltersSelectCode(this string listViewHtml, TemplateListDto template, EntityFullInformationDto entity)
        {
            string templateHtml = template.SelectInputHtml;
            templateHtml = @"
                        <div class=""ml-input"">
                            <select class=""btn btn-sm btn-phoenix-secondary px-7 flex-shrink-0"" {asp-for} {select-name}
        {asp-items}>
                                {select-options}
                            </select>
                        </div>";
            string fromDateHtml = $@"
<div class=""ml-input"">
    <input class=""form-control2 search-input search persian-date-picker"" 
           type=""text"" asp-for=""@Model.FromDate""  placeholder=""@SharedLocalizer.GetString(""From Date"")"" aria-label=""Search"" name=""FromDate"" 
           id=""FromDate"" dir=""ltr"">
</div>";
            string toDateHtml = $@"
<div class=""ml-input"">
    <input class=""form-control2 search-input search persian-date-picker"" 
           type=""text"" asp-for=""@Model.ToDate"" placeholder=""@SharedLocalizer.GetString(""To Date"")"" aria-label=""Search"" name=""ToDate"" 
           id=""ToDate"" dir=""ltr"">
</div>";
            string baseSortName = "BaseSortEntityType";
            string sortTypeName = "SortType";
            string baseSortSelect = GetSingleEnumSelectCode(templateHtml,
                baseSortName, baseSortName);
            string sortTypeSelect = GetSingleEnumSelectCode(templateHtml,
                sortTypeName, sortTypeName);
            return listViewHtml.Replace("{base-select-filters}",
                $@"
            {fromDateHtml}
            {toDateHtml}
            {baseSortSelect}
            {sortTypeSelect}
            ");
        }

        private static string GetSelectAspMapping(this string selectHtml, string name)
            => selectHtml
                .Replace("{asp-for}", $@"asp-for=""@Model.{name}""")
            .Replace("{asp-for}", $@"asp-for=""@Model.{name}""")
            .Replace("{select-name}", $@"name=""{name}""");

        private static string GetSelectEnumOptions(this string selectHtml, string enumName)
            => selectHtml.Replace("{select-options}",
            $@"
                            @foreach ({enumName} {enumName.ToFirstCharLower()} in ({enumName}[])Enum.GetValues(typeof({enumName})))
                            {{
                                <option value=""@{enumName.ToFirstCharLower()}"">@{enumName.ToFirstCharLower()}.GetEnumName()</option>
                            }}");

        public static string GetCreateTitles(this string createViewHtml, EntityFullInformationDto entity)
        => createViewHtml.Replace("{title}", $@"@SharedLocalizer.GetString(""Create New"") @SharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"")");

        public static string GetCreateBtnTitles(this string createViewHtml, EntityFullInformationDto entity)
        => createViewHtml.Replace("{btn-title}", $@"SharedLocalizer.GetString(""Create"")");

        public static string GetCreatePageViewDatas(this EntityFullInformationDto entity)
        {
            List<PropertyListDto> relationsEntities = entity
                .GetAllEntities()
                .SelectMany(a => a.GetSelectProperties())
                 .Distinct()
                .DistinctBy(a => a.EntityRelation!.SecondaryEntityId)
                .ToList();
            return string.Join("\n",
                relationsEntities
                .ConvertAll(GetSingleViewData));
        }

        public static string GetCreatePageFieldInRelationDeclrations(this EntityFullInformationDto entity)
        => string.Join("\n",
                entity
                .GetAllEntities()
                .SelectMany(a => a.FieldInRelationEntities)
                .ToList()
                .ConvertAll(GetCreateEntityViewDeclration));

        public static string GetUpdatePageFieldInRelationDeclrations(this EntityFullInformationDto entity)
=> string.Join("\n",
        entity
        .GetAllEntities()
        .SelectMany(a => a.FieldInRelationEntities)
        .ToList()
        .ConvertAll(GetUpdateEntityViewDeclration));


        public static string GetCreateFormRouting(this string createViewHtml, EntityFullInformationDto entity,
            EntitySelectedProjectAreaListDto area)
        => createViewHtml.Replace("{form-routing}", $@"
                            asp-area=""{area.ProjectAreaTitle}"" asp-controller=""{entity.Entity.SingularName}"" asp-action=""Create""
                            enctype=""multipart/form-data""");

        public static string GetCreateFormInputs(this string createViewHtml, EntityFullInformationDto entity,
            TemplateListDto template)
        {
            List<EntityRelationListDto> middleRelationEntities = entity
                .GetMiddleRelations();
            return createViewHtml.Replace("{form-inputs}",
                entity.GetUpsertFormInputs(middleRelationEntities, template, OperationTypeEnums.Create));
        }

        public static string GetUpdateTitles(this string UpdateViewHtml, EntityFullInformationDto entity)
       => UpdateViewHtml.Replace("{title}", $@"@SharedLocalizer.GetString(""Update"") @SharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"")");

        public static string GetUpdateBtnTitles(this string UpdateViewHtml, EntityFullInformationDto entity)
        => UpdateViewHtml.Replace("{btn-title}", $@"@SharedLocalizer.GetString(""Update"")");

        public static string GetUpdatePageViewDatas(this EntityFullInformationDto entity)
        {
            List<PropertyListDto> selectProperties = entity
                .GetAllEntities()
                .SelectMany(a => a.GetSelectProperties().OnlyUpdatables())
                .Distinct()
                .DistinctBy(a => a.EntityRelation!.SecondaryEntityId)
                .ToList();
            return string.Join("\n",
                selectProperties
                .ConvertAll(GetSingleViewData));
        }

        public static string GetUpdateFormRouting(this string UpdateViewHtml, EntityFullInformationDto entity,
            EntitySelectedProjectAreaListDto area)
        => UpdateViewHtml.Replace("{form-routing}", $@"
                            asp-area=""{area.ProjectAreaTitle}"" asp-controller=""{entity.Entity.SingularName}"" asp-action=""Update""
                            enctype=""multipart/form-data""");

        public static string GetUpdateFormInputs(this string UpdateViewHtml, EntityFullInformationDto entity,
            TemplateListDto template)
        {
            List<EntityRelationListDto> middleRelationEntities = entity.GetMiddleRelations();
            return UpdateViewHtml.Replace("{form-inputs}",
                entity.GetUpsertFormInputs(middleRelationEntities, template, OperationTypeEnums.Update));
        }

        public static string GetDetailTitles(this string DetailViewHtml, EntityFullInformationDto entity)
        => DetailViewHtml.Replace("{title}", $@"@SharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"") @SharedLocalizer.GetString(""Detail"")");

        public static string GetDetailPageBackBtn(this string detailViewHtml, EntityFullInformationDto entity
            , TemplateListDto template, EntitySelectedProjectAreaListDto area)
        {
            return detailViewHtml
                .Replace("{button}", template.AnchorTagCode
                .GetDetailBtnTitles(entity)
                .GetDetailBtnHref(entity, area));
        }

        public static string GetUpsertPageSubmitBtn(this string detailViewHtml, EntityFullInformationDto entity
            , TemplateListDto template)
        {
            return detailViewHtml
                .Replace("{button}", template.SubmitBtnCode
                .SetBtnTitlesToPageTitle(entity));
        }

        private static string GetDetailBtnTitles(this string DetailViewHtml, EntityFullInformationDto entity)
        => DetailViewHtml.Replace("{btn-title}", $@"@SharedLocalizer.GetString(""Close"")");

        private static string SetBtnTitlesToPageTitle(this string viewHtml, EntityFullInformationDto entity)
        => viewHtml.Replace("{btn-title}", "{title}");

        public static string GetDetailBtnHref(this string DetailViewHtml, EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        => DetailViewHtml.Replace("{btn-href}", entity.GetIndexPageRoutingTags(area));

        public static string GetDetailFormInputs(this string DetailViewHtml, EntityFullInformationDto entity,
            TemplateListDto template)
        {

            List<EntityRelationListDto> middleRelationEntities = entity.GetMiddleRelations();

            return DetailViewHtml.Replace("{form-inputs}",
                    $@"
                    {entity.GetUpsertFormInputs(middleRelationEntities, template, OperationTypeEnums.Detail)}
                    <div class=""col-sm-12 col-md-6"">
            <div class=""form-floating"">
                <input class=""form-control"" value=""@Model.CreateDate.ToShamsi()"" asp-for=""@Model.CreateDate"" type=""text"" placeholder="""" />
                <label asp-for=""@Model.CreateDate""></label>
            </div>
            <span asp-validation-for=""@Model.CreateDate""></span>
        </div>
        <div class=""col-sm-12 col-md-6"">
            <div class=""form-floating"">
                <input class=""form-control"" value=""@Model.LatestEditDate.ToShamsi()"" asp-for=""@Model.LatestEditDate"" type=""text"" placeholder="""" />
                <label asp-for=""@Model.LatestEditDate""></label>
            </div>
            <span asp-validation-for=""@Model.LatestEditDate""></span>
        </div>
        <div class=""col-sm-12 col-md-6"">
            <div class=""form-floating"">
                <input class=""form-control"" asp-for=""@Model.EditCounts"" type=""number"" placeholder="""" />
                <label asp-for=""@Model.EditCounts""></label>
            </div>
            <span asp-validation-for=""@Model.EditCounts""></span>
        </div>
                    ");
        }

        private static string GetImagesCode(this List<PropertyListDto> properties
            , string imageHtml, string instanceName)
        {
            return string.Join("\n", properties
                .ConvertAll(a => a.GetSingleImageCode(imageHtml, instanceName)));
        }

        private static string GetSingleImageCode(this PropertyListDto property, string imageHtml, string instanceName)
            => imageHtml.Replace("{src}",
                    $@"src=""@({property.GetOriginalMediaInformation(property.EntityTitle)}.GetAddress+{instanceName}.{property.Name})""")
                    .Replace("{alt}", $@"alt=""{instanceName}.{property.Name}""")
                    .Replace("{title}", $@"title=""{instanceName}.{property.Name}""");

        private static string GetSingleImageCode(this PropertyListDto property, string imageHtml)
        {
            StringBuilder imageName = new StringBuilder();
            imageName.Append(property.AspFor);
            imageName.Replace(property.Name, property.NameInDb);
            return imageHtml.Replace("{src}",
                    $@"src=""@({property.GetOriginalMediaInformation(property.EntityTitle)}.GetAddress+{imageName.ToString()})""")
                    .Replace("{alt}", $@"alt=""@{property.AspFor}""")
                    .Replace("{title}", $@"title=""@{property.AspFor}""");
        }

        private static string GetCreatePagePreviewImage(this string imageHtml, PropertyListDto property)
            => imageHtml.Replace("{src}",
                    $@"src=""/medias/images/upload.jpg""")
                    .GetUpsertPagePreviewImage(property);

        private static string GetUpdatePagePreviewImage(this string imageHtml, PropertyListDto property)
        {
            StringBuilder imageName = new StringBuilder();
            imageName.Append(property.AspFor);
            imageName.Replace(property.Name, property.NameInDb);
            return imageHtml.Replace("{src}",
                    $@"src=""@({property.GetOriginalMediaInformation(property.EntityTitle)}.GetAddress+{imageName})""")
                    .GetUpsertPagePreviewImage(property);
        }

        private static string GetUpsertPagePreviewImage(this string imageHtml, PropertyListDto property)
            => imageHtml
                    .Replace("{alt}", $@"alt=""{property.Name}""")
                    .Replace("<img", $@"<img data-name=""{property.GetImagePreviewClassName()}""")
                    .Replace("{title}", $@"title=""{property.Name}""");

        public static string RemoveDynamicSigns(this string html)
        => Regex.Replace(html, @"\{.*?\}", "");
        public static string DisableInputs(this string html)
            => html
            .Replace("<select ", "<select disabled ")
            .Replace("<input ", "<input disabled ")
            .Replace("<textarea ", "<textarea disabled ");

        private static string GetSingleViewData(this PropertyListDto property)
            => $@"List<SelectListItem> {property.EntityRelation.SecondaryEntity.PluralName.ToFirstCharLower()} = ViewData[""{property.EntityRelation.SecondaryEntity.PluralName}""] as List<SelectListItem>;";
        private static string GetCreateEntityViewDeclration(this EntityFullInformationDto entity)
           => $@"Create{entity.Entity.SingularName}Dto create{entity.Entity.SingularName} = new Create{entity.Entity.SingularName}Dto();";

        private static string GetUpdateEntityViewDeclration(this EntityFullInformationDto entity)
           => $@"Update{entity.Entity.SingularName}Dto update{entity.Entity.SingularName} = new Update{entity.Entity.SingularName}Dto();";

        private static string GetUpsertFormInputs(this EntityFullInformationDto entity,
            List<EntityRelationListDto> middleRelationEntities, TemplateListDto template, OperationTypeEnums operationType)
        {
            List<PropertyListDto> properties = entity.GetViewProperties(operationType);
            //properties = properties
            //    .OrderByDescending(a => !a.IsAnyKindFile())
            //    .ToList();
            string propertiesCode = string.Join("\n",
                           properties
                           .ConvertAll(a => a.GetSingleFormInput(template, operationType)));
            string middleRelationsCode = string.Join("\n",
                middleRelationEntities
                .ConvertAll(a =>
                $"{($"{a.PrimaryPropertyTitle}Ids").GetSingleSelectCode(template.SelectInputHtml,
                a.MiddleEntityTitle.ToFirstCharLower(), true)}"));
            string fieldsInRelationPropertiesCode = string.Join("\n",
                entity
                .FieldInRelationEntities
                .ConvertAll(a => a.GetSingleFieldsInRelationEntityPropertiesCode(a.GetViewProperties(operationType), template, operationType)));
            return $@"
                {propertiesCode}
    
                {middleRelationsCode}
                
                {fieldsInRelationPropertiesCode}
                
                {(operationType == OperationTypeEnums.Detail ? "" : entity.GetAllFieldInRelationsTemplatesCode(template, operationType))}
";
        }

        #region field in relations

        private static string GetAllFieldInRelationsTemplatesCode(this EntityFullInformationDto entity,
            TemplateListDto template, OperationTypeEnums operationType)
        => string.Join("\n",
                entity
                .FieldInRelationEntities.ConvertAll(a => entity.GetSingleFieldInRelationEntityTemplateCode(a, template, operationType)));

        private static string GetSingleFieldInRelationEntityTemplateCode(this EntityFullInformationDto mainEntity, EntityFullInformationDto entity,
            TemplateListDto template, OperationTypeEnums operationType, string middleName = "")
        {
            if (!string.IsNullOrEmpty(middleName))
                middleName = $"{middleName}.";
            middleName += $"{entity.Entity.PluralName}List";
            string middleNameWithIndexes = middleName.Replace(".", "[0].");
            string fieldInRelationTemplates = string.Join("\n", entity
                .FieldInRelationEntities
                .ConvertAll(a => entity.GetSingleFieldInRelationEntityTemplateCode(a, template, operationType, middleName)));
            List<PropertyListDto> properties = entity.GetViewProperties(operationType);
            string templatePropertiesCode = string.Join("\n",
                          properties
                          .ConvertAll(a => entity.GetSingleFieldInRelaitonEntityTemplatePropertyCode(a, template, operationType)));
            string childAttributes = string.Join('\n', entity
                .FieldInRelationEntities
                .ConvertAll(a => $@"data-child-{a.Entity.Id}=""{middleNameWithIndexes}[0].{a.Entity.PluralName}List"""));
            string replaceFromDtoText = operationType switch
            {
                OperationTypeEnums.Create => "create",
                OperationTypeEnums.Update => "update",
                _ => ""
            };
            string addButtons = string.Join("\n",
    entity
    .FieldInRelationEntities
    .ConvertAll(a => $@"
                        <div class=""d-flex justify-content-end mt-3"">
                            <p class=""btn btn-success next-btn mb-0 add-sub-item-btn""
                               onclick=""AddSubItem('@($""{middleNameWithIndexes}[0].{a.Entity.PluralName}List"")')""
                               data-name=""{a.Entity.PluralName}List"">@SharedLocalizer.GetString(""Add"") @SharedEntitiesLocalizer.GetString(""{a.Entity.SingularName}"")</p>
                        </div>"));
            return $@"
                                 <div class=""sub-form-template d-none {(mainEntity.IsRequiredFieldInRelation(entity) ? "add-default-sub-form" : "")}"" 
                                          data-name=""{middleNameWithIndexes}""
                                          data-form-name=""{entity.Entity.PluralName}List""
                                          data-title=""@SharedEntitiesLocalizer.GetString(""{entity.Entity.PluralName}"")""
                                          data-replace-from=""{replaceFromDtoText}{entity.Entity.SingularName}""
                                          data-replace-to=""{middleNameWithIndexes}[0]""
                                          {childAttributes}>
                                         {templatePropertiesCode}
                                      <div class=""buttons-container is-template"" data-name=""{middleNameWithIndexes}[0]"">
                                        {addButtons}
                                         <div class=""d-flex justify-content-end mt-3"">
                                             <p class=""btn btn-danger next-btn mb-0 remove-sub-item-btn""
                                                data-name=""{middleNameWithIndexes}""
                                                onclick=""RemoveSubItem(this)"">@SharedLocalizer.GetString(""Delete"") @SharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"")</p>
                                         </div>                              
                                      </div>
                                  </div>
                                     {fieldInRelationTemplates}";
        }

        private static string GetSingleFieldsInRelationEntityPropertiesCode(this EntityFullInformationDto entity, List<PropertyListDto> properties,
            TemplateListDto template, OperationTypeEnums operationType, string middleName = "", string middleNameWithData = "", int index = 0)
        {
            if (!string.IsNullOrEmpty(middleName))
                middleName = $"{middleName}.";
            if (!string.IsNullOrEmpty(middleNameWithData))
                middleNameWithData += @"+""."" +";
            string entityName = $"{entity.Entity.PluralName}List";
            string loopInstanceName = entity.Entity.SingularName.ToFirstCharLower();
            string middleNameWithoutIndex = middleName + $"{entityName}";
            string middleNameWithDataForSubContainer = middleNameWithData + $@"""{entityName}""";
            string middleNameWithoutLastIndex = middleNameWithData + $@"""{entityName}""";
            middleName += $"{entityName}[{entity.Entity.SingularName.ToFirstCharLower()}]";
            middleNameWithData += $@"""{entityName}"" + ""["" + {entity.Entity.SingularName.ToFirstCharLower()} + ""]""";
            string middleNameToUse = $"@{loopInstanceName}DataName";
            string propertiesCode = string.Join("\n",
                           properties
                           .ConvertAll(a => entity.GetSingleFieldInRelaitonEntityPropertyCode(a, template, operationType, middleName)));
            string subFormsCode = string.Join("\n",
                entity
                .FieldInRelationEntities
                .ConvertAll(a => a.GetSingleFieldsInRelationEntityPropertiesCode(a.GetViewProperties(operationType), template, operationType, middleName, middleNameWithData, index + 1)));

            string subFormsAddButtons = string.Join("\n",
                entity.FieldInRelationEntities
                .ConvertAll(a => $@"
                        <div class=""d-flex justify-content-end mt-3"">
                            <p class=""btn btn-success next-btn mb-0 add-sub-item-btn""
                               onclick=""AddSubItem('@({middleNameWithData}+"".{a.Entity.PluralName}List"")')""
                               data-name=""{a.Entity.PluralName}List"">@SharedLocalizer.GetString(""Add"") @SharedEntitiesLocalizer.GetString(""{a.Entity.SingularName}"")</p>
                        </div>"));
            return $@"
                                 <div class=""sub-form-container"" data-name=""@({middleNameWithDataForSubContainer})"">
                                 <h3>@SharedEntitiesLocalizer.GetString(""{entity.Entity.PluralName}"")</h3>
                                     @if (Model.{middleNameWithoutIndex} is not null)
                                     {{
                                         for (int {loopInstanceName} = 0; {loopInstanceName} < Model.{middleNameWithoutIndex}.Count; {loopInstanceName}++)
                                         {{
                                             
                                                string {loopInstanceName}DataName = {middleNameWithData};
                                              
                                         <div class=""single-sub-form row g-5"" data-name=""@({middleNameWithoutLastIndex})"">
                                                 {propertiesCode}
                                                 {subFormsCode}
                                                 {(operationType != OperationTypeEnums.Detail ? $@"
                                            <div class=""buttons-container"" data-name=""{middleNameToUse}"">
{subFormsAddButtons}
                                                 <div class=""d-flex justify-content-end mt-3"">
                                                     <p class=""btn btn-danger next-btn mb-0 remove-sub-item-btn""
                                                        data-name=""@({middleNameWithoutLastIndex})""
                                                        onclick=""RemoveSubItem(this)"">@SharedLocalizer.GetString(""Delete"") @SharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"")</p>
                                                 </div>
                                             </div>" : "")}
                                         </div>
                                             
                                         }}
                                     }}  
                                 </div>
                                 {(operationType != OperationTypeEnums.Detail && index == 0 ? $@"
 
                                 <div class=""d-flex justify-content-end mt-3"">
                                     <p class=""btn btn-success next-btn mb-0 add-sub-item-btn""
                                        onclick=""AddSubItem('{entityName}')""
                                        data-name=""{entityName}"">@SharedLocalizer.GetString(""Add"") @SharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"")</p>
                                 </div>" : "")}";
        }

        private static string GetSingleFieldInRelaitonEntityPropertyCode(this EntityFullInformationDto entity, PropertyListDto property,
             TemplateListDto template, OperationTypeEnums operationType, string middleName)
        {
            PropertyListDto customProperty = property.DeepCopy();
            string dtoName = $"Create{entity.Entity.SingularName}";

            customProperty.ClassNames = "sub-form-input";
            customProperty.AdditionalAttributes = $@"
               data-name=""{entity.Entity.PluralName}List""
               data-prefix=""{entity.Entity.PluralName}List""
               data-suffix=""{property.Name}""
               {(property.IsFieldInPageRelation() ? $@"data-value=""1""" : "")}";
            if (customProperty.IsFieldInPageRelation())
                customProperty.DataAnnotationDataType = DataAnnotationsDataType.Hidden;
            customProperty.AspFor = $"Model.{middleName}.{property.Name}";
            return customProperty.GetSingleFormInput(template, operationType);
        }

        private static string GetSingleFieldInRelaitonEntityTemplatePropertyCode(this EntityFullInformationDto entity, PropertyListDto property,
            TemplateListDto template, OperationTypeEnums operationType)
        {
            PropertyListDto customProperty = property.DeepCopy();
            customProperty.ClassNames = "sub-form-input";
            customProperty.AdditionalAttributes = $@"
               data-name=""{entity.Entity.PluralName}List""
               data-prefix=""{entity.Entity.PluralName}List""
               data-suffix=""{customProperty.Name}""
               {(customProperty.IsFieldInPageRelation() || customProperty.IsHiddenInput() ||
               (customProperty.IsAnyKindFile() && string.IsNullOrEmpty(customProperty.ForceDataTypeCode))
               ? $@"value=""{property.DataType.GetDataTypeDefaultValue()}""" : "")}";

            if (customProperty.IsFieldInPageRelation())
                customProperty.DataAnnotationDataType = DataAnnotationsDataType.Hidden;
            string dtoName = operationType switch
            {
                OperationTypeEnums.Create => "create",
                OperationTypeEnums.Update => "update",
                _ => ""
            };
            customProperty.AspFor = $"{dtoName}{entity.Entity.SingularName}.{customProperty.Name}";
            return customProperty.GetSingleFormInput(template, operationType);
        }

        public static bool IsFieldInPageRelation(this PropertyListDto property)
            => property.EntityRelation != null && property.EntityRelation.InputType == InputTypeEnum.FieldsInRelationEntityPage;

        #endregion

        private static string GetSingleFormInput(this PropertyListDto property, TemplateListDto template, OperationTypeEnums operationType)
        {
            #region setting asp for

            if (string.IsNullOrEmpty(property.AspFor))
                property.AspFor = $"Model.{property.Name}";
            property.AspFor = property.AspFor.Replace("@", "");

            #endregion

            if (property.IsHiddenInput() && operationType != OperationTypeEnums.Detail) return property.GetSingleHiddenInputCode();

            #region getting html

            string inputHtml = template.TextInputHtml;
            if (property.DataType == DataTypeEnum.String)
            {
                if (property.DataAnnotationDataType == DataAnnotationsDataType.Text)
                    inputHtml = template.TextEditorInputHtml;
                else
                    inputHtml = template.TextInputHtml;
                if (property.DataAnnotationDataType == DataAnnotationsDataType.Color)
                    inputHtml = template.ColorPickerInputCode;
            }
            if (property.DataType == DataTypeEnum.Int || property.DataType == DataTypeEnum.Long ||
                property.DataAnnotationDataType == DataAnnotationsDataType.PhoneNumber ||
                property.DataAnnotationDataType == DataAnnotationsDataType.NaitionalCode)
                inputHtml = template.IntegerInputHtml;
            if (property.DataType == DataTypeEnum.Bool)
                inputHtml = template.CheckBoxInputCode;
            if (property.IsSelect() || property.ProjectEnum != null)
                inputHtml = template.SelectInputHtml;
            if (property.IsAnyKindFile())
                inputHtml = template.FileInputCode;

            #endregion

            #region setting attrs

            if (!string.IsNullOrEmpty(property.AdditionalAttributes))
                inputHtml = inputHtml
                    .Replace("{attrs}",
                    $@"{property.AdditionalAttributes} {{attrs}}");
            inputHtml = inputHtml.Replace("{asp-for}",
                 $@"asp-for=""@{property.AspFor}""")
             .Replace("{validation-for}",
                 $@"asp-validation-for=""@{property.AspFor}""");

            if (!string.IsNullOrEmpty(property.ClassNames))
                inputHtml = inputHtml
                 .Replace("{class}", $"{property.ClassNames} {{class}}");

            #endregion

            #region returning file results

            if (property.IsAnyKindFile())
            {
                string fileInputCode = property.GetSingleFileInputCode(inputHtml);

                if (operationType != OperationTypeEnums.Detail && string.IsNullOrEmpty(property.ForceDataTypeCode))
                    return property.GetSingleHiddenInputCode();
                switch (operationType)
                {
                    case OperationTypeEnums.Detail:
                        return property.GetSingleImageCode(template.SingleImageHtml);
                    case OperationTypeEnums.Update:
                        return $@"
                        {fileInputCode}
                        {(property.DataAnnotationDataType == DataAnnotationsDataType.Image
                        ? template.SingleImageHtml.GetUpdatePagePreviewImage(property)
                        : "")}";
                    case OperationTypeEnums.Create:
                        return $@"
                        {fileInputCode}
                        {(property.DataAnnotationDataType == DataAnnotationsDataType.Image
                        ? template.SingleImageHtml.GetCreatePagePreviewImage(property)
                        : "")}";
                }
            }

            #endregion

            #region returning enum 

            if (property.ProjectEnum is not null)
                return inputHtml.GetSingleEnumSelectCode(
                    property.Name, property.ProjectEnum.EnglishName);

            #endregion

            #region returning select

            if (property.IsSelect())
                return property.Name.GetSingleSelectCode(inputHtml,
                    property.EntityRelation!.SecondaryEntity.PluralName.ToFirstCharLower()
                    , false);

            #endregion

            return property.GetSingleInputCode(inputHtml);
        }

        private static string GetSingleHiddenInputCode(this PropertyListDto property)
        {
            if (string.IsNullOrEmpty(property.AspFor))
                property.AspFor = $"Model.{property.Name}";
            string name = property.Name;
            if (property.DataAnnotationDataType == DataAnnotationsDataType.Image)
                name = name;
            return $@"<input type=""hidden"" asp-for=""@{property.AspFor}"" class=""{property.ClassNames}""
                       {property.AdditionalAttributes}/>";
        }

        private static string GetSingleInputCode(this PropertyListDto property, string inputHtml)
        => property.GetSingleInputCode(property.Name, inputHtml);

        private static string GetSingleInputCode(this PropertyListDto property, string name, string inputHtml)
        {
            inputHtml = inputHtml.Replace("{asp-for}",
                $@"asp-for=""@Model.{name}""")
            .Replace("{validation-for}",
                $@"asp-validation-for=""@Model.{name}""")
            .Replace("{class}", $"{property.DataAnnotationDataType.ToString()}-input");
            if (property.UseEditor)
                inputHtml.Replace("{attrs}", $@"ckeditor=""{property.Id}""");
            else
                inputHtml.Replace("{attrs}", "");
            return inputHtml;
        }

        private static string GetSingleFileInputCode(this PropertyListDto property, string inputHtml)
        {
            string acceptFormats = property.DataAnnotationDataType switch
            {
                DataAnnotationsDataType.Image => $@"@FileAcceptableFormats.Image",
                DataAnnotationsDataType.File => $@"@FileAcceptableFormats.File",
                DataAnnotationsDataType.Video => $@"@FileAcceptableFormats.Video",
                _ => string.Empty
            };
            string acceptCode = $@"accept=""{(string.IsNullOrEmpty(acceptFormats) ? "*" : acceptFormats)}""";
            string dataValueCode = $@"data-value=""{property.GetImagePreviewClassName()}""";
            inputHtml = inputHtml.Replace("{asp-for}",
                $@"asp-for=""@Model.{property.Name}""")
            .Replace("{validation-for}",
                $@"asp-validation-for=""@Model.{property.Name}""")
            .Replace("{class}", $"{property.DataAnnotationDataType.ToString()}-input");
            return inputHtml.Replace("{attrs}", $@"
                            {acceptCode} 
                            {dataValueCode}");
        }

        private static string GetSingleSelectCode(this string name, string inputHtml,
            string aspItemsName, bool multiple)
        {
            return inputHtml.Replace("{asp-for}",
                $@"asp-for=""@Model.{name}""")
            .Replace("{validation-for}",
                $@"asp-validation-for=""@Model.{name}""")
            .Replace("{asp-items}", $@"asp-items=""{aspItemsName}"" {(multiple ? "multiple" : "")}")
            .Replace("{select-options}", "")
            //.Replace("{class}", "select-multiple")
            .Replace("{select-name}", "");
        }
        private static string GetSingleEnumSelectCode(this string inputHtml, string name, string enumName)
            => inputHtml
                .GetSelectAspMapping(name)
                .GetSelectEnumOptions(enumName)
                .Replace("{asp-items}", "")
                .Replace("{validation-for}",
                $@"asp-validation-for=""{name}""");
    }
}
