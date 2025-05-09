using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Properties;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class DtoGeneratorService : IDtoGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public DtoGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public CreateFileResultDto Generate(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            GenerateFileDto generateFile = new GenerateFileDto()
            {
                FileNameWithExtension = $"{entity.Entity.SingularName}DTOs.cs",
                Override = generate.OverrideFiles,
                Path = GetFilePath(entity)
            };
            generateFile.Code = $@"{GetUsings(entity)}

namespace {GetNameSpace(entity)}
{{

{GetFilterDtoCode(entity)}

{GetListDtoCode(entity)}

{GetBaseUpsertDtoCode(entity)}

{GetCreateDtoCode(entity)}

{GetUpdateDtoCode(entity)}

}}
";
            return _baseGeneratorService.GenerateFile(generateFile);
        }
        string GetFilePath(EntityFullInformationDto entity)
       => $@"{entity.Project.Location}\{entity.Project.EnglishName}.Domain\DTOs\{entity.Entity.FolderName}";

        public string GetNameSpace(EntityFullInformationDto entity)
       => $@"{entity.Project.EnglishName}.Domain.DTOs.{entity.Entity.FolderName}";

        #region usings

        string GetUsings(EntityFullInformationDto entity)
        {
            List<string> usings = [.. _baseGeneratorService.GetBaseUsings()];
            usings.Add($"{entity.Project.EnglishName}.Domain.DTOs.Common");
            usings.Add($"{entity.Project.EnglishName}.Domain.Enums.{entity.Entity.FolderName}");
            usings.Add($"{entity.Project.EnglishName}.Domain.Attributes");
            if (entity.Project.UseResources)
                usings.Add($"{entity.Project.EnglishName}.Domain.Resources.DTOs.Common");
            if (!entity.Project.UseFluent)
                usings.Add($"System.ComponentModel.DataAnnotations");
            if (entity.HasAnyFile())
                usings.Add("Microsoft.AspNetCore.Http");
            usings.Add($"{entity.Project.EnglishName}.Domain.DTOs.Paging");
            usings.AddRange(entity.GetEnumUsings());
            foreach (var fieldInRelation in entity.FieldInRelationEntities)
            {
                usings.AddRange($"{fieldInRelation.Project.EnglishName}.Domain.DTOs.{fieldInRelation.Entity.FolderName}");
            }
            return usings.GetUsings(GetNameSpace(entity));
        }

        #endregion

        #region filter dto

        string GetFilterDtoCode(EntityFullInformationDto entity)
        {
            string filterPropertiesCode = string.Join("\n",
                entity.GetFilterProperties()
                .ConvertAll(a => $@"        [Display(Name = ""{a.Name}"")]
        public {a.GetNullableDataType()} {a.Name} {{ get; set; }}"));

            return $@"    public class Filter{entity.Entity.PluralName}Dto : BaseFilterDto
    {{
        #region properties

		public List<{entity.Entity.SingularName}ListDto> {entity.Entity.PluralName} {{ get; set; }} = new List<{entity.Entity.SingularName}ListDto>();

        public {entity.GetSortEntityEnumName()}? SortProperty {{ get; set; }}

        {filterPropertiesCode}

        #endregion

        #region methods

        public Filter{entity.Entity.PluralName}Dto  Set{entity.Entity.PluralName}(List<{entity.Entity.SingularName}ListDto> {entity.Entity.PluralName.ToFirstCharLower()})
		{{
			this.{entity.Entity.PluralName} = {entity.Entity.PluralName.ToFirstCharLower()};
			return this;
		}}

		public Filter{entity.Entity.PluralName}Dto  SetPaging(BasePaging paging)
		{{
			PageId = paging.PageId;
			AllEntitiesCount = paging.AllEntitiesCount;
			StartPage = paging.StartPage;
			EndPage = paging.EndPage;
			HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
			TakeEntity = paging.TakeEntity;
			SkipEntity = paging.SkipEntity;
			PageCount = paging.PageCount;
			return this;
		}}

		#endregion
    }}";
        }


        #endregion

        #region list dto

        string GetListDtoCode(EntityFullInformationDto entity)
        {
            return $@"    public class {entity.Entity.SingularName}ListDto : BaseListDto<{entity.Entity.GetDataType()}>
    {{
        {GetListDtoPropertiesCode(entity)}    
    }}";
        }

        List<PropertyListDto> GetListDtoProperties(EntityFullInformationDto entity)
        {
            List<PropertyListDto> mainEntityPropertiesInList = entity.GetEntityListDtoProperties();
            List<PropertyListDto> middleEntitiesProperties = entity.GetMiddleRelationListDtoProperties();
            List<PropertyListDto> fieldInRelationEntitiesProperties = entity.GetFieldInRelationListDtoProperties();
            //todo add usings for field inr elation entity dtos
            List<PropertyListDto> properties =
            [
                .. mainEntityPropertiesInList,
                .. middleEntitiesProperties,
                .. fieldInRelationEntitiesProperties,
            ];
            return properties;
        }
        string GetListDtoPropertiesCode(EntityFullInformationDto entity)
        {
            List<PropertyListDto> properties = GetListDtoProperties(entity);
            return string.Join("\n", properties
                .ConvertAll(a => a.GetSingleListDtoPropertyCode()));
        }



        #endregion

        #region base uspert dto

        public string GetBaseUpsertDtoCode(EntityFullInformationDto entity)
        {
            List<PropertyListDto> allProperties = entity.GetBaseUpsertDtoAllProperties();

            string propertiesCode = string.Join("\n",
                allProperties
                .ConvertAll(a => a.GetSingleUpsertPropertyText()));

            return $@"    public class BaseUpsert{entity.Entity.SingularName}Dto
    {{
{propertiesCode}
    }}";
        }

        #endregion

        #region create dto

        public string GetCreateDtoCode(EntityFullInformationDto entity)
        {
            List<PropertyListDto> allProperties = entity.GetCreateDtoAllProperties();

            string propertiesCode = string.Join("\n",
                allProperties
                .ConvertAll(a => a.GetSingleUpsertPropertyText()));

            return $@"    public class Create{entity.Entity.SingularName}Dto: BaseUpsert{entity.Entity.SingularName}Dto
    {{
{propertiesCode}
    }}";
        }

        #endregion

        #region update dto

        public string GetUpdateDtoCode(EntityFullInformationDto entity)
        {
            List<PropertyListDto> allProperties = entity.GetUpdateDtoAllProperties();

            string propertiesCode = string.Join("\n",
                allProperties
                .ConvertAll(a => a.GetSingleUpsertPropertyText()));

            return $@"    public class Update{entity.Entity.SingularName}Dto: BaseUpsert{entity.Entity.SingularName}Dto
    {{
{propertiesCode}
    }}";
        }

        #endregion
    }
}
