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
    public class ServiceGeneratorService : IServiceGeneratorService
    {
        #region constructor

        private readonly IBaseGeneratorService _baseGeneratorService;
        public ServiceGeneratorService(IBaseGeneratorService baseGeneratorService)
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
                FileNameWithExtension = $"{firstEntity.Entity.ServiceName}.cs",
                Override = generate.OverrideFiles,
                Path = GetFilePath(firstEntity)
            };
            generateFile.Code = $@"{GetUsings(entities)}

namespace {GetNameSpace(firstEntity)}
{{
    public class {firstEntity.Entity.ServiceName} : I{firstEntity.Entity.ServiceName}
    {{
{GetConstructorCode(entities)}

{GetAllServicesCode(entities)}
    }}
}}
";
            return _baseGeneratorService.GenerateFile(generateFile);
        }

        #endregion
        string GetFilePath(EntityFullInformationDto entity)
       => $@"{entity.Project.Location}\{entity.Project.EnglishName}.Core\Services\Classes";

        public string GetNameSpace(EntityFullInformationDto entity)
       => $@"{entity.Project.EnglishName}.Core.Services.Classes";

        #region usings

        string GetUsings(List<EntityFullInformationDto> entities)
        {
            EntityFullInformationDto firstEntity = entities[0];
            //List<EntityFullInformationDto> allEntities = firstEntity.GetAllEntities()
            //    .Where(a => a.Entity.ServiceName == firstEntity.Entity.ServiceName)
            //    .ToList();
            List<string> usings = [.. _baseGeneratorService.GetBaseUsings()];
            usings.Add($"{firstEntity.Project.EnglishName}.Domain.IRepository");
            usings.Add($"{firstEntity.Project.EnglishName}.Domain.DTOs.Paging");
            usings.Add($"{firstEntity.Project.EnglishName}.Domain.DTOs.Common");
            usings.Add($"{firstEntity.Project.EnglishName}.Domain.Enums.Common");
            usings.Add($"{firstEntity.Project.EnglishName}.Core.Extensions.BasicExtensions");
            usings.Add($"{firstEntity.Project.EnglishName}.Core.Security");
            usings.Add($"{firstEntity.Project.EnglishName}.Core.Services.Interfaces");
            usings.Add($"Microsoft.EntityFrameworkCore");
            //foreach (var singleEntity in allEntities)
            //{
            //    usings.AddRange(singleEntity.GetEnumUsings());
            //    usings.Add($"{singleEntity.Project.EnglishName}.Domain.DTOs.{singleEntity.Entity.FolderName}");
            //    usings.Add($"{singleEntity.Project.EnglishName}.Domain.Entities.{singleEntity.Entity.FolderName}");
            //    usings.Add($"{singleEntity.Project.EnglishName}.Core.Mappers.{singleEntity.Entity.FolderName}");
            //}
            foreach (var singleEntity in entities)
            {
                usings.AddRange(GetSingleEntityCustomUsings(singleEntity));
            }

            List<EntityFullInformationDto> allEntitiesAndTheirRelations = entities.GetAllEntities();

            foreach (var singleEntity in allEntitiesAndTheirRelations)
            {
                usings.AddRange(GetSingleEntityBaseUsigns(singleEntity));
            }

            return usings.GetUsings(GetNameSpace(firstEntity));
        }

        List<string> GetSingleEntityBaseUsigns(EntityFullInformationDto entity)
            => [
                $"{entity.Project.EnglishName}.Domain.Enums.{entity.Entity.FolderName}",
                $"{entity.Project.EnglishName}.Core.Mappers.{entity.Entity.FolderName}",
                $"{entity.Project.EnglishName}.Domain.Entities.{entity.Entity.FolderName}",
                $"{entity.Project.EnglishName}.Domain.DTOs.{entity.Entity.FolderName}",
            ];

        List<string> GetSingleEntityCustomUsings(EntityFullInformationDto entity)
        {
            List<string> usings = GetSingleEntityBaseUsigns(entity);
            List<EntityFullInformationDto> allEntitiesWithSameService = entity.GetAllEntitiesWithSameServiceName();
            List<EntityFullInformationDto> allEntities = entity.GetAllEntities();

            foreach (var singleEntity in allEntitiesWithSameService)
            {
                usings.Add($"{singleEntity.Project.EnglishName}.Domain.DTOs.{singleEntity.Entity.FolderName}");
            }
            foreach (var singleEntity in allEntities)
            {
                usings.AddRange(GetEntityRelationPropertiesUsings(singleEntity));
            }
            return usings;
        }

        List<string> GetEntityRelationPropertiesUsings(EntityFullInformationDto entity)
        {
            List<string> usings = new List<string>();
            foreach (var property in entity.GetRelationProperties())
            {
                usings.Add($"{entity.Project.EnglishName}.Domain.Entities.{property.EntityRelation!.SecondaryEntity.FolderName}");
                if (property.EntityRelation.MiddleEntityId > 0)
                    usings.Add($"{entity.Project.EnglishName}.Domain.Entities.{property.EntityRelation!.MiddleEntityTitle}");
            }
            return usings;
        }

        #endregion

        #region constructor code

        string GetConstructorCode(List<EntityFullInformationDto> entities)
        {
            EntityFullInformationDto firstEntity = entities[0];
            ConstructorListDto constructor = new ConstructorListDto()
            {
                ClassName = $"{firstEntity.Entity.ServiceName}",
            };


            List<DependencyInjectionListDto> repositoriesInjections = entities.GetAllEntities()
                .Select(a => new DependencyInjectionListDto()
                {
                    FileName = $"ICrudRepository<{a.Entity.SingularName}, {a.Entity.GetDataType()}>",
                    Name = $"{a.Entity.SingularName}Repository"
                }).ToList();
            foreach (var entity in entities.GetAllEntities())
            {
                List<DependencyInjectionListDto> otherServicesInjections = GetEntityFieldInRelationsInjections(entity);
                List<DependencyInjectionListDto> relationPropertiesInjections = GetEntityPropertyRelationRepositoryInjections(entity);
                constructor.DependencyInjections.AddRange(otherServicesInjections);
                constructor.DependencyInjections.AddRange(relationPropertiesInjections);
            }
            constructor.DependencyInjections.AddRange(repositoriesInjections);
            constructor.DependencyInjections = constructor.DependencyInjections
                .Where(a => a.Name != constructor.ClassName)
                .ToList();
            return constructor.GetConstructorCode();
        }

        List<DependencyInjectionListDto> GetEntityFieldInRelationsInjections(EntityFullInformationDto entity)
        {
            return entity.GetAllEntitiesWithDistinctServiceName()
                .Where(a => a.Entity.ServiceName != entity.Entity.ServiceName)
                                .Select(a => new DependencyInjectionListDto()
                                {
                                    FileName = $"I{a.Entity.ServiceName}",
                                    Name = $"{a.Entity.ServiceName}"
                                }).ToList();
        }


        List<DependencyInjectionListDto> GetEntityPropertyRelationRepositoryInjections(EntityFullInformationDto entity)
        {
            return entity.
                GetRelationProperties()
                                .Select(a => new DependencyInjectionListDto()
                                {
                                    FileName = $"ICrudRepository<{a.EntityRelation!.SecondaryEntity.SingularName}, {a.EntityRelation!.SecondaryEntity.IdType.GetDataType()}>",
                                    Name = $"{a.EntityRelation.SecondaryEntity.SingularName}Repository"
                                }).ToList();
        }

        #endregion

        #region services code

        string GetAllServicesCode(List<EntityFullInformationDto> entities)
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
                entitiesInterfacesCodeStringBuilder.AppendLine(GetSingleEntityServicsCode(entity));
            }

            return $@"
        #region {group.Key}

        {entitiesInterfacesCodeStringBuilder}

        #endregion";
        }


        string GetSingleEntityServicsCode(EntityFullInformationDto entity)
        {
            StringBuilder methodsStringBuilder = new StringBuilder();
            bool entityIsFieldInRelation = entity.HasFieldInRelationProperty();
            if (entity.Entity.EntitySelectedProjectAreasList.Any(d => d.HasCreate) || entityIsFieldInRelation)
                methodsStringBuilder.AppendLine(GetCreateMethodCode(entity));
            if (entity.Entity.EntitySelectedProjectAreasList.Any(d => d.HasUpdate) || entityIsFieldInRelation)
            {
                methodsStringBuilder.AppendLine(GetSingleUpdateInformationMethodCode(entity));
                methodsStringBuilder.AppendLine(GetUpdateMethodCode(entity));
            }
            if (entity.Entity.EntitySelectedProjectAreasList.Any(d => d.HasDelete) || entityIsFieldInRelation)
            {
                methodsStringBuilder.AppendLine(GetDeleteMethodCode(entity));
                methodsStringBuilder.AppendLine(GetDeleteAllMethodCode(entity));
            }
            return $@"  
        
        #region {entity.Entity.PluralName.AddSpacesBetweenCapitals().ToLower()}

        {GetBaseFilterMethod(entity)}

        {GetFilterMethodCode(entity)}

        {GetAsComboMethodCode(entity)}

        {GetSingleInformationMethodCode(entity)}

        {methodsStringBuilder.ToString()}

        #endregion";
        }

        #region base filter method code

        string GetBaseFilterMethod(EntityFullInformationDto entity)
        {
            List<PropertyListDto> textContainFilterProperties = entity
                .Properties
                .Where(a => a.DataType == DataTypeEnum.String && a.IsFilterContain)
                .ToList();
            List<PropertyListDto> filterEqualProperties = entity
                .Properties
                .Where(a => a.IsFilterEqual)
                .ToList();
            List<PropertyListDto> listProperties = entity
                .GetEntityListDtoProperties();
            List<PropertyListDto> relationProperties = entity
                .GetRelationProperties();
            string filterEqualCode = string.Join("\n", filterEqualProperties
   .ConvertAll(a => $@"            if (filter.{a.Name} is not null)
                query = query.Where(q => q.{a.Name} == filter.{a.Name});
")
   );
            string textContainFilterPropertiesCode = string.Join('\n', textContainFilterProperties
                .ConvertAll(a => $@"EF.Functions.Like(q.{a.Name}, $""%{{filter.Search}}%"") ||
                                         "))
                .ReplaceLastOccurrence('|', ' ')
                .ReplaceLastOccurrence('|', ' ');

            string relationsFilterCode = string.Join("\n", relationProperties
                .ConvertAll(a => $@"            if (filter.{a.Name} > 0)
                query = query.Where(q => q.{a.Name} == filter.{a.Name});
"));

            string advanceSortCode = string.Join('\n',
                listProperties
                .ConvertAll(a => GetSingleAdvanceSortCode(a, entity)));

            return $@"IQueryable<{entity.Entity.SingularName}> Get{entity.Entity.PluralName}WithFilterAndSort(Filter{entity.Entity.PluralName}Dto filter)
        {{
            IQueryable<{entity.Entity.SingularName}> query = _{entity.Entity.SingularName.ToFirstCharLower()}Repository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            {(textContainFilterProperties.Any() ? $@"if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => {textContainFilterPropertiesCode});" : "")}
            {filterEqualCode}
            {relationsFilterCode}

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {{
                {advanceSortCode}
                _ => query
            }};

            #endregion

            #region base sort

            query = filter.BaseSortEntityType switch
            {{
                BaseSortEntityType.Default => query,
                BaseSortEntityType.Newest => query.OrderByDescending(q => q.CreateDate),
                BaseSortEntityType.LatestUpdate => query.OrderByDescending(q => q.LatestEditDate),
                _ => query
            }};

            query = filter.SortType switch
            {{
                SortType.Ascending => query.Reverse(),
                SortType.Descending => query,
                _ => query
            }};

            #endregion
            
            return query;
        }}";

        }

        private string GetSingleAdvanceSortCode(PropertyListDto property, EntityFullInformationDto entity)
        {
            if (property.EntityRelation is not null)
            {
                if (property.Name != property.NameInDb)
                    return $@"                {entity.GetSortEntityEnumName()}.{property.Name} => query.OrderBy(a => a.{property.NameInDb.GetRelationPropertyName()}.{property.EntityRelation.SecondaryEntityTitleProperty.Name}),";
            }
            return $@"                {entity.GetSortEntityEnumName()}.{property.Name} => query.OrderBy(a => a.{property.Name}),";
        }

        #endregion

        #region filter method code

        private string GetFilterMethodCode(EntityFullInformationDto entity)
        {

            return @$"public async Task<Filter{entity.Entity.PluralName}Dto> Filter{entity.Entity.PluralName}(Filter{entity.Entity.PluralName}Dto filter)
        {{
            IQueryable<{entity.Entity.SingularName}> query = 
                Get{entity.Entity.PluralName}WithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId, 
                await query.CountAsync(),
                filter.TakeEntity, 
                filter.HowManyShowPageAfterAndBefore);

            filter.{entity.Entity.PluralName} = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }}";
        }

        #endregion

        #region get single get method code

        private string GetSingleInformationMethodCode(EntityFullInformationDto entity)
        {
            return $@"public async Task<{entity.Entity.SingularName}ListDto?> GetSingle{entity.Entity.SingularName}Information({entity.Entity.GetDataType()} {entity.Entity.SingularName.ToFirstCharLower()}Id)
            => await _{entity.Entity.SingularName.ToFirstCharLower()}Repository
            .GetQueryable()
            .Where(a => a.Id == {entity.Entity.SingularName.ToFirstCharLower()}Id)
            .ToDto()
            .FirstOrDefaultAsync();";
        }

        #endregion

        #region get as combo

        private string GetAsComboMethodCode(EntityFullInformationDto entity)
            => $@"public async Task<List<ComboDto>> Get{entity.Entity.PluralName}AsCombo(Filter{entity.Entity.PluralName}Dto filter)
            => await Get{entity.Entity.PluralName}WithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();";

        #endregion

        #region get single get update dto

        private string GetSingleUpdateInformationMethodCode(EntityFullInformationDto entity)
        {
            return $@"public async Task<Update{entity.Entity.SingularName}Dto?> Get{entity.Entity.SingularName}Information({entity.Entity.GetDataType()} {entity.Entity.SingularName.ToFirstCharLower()}Id)
        =>  await _{entity.Entity.SingularName.ToFirstCharLower()}Repository
            .GetQueryable()
            .Where(a => a.Id == {entity.Entity.SingularName.ToFirstCharLower()}Id)
            .ToUpdateDto()
            .FirstOrDefaultAsync();";
        }

        #endregion

        #region get create method

        private string GetCreateMethodCode(EntityFullInformationDto entity)
        {
            string enumName = entity.GetCreateMethodReturnEnumName();
            string notfoundReturnCode = $"{enumName}.NotFound;";
            bool isAsyncMapper = entity.HasAnyFile();
            return $@"public async Task<{enumName}> Create{entity.Entity.SingularName}(Create{entity.Entity.SingularName}Dto create)
        {{ 
            {GetCreateMethodUniquePropertiesValidationCode(entity)}
            {GetCreateMethodRelationValidationsCode(entity)}
            {entity.Entity.SingularName} {entity.Entity.SingularName.ToFirstCharLower()} = {(isAsyncMapper ? "await" : "")} create.ToModel();
            await _{entity.Entity.SingularName.ToFirstCharLower()}Repository.Add({entity.Entity.SingularName.ToFirstCharLower()});
            await _{entity.Entity.SingularName.ToFirstCharLower()}Repository.SaveChanges();
            {GetCreateMethodAddMiddleEntitiesCode(entity)}      
            {GetAddFieldInRelationsEntityCode(entity, "create")}
            return {enumName}.Success;
        }}";

        }

        private string GetCreateMethodUniquePropertiesValidationCode(EntityFullInformationDto entity)
        {
            var uniqueProperties = entity.Properties
                .GetUniqueProperties()
                .ToList();

            string uniqueConditionsCode = string.Join("\n", uniqueProperties
                .ConvertAll(a =>
                GetCreateMethodSinglePropertyUniqueValidationCode(a, entity)));
            if (!uniqueProperties.Any()) return 2.CreateEmptyLines();
            return $@"

            #region validate unique properties

            {uniqueConditionsCode}

            #endregion
";
        }

        private string GetCreateMethodSinglePropertyUniqueValidationCode(PropertyListDto property, EntityFullInformationDto entity)
        {
            string? checkForNullableCode = null;
            if (!property.IsRequired)
                checkForNullableCode = $@"            if(create.{property.Name}!=null)
";
            string enumExistsName = entity.CountUniqueProperties() > 1 ? $"{property.Name}Exists" : "Exists";
            string enumName = entity.GetCreateMethodReturnEnumName();
            return $@"{checkForNullableCode ?? ""}
            if (await _{entity.Entity.SingularName.ToFirstCharLower()}Repository
                .GetQueryable()
                .AnyAsync(a => a.{property.Name} == create.{property.Name}{property.GetPropertyMappingExtensionMethod()}))
                return {enumName}.{enumExistsName};";
        }

        private string GetCreateMethodRelationValidationsCode(EntityFullInformationDto entity)
        {
            string enumName = entity.GetCreateMethodReturnEnumName();
            string notFoundReturnCode = $"{enumName}.NotFound";
            var relationProperties = entity
                .GetRelationProperties();
            return GetUpsertMethodsRelationValidationsCode(relationProperties, "create", notFoundReturnCode);
        }

        private string GetUpsertMethodsRelationValidationsCode(
          List<PropertyListDto> properties, string instanceName, string notFoundReturnCode)
        {
            if (!properties.Any()) return 2.CreateEmptyLines();
            string conditionsCode = string.Join("\n", properties
            .ConvertAll(property => GetSingleRelationValidationCode(property, instanceName, notFoundReturnCode)));
            return $@"

            #region validate relation ids

            {conditionsCode}

            #endregion


";
        }

        private string GetSingleRelationValidationCode(PropertyListDto property, string instanceName,
           string notFoundReturnCode)
        {
            string checkingForNullCode = $@"
            if({instanceName}.{property.Name}>0)";
            return $@"
            {(!property.IsRequired ? checkingForNullCode : "")}
            if (!await _{property.EntityRelation!.SecondaryEntity.SingularName.ToFirstCharLower()}Repository
                .GetQueryable()
                .AnyAsync(a => a.Id == {instanceName}.{property.Name}))
                return {notFoundReturnCode};";
        }

        private string GetCreateMethodAddMiddleEntitiesCode(EntityFullInformationDto entity)
        {
            List<EntityRelationListDto> middleRelations = entity.
                GetMiddleRelations();
            return GetAddMiddleEntityCode(middleRelations, entity, "create");
        }

        private string GetAddMiddleEntityCode(
    List<EntityRelationListDto> middleRelations, EntityFullInformationDto entity, string instanceName)
        {
            if (!middleRelations.Any()) return 2.CreateEmptyLines();
            string conditionsCode = string.Join("\n", middleRelations
                .ConvertAll(relation =>
                GetSingleAddMiddleRelationCode(relation, entity, instanceName)));
            return $@"

            #region add relations

            {conditionsCode}

            #endregion

";
        }

        private string GetSingleAddMiddleRelationCode(EntityRelationListDto relation, EntityFullInformationDto mainEntity,
    string instanceName)
        {
            string relationServiceName = $"{relation.PrimaryPropertyEntityTitle}Service";
            string addMethodName = relationServiceName == mainEntity.Entity.ServiceName ? $"_{relationServiceName}." : "";
            return $@"
            #region add {relation.MiddleEntityTitle!.AddSpacesBetweenCapitals().ToLower()}

            if ({instanceName}.{relation.MiddleEntityTitle}Ids is not null && {instanceName}.{relation.MiddleEntityTitle}.Any())
            {{
                foreach (var {relation.MiddleEntityTitle!.ToFirstCharLower()}Id in {instanceName}.{relation.MiddleEntityTitle}Ids)
                {{
                    Create{relation.PrimaryPropertyEntityTitle}Dto create{relation.PrimaryPropertyEntityTitle} = new Create{relation.PrimaryPropertyEntityTitle}Dto()
                    {{
                        {relation.PrimaryPropertyTitle} = {relation.SecondaryEntity.SingularName.ToFirstCharLower()}.Id,
                        
                    }};
                    await {addMethodName}Create{relation.PrimaryPropertyEntityTitle}(create{relation.PrimaryPropertyEntityTitle});
                }}

            }}

            #endregion
";
        }

        private string GetAddFieldInRelationsEntityCode(
            EntityFullInformationDto entity, string instanceName)
        {
            if (!entity.FieldInRelationEntities.Any()) return 2.CreateEmptyLines();
            string conditionsCode = string.Join("\n", entity
                .FieldInRelationEntities
                .ConvertAll(relation =>
                GetSingleAddFieldRelationCode(entity, relation, instanceName)));
            return $@"

            #region add relations

            {conditionsCode}

            #endregion

";
        }

        private string GetSingleAddFieldRelationCode(EntityFullInformationDto mainEntity, EntityFullInformationDto relationEntity,
    string instanceName)
        {
            string serviceName = mainEntity.Entity.ServiceName == relationEntity.Entity.ServiceName ? "" : $@"_{relationEntity.Entity.ServiceName.ToFirstCharLower()}.";
            var relation = mainEntity.Relations.FirstOrDefault(a => a.PrimaryPropertyEntityId == relationEntity.Entity.Id);
            return $@"
            #region add {relationEntity.Entity.PluralName.AddSpacesBetweenCapitals().ToLower()}

            if ({instanceName}.{relationEntity.Entity.PluralName}List is not null && {instanceName}.{relationEntity.Entity.PluralName}List.Any())
            {{
                foreach (var {relationEntity.Entity.SingularName.ToFirstCharLower()} in {instanceName}.{relationEntity.Entity.PluralName}List)
                {{
                    {relationEntity.Entity.SingularName.ToFirstCharLower()}.{relation.PrimaryPropertyTitle} = {relation.SecondaryEntity.SingularName.ToFirstCharLower()}.Id;
                    await {serviceName}Create{relationEntity.Entity.SingularName}({relationEntity.Entity.SingularName.ToFirstCharLower()});
                }}

            }}

            #endregion
";
        }

        #endregion

        #region update method

        private string GetUpdateMethodCode(EntityFullInformationDto entity)
        {
            string enumName = entity.GetUpdateMethodReturnEnumName();
            string notfoundReturnCode = $"{enumName}.NotFound;";
            bool isAsyncMapper = entity.HasAnyUpdatableFile();
            return $@"public async Task<{enumName}> Update{entity.Entity.SingularName}(Update{entity.Entity.SingularName}Dto update)
        {{ 
            {entity.Entity.SingularName}? {entity.Entity.SingularName.ToFirstCharLower()} = await _{entity.Entity.SingularName.ToFirstCharLower()}Repository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if ({entity.Entity.SingularName.ToFirstCharLower()} is null) return {enumName}.NotFound;

            {GetUpdateMethodUniquePropertiesValidationCode(entity)}
            {GetUpdateMethodRelationValidationsCode(entity)}
            {entity.Entity.SingularName.ToFirstCharLower()} = {(isAsyncMapper ? "await" : "")} {entity.Entity.SingularName.ToFirstCharLower()}.ToModel(update);
            _{entity.Entity.SingularName.ToFirstCharLower()}Repository.Update({entity.Entity.SingularName.ToFirstCharLower()});
            await _{entity.Entity.SingularName.ToFirstCharLower()}Repository.SaveChanges();
            {GetUpdateFieldInRelationsEntityCode(entity, "update")}

            return {enumName}.Success;
        }}";
            //todo add them before success
            //            {GetUpdateMethodAddMiddleEntitiesCode(entity)}      
        }

        private string GetUpdateMethodUniquePropertiesValidationCode(EntityFullInformationDto entity)
        {
            var uniqueProperties = entity.Properties
                .Where(a => a.IsUpdatable)
                .ToList()
                .GetUniqueProperties()
                .ToList();

            string uniqueConditionsCode = string.Join("\n", uniqueProperties
                .ConvertAll(a =>
                GetUpdateMethodSinglePropertyUniqueValidationCode(a, entity)));
            if (!uniqueProperties.Any()) return 2.CreateEmptyLines();
            return $@"

            #region validate unique properties

            {uniqueConditionsCode}

            #endregion
";
        }

        private string GetUpdateMethodSinglePropertyUniqueValidationCode(PropertyListDto property, EntityFullInformationDto entity)
        {
            string? checkForNullableCode = null;
            if (!property.IsRequired)
                checkForNullableCode = $@"            if(update.{property.Name}!=null)
";
            string enumExistsName = entity.CountUniqueProperties() > 1 ? $"{property.Name}Exists" : "Exists";
            string enumName = entity.GetUpdateMethodReturnEnumName();
            return $@"{checkForNullableCode ?? ""}
            if (await _{entity.Entity.SingularName.ToFirstCharLower()}Repository
                .GetQueryable()
                .AnyAsync(a => a.{property.Name} == update.{property.Name}{property.GetPropertyMappingExtensionMethod()}
                               && a.Id!=update.Id))
                return {enumName}.{enumExistsName};";
        }

        private string GetUpdateMethodRelationValidationsCode(EntityFullInformationDto entity)
        {
            string enumName = entity.GetUpdateMethodReturnEnumName();
            string notFoundReturnCode = $"{enumName}.NotFound";
            var relationProperties = entity
                .GetRelationProperties()
                .Where(a => a.IsUpdatable)
                .ToList();
            return GetUpsertMethodsRelationValidationsCode(relationProperties, "update", notFoundReturnCode);
        }

        private string GetUpdateFieldInRelationsEntityCode(
    EntityFullInformationDto entity, string instanceName)
        {
            if (!entity.FieldInRelationEntities.Any()) return 2.CreateEmptyLines();
            string conditionsCode = string.Join("\n", entity
                .FieldInRelationEntities
                .ConvertAll(relation =>
                GetSingleUpdateFieldRelationCode(entity, relation, instanceName)));
            return $@"

            #region update relations

{conditionsCode}

            #endregion

";
        }

        private string GetSingleUpdateFieldRelationCode(EntityFullInformationDto mainEntity, EntityFullInformationDto relationEntity,
    string instanceName)
        {
            string serviceName = mainEntity.Entity.ServiceName == relationEntity.Entity.ServiceName ? "" : $@"_{relationEntity.Entity.ServiceName.ToFirstCharLower()}.";
            var relation = mainEntity.Relations.FirstOrDefault(a => a.PrimaryPropertyEntityId == relationEntity.Entity.Id);
            return $@"            #region handle {relationEntity.Entity.PluralName.AddSpacesBetweenCapitals().ToLower()} operations

            List<{relationEntity.Entity.SingularName}> current{relationEntity.Entity.PluralName} = await _{relationEntity.Entity.SingularName.ToFirstCharLower()}Repository
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.{relation.PrimaryPropertyTitle} == {relation.SecondaryEntity.SingularName.ToFirstCharLower()}.Id)
                .ToListAsync();

            #region add new {relationEntity.Entity.PluralName.AddSpacesBetweenCapitals().ToLower()}

            List<Create{relationEntity.Entity.SingularName}Dto> new{relationEntity.Entity.PluralName} = update.{relationEntity.Entity.PluralName}List
                                .Where(a => !current{relationEntity.Entity.PluralName}.Any(b => b.Id == a.Id))
                                .ToCreateDto()
                                .ToList();

            foreach (Create{relationEntity.Entity.SingularName}Dto {relationEntity.Entity.SingularName.ToFirstCharLower()} in new{relationEntity.Entity.PluralName})
            {{
                {relationEntity.Entity.SingularName.ToFirstCharLower()}.{relation.PrimaryPropertyTitle} = {relation.SecondaryEntity.SingularName.ToFirstCharLower()}.Id;
                await {serviceName}Create{relationEntity.Entity.SingularName}({relationEntity.Entity.SingularName.ToFirstCharLower()});
            }}

            #endregion

            #region update {relationEntity.Entity.PluralName.AddSpacesBetweenCapitals().ToLower()}

            List<Update{relationEntity.Entity.SingularName}Dto> updated{relationEntity.Entity.PluralName} =
                    update.{relationEntity.Entity.PluralName}List
                    .Where(a => current{relationEntity.Entity.PluralName}.Any(b => b.Id == a.Id))
                    .ToList();

            foreach (Update{relationEntity.Entity.SingularName}Dto {relationEntity.Entity.SingularName.ToFirstCharLower()} in updated{relationEntity.Entity.PluralName})
            {{
                await {serviceName}Update{relationEntity.Entity.SingularName}({relationEntity.Entity.SingularName.ToFirstCharLower()});
            }}

            #endregion

            #region delete {relationEntity.Entity.PluralName.AddSpacesBetweenCapitals().ToLower()}

            List<{relationEntity.Entity.IdType.GetDataType()}> removed{relationEntity.Entity.PluralName}Ids =
                    current{relationEntity.Entity.PluralName}
                    .Where(a => !update.{relationEntity.Entity.PluralName}List.Any(b => b.Id == a.Id))
                    .Select(a => a.Id)
                    .ToList();

            await {serviceName}Delete{relationEntity.Entity.SingularName}(removed{relationEntity.Entity.PluralName}Ids);
            
            #endregion

            #endregion
";
        }


        private string GetUpdateMethodAddMiddleEntitiesCode(EntityFullInformationDto entity)
        {
            List<EntityRelationListDto> middleRelations = entity.
                GetMiddleRelations();
            return GetUpdateMiddleEntityCode(middleRelations, entity, "create");
        }

        private string GetUpdateMiddleEntityCode(
    List<EntityRelationListDto> middleRelations, EntityFullInformationDto entity, string instanceName)
        {
            if (!middleRelations.Any()) return 2.CreateEmptyLines();
            string conditionsCode = string.Join("\n", middleRelations
                .ConvertAll(relation =>
                GetSingleUpdateMiddleRelationCode(relation, entity, instanceName)));
            return $@"

            #region add relations

            {conditionsCode}

            #endregion

";
        }

        private string GetSingleUpdateMiddleRelationCode(EntityRelationListDto relation, EntityFullInformationDto mainEntity,
    string instanceName)
        {
            string serviceName = mainEntity.Entity.ServiceName == relation.SecondaryEntity.ServiceName ? "" : $@"_{relation.SecondaryEntity.ServiceName.ToFirstCharLower()}.";
            return $@"
            #region add {relation.MiddleEntityTitle!.AddSpacesBetweenCapitals().ToLower()}

            if ({instanceName}.{relation.MiddleEntityTitle}Ids is not null && {instanceName}.{relation.MiddleEntityTitle}.Any())
            {{
                foreach (var {relation.MiddleEntityTitle!.ToFirstCharLower()}Id in {instanceName}.{relation.MiddleEntityTitle}Ids)
                {{
                    Create{relation.PrimaryPropertyEntityTitle}Dto create{relation.PrimaryPropertyEntityTitle} = new Create{relation.PrimaryPropertyEntityTitle}Dto()
                    {{
                        {relation.PrimaryPropertyTitle} = {relation.SecondaryEntity.SingularName.ToFirstCharLower()}.Id,
                        
                    }};
                    await {serviceName}Create{relation.PrimaryPropertyEntityTitle}(create{relation.PrimaryPropertyEntityTitle});
                }}

            }}

            #endregion
";
            return $@"            #region handle {relation.SecondaryEntity.PluralName.AddSpacesBetweenCapitals().ToLower()} operations

            List<{relation.SecondaryEntity.SingularName}> current{relation.SecondaryEntity.PluralName} = await _{relation.SecondaryEntity.SingularName.ToFirstCharLower()}Repository
                .GetQueryable()
                .Where(a => a.{relation.PrimaryPropertyTitle} == {relation.SecondaryEntity.SingularName.ToFirstCharLower()}.Id)
                .ToListAsync();

            #region add new {relation.SecondaryEntity.PluralName.AddSpacesBetweenCapitals().ToLower()}

            List<{relation.SecondaryEntity.IdType.GetDataType()}> new{relation.SecondaryEntity.PluralName} = update.{relation.SecondaryEntity.PluralName}Name
                                .Where(a => !current{relation.SecondaryEntity.PluralName}.Any(b => b.Id == a.Id))
                                .ToList();

            foreach ({relation.SecondaryEntity.IdType.GetDataType()} {relation.SecondaryEntity.SingularName.ToFirstCharLower()} in new{relation.SecondaryEntity.PluralName})
            {{
                await {serviceName}Create{relation.SecondaryEntity.SingularName}({relation.SecondaryEntity.SingularName.ToFirstCharLower()});
            }}

            #endregion

            #region update {relation.SecondaryEntity.PluralName.AddSpacesBetweenCapitals().ToLower()}

            List<{relation.SecondaryEntity.IdType.GetDataType()}> updated{relation.SecondaryEntity.PluralName} =
                    update.{relation.SecondaryEntity.PluralName}
                    .Where(a => current{relation.SecondaryEntity.PluralName}.Any(b => b.Id == a.Id))
                    .ToList();

            foreach ({relation.SecondaryEntity.IdType.GetDataType()} {relation.SecondaryEntity.SingularName.ToFirstCharLower()} in updated{relation.SecondaryEntity.PluralName})
            {{
                await {serviceName}Update{relation.SecondaryEntity.SingularName}({relation.SecondaryEntity.SingularName.ToFirstCharLower()});
            }}

            #endregion

            #region delete {relation.SecondaryEntity.PluralName.AddSpacesBetweenCapitals().ToLower()}

            List<{relation.SecondaryEntity.IdType.GetDataType()}> removed{relation.SecondaryEntity.PluralName}Ids =
                    current{relation.SecondaryEntity.PluralName}
                    .Where(a => !update.{relation.SecondaryEntity.PluralName}.Any(b => b.Id == a.Id))
                    .Select(a => a.Id)
                    .ToList();

            await {serviceName}Delete{relation.SecondaryEntity.SingularName}(removed{relation.SecondaryEntity.PluralName}Ids);

            #endregion

            #endregion";

        }



        #endregion

        #region delete method code

        private string GetDeleteMethodCode(EntityFullInformationDto entity)
   => $@"public async Task<BaseChangeEntityResult> Delete{entity.Entity.SingularName}({entity.Entity.GetDataType()} {entity.Entity.SingularName.ToFirstCharLower()}Id)
        {{
            {entity.Entity.SingularName}? {entity.Entity.SingularName.ToFirstCharLower()} = await _{entity.Entity.SingularName.ToFirstCharLower()}Repository.GetAsTracking({entity.Entity.SingularName.ToFirstCharLower()}Id);
            if ({entity.Entity.SingularName.ToFirstCharLower()} is null) return BaseChangeEntityResult.NotFound;
    
            _{entity.Entity.SingularName.ToFirstCharLower()}Repository.SoftDelete({entity.Entity.SingularName.ToFirstCharLower()});

            await _{entity.Entity.SingularName.ToFirstCharLower()}Repository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }}";

        #endregion

        #region delete all method code

        private string GetDeleteAllMethodCode(EntityFullInformationDto entity)
           => $@"public async Task Delete{entity.Entity.SingularName}(List<{entity.Entity.GetDataType()}> {entity.Entity.SingularName.ToFirstCharLower()}Ids)
        {{
            foreach ({entity.Entity.GetDataType()} {entity.Entity.SingularName.ToFirstCharLower()}Id in {entity.Entity.SingularName.ToFirstCharLower()}Ids)
            {{ 
                {entity.Entity.SingularName}? {entity.Entity.SingularName.ToFirstCharLower()} = await _{entity.Entity.SingularName.ToFirstCharLower()}Repository.GetAsTracking({entity.Entity.SingularName.ToFirstCharLower()}Id);
                if ({entity.Entity.SingularName.ToFirstCharLower()} is not null)
                    _{entity.Entity.SingularName.ToFirstCharLower()}Repository.SoftDelete({entity.Entity.SingularName.ToFirstCharLower()});
            }}
            await _{entity.Entity.SingularName.ToFirstCharLower()}Repository.SaveChanges();
        }}";

        #endregion

        #endregion
    }
}
