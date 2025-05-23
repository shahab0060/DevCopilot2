using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Properties;
using System.Text;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class ApiControllerGeneratorService : IApiControllerGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public ApiControllerGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public CreateFileResultDto Generate(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            if (!entity.Entity.EntitySelectedProjectAreasList.Any(a => a.HasApi))
                return new CreateFileResultDto();
            GenerateFileDto generateFile = new GenerateFileDto()
            {
                FileNameWithExtension = $"{entity.Entity.SingularName}Controller.cs",
                Override = generate.OverrideFiles,
                Path = GetFilePath(entity)
            };
            generateFile.Code = $@"{GetUsings(entity)}

namespace {GetNameSpace(entity)}
{{
	{$@"//[PermissionChecker(""{entity.Entity.SingularName}Management"")]"}
    public class {entity.Entity.SingularName}Controller : BaseApiController
    {{
        
{GetConstructorCode(entity)}

{GetMethods(entity)}

    }}
}}
";
            return _baseGeneratorService.GenerateFile(generateFile);
        }

        string GetFilePath(EntityFullInformationDto entity)
       => $@"{entity.Project.Location}\{entity.Project.EnglishName}.API\Controllers\{entity.Entity.FolderName}";

        public string GetNameSpace(EntityFullInformationDto entity)
       => $@"{entity.Project.EnglishName}.API.Controllers.{entity.Entity.FolderName}";

        #region usings

        string GetUsings(EntityFullInformationDto entity)
        {
            List<string> usings = [.. _baseGeneratorService.GetBaseUsings(),
            "System.Text",
            $"{entity.Project.EnglishName}.Core.Extensions.BasicExtensions",
            $"{entity.Project.EnglishName}.Core.Services.Interfaces",
            $"{entity.Project.EnglishName}.Domain.DTOs.{ entity.Entity.FolderName}",
            $"{entity.Project.EnglishName}.Domain.Enums.Common",
            $"{entity.Project.EnglishName}.API.PresentationExtensions",
            $"{entity.Project.EnglishName}.Domain.Enums.{entity.Entity.FolderName}",
            $"{entity.Project.EnglishName}.Api.Controllers.Base",
            $"{entity.Project.EnglishName}.Api",
            "Microsoft.AspNetCore.Mvc",
            "HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute",
            "HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute",
            $"{entity.Project.EnglishName}.Core.Exporters",
            ];
            return usings.GetUsings(GetNameSpace(entity));
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

        string GetMethods(EntityFullInformationDto entity)
        {
            StringBuilder methodsStringBuilder = new StringBuilder();
            if (entity.Entity.EntitySelectedProjectAreasList.Any(d => d.HasApi && d.HasIndex))
            {
                methodsStringBuilder.AppendLine(GetIndexMethodCode(entity));
                methodsStringBuilder.AppendLine(GetDetailMethodCode(entity));
            }
            if (entity.Entity.EntitySelectedProjectAreasList.Any(d => d.HasApi && d.HasCreate))
                methodsStringBuilder.AppendLine(GetCreateMethodCode(entity));
            if (entity.Entity.EntitySelectedProjectAreasList.Any(d => d.HasApi && d.HasUpdate))
                methodsStringBuilder.AppendLine(GetUpdateMethodCode(entity));
            if (entity.Entity.EntitySelectedProjectAreasList.Any(d => d.HasApi && d.HasDelete))
                methodsStringBuilder.AppendLine(GetDeleteMethodCode(entity));
            return methodsStringBuilder.ToString();
        }

        #endregion

        #region index method

        private string GetIndexMethodCode(EntityFullInformationDto entity)
        {
            return $@"        #region index

        [HttpGet(""list"")]
        public async Task<IActionResult> Index([FromQuery]Filter{entity.Entity.PluralName}Dto filter)
        {{
            return Ok(await _{entity.Entity.ServiceName!.ToFirstCharLower()}.Filter{entity.Entity.PluralName}(filter));
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
		
        [HttpGet(""{{id}}"")]
		public async Task<IActionResult>Detail({entity.Entity.GetDataType()} id)
		{{
			{entity.Entity.SingularName}ListDto? {entity.Entity.SingularName.ToFirstCharLower()}Information = await _{entity.Entity.ServiceName!.ToFirstCharLower()}.GetSingle{entity.Entity.SingularName}Information(id);
			if ({entity.Entity.SingularName.ToFirstCharLower()}Information is null)return NotFound();
            return Ok({entity.Entity.SingularName.ToFirstCharLower()}Information);	
		}}

		#endregion
";
        }

        #endregion

        #region create method

        private string GetCreateMethodCode(EntityFullInformationDto entity)
        {
            string instanceName = "create";
            return $@"        #region create

		[HttpPost]
		public async Task<IActionResult> Create(Create{entity.Entity.SingularName}Dto {instanceName})
		{{
            {GetUpsertMethodPostCode(entity, instanceName, "Created Successfully.", false)}
        }}

		#endregion
";
        }


        private string GetUpsertMethodPostCode(EntityFullInformationDto entity, string instanceName, string instanceTitle, bool onlyUpdatables)
        {
            List<PropertyListDto> selectProperties = entity.GetSelectProperties();
            List<PropertyListDto> routingProperties = entity.GetRelationsInRouting();
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
            string routingVariablesRedirectionCode = string.Join("\n", routingProperties
                .ConvertAll(a => $@"{a.Name.ToFirstCharLower()}={instanceName}.{a.Name},"));
            routingVariablesRedirectionCode.ReplaceLastOccurrence(',', ' ');
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
			    return ReturnErrors(ModelState);
			{enumName} result = await _{entity.Entity.ServiceName!.ToFirstCharLower()}.{instanceName.ToFirstCharUpper()}{entity.Entity.SingularName}({instanceName});
    			
            #region handling different types

            switch (result)
			{{
                {(uniqueProperties.Count() <= 1 ?
                GetBaseChangeEntityResultCases(entity, instanceName, routingVariablesRedirectionCode, uniqueProperties.FirstOrDefault()) :
                GetCustomChangeEntityResultCases(entity, instanceTitle, instanceName, routingVariablesRedirectionCode
                , enumName, uniqueProperties))}
			}}

            #endregion

			return NotFound();";
        }

        private string GetCustomChangeEntityResultCases(EntityFullInformationDto entity, string instanceTitle, string instanceName,
       string routingVariablesRedirectionCode
       , string enumName, List<PropertyListDto> uniqueProperties)
  => $@"
				case {enumName}.Success:
                    {{
                        string message = $""{entity.Entity.SingularName} {instanceTitle}"";
                        return Ok(message);
                    }}

				case {enumName}.NotFound:
                    {{
                        string message = $""Invalid Request."";
                        return NotFound(message);
                    }}
        
                {string.Join("\n",
            uniqueProperties
            .ConvertAll(a => GetSingleExistCaseCode(entity, a, enumName, instanceName, false)))}
";

        private string GetSingleExistCaseCode(EntityFullInformationDto entity, PropertyListDto property, string enumName, string instanceName, bool defaultExists)
            => $@"
				case {enumName}.{(defaultExists ? "Exists" : $"{property.Name}Exists")}:
                    {{
                        string message = $""A {entity.Entity.SingularName} Exists With This {property.Name} {{{instanceName}.{property.Name}}}"";
                        return ReturnError(message);
                    }}";


        private string GetBaseChangeEntityResultCases(EntityFullInformationDto entity, string instanceName,
            string routingVariablesRedirectionCode, PropertyListDto? uniqueProperty)
        {
            string existsCode = $@"
				case BaseChangeEntityResult.Exists:
				{{
					string message = $""Item Exists."";
					return ReturnError(message);
				}}";
            if (uniqueProperty is not null)
                existsCode = GetSingleExistCaseCode(entity, uniqueProperty, "BaseChangeEntityResult", instanceName, true);
            return $@"
				case BaseChangeEntityResult.Success:
				{{
					string message = $""{entity.Entity.SingularName} {instanceName}"";
					return Ok(message);
				}}

				case BaseChangeEntityResult.NotFound:
				{{
					string message = $""Invalid Request."";
					return NotFound(message);
				}}
                
                {existsCode}";
        }

        #endregion

        #region update method

        private string GetUpdateMethodCode(EntityFullInformationDto entity)
        {
            bool hasSelectViewData = entity
                .GetSelectProperties()
                .OnlyUpdatables()
                .Any();
            string instanceName = "update";
            return $@"        #region update

		[HttpPut]
		public async Task<IActionResult> Update(Update{entity.Entity.SingularName}Dto {instanceName})
		{{
			{GetUpsertMethodPostCode(entity, instanceName, "Updated Successfully.", true)}
		}}

		#endregion
";
        }

        #endregion

        #region delete method


        private string GetDeleteMethodCode(EntityFullInformationDto entity)
        {
            return $@"        #region delete

		[HttpDelete]
		public async Task<IActionResult> Delete({entity.Entity.GetDataType()} id)
		{{
			BaseChangeEntityResult result = await _{entity.Entity.ServiceName.ToFirstCharLower()}.Delete{entity.Entity.SingularName}(id);
			switch (result)
			{{
				case BaseChangeEntityResult.Success:
					{{
						string message = $""{entity.Entity.SingularName} Deleted Successfully."";
						return Ok(message);
					}}
			}}
			return NotFound();
		}}

		[HttpDelete(""range"")]
		public async Task<IActionResult> DeleteRange(List<{entity.Entity.GetDataType()}> ids)
		{{
			if (!ids.Distinct().Any())
			{{
				string deleteMessage = $""Please AtLeast Choose One Item."";
				return ReturnError(deleteMessage);
			}}
			await _{entity.Entity.ServiceName.ToFirstCharLower()}.Delete{entity.Entity.SingularName}(ids);
			string message = $""{entity.Entity.PluralName} Deleted Successfully."";
			return Ok(message);
		}}

		#endregion";
        }


        #endregion
    }
}
