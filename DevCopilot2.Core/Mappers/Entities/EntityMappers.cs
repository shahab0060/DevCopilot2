using System;
using DevCopilot2.Domain.Entities.Entities;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;
using DevCopilot2.Domain.Enums.DataTypes;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.Enums.Relations;

namespace DevCopilot2.Core.Mappers.Entities
{
    public static class EntityMappers
    {
        #region to dto

        public static IQueryable<EntityListDto>ToDto(this IQueryable<Entity> query)
                    => query.Select(entity => new EntityListDto()
                    {

                        Id = entity.Id,
                        LatestEditDate = entity.LatestEditDate,
                        CreateDate = entity.CreateDate,
                        EditCounts = entity.EditCounts,

                        SingularName = entity.SingularName,
                        PluralName = entity.PluralName,
                        FolderName = entity.FolderName,
                        InheritedEntityPluralName = entity.InheritedEntity.PluralName,
                        InheritedEntityId = entity.InheritedEntityId,
                        IdType = entity.IdType,
                        ServiceName = entity.ServiceName,
                        AuthorPhoneNumber = entity.Author.PhoneNumber,
                        AuthorId = entity.AuthorId,
                        ProjectTitle = entity.Project.Title,
                        ProjectId = entity.ProjectId,
                        IsExcluded = entity.IsExcluded,
                        AddToMenu = entity.AddToMenu,

                        PropertiesList = entity.Properties
                        .Select(property => new PropertyListDto()
                        {

                        Id = property.Id,
                        LatestEditDate = property.LatestEditDate,
                        CreateDate = property.CreateDate,
                        EditCounts = property.EditCounts,

                        Name = property.Name,
                        DataType = property.DataType,
                        MaxLength = property.MaxLength,
                        MinLength = property.MinLength,
                        RangeFrom = property.RangeFrom,
                        RangeTo = property.RangeTo,
                        IsRequired = property.IsRequired,
                        ProjectEnumEnglishName = property.ProjectEnum.EnglishName,
                        ProjectEnumId = property.ProjectEnumId,
                        DataAnnotationDataType = property.DataAnnotationDataType,
                        IsUnique = property.IsUnique,
                        IsUpdatable = property.IsUpdatable,
                        ShowInList = property.ShowInList,
                        IsFilterContain = property.IsFilterContain,
                        IsFilterEqual = property.IsFilterEqual,
                        Order = property.Order,
                        UseEditor = property.UseEditor,
                        EntityPluralName = property.Entity.PluralName,
                        EntityId = property.EntityId,
                        ForceMapperCode = property.ForceMapperCode,
                        ExcludeFromListDto = property.ExcludeFromListDto,

                        EntityRelationsList = property.EntityRelations
                        .Select(entityRelation => new EntityRelationListDto()
                        {

                        Id = entityRelation.Id,
                        LatestEditDate = entityRelation.LatestEditDate,
                        CreateDate = entityRelation.CreateDate,
                        EditCounts = entityRelation.EditCounts,

                        PrimaryPropertyName = entityRelation.PrimaryProperty.Name,
                        PrimaryPropertyId = entityRelation.PrimaryPropertyId,
                        SecondaryEntityPluralName = entityRelation.SecondaryEntity.PluralName,
                        SecondaryEntityId = entityRelation.SecondaryEntityId,
                        MiddleEntityPluralName = entityRelation.MiddleEntity.PluralName,
                        MiddleEntityId = entityRelation.MiddleEntityId,
                        RelationType = entityRelation.RelationType,
                        InputType = entityRelation.InputType,
                        FillingType = entityRelation.FillingType,
                        FillingCode = entityRelation.FillingCode,

                        })
                        .ToList(),
                        PropertyImageResizeInformationList = property.PropertyImageResizeInformation
                        .Select(propertyImageResizeInformation => new PropertyImageResizeInformationListDto()
                        {

                        Id = propertyImageResizeInformation.Id,
                        LatestEditDate = propertyImageResizeInformation.LatestEditDate,
                        CreateDate = propertyImageResizeInformation.CreateDate,
                        EditCounts = propertyImageResizeInformation.EditCounts,

                        PropertyName = propertyImageResizeInformation.Property.Name,
                        PropertyId = propertyImageResizeInformation.PropertyId,
                        Name = propertyImageResizeInformation.Name,
                        Width = propertyImageResizeInformation.Width,
                        Height = propertyImageResizeInformation.Height,

                        })
                        .ToList(),
                        PropertySelectedLanguagesList = property.PropertySelectedLanguages
                        .Select(propertySelectedLanguage => new PropertySelectedLanguageListDto()
                        {

                        Id = propertySelectedLanguage.Id,
                        LatestEditDate = propertySelectedLanguage.LatestEditDate,
                        CreateDate = propertySelectedLanguage.CreateDate,
                        EditCounts = propertySelectedLanguage.EditCounts,

                        PropertyName = propertySelectedLanguage.Property.Name,
                        PropertyId = propertySelectedLanguage.PropertyId,
                        LanguageName = propertySelectedLanguage.Language.Name,
                        LanguageId = propertySelectedLanguage.LanguageId,
                        Title = propertySelectedLanguage.Title,

                        })
                        .ToList(),
                        })
                        .ToList(),
                        EntitySelectedProjectAreasList = entity.EntitySelectedProjectAreas
                        .Select(entitySelectedProjectArea => new EntitySelectedProjectAreaListDto()
                        {

                        Id = entitySelectedProjectArea.Id,
                        LatestEditDate = entitySelectedProjectArea.LatestEditDate,
                        CreateDate = entitySelectedProjectArea.CreateDate,
                        EditCounts = entitySelectedProjectArea.EditCounts,

                        EntityPluralName = entitySelectedProjectArea.Entity.PluralName,
                        EntityId = entitySelectedProjectArea.EntityId,
                        ProjectAreaTitle = entitySelectedProjectArea.ProjectArea.Title,
                        ProjectAreaId = entitySelectedProjectArea.ProjectAreaId,
                        HasIndex = entitySelectedProjectArea.HasIndex,
                        HasCreate = entitySelectedProjectArea.HasCreate,
                        HasUpdate = entitySelectedProjectArea.HasUpdate,
                        HasDelete = entitySelectedProjectArea.HasDelete,
                        HasApi = entitySelectedProjectArea.HasApi,
                        HasWeb = entitySelectedProjectArea.HasWeb,

                        EntitySelectedProjectAreaSelectedFiltersList = entitySelectedProjectArea.EntitySelectedProjectAreaSelectedFilters
                        .Select(entitySelectedProjectAreaSelectedFilter => new EntitySelectedProjectAreaSelectedFilterListDto()
                        {

                        Id = entitySelectedProjectAreaSelectedFilter.Id,
                        LatestEditDate = entitySelectedProjectAreaSelectedFilter.LatestEditDate,
                        CreateDate = entitySelectedProjectAreaSelectedFilter.CreateDate,
                        EditCounts = entitySelectedProjectAreaSelectedFilter.EditCounts,

                        EntitySelectedProjectAreaHasWeb = entitySelectedProjectAreaSelectedFilter.EntitySelectedProjectArea.HasWeb,
                        EntitySelectedProjectAreaId = entitySelectedProjectAreaSelectedFilter.EntitySelectedProjectAreaId,
                        PropertyName = entitySelectedProjectAreaSelectedFilter.Property.Name,
                        PropertyId = entitySelectedProjectAreaSelectedFilter.PropertyId,
                        Value = entitySelectedProjectAreaSelectedFilter.Value,

                        })
                        .ToList(),
                        })
                        .ToList(),
                        EntitySelectedLanguagesList = entity.EntitySelectedLanguages
                        .Select(entitySelectedLanguage => new EntitySelectedLanguageListDto()
                        {

                        Id = entitySelectedLanguage.Id,
                        LatestEditDate = entitySelectedLanguage.LatestEditDate,
                        CreateDate = entitySelectedLanguage.CreateDate,
                        EditCounts = entitySelectedLanguage.EditCounts,

                        EntityPluralName = entitySelectedLanguage.Entity.PluralName,
                        EntityId = entitySelectedLanguage.EntityId,
                        LanguageName = entitySelectedLanguage.Language.Name,
                        LanguageId = entitySelectedLanguage.LanguageId,
                        SingularTitle = entitySelectedLanguage.SingularTitle,
                        PluralTitle = entitySelectedLanguage.PluralTitle,

                        })
                        .ToList(),
                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateEntityDto>ToUpdateDto(this IQueryable<Entity> query)
                    => query.Select(entity => new UpdateEntityDto()
                    {

                        Id = entity.Id,

                        SingularName = entity.SingularName,
                        PluralName = entity.PluralName,
                        FolderName = entity.FolderName,
                        InheritedEntityId = entity.InheritedEntityId,
                        IdType = entity.IdType,
                        ServiceName = entity.ServiceName,
                        AuthorId = entity.AuthorId,
                        ProjectId = entity.ProjectId,
                        IsExcluded = entity.IsExcluded,
                        AddToMenu = entity.AddToMenu,

                        PropertiesList = entity.Properties
                        .Select(property => new UpdatePropertyDto()
                        {

                        Id = property.Id,

                        Name = property.Name,
                        DataType = property.DataType,
                        MaxLength = property.MaxLength,
                        MinLength = property.MinLength,
                        RangeFrom = property.RangeFrom,
                        RangeTo = property.RangeTo,
                        IsRequired = property.IsRequired,
                        ProjectEnumId = property.ProjectEnumId,
                        DataAnnotationDataType = property.DataAnnotationDataType,
                        IsUnique = property.IsUnique,
                        IsUpdatable = property.IsUpdatable,
                        ShowInList = property.ShowInList,
                        IsFilterContain = property.IsFilterContain,
                        IsFilterEqual = property.IsFilterEqual,
                        Order = property.Order,
                        UseEditor = property.UseEditor,
                        EntityId = property.EntityId,
                        ForceMapperCode = property.ForceMapperCode,
                        ExcludeFromListDto = property.ExcludeFromListDto,

                        EntityRelationsList = property.EntityRelations
                        .Select(entityRelation => new UpdateEntityRelationDto()
                        {

                        Id = entityRelation.Id,

                        PrimaryPropertyId = entityRelation.PrimaryPropertyId,
                        SecondaryEntityId = entityRelation.SecondaryEntityId,
                        MiddleEntityId = entityRelation.MiddleEntityId,
                        RelationType = entityRelation.RelationType,
                        InputType = entityRelation.InputType,
                        FillingType = entityRelation.FillingType,
                        FillingCode = entityRelation.FillingCode,

                        })
                        .ToList(),
                        PropertyImageResizeInformationList = property.PropertyImageResizeInformation
                        .Select(propertyImageResizeInformation => new UpdatePropertyImageResizeInformationDto()
                        {

                        Id = propertyImageResizeInformation.Id,

                        PropertyId = propertyImageResizeInformation.PropertyId,
                        Name = propertyImageResizeInformation.Name,
                        Width = propertyImageResizeInformation.Width,
                        Height = propertyImageResizeInformation.Height,

                        })
                        .ToList(),
                        PropertySelectedLanguagesList = property.PropertySelectedLanguages
                        .Select(propertySelectedLanguage => new UpdatePropertySelectedLanguageDto()
                        {

                        Id = propertySelectedLanguage.Id,

                        PropertyId = propertySelectedLanguage.PropertyId,
                        LanguageId = propertySelectedLanguage.LanguageId,
                        Title = propertySelectedLanguage.Title,

                        })
                        .ToList(),
                        })
                        .ToList(),
                        EntitySelectedProjectAreasList = entity.EntitySelectedProjectAreas
                        .Select(entitySelectedProjectArea => new UpdateEntitySelectedProjectAreaDto()
                        {

                        Id = entitySelectedProjectArea.Id,

                        EntityId = entitySelectedProjectArea.EntityId,
                        ProjectAreaId = entitySelectedProjectArea.ProjectAreaId,
                        HasIndex = entitySelectedProjectArea.HasIndex,
                        HasCreate = entitySelectedProjectArea.HasCreate,
                        HasUpdate = entitySelectedProjectArea.HasUpdate,
                        HasDelete = entitySelectedProjectArea.HasDelete,
                        HasApi = entitySelectedProjectArea.HasApi,
                        HasWeb = entitySelectedProjectArea.HasWeb,

                        EntitySelectedProjectAreaSelectedFiltersList = entitySelectedProjectArea.EntitySelectedProjectAreaSelectedFilters
                        .Select(entitySelectedProjectAreaSelectedFilter => new UpdateEntitySelectedProjectAreaSelectedFilterDto()
                        {

                        Id = entitySelectedProjectAreaSelectedFilter.Id,

                        EntitySelectedProjectAreaId = entitySelectedProjectAreaSelectedFilter.EntitySelectedProjectAreaId,
                        PropertyId = entitySelectedProjectAreaSelectedFilter.PropertyId,
                        Value = entitySelectedProjectAreaSelectedFilter.Value,

                        })
                        .ToList(),
                        })
                        .ToList(),
                        EntitySelectedLanguagesList = entity.EntitySelectedLanguages
                        .Select(entitySelectedLanguage => new UpdateEntitySelectedLanguageDto()
                        {

                        Id = entitySelectedLanguage.Id,

                        EntityId = entitySelectedLanguage.EntityId,
                        LanguageId = entitySelectedLanguage.LanguageId,
                        SingularTitle = entitySelectedLanguage.SingularTitle,
                        PluralTitle = entitySelectedLanguage.PluralTitle,

                        })
                        .ToList(),
                    });

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<Entity> query)
			    => query.Select(entity => new ComboDto()
			{
            Title = entity.SingularName,
            Value = entity.Id.ToString()
            });

        #endregion

        #region to create model

        public static Entity ToModel(this CreateEntityDto create)
				=> new Entity()
				{
                    SingularName = create.SingularName.ToTitle()!,
                    PluralName = create.PluralName.ToTitle()!,
                    FolderName = create.FolderName.ToTitle()!,
                    InheritedEntityId = create.InheritedEntityId > 0 ? create.InheritedEntityId: null,
                    IdType = create.IdType,
                    ServiceName = create.ServiceName.SanitizeText()!,
                    AuthorId = create.AuthorId > 0 ? create.AuthorId: null,
                    ProjectId = create.ProjectId,
                    IsExcluded = create.IsExcluded,
                    AddToMenu = create.AddToMenu,
				};

        #endregion

        #region to update model

        public static Entity ToModel(this Entity entity, UpdateEntityDto update)
        {
            entity.SingularName = update.SingularName.ToTitle()!;
            entity.PluralName = update.PluralName.ToTitle()!;
            entity.FolderName = update.FolderName.ToTitle()!;
            entity.InheritedEntityId = update.InheritedEntityId > 0 ? update.InheritedEntityId: null;
            entity.IdType = update.IdType;
            entity.ServiceName = update.ServiceName.SanitizeText()!;
            entity.AuthorId = update.AuthorId > 0 ? update.AuthorId: null;
            entity.ProjectId = update.ProjectId;
            entity.IsExcluded = update.IsExcluded;
            entity.AddToMenu = update.AddToMenu;
            return entity;
        }

        #endregion

    }
}
