using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.New.Enums.DTOs;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.Enums.DataTypes;
using DevCopilot2.Domain.Enums.Relations;
using System.Text;

namespace DevCopilot2.Core.Extensions.AdvanceExtensions.Generators
{
    public static class BaseGeneratorExtensionMethods
    {
        public static string GetUsings(this List<string> nameSpaces, string currentNameSpace)
            => string.Join("\n", nameSpaces
                .Distinct()
                .Where(a => a != currentNameSpace)
                .ToList()
                .ConvertAll(a => $"using {a};"));

        public static string GetRazorUsings(this List<string> nameSpaces, string currentNameSpace)
            => string.Join("\n", nameSpaces
                .Distinct()
                .Where(a => a != currentNameSpace)
                .ToList()
                .ConvertAll(a => $"@using {a}"));

        public static bool HasRelations(this EntityFullInformationDto entity)
        => entity.Relations.Any() || entity.Properties.Any(p => p.EntityRelation != null);

        public static bool HasFieldInRelationProperty(this EntityFullInformationDto entity)
        => entity.Properties.Any(p => p.EntityRelation != null && p.EntityRelation.InputType == InputTypeEnum.FieldsInRelationEntityPage);


        public static bool PropertiesHaveFieldInRelation(this EntityFullInformationDto entity)
        => entity.Properties.Any(p => p.EntityRelation != null &&
           p.EntityRelation.InputType == InputTypeEnum.FieldsInRelationEntityPage);

        public static bool PropertiesHaveRelation(this EntityFullInformationDto entity)
        => entity.Properties.Any(p => p.EntityRelation != null);


        public static string GetNullableDataType(this PropertyListDto property)
        {
            string dataType = property.GetDataType();
            if (property.IsRequired)
                dataType = $"{dataType}?";
            return dataType;
        }
        public static string GetDataType(this PropertyListDto property)
        {
            string dataType = string.Empty;
            if (property.ProjectEnumId > 0) dataType = property.ProjectEnumEnglishName ?? "";
            else
                dataType = property.DataType.GetDataType();
            if (!string.IsNullOrEmpty(property.ForceDataTypeCode)) dataType = property.ForceDataTypeCode;

            return property.IsRequired || property.DataType == DataTypeEnum.Bool ? dataType : $"{dataType}?";
        }

        public static string GetDataType(this DataTypeEnum dataType, bool isRequired = true)
        {
            string dataTypeCode = string.Empty;
            dataTypeCode = dataType switch
            {
                DataTypeEnum.String => "string",
                DataTypeEnum.Int => "int",
                DataTypeEnum.Long => "long",
                DataTypeEnum.Bool => "bool",
                DataTypeEnum.Guid => "Guid",
                DataTypeEnum.DateTime => "DateTime",
                DataTypeEnum.Decimal => "decimal",
                DataTypeEnum.Double => "double",
                DataTypeEnum.Byte => "byte",
                _ => "string"
            };
            return isRequired ? dataTypeCode : $"{dataTypeCode}?";
        }



        public static List<PropertyListDto> GetIndexableProperties(this EntityFullInformationDto entity)
            => entity
                .Properties
                .Where(a => a.IsUnique || a.IsFilterContain || a.IsFilterEqual)
                .ToList();

        public static string GetDataType(this EntityListDto entity)
       => entity.IdType.GetDataType();

        public static string GetDataType(this IdTypeEnum idType)
        {
            return idType switch
            {
                IdTypeEnum.Int => "int",
                IdTypeEnum.Long => "long",
                IdTypeEnum.Byte => "byte",
                _ => "int"
            };
        }

        public static DataTypeEnum GetDataTypeEnum(this IdTypeEnum idType)
        {
            return idType switch
            {
                IdTypeEnum.Int => DataTypeEnum.Int,
                IdTypeEnum.Long => DataTypeEnum.Long,
                IdTypeEnum.Byte => DataTypeEnum.Byte,
                _ => DataTypeEnum.Int
            };
        }

        public static string GetRelationPropertyName(this PropertyListDto property)
            => property.Name.GetRelationPropertyName();

        public static string GetRelationPropertyName(this string propertyName)
            => propertyName.Replace("Id", "").Replace("ID", "");


        public static bool IsAnyKindFile(this PropertyListDto property)
        {
            return property.DataAnnotationDataType == DataAnnotationsDataType.Image ||
                property.DataAnnotationDataType == DataAnnotationsDataType.Video ||
                property.DataAnnotationDataType == DataAnnotationsDataType.File;
        }

        public static bool IsSelect(this PropertyListDto property)
            => property.EntityRelation is not null &&
                property.EntityRelation.InputType is InputTypeEnum.Select;

        public static bool HasAnyFile(this EntityFullInformationDto entity)
            => entity
            .Properties
            .Any(a => a.IsAnyKindFile());
        public static bool HasAnyUpdatableFile(this EntityFullInformationDto entity)
    => entity
    .Properties
    .Any(a => a.IsAnyKindFile() && a.IsUpdatable);

        public static List<string> GetEnumUsings(this EntityFullInformationDto entity)
        => entity
           .Properties
           .Where(a => a.ProjectEnum != null)
           .ToList()
           .Select(a => $"{entity.Project.EnglishName}.Domain.Enums.{a.ProjectEnum!.FolderName}")
           .ToList();

        public static List<long> GetFieldInRelationEntityIds(this EntityFullInformationDto entity)
            => entity
                .Relations
                .Where(a => a.InputType == InputTypeEnum.FieldsInRelationEntityPage)
                .Select(a => a.PrimaryPropertyEntityId)
                .ToList();

        public static List<PropertyListDto> GetRelationProperties(this EntityFullInformationDto entity)
        => entity.Properties
                //.Where(a => a.EntityRelation != null && !a.EntityRelation.SecondaryEntity.IsExcluded)
                .Where(a => a.EntityRelation != null)
                .ToList();

        public static List<PropertyListDto> GetFilterProperties(this EntityFullInformationDto entity)
        {
            List<PropertyListDto> relationProperties = entity.GetRelationProperties();
            List<PropertyListDto> textFilterProperties = entity.Properties
                .Where(a => a.IsFilterEqual)
                .ToList();
            List<PropertyListDto> customFilterProperties = entity.Properties
                .Where(a => a.EntitySelectedProjectAreaSelectedFilters.Any() && a.EntityRelation == null)
                .ToList();
            List<PropertyListDto> filterProperties =
            [
                .. relationProperties,
                .. textFilterProperties,
                .. customFilterProperties,
            ];
            return filterProperties;
        }

        public static string GetSortEntityEnumName(this EntityFullInformationDto entity)
          => $@"Sort{entity.Entity.SingularName}Type";

        public static PropertyListDto GetTitleProperty(this EntityFullInformationDto entity)
        {
            PropertyListDto? titleProperty =
            entity
    .Properties
    .OrderBy(a => a.Order)
    .ThenByDescending(a => a.DataType == DataTypeEnum.String)
    .FirstOrDefault(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title);
            if (titleProperty is null)
                titleProperty = entity.Properties
                    .OrderBy(a => a.Order)
                    .FirstOrDefault();
            return titleProperty!;
        }

        public static bool IsRequiredFieldInRelation(this EntityFullInformationDto entity, EntityFullInformationDto relationEntity)
            => relationEntity
              .Properties
              .Where(a => a.EntityRelation != null && a.EntityRelation.SecondaryEntityId == entity.Entity.Id)
              .Select(a => a.IsRequired)
            .FirstOrDefault();

        public static string GetSingleListDtoPropertyCode(this PropertyListDto property)
        {
            if (string.IsNullOrEmpty(property.InitializationCode))
                if (property.DataType == DataTypeEnum.String)
                    property.InitializationCode = "= null!;";
            return $@"{2.CreateEmptyLines()}
        [Display(Name=""{(string.IsNullOrEmpty(property.NameInDb) ? property.Name : property.NameInDb)}"")]
        public {property.GetDataType()} {property.Name} {{ get; set; }} {property.InitializationCode}
";

        }

        public static string GetRelationTitlePropertyNameInListDto(this PropertyListDto titleProperty,
            PropertyListDto property)
        => $"{property.Name.GetRelationPropertyName()}{titleProperty.Name}";

        public static string GetMiddleRelationEntityPropertyNameInListDto(this EntityRelationListDto entityRelation)
        => $"{entityRelation.MiddleEntityTitle}Ids";

        public static string GetMiddleRelationEntityPropertyDataType(this EntityRelationListDto entityRelation)
            => $"List<{entityRelation.PrimaryPropertyDataType.GetDataType()}>";

        public static string GetFieldRelationEntityPropertyNameInDto(this EntityFullInformationDto entity)
        => $"{entity.Entity.PluralName}List";
        public static string GetFieldRelationEntityPropertyNameInListDto(this EntityListDto entity)
        => $"{entity.PluralName}List";

        public static string GetFieldRelationEntityPropertyDataTypeInListDto(this EntityFullInformationDto entity)
            => $@"List<{entity.Entity.SingularName}ListDto>";
        public static string GetFieldRelationEntityPropertyDataTypeInCreateDto(this EntityFullInformationDto entity)
           => $@"List<Create{entity.Entity.SingularName}Dto>";
        public static string GetFieldRelationEntityPropertyDataTypeInUpdateDto(this EntityFullInformationDto entity)
         => $@"List<Update{entity.Entity.SingularName}Dto>";

        public static List<EntityRelationListDto> GetMiddleRelations(this EntityFullInformationDto entity)
            => entity
                .Relations
                .Where(a => a.MiddleEntityId > 0)
                .ToList();

        public static List<EntityRelationListDto> GetAllMiddleRelations(this EntityFullInformationDto entity)
            => entity
            .GetAllEntities()
            .SelectMany(a => a.Relations
            .Where(a => a.MiddleEntityId > 0))
            .ToList();

        public static string GetDataTypeDefaultValue(this DataTypeEnum dataType)
        => dataType switch
        {
            DataTypeEnum.DateTime => "DateTime.Now",
            DataTypeEnum.Guid => "Guid.NewGuid()",
            DataTypeEnum.String => "fake string",
            _ => "0"
        };


        public static string GetSingleUpsertPropertyText(this PropertyListDto property)
        {
            if (string.IsNullOrEmpty(property.InitializationCode))
            {
                if (property.IsHiddenInput())
                    property.InitializationCode = $"= {property.DataType.GetDataTypeDefaultValue()};";
                else
                if (property.DataType == DataTypeEnum.String || property.IsAnyKindFile())
                    property.InitializationCode = "= null!;";
            }

            string displayName = string.IsNullOrEmpty(property.NameInDb) ? property.Name : property.NameInDb;
            return $@"        [Display(Name = ""{displayName}"")]
{property.GetDataAnnotationsCode()}
{property.DataAnnotationDataType.GetDataAnnotationTypeSpecificDataAttributes()}
        public {property.GetDataType()} {property.Name} {{ get; set; }} {property.InitializationCode}
";
        }

        public static string GetSingleUpsertPropertyDataType(this PropertyListDto property)
        {
            if (property.DataType == DataTypeEnum.DateTime &&
                property.DataAnnotationDataType == DataAnnotationsDataType.PersianDate)
                return $@"string{(property.IsRequired ? "" : "?")}";
            return property.GetDataType();
        }

        public static string GetDataAnnotationsCode(this PropertyListDto property)
        {
            StringBuilder dataAnnotationsSb = new StringBuilder();
            if (property.IsRequired && property.DataType != DataTypeEnum.Bool)
                dataAnnotationsSb.AppendLine($@"        [Required(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]");
            if (property.IsRequired && property.DataAnnotationDataType is DataAnnotationsDataType.List or DataAnnotationsDataType.DtoList)
                dataAnnotationsSb.AppendLine($@"        [NotEmptyList(ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RequiredErrorMessage))]");
            if (property.MinLength > 0)
                dataAnnotationsSb.AppendLine($@"        [MinLength({property.MinLength}, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MinLengthErrorMessage))]");
            if (property.MaxLength > 0)
                dataAnnotationsSb.AppendLine($@"        [MaxLength({property.MaxLength}, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.MaxLengthErrorMessage))]");
            if (property.RangeFrom is not null && property.RangeTo is not null)
                dataAnnotationsSb.AppendLine($@"        [Range({property.RangeFrom}, {property.RangeTo}, ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.RangeErrorMessage))]");
            //if (property.RangeFrom is null && property.RangeTo is null && property.EntityRelation is not null &&
            // property.EntityRelation.InputType == InputTypeEnum.Select && !property.IsRequired)
            //dataAnnotationsSb.AppendLine($@"        [Range(1, {property.EntityRelation.PrimaryPropertyDataType.GetDataType()}.MaxValue, ErrorMessage = ""{{0}} Is Required"")]");
            return dataAnnotationsSb.ToString();

        }

        public static string GetDataAnnotationTypeSpecificDataAttributes(this DataAnnotationsDataType type)
        {
            string isNotValidErrorMessage = @"ErrorMessageResourceType = typeof(BaseListDtoResources), ErrorMessageResourceName = nameof(BaseListDtoResources.IsNotValidErrorMessage)";
            return type switch
            {
                DataAnnotationsDataType.Url => $@"        [Url({isNotValidErrorMessage})]",
                DataAnnotationsDataType.Email => $@"        [EmailAddress({isNotValidErrorMessage})]",
                DataAnnotationsDataType.PersianDate => $@"        [PersianCalender]",
                DataAnnotationsDataType.Password => $@"        [DataType(DataType.Password,{isNotValidErrorMessage})]",
                DataAnnotationsDataType.NationalCode => $@"        [ValidNationalCode({isNotValidErrorMessage})]",
                DataAnnotationsDataType.PhoneNumber => $@"                [PersianPhoneNumber({isNotValidErrorMessage})]",
                _ => ""
            };
        }
        public static List<PropertyListDto> GetBaseUpsertDtoProperties(this EntityFullInformationDto entity)
        {
            List<PropertyListDto> properties = entity
                .Properties
                .Where(a => a.IsUpdatable && !a.IsAnyKindFile() && string.IsNullOrEmpty(a.ForceMapperCode))
                .ToList();
            return
                [
                ..properties
                ];
        }
        public static List<PropertyListDto> GetBaseUpsertDtoAllProperties(this EntityFullInformationDto entity)
        {
            List<PropertyListDto> properties = entity.GetBaseUpsertDtoProperties();
            List<PropertyListDto> middleRelationProperties = entity
                .GetMiddleRelations()
                .Select(a => new PropertyListDto()
                {
                    Name = a.GetMiddleRelationEntityPropertyNameInListDto(),
                    ForceDataTypeCode = a.GetMiddleRelationEntityPropertyDataType(),
                    DataAnnotationDataType = Domain.Enums.DataTypes.DataAnnotationsDataType.List,
                    InitializationCode = $"=new {a.GetMiddleRelationEntityPropertyDataType()}();",
                    DataType = Domain.Enums.DataTypes.DataTypeEnum.Int,
                    IsRequired = true
                }).ToList();

            List<PropertyListDto> allProperties = [
                ..properties,
                ..middleRelationProperties
                ];
            return allProperties;
        }

        public static List<PropertyListDto> GetCreateViewProperties(this EntityFullInformationDto entity)
        {
            return [
                    ..entity.GetCreateDtoProperties(),
                    ..entity.GetBaseUpsertDtoProperties()
                ];
        }

        public static List<PropertyListDto> GetUpsertViewProperties(this EntityFullInformationDto entity, bool isCreate)
        {
            if (isCreate) return entity.GetCreateViewProperties();
            return entity.GetUpdateViewProperties();
        }

        public static List<PropertyListDto> GetViewProperties(this EntityFullInformationDto entity, OperationTypeEnums operation)
        {
            List<PropertyListDto> properties = operation switch
            {
                OperationTypeEnums.Create => entity.GetCreateViewProperties(),
                OperationTypeEnums.Update => entity.GetUpdateViewProperties(),
                OperationTypeEnums.Detail => entity.GetDetailViewProperties(),
                _ => new List<PropertyListDto>()
            };
            return properties
                .OrderBy(a => a.Order)
                .ThenBy(a => a.DataType == DataTypeEnum.Bool)
                .ToList();
        }

        public static List<PropertyListDto> GetUpdateViewProperties(this EntityFullInformationDto entity)
        {
            return [
                    ..entity.GetUpdateDtoProperties(),
                    ..entity.GetBaseUpsertDtoProperties()
                ];
        }

        public static List<PropertyListDto> GetCreateDtoProperties(this EntityFullInformationDto mainEntity)
        {
            EntityFullInformationDto entity = mainEntity.DeepCopy();
            List<PropertyListDto> properties = entity
                .Properties
                .Where(a => !a.IsUpdatable && !a.IsAnyKindFile() && string.IsNullOrEmpty(a.ForceMapperCode))
                .ToList();

            List<PropertyListDto> fileProperties = entity
                .Properties
                .Where(a => a.IsAnyKindFile() && string.IsNullOrEmpty(a.ForceMapperCode))
                .ToList()
                .Select(a =>
                {
                    a.ForceDataTypeCode = "IFormFile";
                    a.DataAnnotationDataType = a.DataAnnotationDataType;
                    a.Name = a.GetFilePropertyDtoName();
                    a.PropertySelectedLanguagesList = a.PropertySelectedLanguagesList;
                    return a;
                }).ToList();
            return [
                ..properties,
                ..fileProperties
                ];
        }

        public static List<PropertyListDto> GetCreateDtoAllProperties(this EntityFullInformationDto entity)
        {

            List<PropertyListDto> fieldInPageRelationProperties = entity
                .FieldInRelationEntities
                .Select(a => new PropertyListDto()
                {
                    Name = a.GetFieldRelationEntityPropertyNameInDto(),
                    ForceDataTypeCode = a.GetFieldRelationEntityPropertyDataTypeInCreateDto(),
                    DataAnnotationDataType = DataAnnotationsDataType.List,
                    InitializationCode = $"=new {a.GetFieldRelationEntityPropertyDataTypeInCreateDto()}();",
                    DataType = DataTypeEnum.Int,
                    IsRequired = entity.IsRequiredFieldInRelation(a),
                    PropertySelectedLanguagesList = a.Entity.EntitySelectedLanguagesList
                    .Select(d => new PropertySelectedLanguageListDto()
                    {
                        LanguageId = d.LanguageId,
                        LanguageName = d.LanguageName,
                        Title = d.PluralTitle
                    }).ToList()

                }).ToList();
            List<PropertyListDto> allProperties = [
                ..entity.GetCreateDtoProperties(),
                ..fieldInPageRelationProperties,
                ];
            return allProperties;
        }
        public static List<PropertyListDto> GetUpdateDtoProperties(this EntityFullInformationDto currentEntity)
        {
            EntityFullInformationDto entity = currentEntity.DeepCopy();

            List<PropertyListDto> properties = new List<PropertyListDto>()
            {
                new PropertyListDto()
                {
                    Name = "Id",
                    NameInDb = "Id",
                    IsRequired = true,
                    DataAnnotationDataType = DataAnnotationsDataType.Hidden,
                    DataType = entity.Entity.IdType.GetDataTypeEnum(),
                    ForceDataTypeCode = entity.Entity.IdType.GetDataType()
                }
            };

            List<PropertyListDto> fileProperties = entity
                .Properties
                .Where(a => a.IsAnyKindFile() && a.IsUpdatable && string.IsNullOrEmpty(a.ForceMapperCode))
                .ToList();

            properties.AddRange(fileProperties);

            List<PropertyListDto> filePropertiesCloned = fileProperties.DeepCopy();

            List<PropertyListDto> iFormFileProperties =
                filePropertiesCloned
                .Select(a =>
                {
                    a.IsRequired = false;
                    a.ForceDataTypeCode = "IFormFile";
                    a.DataAnnotationDataType = a.DataAnnotationDataType;
                    a.Name = a.GetFilePropertyDtoName();
                    return a;
                }).ToList();
            return [
                ..properties,
                ..iFormFileProperties
                ];
        }

        public static List<PropertyListDto> GetDetailViewProperties(this EntityFullInformationDto currentEntity)
        {
            EntityFullInformationDto entity = currentEntity.DeepCopy();

            List<PropertyListDto> properties = new List<PropertyListDto>()
            {
                new PropertyListDto()
                {
                    Name = "Id",
                    IsRequired = true,
                    DataAnnotationDataType = DataAnnotationsDataType.Hidden,
                    ForceDataTypeCode = entity.Entity.IdType.GetDataType()
                }
            };

            return [
                ..properties,
                ..entity.GetEntityListDtoProperties(false)
                ];
        }


        public static List<PropertyListDto> GetUpdateDtoAllProperties(this EntityFullInformationDto entity)
        {
            List<PropertyListDto> fieldInPageRelationProperties = entity
                .FieldInRelationEntities
                .Select(a => new PropertyListDto()
                {
                    Name = a.GetFieldRelationEntityPropertyNameInDto(),
                    ForceDataTypeCode = a.GetFieldRelationEntityPropertyDataTypeInUpdateDto(),
                    DataAnnotationDataType = DataAnnotationsDataType.List,
                    InitializationCode = $"=new {a.GetFieldRelationEntityPropertyDataTypeInUpdateDto()}();",
                    DataType = DataTypeEnum.Int,
                    IsRequired = entity.IsRequiredFieldInRelation(a),
                    PropertySelectedLanguagesList = a.Entity.EntitySelectedLanguagesList
                    .Select(d => new PropertySelectedLanguageListDto()
                    {
                        LanguageId = d.LanguageId,
                        LanguageName = d.LanguageName,
                        Title = d.PluralTitle
                    }).ToList()

                }).ToList();
            List<PropertyListDto> allProperties = [
                ..entity.GetUpdateDtoProperties(),
                ..fieldInPageRelationProperties,
                ];
            return allProperties;
        }


        public static List<PropertyListDto> GetMiddleRelationListDtoProperties(this EntityFullInformationDto entity)
         => entity
             .GetMiddleRelations()
             .Select(a => new PropertyListDto()
             {
                 ForceDataTypeCode = a.GetMiddleRelationEntityPropertyDataType(),
                 Name = a.GetMiddleRelationEntityPropertyNameInListDto(),
                 IsRequired = true,
                 InitializationCode = $"= new {a.GetMiddleRelationEntityPropertyDataType()}();",
                 EntityRelationsList = new List<EntityRelationListDto>() { a }
             }).ToList();

        public static List<PropertyListDto> GetFieldInRelationListDtoProperties(this EntityFullInformationDto entity)
        => entity
            .FieldInRelationEntities
            .Select(a => new PropertyListDto()
            {
                ForceDataTypeCode = a.GetFieldRelationEntityPropertyDataTypeInListDto(),
                Name = a.GetFieldRelationEntityPropertyNameInDto(),
                IsRequired = true,
                InitializationCode = $"= new {a.GetFieldRelationEntityPropertyDataTypeInListDto()}();"
            }).ToList();


        public static List<PropertyListDto> GetEntityListDtoProperties(this EntityFullInformationDto entity, bool addMainProperty = true)
        {
            var allProperties = entity
                .Properties
                .Where(a => !a.ExcludeFromListDto)
                .ToList();
            List<PropertyListDto> notExcludedProperties = new List<PropertyListDto>();
            foreach (var property in allProperties)
            {
                notExcludedProperties.AddRange(property.GetSingleListDtoProperty(true, addMainProperty));
            }
            return notExcludedProperties;
        }

        public static List<PropertyListDto> GeIndexViewProperties(this EntityFullInformationDto entity)
        => entity
            .GetEntityListDtoProperties()
            .Where(a => a.ShowInList)
            .ToList();

        public static List<PropertyListDto> GetAllEntitiesListDtoProperties(this EntityFullInformationDto entity)
        => entity
            .GetAllEntities()
            .SelectMany(a => a.GetEntityListDtoProperties())
            .ToList();

        public static List<PropertyListDto> GetEntityUpdatableProperties(this EntityFullInformationDto entity)
        {
            var allProperties = entity
                .Properties
                .Where(a => a.IsUpdatable)
                .ToList();
            List<PropertyListDto> notExcludedProperties = new List<PropertyListDto>();
            foreach (var property in allProperties)
            {
                notExcludedProperties.AddRange(property.GetSingleListDtoProperty(false));
            }
            return notExcludedProperties;
        }

        public static List<PropertyListDto> GetEntityUpdatablePropertiesForToCreateDtoMapper(this EntityFullInformationDto entity)
        {
            List<PropertyListDto> properties = [.. entity.GetEntityUpdatableProperties()];
            properties
                .Where(a => a.IsAnyKindFile())
                .ToList()
                .ForEach(a => a.Name = a.GetFilePropertyDtoName());
            return properties;
        }

        public static List<PropertyListDto> GetSingleListDtoProperty(this PropertyListDto property,
            bool addRelationProperty = true, bool addMainProperty = true)
        {
            List<PropertyListDto> propertiesInList = new List<PropertyListDto>();
            PropertyListDto propertyCopy = property.DeepCopy();

            #region property has relation and needs another property aswell

            if (addRelationProperty)
                if (property.EntityRelation is not null)
                {
                    PropertyListDto relationPropertyCopy = propertyCopy.DeepCopy();
                    PropertyListDto relationProperty = relationPropertyCopy;
                    relationProperty.Name = relationPropertyCopy.EntityRelation!.SecondaryEntityTitleProperty!.GetRelationTitlePropertyNameInListDto(relationPropertyCopy);
                    relationProperty.IsRequired = relationPropertyCopy.EntityRelation!.SecondaryEntityTitleProperty!.IsRequired;
                    relationProperty.DataType = relationPropertyCopy.EntityRelation.SecondaryEntityTitleProperty.DataType;
                    relationProperty.ProjectEnumEnglishName = relationPropertyCopy.EntityRelation.SecondaryEntityTitleProperty.ProjectEnumEnglishName;
                    relationProperty.EntityRelationsList = relationPropertyCopy.EntityRelationsList;
                    //relationProperty.NameInDb = relationPropertyCopy.Name;
                    propertiesInList.Add(relationProperty);
                    propertyCopy.ShowInList = false;
                }

            #endregion

            if (addMainProperty || property.EntityRelation is null)
                propertiesInList.Add(propertyCopy);

            return propertiesInList;
        }


        public static List<EntityFullInformationDto> GetAllEntities(this List<EntityFullInformationDto> entities)
            => entities
            .SelectMany(a => a.GetAllEntities())
            .DistinctBy(a => a.Entity.Id)
            .ToList();

        public static List<EntityFullInformationDto> GetAllEntities(this EntityFullInformationDto entity)
        {
            var entities = new List<EntityFullInformationDto> { entity };

            foreach (var subEntity in entity.FieldInRelationEntities)
            {
                entities.AddRange(GetAllEntities(subEntity));
            }
            entities = entities
                .DistinctBy(a => a.Entity.Id)
                .Where(a => !a.Entity.IsExcluded)
                .ToList();

            return entities;
        }

        public static List<PropertyListDto> GetAllSelectProperties(this EntityFullInformationDto entity)
            => entity.GetAllEntities()
            .SelectMany(a => a.Properties
            .Where(a => a.IsSelect()))
            .ToList();

        public static List<PropertyListDto> GetSelectProperties(this EntityFullInformationDto entity)
    => entity.Properties
    .Where(a => a.IsSelect())
    .ToList();

        public static List<PropertyListDto> GetCustomFillingProperties(this EntityFullInformationDto entity)
=> entity.Properties
.Where(a => a.EntityRelation != null && !string.IsNullOrEmpty(a.EntityRelation.FillingCode))
.ToList();

        public static List<PropertyListDto> GetRelationsInRouting(this EntityFullInformationDto entity)
            => entity
                .Properties
                .Where(a => a.EntityRelation != null &&
                (a.EntityRelation.FillingType == FillingTypeEnum.Routing || a.EntityRelation.InputType == InputTypeEnum.FieldsInRelationEntityPage)
                )
                .ToList();


        public static string GetCreatePropertyMapperCode(this PropertyListDto property, EntityFullInformationDto entity)
        {
            string lineSeperator = ",";
            string instanceName = "create";
            string mappingCode = property.GetSharePropertyModelMappingCode(entity, instanceName, lineSeperator);
            return $@"                    {property.Name} = {mappingCode}";
        }

        public static string GetUpdatePropertyMapperCode(this PropertyListDto property, EntityFullInformationDto entity)
        {
            string lineSeperator = ";";
            string instanceName = "update";
            string mappingCode = property.GetSharePropertyModelMappingCode(entity, instanceName, lineSeperator);
            if (property.IsAnyKindFile())
                return $@"            if({instanceName}.{property.GetFilePropertyDtoName()} is not null)
                                              {entity.Entity.SingularName.ToFirstCharLower()}.{property.Name} = {mappingCode}";

            return $@"            {entity.Entity.SingularName.ToFirstCharLower()}.{property.Name} = {mappingCode}";
        }

        static string GetSharePropertyModelMappingCode(this PropertyListDto property, EntityFullInformationDto entity, string instanceName, string lineSeperator)
        {
            if (!string.IsNullOrEmpty(property.ForceMapperCode))
                return $@"{property.ForceMapperCode}{lineSeperator}";
            if (property.IsAnyKindFile())
                return $@"{property.GetUploadMediaMappingText(entity, instanceName, lineSeperator)}";
            if (property.EntityRelation != null && !property.IsRequired)
                return $@"{instanceName}.{property.Name} > 0 ? {instanceName}.{property.Name}: null{lineSeperator}";
            return $@"{instanceName}.{property.Name}{property.GetPropertyMappingExtensionMethod()}{lineSeperator}";
        }


        public static string GetUploadMediaMappingText(this PropertyListDto property, EntityFullInformationDto entity, string instanceName, string lineSeperator)
        {
            if (property.DataAnnotationDataType == DataAnnotationsDataType.Image)
                return property.GetUploadImageMappingText(entity, instanceName, lineSeperator);
            if (property.DataAnnotationDataType == DataAnnotationsDataType.Video)
                return property.GetUploadVideoMappingText(instanceName, lineSeperator);
            return property.GetUploadFileMappingText(instanceName, lineSeperator);
        }

        private static string GetUploadImageMappingText(this PropertyListDto property, EntityFullInformationDto entity, string instanceName, string lineSeperator)
        {
            string originalPathExtensionName = property.GetPathExtensionServerName("Original");
            string resizesCode = property.GetMediaInformation(entity.Entity);
            string preferedName = property.GetImagePreferedName(entity, instanceName);
            string editCountsCode = instanceName == "update" ? $"{entity.Entity.SingularName.ToFirstCharLower()}.EditCounts+1" : "0";
            string deleteImageNameCode = instanceName == "update" ? $"{entity.Entity.SingularName.ToFirstCharLower()}.{property.Name}" : "null";
            return $@"await {instanceName}.{property.GetFilePropertyDtoName()}.UploadLocalImageAsync(
                          {resizesCode},
                          {preferedName}, {editCountsCode}, {deleteImageNameCode}) ??""""{lineSeperator}";
            // return $@"await {instanceName}.{property.SingularName}.UploadImageAsync(PathExtension.{originalPathExtensionName},{resizesCode},{preferedName},{editCountsCode})??""""{lineSeperator}";
        }

        private static string GetUploadVideoMappingText(this PropertyListDto property, string instanceName, string lineSeperator)
        {
            string originalPathExtensionName = property.GetPathExtensionServerName("Original");
            string preferedName = property.GetVideoPreferedName();
            return $@"await {instanceName}.{property.GetFilePropertyDtoName()}.UploadVideoAsync(PathExtension.{originalPathExtensionName},{preferedName},0)??""""{lineSeperator}";
        }

        private static string GetUploadFileMappingText(this PropertyListDto property, string instanceName, string lineSeperator)
        {
            string originalPathExtensionName = property.GetPathExtensionServerName("Original");
            string preferedName = property.GetFilePreferedName();
            return $@"await {instanceName}.{property.GetFilePropertyDtoName()}.UploadFileAsync(PathExtension.{originalPathExtensionName},{preferedName},0)??""""{lineSeperator}";
        }


        public static string GetMediaInformation(this PropertyListDto property, EntityListDto entity)
            => $"{entity.SingularName}MediaInformation.{property.NameInDb}MediasInformation";
        public static string GetOriginalMediaInformation(this PropertyListDto property, EntityListDto entity)
            => $"{entity.SingularName}MediaInformation.Original{property.NameInDb}";
        public static string GetOriginalMediaInformation(this PropertyListDto property, string entitySingularName)
    => $"{entitySingularName}MediaInformation.Original{property.NameInDb}";

        public static string GetPathExtensionServerName(this PropertyListDto property, string pathTitle)
        {
            return $"{property.GetPathExtensionName(pathTitle)}Server";
        }

        public static string GetPathExtensionName(this PropertyListDto property, string pathTitle)
        {
            return $"{pathTitle}{property.EntityTitle}Image";
        }

        public static string GetImageResizesCode(this PropertyListDto property)
        {
            string codes = string.Join("\n", property.PropertyImageResizeInformationList
                .ToList()
                .ConvertAll(a => $@"new ResizeImageDto()
                {{
                    ImageHeight = {a.Height},
                    ImageWidth = {a.Width},
                    ResizedImagePath = PathExtension.{property.GetPathExtensionServerName(a.Name)},
                }},"));
            return $@" new List<ResizeImageDto>(){{{codes}}}";
        }

        public static string GetImagePreferedName(this PropertyListDto property, EntityFullInformationDto entity, string instanceName)
        {
            var firstUniqueProperty = entity.Properties
                .OrderBy(a => a.Order)
                .FirstOrDefault(a => a.IsUnique && a.IsRequired);
            return firstUniqueProperty is null ? $"Guid.NewGuid().ToString()" :
                $"{instanceName}.{firstUniqueProperty.Name}.ToUrl()";
        }

        public static string GetVideoPreferedName(this PropertyListDto property)
        {
            return $"Guid.NewGuid().ToString()";
        }

        public static string GetFilePreferedName(this PropertyListDto property)
        {
            return $"Guid.NewGuid().ToString()";
        }
        public static string FormatProperty(this string input)
        {
            // Split the string by '.' to separate different levels
            var parts = input.Split('.');

            // Initialize the formatted result with the first part (prefix)
            string formattedString = $"\"{parts[0]}\"";

            // Process each part to format brackets correctly
            for (int i = 1; i < parts.Length; i++)
            {
                int bracketIndex = parts[i].IndexOf('[');
                if (bracketIndex != -1)
                {
                    // Extract the name and bracketed value separately
                    string name = parts[i].Substring(0, bracketIndex);
                    string bracketValue = parts[i].Substring(bracketIndex + 1, parts[i].Length - bracketIndex - 2); // Remove '[' and ']'

                    formattedString += $" + \".{name}\" + \"[\" + {bracketValue} + \"]\"";
                }
                else
                {
                    formattedString += $" + \".{parts[i]}\"";
                }
            }

            return formattedString;
        }

        public static string GetImagePreviewClassName(this PropertyListDto property)
    => $@"@({property.AspFor.FormatProperty()})";

        public static string GetPropertyMappingExtensionMethod(this PropertyListDto property)
        {
            if (property.ProjectEnum is not null) return "";
            if (property.DataType == DataTypeEnum.String)
                return property.DataAnnotationDataType switch
                {
                    DataAnnotationsDataType.Title => ".ToTitle()!",
                    DataAnnotationsDataType.Text => ".ToText()!",
                    DataAnnotationsDataType.Others => property.DataType == DataTypeEnum.String ? ".SanitizeText()!" : "",
                    _ => ""
                };
            if (property.DataType == DataTypeEnum.DateTime)
                return property.DataAnnotationDataType switch
                {
                    DataAnnotationsDataType.PersianDate => $"{(property.IsRequired ? "" : "?")}.ToMiladiDateTime(){(property.IsRequired ? "?? new DateTime()" : "")}",
                    _ => ""
                };
            return string.Empty;
        }

        public static string GetFilePropertyDtoName(this PropertyListDto property)
            => $"{property.NameInDb}File";

        public static int CountUniqueProperties(this EntityFullInformationDto entity)
           => entity
                   .Properties
                   .GetUniqueProperties()
                   .Count();

        public static List<PropertyListDto> GetUniqueProperties(this EntityFullInformationDto entity)
   => entity
           .Properties
            .Where(a => a.IsUnique)
           .ToList();

        public static List<PropertyListDto> OnlyUpdatables(this List<PropertyListDto> properties)
            => properties
               .Where(a => a.IsUpdatable)
               .ToList();

        public static int CountUpdatableUniqueProperties(this EntityFullInformationDto entity)
            => entity
                    .Properties
                    .Where(a => a.IsUpdatable)
                    .ToList()
                    .GetUniqueProperties()
                    .Count();

        public static List<PropertyListDto> GetUniqueProperties(this List<PropertyListDto> properties)
=> properties
    .Where(a => string.IsNullOrEmpty(a.ForceMapperCode) && a.IsUnique)
    .ToList();

        public static string GetUpsertMethodReturnEnumName(this EntityFullInformationDto entity, int uniqueProperteisCount)
            => uniqueProperteisCount > 1 ? entity.GetChangeEntityEnumName()
                : "BaseChangeEntityResult";

        public static string GetCreateMethodReturnEnumName(this EntityFullInformationDto entity)
        => entity.GetUpsertMethodReturnEnumName(
   entity.CountUniqueProperties());
        public static string GetUpdateMethodReturnEnumName(this EntityFullInformationDto entity)
=> entity.GetUpsertMethodReturnEnumName(
entity.CountUpdatableUniqueProperties());


        public static string GetChangeEntityEnumName(this EntityFullInformationDto entity)
    => $@"Change{entity.Entity.SingularName}Result";

        public static string GetConstructorCode(this ConstructorListDto constructor)
        {
            constructor.DependencyInjections = constructor
                .DependencyInjections
                .DistinctBy(a => a.FileName)
                .ToList();
            string declrationCode = string.Join("\n", constructor.DependencyInjections
                .ConvertAll(a => $@"        private readonly {a.FileName} {a.Name.ToUnderscoreLower()};"));
            string insideParanthesisCode = string.Join("\n", constructor.DependencyInjections
                .ConvertAll(a => $@"                           {a.FileName} {a.Name.ToFirstCharLower()},"));
            insideParanthesisCode = insideParanthesisCode.ReplaceLastOccurrence(',', ' ');
            string mappingCode = string.Join("\n", constructor.DependencyInjections
    .ConvertAll(a => $@"            this.{a.Name.ToUnderscoreLower()} = {a.Name.ToFirstCharLower()};"));

            return $@"        #region constructor

{declrationCode}
        public {constructor.ClassName}(
{insideParanthesisCode}
                                      )
        {{
{mappingCode}
        }}

        #endregion";
        }

        public static List<EntityFullInformationDto> GetAllEntitiesWithSameServiceName(this EntityFullInformationDto entity)
            => entity.GetAllEntities()
                .Where(a => a.Entity.ServiceName == entity.Entity.ServiceName)
                .ToList();

        public static List<EntityFullInformationDto> GetAllEntitiesWithDistinctServiceName(this EntityFullInformationDto entity)
            => entity.GetAllEntities()
                .DistinctBy(a => a.Entity.ServiceName)
                .ToList();

        public static string ToUnderscoreLower(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return "_" + char.ToLower(input[0]) + input.Substring(1);
        }

        public static string GetViewDatasMethodsCodes(this List<ViewDataListDto> viewDatas)
            => string.Join('\n', viewDatas
                .Distinct()
                .ToList()
                .Select(a => a.GetSingleViewDataMethodCode()));

        public static string GetViewDatasMethodsNames(this List<ViewDataListDto> viewDatas)
            => string.Join('\n', viewDatas
                .Distinct()
                .ToList()
                .Select(a => a.GetSingleViewDataMethodName()));

        public static string GetSingleViewDataMethodCode(this ViewDataListDto viewData)
        {
            string options = viewData.IsRequired ? "" : "false,true";
            return $@"
        async Task Get{viewData.Name}ViewData()
        => ViewData[""{viewData.Name}""] = (await _{viewData.ServiceName.ToFirstCharLower()}
        .Get{viewData.Name}AsCombo(new Filter{viewData.Name}Dto()))
        .ToSelectListItem({options});";
        }

        public static string GetSingleViewDataMethodName(this ViewDataListDto viewData)
        => $"            await Get{viewData.Name}ViewData();";

        public static string GetSingleResource(this ResourceListDto resource)
        => $@"
  <data name=""{resource.Name}"" xml:space=""preserve"">
    <value>{(string.IsNullOrEmpty(resource.Value) ? resource.Name : resource.Value)}</value>
  </data>";

        public static string GetResources(this List<ResourceListDto> resources)
        {
            resources = resources
                .DistinctBy(a => a.Name)
                .ToList();
            string itemsCode = string.Join("\n",
                resources
                .ConvertAll(a => a.GetSingleResource()));
            return $@"<?xml version=""1.0"" encoding=""utf-8""?>
<root>
{ResourceExtensionMethods.GetHeadersAndComments()}
{itemsCode}
</root>";
        }

        public static string GetResources(this List<PropertyListDto> properties, ProjectSelectedLanguageListDto language)
        {
            List<ResourceListDto> resources = properties
                .Select(a => new ResourceListDto()
                {
                    Name = (string.IsNullOrEmpty(a.NameInDb) ? a.Name : a.NameInDb),
                    Value = a.PropertySelectedLanguagesList
                    .Where(b => b.LanguageId == language.LanguageId)
                    .Select(b => b.Title)
                    .FirstOrDefault() ?? "",
                }).ToList();
            return resources.GetResources();
        }

        public static string GetResources(this List<ProjectEnumPropertyListDto> properties, ProjectSelectedLanguageListDto language)
        {
            List<ResourceListDto> resources = properties
                .Select(a => new ResourceListDto()
                {
                    Name = a.Name,
                    Value = a.ProjectEnumPropertySelectedLanguagesList
                    .Where(b => b.LanguageId == language.LanguageId)
                    .Select(b => b.Title)
                    .FirstOrDefault() ?? "",
                }).ToList();
            return resources.GetResources();
        }


        public static GenerateFileDto GetDesignerFile(this string name, string nameSpace, string path, List<string> properties)
        {
            string propertiesCode = string.Join("\n", properties
                .ConvertAll(a => $@"
        /// <summary>
        ///   Looks up a localized string similar to {a}
        /// </summary>
        public static string {a} {{
            get {{
                return ResourceManager.GetString(""{a}"", resourceCulture);
            }}
        }}"));
            return new GenerateFileDto()
            {
                FileNameWithExtension = $"{name}.Designer.cs",
                Path = path,
                Override = false,
                Code = $@"//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace {nameSpace} {{
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute(""System.Resources.Tools.StronglyTypedResourceBuilder"", ""17.0.0.0"")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class {name} {{
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(""Microsoft.Performance"", ""CA1811:AvoidUncalledPrivateCode"")]
        internal {name}() {{
        }}
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {{
            get {{
                if (object.ReferenceEquals(resourceMan, null)) {{
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager(""{nameSpace}.{name}"", typeof({name}).Assembly);
                    resourceMan = temp;
                }}
                return resourceMan;
            }}
        }}
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {{
            get {{
                return resourceCulture;
            }}
            set {{
                resourceCulture = value;
            }}
        }}
        
        {propertiesCode}
    }}
}}
"
            };
        }

        public static string GetResourceName(this ProjectEnumListDto projectEnum)
    => $@"{projectEnum.EnglishName}Resources";

        public static List<PropertyListDto> GetHiddenInputs(this EntityFullInformationDto entity)
    => entity.Properties
    .Where(a => a.IsHiddenInput())
    .ToList();

        public static List<PropertyListDto> GetImageInputs(this EntityFullInformationDto entity)
=> entity.Properties
.Where(a => a.DataAnnotationDataType == DataAnnotationsDataType.Image)
.ToList();

        public static bool IsHiddenInput(this PropertyListDto property)
        {
            if (property.DataAnnotationDataType == DataAnnotationsDataType.Hidden) return true;
            return property.EntityRelation != null && property.EntityRelation.InputType is InputTypeEnum.Hidden or InputTypeEnum.FieldsInRelationEntityPage;
        }
        public static bool IsRouting(this PropertyListDto property)
           => property.EntityRelation != null && property.EntityRelation.FillingType == FillingTypeEnum.Routing;

        public static bool IsNoInput(this PropertyListDto property)
            => !string.IsNullOrEmpty(property.ForceMapperCode) ||
               property.EntityRelation != null && property.EntityRelation.InputType == InputTypeEnum.NoInput;

        public static MenuItemDto GetMenuItem(this EntityFullInformationDto entity)
    => new MenuItemDto()
    {
        ControllerName = entity.Entity.SingularName,
        ActionName = "index",
        Enabled = true,
        HasCrud = true,
        IconName = entity.Entity.SingularName.ToFirstCharLower(),
        Title = entity.Entity.SingularName,
        PluralTitle = entity.Entity.PluralName,
        FolderName = $"{entity.Entity.PluralName}Management",

    };

        public static string GetPostMethodRoutingVariablesRediretionCode(this EntityFullInformationDto entity, string instanceName)
        {
            List<PropertyListDto> routingProperties = entity.GetRelationsInRouting();
            string routingVariablesRedirectionCode = string.Join("\n", routingProperties
                .ConvertAll(a => $@"{a.Name.ToFirstCharLower()}={instanceName}.{a.Name},"));
            routingVariablesRedirectionCode.ReplaceLastOccurrence(',', ' ');
            return routingVariablesRedirectionCode;
        }

    }
}
