using DevCopilot2.Core.Extensions.AdvanceExtensions.Generators;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.New.Enums.DTOs;
using DevCopilot2.Core.Services.Interfaces.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.Enums.DataTypes;
using System.Text;

namespace DevCopilot2.Core.Services.Classes.Generators
{
    public class MapperGeneratorService : IMapperGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public MapperGeneratorService(IBaseGeneratorService baseGeneratorService)
        {
            this._baseGeneratorService = baseGeneratorService;
        }

        #endregion

        public CreateFileResultDto Generate(GenerateEntityDto generate, EntityFullInformationDto entity)
        {
            GenerateFileDto generateFile = new GenerateFileDto()
            {
                FileNameWithExtension = $"{entity.Entity.SingularName}Mappers.cs",
                Override = generate.OverrideFiles,
                Path = GetFilePath(entity)
            };
            generateFile.Code = $@"{GetUsings(entity)}

namespace {GetNameSpace(entity)}
{{
    public static class {entity.Entity.SingularName}Mappers
    {{
{GetToDtoMapperCode(entity)}

{GetToUpdateDtoMapperCode(entity)}

{GetToCreateDtoMapperCode(entity)}

{GetToComboDtoMapperCode(entity)}

{GetToCreateModelMapperCode(entity)}

{GetToUpdateModelMapperCode(entity)}

    }}
}}
";
            return _baseGeneratorService.GenerateFile(generateFile);
        }
        string GetFilePath(EntityFullInformationDto entity)
       => $@"{entity.Project.Location}\{entity.Project.EnglishName}.Core\Mappers\{entity.Entity.FolderName}";

        public string GetNameSpace(EntityFullInformationDto entity)
       => $@"{entity.Project.EnglishName}.Core.Mappers.{entity.Entity.FolderName}";

        #region usings

        string GetUsings(EntityFullInformationDto entity)
        {
            List<string> usings = [.. _baseGeneratorService.GetBaseUsings()];
            usings.Add($"{entity.Project.EnglishName}.Domain.Entities.{entity.Entity.FolderName}");
            usings.Add($"{entity.Project.EnglishName}.Domain.DTOs.{entity.Entity.FolderName}");
            usings.Add($"{entity.Project.EnglishName}.Domain.DTOs.Common");
            usings.Add($"{entity.Project.EnglishName}.Core.Extensions.BasicExtensions");
            usings.Add($"{entity.Project.EnglishName}.Core.Utils");
            usings.Add($"{entity.Project.EnglishName}.Core.Security");
            if (entity.HasAnyFile())
                usings.Add($"{entity.Project.EnglishName}.Core.MediasInformation.{entity.Entity.FolderName}");

            List<EntityFullInformationDto> allEntities = entity.GetAllEntities();

            foreach (var subEntity in allEntities)
            {
                usings.AddRange(subEntity.GetEnumUsings());
                usings.AddRange($"{entity.Project.EnglishName}.Domain.DTOs.{subEntity.Entity.FolderName}");
                if (subEntity.HasAnyFile())
                    usings.Add($"{entity.Project.EnglishName}.Core.MediasInformation.{subEntity.Entity.FolderName}");
            }
            return usings.GetUsings(GetNameSpace(entity));
        }

        #endregion

        #region list dto

        string GetToDtoMapperCode(EntityFullInformationDto entity)
        {
            var instanceName = entity.Entity.SingularName.ToFirstCharLower();
            return $@"        #region to dto

        public static IQueryable<{entity.Entity.SingularName}ListDto>ToDto(this IQueryable<{entity.Entity.SingularName}> query)
                    => query.Select({instanceName} => new {entity.Entity.SingularName}ListDto()
                    {{
{(GetToDtoPropertiesMappingCode(entity, instanceName, OperationTypeEnums.Detail))}
                    }});

        #endregion";
        }

        string GetToDtoPropertiesMappingCode(EntityFullInformationDto entity, string instanceName, OperationTypeEnums type)
        {
            List<PropertyListDto> entityProperties = type switch
            {
                OperationTypeEnums.Detail => entity.GetEntityListDtoProperties(),
                OperationTypeEnums.Update => entity.GetEntityUpdatableProperties(),
                OperationTypeEnums.Create => entity.GetEntityUpdatablePropertiesForToCreateDtoMapper(),
                _ => new List<PropertyListDto>()
            };

            List<PropertyListDto> middleRelationProperties = entity.GetMiddleRelationListDtoProperties();
            List<string> fieldInRelationEntitiesCode = new List<string>();
            foreach (EntityFullInformationDto relationEntity in entity.FieldInRelationEntities)
            {
                string code = GetSingleFieldInRelationEntityPropertiesListMappingCode(relationEntity, entity.Entity.SingularName.ToFirstCharLower(), type);
                fieldInRelationEntitiesCode.Add(code);
            }
            StringBuilder entityPropertiesCodeStringBuilder = new StringBuilder();
            foreach (PropertyListDto property in entityProperties)
            {
                entityPropertiesCodeStringBuilder.AppendLine(GetSingleEntityPropertyListMappingCode(property, instanceName));
            }
            string entityPropertiesCode = entityPropertiesCodeStringBuilder.ToString();
            string middleRelationPropertiesCode = string.Join("\n",
                middleRelationProperties
                .ConvertAll(a => GetSingleMiddleEntityPropertyListMappingCode(a, instanceName)));
            string fieldInRelationsPropertiesCode = string.Join('\n', fieldInRelationEntitiesCode
                .ConvertAll(a => a));
            StringBuilder basePropertiesMappingStringBuilder = new StringBuilder();
            if (type != OperationTypeEnums.Create)
                basePropertiesMappingStringBuilder.AppendLine($"                        Id = {instanceName}.Id,");
            if (type == OperationTypeEnums.Detail)
                basePropertiesMappingStringBuilder.AppendLine($@"                        LatestEditDate = {instanceName}.LatestEditDate,
                        CreateDate = {instanceName}.CreateDate,
                        EditCounts = {instanceName}.EditCounts,");
            return $@"
{basePropertiesMappingStringBuilder.ToString()}
{entityPropertiesCode}
{middleRelationPropertiesCode}
{fieldInRelationsPropertiesCode}";
        }

        string GetSingleEntityPropertyListMappingCode(PropertyListDto property, string instanceName)
        {
            if (property.EntityRelation is not null)
            {
                if (property.Name == property.NameInDb)
                    return $@"                        {property.Name} = {instanceName}.{property.NameInDb},";
                return $@"                        {property.Name} = {instanceName}.{property.NameInDb.GetRelationPropertyName()}.{property.EntityRelation.SecondaryEntityTitleProperty.Name},";
            }
            return $@"                        {property.Name} = {instanceName}.{property.Name},";
        }

        string GetSingleMiddleEntityPropertyListMappingCode(PropertyListDto property, string instanceName)
        {
            return $@"                        {property.Name} = {instanceName}.{property.EntityRelation!.PrimaryPropertyEntityPluralTitle}
                        .Select(d => d.{property.EntityRelation.MiddleEntityTitle}.Id)
                        .ToList(),";
        }

        string GetSingleFieldInRelationEntityPropertiesListMappingCode(EntityFullInformationDto entity,
            string instanceName, OperationTypeEnums type)
        {
            string objectName = type switch
            {
                OperationTypeEnums.Detail => $"{entity.Entity.SingularName}ListDto",
                OperationTypeEnums.Update => $"Update{entity.Entity.SingularName}Dto",
                OperationTypeEnums.Create => $"Create{entity.Entity.SingularName}Dto",
                _ => "",
            };
            string newInstanceName = entity.Entity.SingularName.ToFirstCharLower();
            string proeprtiesCode = GetToDtoPropertiesMappingCode(entity, newInstanceName, type);
            return $@"                        {entity.Entity.GetFieldRelationEntityPropertyNameInListDto()} = {instanceName}.{entity.Entity.PluralName}{(type==OperationTypeEnums.Create?"List":"")}
                        .Select({newInstanceName} => new {objectName}()
                        {{
                                    {proeprtiesCode}
                        }})
                        .ToList(),";
        }

        #endregion

        #region to update dto

        string GetToUpdateDtoMapperCode(EntityFullInformationDto entity)
        {
            var instanceName = entity.Entity.SingularName.ToFirstCharLower();
            return $@"        #region to update dto

        public static IQueryable<Update{entity.Entity.SingularName}Dto>ToUpdateDto(this IQueryable<{entity.Entity.SingularName}> query)
                    => query.Select({instanceName} => new Update{entity.Entity.SingularName}Dto()
                    {{
    {GetToDtoPropertiesMappingCode(entity, instanceName, OperationTypeEnums.Update)}
                    }});

        #endregion";
        }

        #endregion

        #region to create dto

        string GetToCreateDtoMapperCode(EntityFullInformationDto entity)
        {
            if (!entity.PropertiesHaveFieldInRelation()) return string.Empty;
            var instanceName = entity.Entity.SingularName.ToFirstCharLower();
            return $@"        #region to create dto

        public static List<Create{entity.Entity.SingularName}Dto>ToCreateDto(this IEnumerable<Update{entity.Entity.SingularName}Dto> {entity.Entity.PluralName.ToFirstCharLower()})
                    =>  {entity.Entity.PluralName.ToFirstCharLower()}.Select({instanceName} => new Create{entity.Entity.SingularName}Dto()
                    {{
    {GetToDtoPropertiesMappingCode(entity, instanceName, OperationTypeEnums.Create)}
                    }}).ToList();

        #endregion";
        }

        #endregion

        #region to combo dto

        string GetToComboDtoMapperCode(EntityFullInformationDto entity)
        {
            var instanceName = entity.Entity.SingularName.ToFirstCharLower();
            PropertyListDto titleProperty = entity.GetTitleProperty();
            return $@"        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<{entity.Entity.SingularName}> query)
			    => query.Select({instanceName} => new ComboDto()
			{{
            Title = {instanceName}.{titleProperty.Name}{(titleProperty.DataType == DataTypeEnum.String ? "" : ".ToString()")},
            Value = {instanceName}.Id.ToString()
            }});

        #endregion";
        }

        #endregion

        #region to create model

        string GetToCreateModelMapperCode(EntityFullInformationDto entity)
        {
            string properties = string.Join("\n", entity.Properties
                .ConvertAll(a => a.GetCreatePropertyMapperCode(entity)));
            bool isAsync = entity
                .HasAnyFile();
            string methodReturnType = isAsync ?
                $@"public static async Task<{entity.Entity.SingularName}>"
                : $@"public static {entity.Entity.SingularName}";
            return $@"        #region to create model

        {methodReturnType} ToModel(this Create{entity.Entity.SingularName}Dto create)
				=> new {entity.Entity.SingularName}()
				{{
{properties}
				}};

        #endregion";
        }

        #endregion

        #region to update model

        string GetToUpdateModelMapperCode(EntityFullInformationDto entity)
        {
            string properties = string.Join("\n", entity.GetEntityUpdatableProperties()
                .ConvertAll(a => a.GetUpdatePropertyMapperCode(entity)));
            bool isAsync = entity
                .HasAnyUpdatableFile();
            string methodReturnType = isAsync ?
                $@"public static async Task<{entity.Entity.SingularName}>"
                : $@"public static {entity.Entity.SingularName}";
            return $@"        #region to update model

        {methodReturnType} ToModel(this {entity.Entity.SingularName} {entity.Entity.SingularName.ToFirstCharLower()}, Update{entity.Entity.SingularName}Dto update)
        {{
{properties}
            return {entity.Entity.SingularName.ToFirstCharLower()};
        }}

        #endregion";
        }

        #endregion
    }
}
