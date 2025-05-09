using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Security;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.Entities.Properties;

namespace DevCopilot2.Core.Mappers.Properties
{
    public static class PropertyMappers
    {
        #region to dto

        public static IQueryable<PropertyListDto> ToDto(this IQueryable<Property> query)
                    => query.Select(property => new PropertyListDto()
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

                            PrimaryPropertyTitle = entityRelation.PrimaryProperty.Name,
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
                        ProjectId = property.Entity.ProjectId
                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdatePropertyDto> ToUpdateDto(this IQueryable<Property> query)
                    => query.Select(property => new UpdatePropertyDto()
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
                    });

        #endregion

        #region to create dto

        public static List<CreatePropertyDto> ToCreateDto(this IEnumerable<UpdatePropertyDto> properties)
                    => properties.Select(property => new CreatePropertyDto()
                    {

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

                        EntityRelationsList = property.EntityRelationsList
                        .Select(entityRelation => new CreateEntityRelationDto()
                        {

                            PrimaryPropertyId = entityRelation.PrimaryPropertyId,
                            SecondaryEntityId = entityRelation.SecondaryEntityId,
                            MiddleEntityId = entityRelation.MiddleEntityId,
                            RelationType = entityRelation.RelationType,
                            InputType = entityRelation.InputType,
                            FillingType = entityRelation.FillingType,
                            FillingCode = entityRelation.FillingCode,

                        })
                        .ToList(),
                        PropertyImageResizeInformationList = property.PropertyImageResizeInformationList
                        .Select(propertyImageResizeInformation => new CreatePropertyImageResizeInformationDto()
                        {

                            PropertyId = propertyImageResizeInformation.PropertyId,
                            Name = propertyImageResizeInformation.Name,
                            Width = propertyImageResizeInformation.Width,
                            Height = propertyImageResizeInformation.Height,

                        })
                        .ToList(),
                        PropertySelectedLanguagesList = property.PropertySelectedLanguagesList
                        .Select(propertySelectedLanguage => new CreatePropertySelectedLanguageDto()
                        {

                            PropertyId = propertySelectedLanguage.PropertyId,
                            LanguageId = propertySelectedLanguage.LanguageId,
                            Title = propertySelectedLanguage.Title,

                        })
                        .ToList(),
                    }).ToList();

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<Property> query)
                => query.Select(property => new ComboDto()
                {
                    Title = property.Name,
                    Value = property.Id.ToString()
                });

        #endregion

        #region to create model

        public static Property ToModel(this CreatePropertyDto create)
                => new Property()
                {
                    Name = create.Name.ToTitle()!,
                    DataType = create.DataType,
                    MaxLength = create.MaxLength,
                    MinLength = create.MinLength,
                    RangeFrom = create.RangeFrom,
                    RangeTo = create.RangeTo,
                    IsRequired = create.IsRequired,
                    ProjectEnumId = create.ProjectEnumId > 0 ? create.ProjectEnumId : null,
                    DataAnnotationDataType = create.DataAnnotationDataType,
                    IsUnique = create.IsUnique,
                    IsUpdatable = create.IsUpdatable,
                    ShowInList = create.ShowInList,
                    IsFilterContain = create.IsFilterContain,
                    IsFilterEqual = create.IsFilterEqual,
                    Order = create.Order,
                    UseEditor = create.UseEditor,
                    EntityId = create.EntityId,
                    ForceMapperCode = create.ForceMapperCode.SanitizeText()!,
                    ExcludeFromListDto = create.ExcludeFromListDto,
                };

        #endregion

        #region to update model

        public static Property ToModel(this Property property, UpdatePropertyDto update)
        {
            property.Name = update.Name.ToTitle()!;
            property.DataType = update.DataType;
            property.MaxLength = update.MaxLength;
            property.MinLength = update.MinLength;
            property.RangeFrom = update.RangeFrom;
            property.RangeTo = update.RangeTo;
            property.IsRequired = update.IsRequired;
            property.ProjectEnumId = update.ProjectEnumId > 0 ? update.ProjectEnumId : null;
            property.DataAnnotationDataType = update.DataAnnotationDataType;
            property.IsUnique = update.IsUnique;
            property.IsUpdatable = update.IsUpdatable;
            property.ShowInList = update.ShowInList;
            property.IsFilterContain = update.IsFilterContain;
            property.IsFilterEqual = update.IsFilterEqual;
            property.Order = update.Order;
            property.UseEditor = update.UseEditor;
            property.EntityId = update.EntityId;
            property.ForceMapperCode = update.ForceMapperCode.SanitizeText()!;
            property.ExcludeFromListDto = update.ExcludeFromListDto;
            return property;
        }

        #endregion

    }
}
