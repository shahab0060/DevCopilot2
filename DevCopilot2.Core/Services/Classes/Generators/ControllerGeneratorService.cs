using AngleSharp.Dom;
using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.Enums.DataTypes;
using System.Text;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class ControllerGeneratorService : IControllerGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public ControllerGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public List<CreateFileResultDto> Generate(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            List<CreateFileResultDto> results = new List<CreateFileResultDto>();
            foreach (var area in entity.Entity.EntitySelectedProjectAreasList)
            {
                results.Add(Generate(generate, entity, area));
            }
            return results;
        }

        public CreateFileResultDto Generate(GenerateEntityDto generate, EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            GenerateFileDto generateFile = new GenerateFileDto()
            {
                FileNameWithExtension = $"{entity.Entity.SingularName}Controller.cs",
                Override = generate.OverrideFiles,
                Path = GetFilePath(entity, area)
            };
            generateFile.Code = $@"{GetUsings(entity, area)}

namespace {GetNameSpace(entity, area)}
{{
	{$@"[PermissionChecker(""{entity.Entity.SingularName}Management"")]"}
    public class {entity.Entity.SingularName}Controller : Base{area.ProjectAreaTitle}Controller<{entity.Entity.SingularName}ListDto>
    {{
        
{GetConstructorCode(entity)}

{GetMethods(entity, area)}

    }}
}}
";
            return _baseGeneratorService.GenerateFile(generateFile);
        }

        string GetFilePath(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
       => $@"{entity.Project.Location}\{entity.Project.EnglishName}.Web{(area.ProjectAreaTitle == "Site" ? "" : @$"\Areas\{area.ProjectAreaTitle}")}\Controllers\{entity.Entity.FolderName}";

        public string GetNameSpace(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
       => $@"{entity.Project.EnglishName}.Web{(area.ProjectAreaTitle == "Site" ? "" : @$".Areas.{area.ProjectAreaTitle}")}.Controllers.{entity.Entity.FolderName}";

        #region usings

        string GetUsings(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            List<string> usings = [.. _baseGeneratorService.GetBaseUsings(),
            "System.Text",
            $"{entity.Project.EnglishName}.Core.Extensions.BasicExtensions",
            $"{entity.Project.EnglishName}.Core.Services.Interfaces",
            $"{entity.Project.EnglishName}.Domain.Enums.Common",
            $"{entity.Project.EnglishName}.Web.PresentationExtensions",
            $"{entity.Project.EnglishName}.Web.PresentationMappers",
            $"{entity.Project.EnglishName}.Domain.Enums.{entity.Entity.FolderName}",
            $"{entity.Project.EnglishName}.Domain.DTOs.{entity.Entity.FolderName}",
            "Microsoft.Extensions.Localization",
            "Microsoft.AspNetCore.Mvc",
            "HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute",
            "HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute",
            "ClosedXML.Excel",
            $"{entity.Project.EnglishName}.Core.Exporters",
            ];
            foreach (EntityFullInformationDto singleEntity in entity.GetAllEntities())
            {
                foreach (var property in singleEntity.GetAllSelectProperties())
                {
                    usings.Add($"{singleEntity.Project.EnglishName}.Domain.DTOs.{property.EntityRelation.SecondaryEntity.FolderName}");
                }
                foreach (var entityRelation in singleEntity.GetAllMiddleRelations())
                {
                    usings.Add($"{singleEntity.Project.EnglishName}.Domain.DTOs.{entityRelation.MiddleEntityFolderName}");
                }
            }
            return usings.GetUsings(GetNameSpace(entity, area));
        }

        #endregion

        #region get constructor

        private string GetConstructorCode(EntityFullInformationDto entity)
        {
            ConstructorListDto constructor = new ConstructorListDto()
            {
                ClassName = $"{entity.Entity.SingularName}Controller",
                DependencyInjections = new List<DependencyInjectionListDto>()
                {
                    new DependencyInjectionListDto()
                    {
                        Name = "SharedLocalizer",
                        FileName = "IStringLocalizer<SharedResources>"
                    },
                    new DependencyInjectionListDto()
                    {
                        Name = "SharedLocalizer",
                        FileName = "IStringLocalizer<SharedResources>"
                    },
                    new DependencyInjectionListDto()
                    {
                        Name = "SharedEntitiesLocalizer",
                        FileName = "IStringLocalizer<EntitiesSharedResources>"
                    },
                    new DependencyInjectionListDto()
                    {
                        Name = "Localizer",
                        FileName = $"IStringLocalizer<{entity.Entity.SingularName}Controller>"
                    },
                    new DependencyInjectionListDto()
                    {
                        Name = $"{entity.Entity.ServiceName}",
                        FileName = $"I{entity.Entity.ServiceName}"
                    },
                }
            };
            List<DependencyInjectionListDto> allSelectPropertiesServices = entity
                .GetAllSelectProperties()
                .Select(a => new DependencyInjectionListDto()
                {
                    FileName = $"I{a.EntityRelation.SecondaryEntity.ServiceName}",
                    Name = $"{a.EntityRelation.SecondaryEntity.ServiceName}"
                })
                .ToList();
            constructor.DependencyInjections.AddRange(allSelectPropertiesServices);


            return constructor.GetConstructorCode();
        }

        #endregion

        #region methods

        string GetMethods(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            StringBuilder methodsStringBuilder = new StringBuilder();
            if (area.HasIndex)
            {
                methodsStringBuilder.AppendLine(GetIndexMethodCode(entity, area));
                methodsStringBuilder.AppendLine(GetDetailMethodCode(entity));
            }
            if (area.HasCreate)
                methodsStringBuilder.AppendLine(GetCreateMethodCode(entity, area));
            if (area.HasUpdate)
                methodsStringBuilder.AppendLine(GetUpdateMethodCode(entity, area));
            if (area.HasIndex || area.HasCreate || area.HasUpdate)
                methodsStringBuilder.AppendLine(GetViewDatasMethodsCode(entity));
            if (area.HasDelete)
                methodsStringBuilder.AppendLine(GetDeleteMethodCode(entity, area));
            if (area.HasIndex)
            {
                methodsStringBuilder.AppendLine(GetExportExcelMethodCode(entity));
                methodsStringBuilder.AppendLine(GetExportPdfMethodCode(entity));
            }
            return methodsStringBuilder.ToString();
        }

        #endregion

        #region index method

        private string GetIndexMethodCode(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            List<EntitySelectedProjectAreaSelectedFilterListDto> customFilters = entity
                .Properties
                .SelectMany(d => d.EntitySelectedProjectAreaSelectedFilters.Where(a => a.EntitySelectedProjectAreaId == area.Id))
                .ToList();
            string customFiltersCode = string.Join("\n", customFilters
                .ConvertAll(a => $@"filter.{a.PropertyName}= {a.Value};"));
            return $@"        #region index

        [HttpGet]
        public async Task<IActionResult> Index(Filter{entity.Entity.PluralName}Dto filter)
        {{
            {customFiltersCode}
            return View(await _{entity.Entity.ServiceName!.ToFirstCharLower()}.Filter{entity.Entity.PluralName}(filter));
        }}

        #endregion
";
        }

        #endregion

        #region detail method

        private string GetDetailMethodCode(EntityFullInformationDto entity)
        {
            //if (!area.Detail) return string.Empty;
            bool hasSelectViewData = entity.GetAllSelectProperties().Any();
            return $@"        #region detail
		
		[HttpGet]
		public async Task<IActionResult>Detail({entity.Entity.GetDataType()} id)
		{{
			
            {entity.Entity.SingularName}ListDto? {entity.Entity.SingularName.ToFirstCharLower()}Information = await _{entity.Entity.ServiceName!.ToFirstCharLower()}.GetSingle{entity.Entity.SingularName}Information(id);
			if (
            {entity.Entity.SingularName.ToFirstCharLower()}Information is null)return NotFound();
			
            {(hasSelectViewData ? "await GetViewDatas();" : "")}
            return View(
            {entity.Entity.SingularName.ToFirstCharLower()}Information);	
		}}

		#endregion
";
        }

        #endregion

        #region export pdf

        private string GetExportPdfMethodCode(EntityFullInformationDto entity)
        {
            List<PropertyListDto> listProperties = entity.GetEntityListDtoProperties()
                .Where(a => !a.IsAnyKindFile())
                .ToList();
            string dataTableHeaders = string.Join("\n",
               listProperties
                .ConvertAll(a => $@"
            dataTable = dataTable.AddHeader($""{{_localizer.GetString(""{a.Name}"")}}"");"));
            string dataTableRows = string.Join("\n",
                listProperties
                .ConvertAll(a => $@"
item.{a.Name}{(a.IsRequired || a.DataType == DataTypeEnum.Bool ? "" : @"?")}{a.GetPropertyShowingExtensionMethod()},"))
                .ReplaceLastOccurrence(',', ' ');
            return $@"
        #region export pdf

        [HttpGet]
        public async Task<IActionResult> ExportPdf(Filter{entity.Entity.PluralName}Dto filter)
        {{
            List<{entity.Entity.SingularName}ListDto> result = (await _{entity.Entity.ServiceName!.ToFirstCharLower()}.Filter{entity.Entity.PluralName}(filter)).{entity.Entity.PluralName};
            string title = $""{{_sharedEntitiesLocalizer.GetString(""{entity.Entity.PluralName}"")}}"";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = dataTable.AddHeader($""{{_sharedLocalizer.GetString(""Row"")}}"");
            {dataTableHeaders}
            int index = 1;
            foreach (var item in result)
            {{
                dataTable = dataTable.AddContentRow(index,{dataTableRows});
                index++;
            }}

            MemoryStream stream = dataTable.CreatePDF(title);
            return ReturnPdf(stream, title);
        }}

        #endregion
";
        }

        #endregion

        #region export excel


        private string GetExportExcelMethodCode(EntityFullInformationDto entity)
        {
            List<PropertyListDto> listProperties = entity.GetEntityListDtoProperties()
                .Where(a => !a.IsAnyKindFile())
                .ToList();
            StringBuilder headerSb = new StringBuilder();
            StringBuilder rowsSb = new StringBuilder();
            int index = 2;
            foreach (var item in listProperties)
            {
                headerSb.AppendLine($@"ws = excelExporter.AddColumn(ws,$""{{_localizer.GetString(""{item.Name}"")}}"", {index}, 3);");
                rowsSb.AppendLine($@"
                    ws = excelExporter.AddColumn(ws, item.{item.Name}{(item.IsRequired || item.DataType == DataTypeEnum.Bool ? "" : @"?")}{item.GetPropertyShowingExtensionMethod()}, {index}, rowIndex);");
                index++;
            }
            return $@"
        #region export excel

        [HttpGet]
        public async Task<IActionResult> ExportExcel(Filter{entity.Entity.PluralName}Dto filter)
        {{
            List<{entity.Entity.SingularName}ListDto> result = (await _{entity.Entity.ServiceName!.ToFirstCharLower()}.Filter{entity.Entity.PluralName}(filter)).{entity.Entity.PluralName};
            string title = $""{{_sharedEntitiesLocalizer.GetString(""{entity.Entity.PluralName}"")}}"";
            using (var wbook = new XLWorkbook() {{ RightToLeft = true }})
            {{
                ExcelExporter<{entity.Entity.SingularName}ListDto> excelExporter = new ExcelExporter<{entity.Entity.SingularName}ListDto>();
                var ws = wbook.Worksheets.Add(title);
                ws = excelExporter.AddHeaders(ws, title);
                ws.Columns().AdjustToContents();
                ws = excelExporter.AddColumn(ws, $""{{_sharedLocalizer.GetString(""Row"")}}"", 1, 3);
                {headerSb.ToString()}
                int rowIndex = 4;
                foreach (var item in result)
                {{
                    ws = excelExporter.AddColumn(ws,(rowIndex-3).ToString(), 1, rowIndex);
                    {rowsSb.ToString()}
                    rowIndex++;
                }}
                ws.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {{
                    wbook.SaveAs(stream);
                    stream.Position = 0;
                    return ReturnExcel(stream, title);
                }}
            }}
        }}

        #endregion
";
        }


        #endregion

        #region create method

        private string GetCreateMethodCode(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            string instanceName = "create";
            return $@"        #region create

{GetCreateGetMethodCode(entity)}

		[HttpPost]
		public async Task<IActionResult> Create(Create{entity.Entity.SingularName}Dto {instanceName})
		{{
            {GetUpsertMethodPostCode(entity, area, instanceName, "Created Successfully", false)}
		}}

		#endregion
";
        }

        private string GetCreateGetMethodCode(EntityFullInformationDto entity)
        {
            bool hasSelectViewData = entity.GetAllSelectProperties().Any();
            List<PropertyListDto> routingProperties = entity.GetRelationsInRouting();
            string dtoName = $"Create{entity.Entity.SingularName}Dto";
            string instanceName = "create";
            string propertiesInRoutingCode = string.Join("\n",
                routingProperties
                .Select(a => $@"{a.GetDataType()} {a.Name.ToFirstCharLower()},"));
            propertiesInRoutingCode = propertiesInRoutingCode.
                ReplaceLastOccurrence(',', ' ').Trim();
            string settingVariablesToDtoCode = string.Join("\n", routingProperties
                .ConvertAll(a => $@"{a.Name} = {a.Name.ToFirstCharLower()},"));
            string routingBodyCode = $@"{dtoName} {instanceName} = new {dtoName}()
            {{
                {settingVariablesToDtoCode} 
            }};";
            bool passNewDtoInstance = routingProperties.Any() || entity.FieldInRelationEntities.Any() || entity.PropertiesHaveRelation();
            return $@"		[HttpGet]
		public {(hasSelectViewData ? "async Task<IActionResult>" : "IActionResult")} Create({propertiesInRoutingCode})
		{{
            {(hasSelectViewData ? "await GetViewDatas();" : "")}
            {(passNewDtoInstance ? routingBodyCode : "")}
            return View({(passNewDtoInstance ? instanceName : "")});
        }}
";
        }


        private string GetUpsertMethodPostCode(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area, string instanceName, string successMessage, bool onlyUpdatables)
        {
            List<PropertyListDto> selectProperties = entity.GetAllSelectProperties();

            List<PropertyListDto> customFillingProperties = entity.GetCustomFillingProperties();

            List<PropertyListDto> uniqueProperties = entity.GetUniqueProperties();
            if (onlyUpdatables)
            {
                selectProperties = selectProperties.OnlyUpdatables();
                customFillingProperties = customFillingProperties.OnlyUpdatables();
                uniqueProperties = uniqueProperties.OnlyUpdatables();
            }
            bool hasSelectViewData = selectProperties.Any();
            string customFillingPropertiesCode = string.Join("\n", customFillingProperties
                .ConvertAll(a => $@"{instanceName}.{a.Name} = {a.EntityRelation.FillingCode};"));
            string routingVariablesRedirectionCode = entity.GetPostMethodRoutingVariablesRediretionCode(instanceName);
            //            string middleRelationsCheckCode = string.Join("\n",
            //                middleRelations.ConvertAll(a =>
            //                $@"{instanceName}.{a.MiddleEntity!.SingularName}Ids = {instanceName}.{a.MiddleEntity!.SingularName}Ids
            //                                                                             .Where(a=>a>0)
            //                                                                             .ToList();
            //"));
            string enumName = entity.GetUpsertMethodReturnEnumName(uniqueProperties.Count);
            return $@"
            {customFillingPropertiesCode}

			if (!ModelState.IsValid)
			{{
                {(hasSelectViewData ? "await GetViewDatas();" : "")}
                return View({instanceName});
            }}
			{enumName} result = await _{entity.Entity.ServiceName!.ToFirstCharLower()}.{instanceName.ToFirstCharUpper()}{entity.Entity.SingularName}({instanceName});
    			
            #region handling different types

            switch (result)
			{{
                {(uniqueProperties.Count() <= 1 ?
                GetBaseChangeEntityResultCases(entity, area, instanceName, routingVariablesRedirectionCode, successMessage, uniqueProperties.FirstOrDefault()) :
                GetCustomChangeEntityResultCases(entity, area, instanceName, routingVariablesRedirectionCode
                , enumName, successMessage, uniqueProperties))}
			}}

            #endregion

            {(hasSelectViewData ? "await GetViewDatas();" : "")}
			return View({instanceName});";
        }

        private string GetCustomChangeEntityResultCases(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area, string instanceName,
            string routingVariablesRedirectionCode
            , string enumName, string successMessage, List<PropertyListDto> uniqueProperties)
       => $@"
				case {enumName}.Success:
                    {{
                        TempData[SuccessMessage] = $""{{_sharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"")}} {{_sharedLocalizer.GetString(""{successMessage}"")}}"";
                        return RedirectToAction(""Index"", ""{entity.Entity.SingularName}"", new {{ Area = ""{area.ProjectAreaTitle}"",{routingVariablesRedirectionCode} }});
                    }}

				case {enumName}.NotFound:
                    {{
                        TempData[ErrorMessage] = $""{{_sharedLocalizer.GetString(""Invalid Request."")}}"";
                        return NotFound();
                    }}
        
                {string.Join("\n",
                 uniqueProperties
                 .ConvertAll(a => GetSingleExistCaseCode(entity, a, enumName, instanceName, false)))}
";

        private string GetSingleExistCaseCode(EntityFullInformationDto entity, PropertyListDto property, string enumName, string instanceName, bool defaultExists)
            => $@"
				case {enumName}.{(defaultExists ? "Exists" : $"{property.Name}Exists")}:
                {{
                        TempData[ErrorMessage] = $""{{_sharedLocalizer.GetString(""A"")}} {{_sharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"")}} {{_sharedLocalizer.GetString(""Exists With This"")}} {{_localizer.GetString(""{property.Name}"")}} {{{instanceName}.{property.Name}}}"";
                        break;
                }}";


        private string GetBaseChangeEntityResultCases(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area, string instanceName,
            string routingVariablesRedirectionCode, string successMessage, PropertyListDto? uniqueProperty)
        {
            string existsCode = $@"
				case BaseChangeEntityResult.Exists:
				{{
					TempData[ErrorMessage] = $""{{_sharedLocalizer.GetString(""Item Exists."")}}"";
					break;
				}}";
            if (uniqueProperty is not null)
                existsCode = GetSingleExistCaseCode(entity, uniqueProperty, "BaseChangeEntityResult", instanceName, true);
            return $@"
				case BaseChangeEntityResult.Success:
				{{
					TempData[SuccessMessage] = $""{{_sharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"")}} {{_sharedLocalizer.GetString(""{successMessage}"")}}"";
					return RedirectToAction(""Index"", ""{entity.Entity.SingularName}"", new {{ Area = ""{area.ProjectAreaTitle}"",{routingVariablesRedirectionCode} }});
				}}

                case BaseChangeEntityResult.NotFound:
                {{
                        TempData[ErrorMessage] = $""{{_sharedLocalizer.GetString(""Invalid Request."")}}"";
                        return NotFound();
                }}
				{existsCode}

";
        }


        #endregion

        #region update method

        private string GetUpdateMethodCode(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            bool hasSelectViewData = entity
                .GetAllSelectProperties()
                .OnlyUpdatables()
                .Any();
            string instanceName = "update";
            return $@"        #region update

		[HttpGet]
		public async Task<IActionResult> Update({entity.Entity.GetDataType()} id)
		{{
			Update{entity.Entity.SingularName}Dto? {entity.Entity.SingularName.ToFirstCharLower()}Information = await _{entity.Entity.ServiceName.ToFirstCharLower()}.Get{entity.Entity.SingularName}Information(id);
			if ({entity.Entity.SingularName.ToFirstCharLower()}Information is null) return NotFound();
			{(hasSelectViewData ? "await GetViewDatas();" : "")}
            return View({entity.Entity.SingularName.ToFirstCharLower()}Information);
		}}

		[HttpPost]
		public async Task<IActionResult> Update(Update{entity.Entity.SingularName}Dto {instanceName})
		{{
			{GetUpsertMethodPostCode(entity, area, instanceName, "Updated Successfully.", true)}
		}}

		#endregion
";
        }

        #endregion

        #region view datas methods


        private string GetViewDatasMethodsCode(EntityFullInformationDto entity)
        {
            List<ViewDataListDto> allViewDatas = new List<ViewDataListDto>();
            List<ViewDataListDto> relationsWithSelectViewDatas = entity
                .GetAllSelectProperties()
                .Select(a => new ViewDataListDto()
                {
                    Name = a.EntityRelation!.SecondaryEntity.PluralName,
                    ServiceName = a.EntityRelation.SecondaryEntity.ServiceName,
                    IsRequired = a.IsRequired
                }).ToList();

            List<ViewDataListDto> middleRelationsViewDatas = entity
                .GetAllMiddleRelations()
                .Select(a => new ViewDataListDto()
                {
                    Name = a.MiddleEntityTitle,
                    ServiceName = a.MiddleEntityServiceName,
                    IsRequired = true
                }).ToList();
            allViewDatas.AddRange(relationsWithSelectViewDatas);
            allViewDatas.AddRange(middleRelationsViewDatas);
            allViewDatas = allViewDatas
                .DistinctBy(a => a.Name)
                .ToList();
            if (!allViewDatas.Any()) return string.Empty;
            string viewDatasMethodsCode = allViewDatas.GetViewDatasMethodsCodes();
            string viewDatasMethodsNames = allViewDatas.GetViewDatasMethodsNames();

            return $@"
        #region view datas

        async Task GetViewDatas()
        {{
{viewDatasMethodsNames}
        }}
        
        {viewDatasMethodsCode}
        
        #endregion
";
        }


        #endregion

        #region delete method

        private string GetDeleteMethodCode(EntityFullInformationDto entity, EntitySelectedProjectAreaListDto area)
        {
            string instanceName = $"{entity.Entity.SingularName.ToFirstCharLower()}Information";
            string routingVariablesRedirectionCode = entity.GetPostMethodRoutingVariablesRediretionCode(instanceName);
            return $@"        #region delete

		[HttpGet]
		public async Task<IActionResult> Delete({entity.Entity.GetDataType()} id)
		{{
            {entity.Entity.SingularName}ListDto? {instanceName} = await _{entity.Entity.ServiceName.ToFirstCharLower()}.GetSingle{entity.Entity.SingularName}Information(id);
            if ({instanceName} is null) return NotFound();
			BaseChangeEntityResult result = await _{entity.Entity.ServiceName.ToFirstCharLower()}.Delete{entity.Entity.SingularName}(id);
			switch (result)
			{{
				case BaseChangeEntityResult.Success:
					{{
						TempData[SuccessMessage] = $""{{_sharedEntitiesLocalizer.GetString(""{entity.Entity.SingularName}"")}} {{_sharedLocalizer.GetString(""Deleted Successfully."")}}"";
					return RedirectToAction(""Index"", ""{entity.Entity.SingularName}"", new {{ Area = ""{area.ProjectAreaTitle}"",{routingVariablesRedirectionCode} }});
					}}
			}}
			return NotFound();
		}}

		[HttpGet]
		public async Task<IActionResult> DeleteRange(List<{entity.Entity.GetDataType()}> ids)
		{{
			if (!ids.Distinct().Any())
			{{
				TempData[ErrorMessage] = $""{{_sharedLocalizer.GetString(""Please AtLeast Choose One Item."")}}"";
				return RedirectToAction(""Index"", ""{entity.Entity.SingularName}"", new {{ Area = ""{area.ProjectAreaTitle}"" }});
			}}
			await _{entity.Entity.ServiceName.ToFirstCharLower()}.Delete{entity.Entity.SingularName}(ids);
			TempData[SuccessMessage] =$""{{_sharedEntitiesLocalizer.GetString(""{entity.Entity.PluralName}"")}} {{_sharedLocalizer.GetString(""Deleted Successfully."")}}"";
			return RedirectToAction(""Index"", ""{entity.Entity.SingularName}"", new {{ Area = ""{area.ProjectAreaTitle}"" }});
		}}

		#endregion
";
        }


        #endregion
    }
}
