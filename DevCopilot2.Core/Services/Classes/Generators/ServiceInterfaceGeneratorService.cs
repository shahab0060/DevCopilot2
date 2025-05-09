using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using System.Text;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class ServiceInterfaceGeneratorService : IServiceInterfaceGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public ServiceInterfaceGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        #region generate

        public CreateFileResultDto Generate(GenerateEntityDto generate, List<EntityFullInformationDto> entities)
        {
            EntityFullInformationDto firstEntity = entities[0];
            GenerateFileDto generateFile = new GenerateFileDto()
            {
                FileNameWithExtension = $"I{firstEntity.Entity.ServiceName}.cs",
                Override = generate.OverrideFiles,
                Path = GetFilePath(firstEntity)
            };
            generateFile.Code = $@"{GetUsings(entities)}

namespace {GetNameSpace(firstEntity)}
{{
    public interface I{firstEntity.Entity.ServiceName} : IService
    {{
{GetAllInterfacesCode(entities)}
    }}
}}
";
            return _baseGeneratorService.GenerateFile(generateFile);
        }

        #endregion

        string GetFilePath(EntityFullInformationDto entity)
       => $@"{entity.Project.Location}\{entity.Project.EnglishName}.Core\Services\Interfaces";

        public string GetNameSpace(EntityFullInformationDto entity)
       => $@"{entity.Project.EnglishName}.Core.Services.Interfaces";

        #region usings

        string GetUsings(List<EntityFullInformationDto> entities)
        {
            EntityFullInformationDto firstEntity = entities[0];
            List<string> usings = [.. _baseGeneratorService.GetBaseUsings()];
            usings.Add($"{firstEntity.Project.EnglishName}.Domain.IRepository");
            usings.Add($"{firstEntity.Project.EnglishName}.Domain.DTOs.Paging");
            usings.Add($"{firstEntity.Project.EnglishName}.Domain.DTOs.Common");
            usings.Add($"{firstEntity.Project.EnglishName}.Domain.Enums.Common");
            usings.Add($"{firstEntity.Project.EnglishName}.Core.Extensions.BasicExtensions");
            usings.Add($"{firstEntity.Project.EnglishName}.Core.Services.Interfaces");
            usings.Add($"Microsoft.EntityFrameworkCore");
            foreach (var entity in entities)
            {
                usings.AddRange(GetSingleEntityCustomUsings(entity));
            }

            //these usings are for service usings
            //we can remove some of them like mapper for interface usings
            return usings.GetUsings(GetNameSpace(firstEntity));
        }

        List<string> GetSingleEntityCustomUsings(EntityFullInformationDto entity)
        {
            List<string> usings =
            [
                $"{entity.Project.EnglishName}.Domain.Enums.{entity.Entity.FolderName}",
                $"{entity.Project.EnglishName}.Core.Mappers.{entity.Entity.FolderName}",
            ];

            List<EntityFullInformationDto> allEntities = entity.GetAllEntities();

            foreach (var singleEntity in allEntities)
            {
                usings.Add($"{singleEntity.Project.EnglishName}.Domain.DTOs.{singleEntity.Entity.FolderName}");
            }
            return usings;
        }

        #endregion

        #region interfaces code

        string GetAllInterfacesCode(List<EntityFullInformationDto> entities)
        {
            List<IGrouping<string, EntityFullInformationDto>> groupedEntitiesByFolderName = entities
                .GroupBy(a => a.Entity.FolderName)
                .ToList();
            return string.Join("\n", groupedEntitiesByFolderName
                .ConvertAll(GetSingleFolderRegion));
        }

        string GetSingleFolderRegion(IGrouping<string, EntityFullInformationDto> group)
        {
            StringBuilder entitiesInterfacesCodeStringBuilder = new StringBuilder();
            foreach (var entity in group)
            {
                entitiesInterfacesCodeStringBuilder.AppendLine(GetSingleEntityInterfacesCode(entity));
            }

            return $@"
        #region {group.Key}

        {entitiesInterfacesCodeStringBuilder}

        #endregion";
        }


        string GetSingleEntityInterfacesCode(EntityFullInformationDto entity)
        {
            string createEnumName = entity.GetCreateMethodReturnEnumName();
            string updateEnumName = entity.GetUpdateMethodReturnEnumName();
            StringBuilder methodsStringBuilder = new StringBuilder();
            bool entityIsFieldInRelation = entity.HasFieldInRelationProperty();
            if (entity.Entity.EntitySelectedProjectAreasList.Any(a => a.HasCreate) || entityIsFieldInRelation)
                methodsStringBuilder.AppendLine($@"		Task<{createEnumName}> Create{entity.Entity.SingularName}(Create{entity.Entity.SingularName}Dto create);");
            if (entity.Entity.EntitySelectedProjectAreasList.Any(a => a.HasUpdate) || entityIsFieldInRelation)
            {
                methodsStringBuilder.AppendLine($@"		Task<Update{entity.Entity.SingularName}Dto?> Get{entity.Entity.SingularName}Information({entity.Entity.GetDataType()} {entity.Entity.SingularName.ToFirstCharLower()}Id);");
                methodsStringBuilder.AppendLine($@"		Task<{updateEnumName}> Update{entity.Entity.SingularName}(Update{entity.Entity.SingularName}Dto update);");
            }
            if (entity.Entity.EntitySelectedProjectAreasList.Any(a => a.HasDelete) || entityIsFieldInRelation)
            {
                methodsStringBuilder.AppendLine($@"		Task<BaseChangeEntityResult> Delete{entity.Entity.SingularName}({entity.Entity.GetDataType()} {entity.Entity.SingularName.ToFirstCharLower()}Id);");
                methodsStringBuilder.AppendLine($@"		Task Delete{entity.Entity.SingularName}(List<{entity.Entity.GetDataType()}> {entity.Entity.PluralName.ToFirstCharLower()}Id);");
            }
            return $@"  
        
        #region {entity.Entity.PluralName.AddSpacesBetweenCapitals().ToLower()}

		Task<Filter{entity.Entity.PluralName}Dto> Filter{entity.Entity.PluralName}(Filter{entity.Entity.PluralName}Dto filter);
        Task<List<ComboDto>> Get{entity.Entity.PluralName}AsCombo(Filter{entity.Entity.PluralName}Dto filter);
		Task<{entity.Entity.SingularName}ListDto?> GetSingle{entity.Entity.SingularName}Information({entity.Entity.GetDataType()} {entity.Entity.SingularName.ToFirstCharLower()}Id);
        
        {methodsStringBuilder.ToString()}        

        #endregion";
        }

        #endregion
    }
}
