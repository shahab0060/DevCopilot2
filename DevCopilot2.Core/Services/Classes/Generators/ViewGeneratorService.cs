using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.New.Enums.DTOs;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class ViewGeneratorService : IViewGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public ViewGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public List<CreateFileResultDto> Generate(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            List<CreateFileResultDto> results = new List<CreateFileResultDto>();
            List<GenerateFileDto> files = GetAreaFiles(generate, entity);
            foreach (var file in files)
            {
                results.Add(_baseGeneratorService.GenerateFile(file));
            }
            return results;
        }

        List<GenerateFileDto> GetAreaFiles(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            List<GenerateFileDto> files = new List<GenerateFileDto>();
            foreach (var area in entity.Entity.EntitySelectedProjectAreasList.Where(a => a.HasWeb))
            {
                files.AddRange(GetSingleAreaFiles(generate, entity, area));
            }
            return files;
        }

        List<GenerateFileDto> GetSingleAreaFiles(GenerateEntityDto generate, EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            List<GenerateFileDto> files = new List<GenerateFileDto>();
            if (area.HasIndex)
            {
                files.Add(new GenerateFileDto()
                {
                    Override = generate.OverrideFiles,
                    Code = GetIndexViewCode(entity, area),
                    FileNameWithExtension = $"Index.Cshtml",
                    Path = GetFullPath(entity, area)
                });
                files.Add(new GenerateFileDto()
                {
                    Override = generate.OverrideFiles,
                    Code = GetDetailViewCode(entity, area),
                    FileNameWithExtension = $"Detail.Cshtml",
                    Path = GetFullPath(entity, area)
                });
            }
            if (area.HasCreate)
                files.Add(new GenerateFileDto()
                {
                    Override = generate.OverrideFiles,
                    Code = GetCreateViewCode(entity, area),
                    FileNameWithExtension = $"Create.Cshtml",
                    Path = GetFullPath(entity, area)
                });
            if (area.HasUpdate)
                files.Add(new GenerateFileDto()
                {
                    Override = generate.OverrideFiles,
                    Code = GetUpdateViewCode(entity, area),
                    FileNameWithExtension = $"Update.Cshtml",
                    Path = GetFullPath(entity, area)
                });
            return files;
        }

        string GetUsings(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            List<string> usings = [.. _baseGeneratorService.GetBaseUsings()];
            usings.Add($"System.ComponentModel.DataAnnotations");
            List<EntityFullInformationDto> allEntities = entity.GetAllEntities();
            foreach (EntityFullInformationDto singleEntity in allEntities)
            {
                usings.AddRange(GetSingleEntityUsings(singleEntity));
            }
            return usings.GetRazorUsings(GetNameSpace(entity, area));
        }

        List<string> GetSingleEntityUsings(EntityFullInformationDto entity)
        {
            List<string> usings =
            [
               ..entity.GetEnumUsings(),
                $"{entity.Project.EnglishName}.Domain.DTOs.{entity.Entity.FolderName}"
            ];
            if (entity.HasAnyFile())
                usings.Add($"{entity.Project.EnglishName}.Core.MediasInformation.{entity.Entity.FolderName}");
            return usings;
        }


        string GetInjects()
        {
            return $@"@inject IViewLocalizer Localizer
@inject IHtmlLocalizer<SharedResources> SharedLocalizer
@inject IHtmlLocalizer<EntitiesSharedResources> SharedEntitiesLocalizer";
        }


        string GetFullPath(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
            => $@"{entity.Project.Location}\{GetPath(entity, area)}";

        string GetPath(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            if (area.ProjectAreaTitle.ToLower() == "site")
                return $@"{entity.Project.EnglishName}.Web\Views\{entity.Entity.SingularName}";
            return $@"{entity.Project.EnglishName}.Web\Areas\{area.ProjectAreaTitle}\Views\{entity.Entity.SingularName}";
        }

        public string GetNameSpace(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        => GetPath(entity, area).Replace(@"\", ".");

        #region index

        public string GetIndexViewCode(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            string listViewHtml = area.Template.ListViewHtml;
            string entityListViewCode = listViewHtml
                .GetIndexTitles(entity)
                .GetItemsCount()
                .GetCountText(entity)
                .GetCreateBtnRouting(entity, area)
                .GetCreateBtnText(entity)
                .GetIndexCardHtml(entity, area.Template)
                .GetFilterFormRouting(entity, area)
                .GetFilterFormHiddenInputs(entity)
                .GetTableHeadersHtml(entity, area.Template)
                .GetTableBodyRows(entity, area.Template, area)
                .GetBaseFiltersSelectCode(area.Template, entity)
                .GetExcelBtnRouting(entity, area)
                .GetPdfBtnRouting(entity, area)
                .GetIndexPaging(area)
                .SetBreadCrumbs(entity, area, area.Template, OperationTypeEnums.List)
                .RemoveDynamicSigns();

            return $@"{GetInjects()}
{GetUsings(entity, area)}
@model Filter{entity.Entity.PluralName}Dto
@{{
    ViewData[""Title""] = $""{{SharedEntitiesLocalizer.GetString(""{entity.Entity.PluralName}"")}}"";
}}

{entityListViewCode}

{GetBaseScripts(entity, false)}";
        }

        #endregion

        #region create

        public string GetCreateViewCode(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            string createPageHtml = area
                .Template
                .CreatePageHtml;
            string entityCreateViewCode = createPageHtml
                .GetUpsertPageSubmitBtn(entity, area.Template)
                .GetCreateTitles(entity)
                .GetCreateBtnTitles(entity)
                .GetCreateFormRouting(entity, area)
                .GetCreateFormInputs(entity, area.Template)
                .SetBreadCrumbs(entity, area, area.Template, OperationTypeEnums.Create)
                .RemoveDynamicSigns();

            return $@"{GetInjects()}
{GetUsings(entity, area)}
@model Create{entity.Entity.SingularName}Dto
@{{
	ViewData[""Title""] = $@""{{SharedLocalizer.GetString(""Create New"")}} {{SharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"")}}"";
        
    {entity.GetCreatePageViewDatas()}    
    {entity.GetCreatePageFieldInRelationDeclrations()}

}}

{entityCreateViewCode}

{GetBaseScripts(entity)}";
        }

        #endregion

        #region update

        public string GetUpdateViewCode(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            string updatePageHtml = area
                .Template
                .CreatePageHtml;
            string entityUpdateViewCode = updatePageHtml
                .GetUpsertPageSubmitBtn(entity, area.Template)
                .GetUpdateTitles(entity)
                .GetUpdateBtnTitles(entity)
                .GetUpdateFormRouting(entity, area)
                .GetUpdateFormInputs(entity, area.Template)
                .SetBreadCrumbs(entity, area, area.Template, OperationTypeEnums.Update)
                .RemoveDynamicSigns();

            return $@"{GetInjects()}
{GetUsings(entity, area)}
@model Update{entity.Entity.SingularName}Dto
@{{
	ViewData[""Title""] = $@""{{SharedLocalizer.GetString(""Update"")}} {{SharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"")}}"";
        
    {entity.GetUpdatePageViewDatas()}
    {entity.GetUpdatePageFieldInRelationDeclrations()}
}}

{entityUpdateViewCode}

{GetBaseScripts(entity)}";
        }

        #endregion

        #region detail

        public string GetDetailViewCode(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            string entityDetailViewCode = area
                .Template
                .CreatePageHtml
                .GetDetailTitles(entity)
                .GetDetailPageBackBtn(entity, area.Template, area)
                .GetDetailFormInputs(entity, area.Template)
                .DisableInputs()
                .SetBreadCrumbs(entity, area, area.Template, OperationTypeEnums.Detail)
                .RemoveDynamicSigns();

            return $@"{GetInjects()}
{GetUsings(entity, area)}
@model {entity.Entity.SingularName}ListDto
@{{
	ViewData[""Title""] = $""{{SharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"")}} {{SharedLocalizer.GetString(""Detail"")}}"";
    {entity.GetCreatePageViewDatas()}
    {entity.GetCreatePageFieldInRelationDeclrations()}
}}

{entityDetailViewCode}

{GetBaseScripts(entity, false)}";
        }

        #endregion

        #region get base scripts

        string GetBaseScripts(EntityFullInformationDto entity, bool hasValidation = true)
        => $@"@section Scripts
{{
{(hasValidation ? $@"	<script src=""/Shared/JqueryValidation.min.js""></script>" : string.Empty)}
	<script>
		$(document).ready(function (e) {{
			$('.{entity.Entity.PluralName}Management').addClass('active');
            $('.{entity.Entity.PluralName}Management').addClass('show');
		}});
	</script>
}}";

        #endregion


    }
}
