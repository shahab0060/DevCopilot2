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
using DevCopilot2.Domain.DTOs.Generators;
using DevCopilot2.Domain.DTOs.Templates;
using DevCopilot2.Domain.DTOs.Projects;

namespace DevCopilot2.Core.Mappers.Entities
{
    public static class EntityMappers
    {
        #region to full Dto

        public static IQueryable<EntityFullInformationDto> ToFullDto(this IQueryable<Entity> query)
    => query
    .Select(a => new EntityFullInformationDto()
    {
        Entity = new DevCopilot2.Domain.DTOs.Entities.EntityListDto()
        {
            AddToMenu = a.AddToMenu,
            AuthorId = a.AuthorId,
            AuthorName = a.Author.FirstName,
            AuthorPhoneNumber = a.Author.PhoneNumber,
            CreateDate = a.CreateDate,
            EditCounts = a.EditCounts,
            FolderName = a.FolderName,
            Id = a.Id,
            IdType = a.IdType,
            InheritedEntityId = a.InheritedEntityId,
            InheritedEntityPluralName = a.InheritedEntity!.SingularName,
            SingularName = a.SingularName,
            IsExcluded = a.IsExcluded,
            LatestEditDate = a.LatestEditDate,
            PluralName = a.PluralName,
            ProjectId = a.ProjectId,
            ProjectTitle = a.Project.EnglishName,
            ServiceName = a.ServiceName!,
            EntitySelectedProjectAreasList = a.EntitySelectedProjectAreas
            .Select(r => new DevCopilot2.Domain.DTOs.Entities.EntitySelectedProjectAreaListDto()
            {
                CreateDate = r.CreateDate,
                LatestEditDate = r.LatestEditDate,
                Id = r.Id,
                EditCounts = r.EditCounts,
                EntityId = r.EntityId,
                EntityPluralName = r.Entity.SingularName,
                HasApi = r.HasApi,
                HasCreate = r.HasCreate,
                HasDelete = r.HasDelete,
                HasIndex = r.HasIndex,
                HasUpdate = r.HasUpdate,
                HasWeb = r.HasWeb,
                ProjectAreaId = r.ProjectAreaId,
                ProjectAreaTitle = r.ProjectArea.EnglishName,
                Template = new TemplateListDto()
                {
                    Id = r.ProjectArea.Template.Id,
                    LatestEditDate = r.ProjectArea.Template.LatestEditDate,
                    CreateDate = r.ProjectArea.Template.CreateDate,
                    EditCounts = r.ProjectArea.Template.EditCounts,
                    Title = r.ProjectArea.Template.Title,
                    ProjectId = r.ProjectArea.Template.ProjectId,
                    ListViewHtml = r.ProjectArea.Template.ListViewHtml,
                    ListFirstThCode = r.ProjectArea.Template.ListFirstThCode,
                    ListOtherThCodes = r.ProjectArea.Template.ListOtherThCodes,
                    ListBoolTdHtml = r.ProjectArea.Template.ListBoolTdHtml,
                    ListTextTdHtml = r.ProjectArea.Template.ListTextTdHtml,
                    ListImageTdHtml = r.ProjectArea.Template.ListImageTdHtml,
                    ListPriceTdHtml = r.ProjectArea.Template.ListPriceTdHtml,
                    ListDefaultTdCode = r.ProjectArea.Template.ListDefaultTdCode,
                    CreatePageHtml = r.ProjectArea.Template.CreatePageHtml,
                    CheckBoxInputCode = r.ProjectArea.Template.CheckBoxInputCode,
                    FileInputCode = r.ProjectArea.Template.FileInputCode,
                    TextInputHtml = r.ProjectArea.Template.TextInputHtml,
                    TextEditorInputHtml = r.ProjectArea.Template.TextEditorInputHtml,
                    IntegerInputHtml = r.ProjectArea.Template.IntegerInputHtml,
                    ProjectTitle = r.ProjectArea.Template.Project.Title,
                    AuthorId = r.ProjectArea.Template.AuthorId,
                    AuthorName = r.ProjectArea.Template.Author.FirstName,
                    ListViewCardHtml = r.ProjectArea.Template.ListViewCardHtml,
                    SelectInputHtml = r.ProjectArea.Template.SelectInputHtml,
                    SingleImageHtml = r.ProjectArea.Template.SingleImageHtml,
                    ColorPickerInputCode = r.ProjectArea.Template.ColorPickerInputCode,
                    BreadCrumbCode = r.ProjectArea.Template.BreadCrumbCode,
                    AnchorTagCode = r.ProjectArea.Template.AnchorTagCode,
                    SubmitBtnCode = r.ProjectArea.Template.SubmitBtnCode
                },

                EntitySelectedProjectAreaSelectedFiltersList = r.EntitySelectedProjectAreaSelectedFilters
                .Select(f => new DevCopilot2.Domain.DTOs.Entities.EntitySelectedProjectAreaSelectedFilterListDto()
                {
                    CreateDate = f.CreateDate,
                    LatestEditDate = f.LatestEditDate,
                    EditCounts = f.EditCounts,
                    EntitySelectedProjectAreaId = f.EntitySelectedProjectAreaId,
                    EntitySelectedProjectAreaName = f.EntitySelectedProjectArea.ProjectArea.Title,
                    Id = f.Id,
                    PropertyId = f.PropertyId,
                    PropertyName = f.Property.Name,
                    Value = f.Value,
                }).ToList(),
            }).ToList(),
            EntitySelectedLanguagesList = a.EntitySelectedLanguages
            .Select(r => new DevCopilot2.Domain.DTOs.Entities.EntitySelectedLanguageListDto()
            {
                CreateDate = r.CreateDate,
                LatestEditDate = r.LatestEditDate,
                Id = r.Id,
                EditCounts = r.EditCounts,
                LanguageId = r.LanguageId,
                EntityId = r.EntityId,
                EntityPluralName = r.Entity.SingularName,
                PluralTitle = r.PluralTitle,
                SingularTitle = r.SingularTitle,
                LanguageName = r.Language.Name,
            }).ToList(),
        },
        Project = new DevCopilot2.Domain.DTOs.Projects.ProjectListDto()
        {
            Architecture = a.Project.Architecture,
            AuthorId = a.Project.AuthorId,
            AuthorName = a.Project.Author.PhoneNumber,
            AuthorPhoneNumber = a.Project.Author.PhoneNumber,
            EditCounts = a.Project.EditCounts,
            CreateDate = a.Project.CreateDate,
            LatestEditDate = a.Project.LatestEditDate,
            Id = a.ProjectId,
            EnglishName = a.Project.EnglishName,
            Location = a.Project.Location,
            ReactProjectLocation = a.Project.ReactProjectLocation,
            Title = a.Project.Title,
            ProjectAreasList = a.Project.ProjectAreas!
            .Select(r => new ProjectAreaListDto()
            {
                Title = r.Title,
                CreateDate = r.CreateDate,
                EditCounts = r.EditCounts,
                EnglishName = r.EnglishName,
                Id = r.Id,
                LatestEditDate = r.LatestEditDate,
                ProjectId = r.ProjectId,
                ProjectTitle = r.Project!.Title,
                TemplateId = r.TemplateId,
                TemplateTitle = r.Template.Title
            }).ToList(),
            ProjectSelectedLanguagesList = a.Project.ProjectSelectedLanguages
            .Select(r => new ProjectSelectedLanguageListDto()
            {
                LanguageId = r.LanguageId,
                LanguageName = r.Language.Name,
                ProjectId = r.ProjectId,
                ProjectTitle = r.Project.Title,
                LanguageCulture = r.Language.Culture
            }).ToList()
        },
        Relations = a.SecondaryEntityList!
        //.Where(a => !a.PrimaryProperty.Entity.IsExcluded)
        .Select(r => new DevCopilot2.Domain.DTOs.Entities.EntityRelationListDto()
        {

            CreateDate = r.CreateDate,
            EditCounts = r.EditCounts,
            Id = r.Id,
            LatestEditDate = r.LatestEditDate,
            FillingCode = r.FillingCode,
            FillingType = r.FillingType,
            InputType = r.InputType,
            MiddleEntityId = r.MiddleEntityId,
            MiddleEntityTitle = r.MiddleEntity!.SingularName,
            MiddleEntityFolderName = r.MiddleEntity!.FolderName,
            MiddleEntityServiceName = r.MiddleEntity!.ServiceName,
            PrimaryPropertyEntityFolderName = r.PrimaryProperty.Entity.FolderName,
            PrimaryPropertyEntityPluralTitle = r.PrimaryProperty.Entity.PluralName,
            PrimaryPropertyEntityTitle = r.PrimaryProperty.Entity.SingularName,
            PrimaryPropertyId = r.PrimaryPropertyId,
            PrimaryPropertyTitle = r.PrimaryProperty.Name,
            PrimaryPropertyDataType = r.PrimaryProperty.DataType,
            RelationType = r.RelationType,
            SecondaryEntityId = r.SecondaryEntityId,
            PrimaryPropertyEntityId = r.PrimaryProperty.EntityId,
            SecondaryEntityTitleProperty =
            r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault() == null ? new PropertyListDto() :
            new PropertyListDto()
            {
                CreateDate = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .CreateDate,
                EditCounts = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .EditCounts,
                Id = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .Id,
                LatestEditDate = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .LatestEditDate,
                DataAnnotationDataType = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .DataAnnotationDataType,
                DataType = r.SecondaryEntity
                .Properties
               .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .DataType,
                EntityId = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .EntityId,
                Name = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .Name,
                EntityTitle = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .Entity!.SingularName,
                ExcludeFromListDto = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .ExcludeFromListDto,
                IsFilterContain = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!.IsFilterContain,
                ForceMapperCode = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!.ForceMapperCode,
                IsRequired = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .IsRequired,
                IsFilterEqual = r.SecondaryEntity
                .Properties
               .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .IsFilterEqual,
                IsUnique = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .IsUnique,
                IsUpdatable = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .IsUpdatable,
                MaxLength = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .MaxLength,
                MinLength = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .MinLength,
                NameInDb = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .Name,
                Order = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .Order,
                ProjectEnumId = r.SecondaryEntity
                .Properties
               .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .ProjectEnumId,
                ProjectEnumEnglishName = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .ProjectEnum!.EnglishName,
                UseEditor = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!.UseEditor,
                ShowInList = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!.ShowInList,
                RangeTo = r.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!.RangeTo,
                RangeFrom = r.SecondaryEntity
                .Properties
               .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
               .ThenBy(a => a.Order)
               .ThenByDescending(a => a.DataType == DataTypeEnum.String)
               .FirstOrDefault()!.RangeFrom
            },
            SecondaryEntity = new EntityListDto()
            {
                AddToMenu = r.SecondaryEntity.AddToMenu,
                AuthorId = r.SecondaryEntity.AuthorId,
                CreateDate = r.SecondaryEntity.CreateDate,
                FolderName = r.SecondaryEntity.FolderName,
                Id = r.SecondaryEntity.Id,
                IdType = r.SecondaryEntity.IdType,
                InheritedEntityId = r.SecondaryEntity.InheritedEntityId,
                IsExcluded = r.SecondaryEntity.IsExcluded,
                PluralName = r.SecondaryEntity.PluralName,
                SingularName = r.SecondaryEntity.SingularName,
                ProjectId = r.SecondaryEntity.ProjectId,
                ProjectTitle = r.SecondaryEntity.Project.Title,
                ServiceName = r.SecondaryEntity.ServiceName,
                LatestEditDate = r.SecondaryEntity.LatestEditDate,
                InheritedEntityPluralName = r.SecondaryEntity.InheritedEntity!.SingularName
            }

        }).ToList(),
        Properties = a.Properties
        .Select(r => new DevCopilot2.Domain.DTOs.Properties.PropertyListDto()
        {
            CreateDate = r.CreateDate,
            EditCounts = r.EditCounts,
            Id = r.Id,
            LatestEditDate = r.LatestEditDate,
            DataAnnotationDataType = r.DataAnnotationDataType,
            DataType = r.DataType,
            EntityId = r.EntityId,
            Name = r.Name,
            EntityTitle = r.Entity.SingularName,
            ExcludeFromListDto = r.ExcludeFromListDto,
            IsFilterContain = r.IsFilterContain,
            ForceMapperCode = r.ForceMapperCode,
            IsRequired = r.IsRequired,
            IsFilterEqual = r.IsFilterEqual,
            IsUnique = r.IsUnique,
            IsUpdatable = r.IsUpdatable,
            MaxLength = r.MaxLength,
            MinLength = r.MinLength,
            NameInDb = r.Name,
            Order = r.Order,
            ProjectEnumId = r.ProjectEnumId,
            ProjectEnumEnglishName = r.ProjectEnum != null ? r.ProjectEnum.EnglishName : null,
            UseEditor = r.UseEditor,
            ShowInList = r.ShowInList,
            RangeTo = r.RangeTo,
            RangeFrom = r.RangeFrom,
            ProjectId = r.Entity.ProjectId,
            EntityRelationsList = r.EntityRelations
            .Select(d => new EntityRelationListDto()
            {
                CreateDate = d.CreateDate,
                EditCounts = d.EditCounts,
                Id = d.Id,
                LatestEditDate = d.LatestEditDate,
                FillingCode = d.FillingCode,
                FillingType = d.FillingType,
                InputType = d.InputType,
                MiddleEntityId = d.MiddleEntityId,
                MiddleEntityTitle = d.MiddleEntity!.SingularName,
                MiddleEntityFolderName = d.MiddleEntity!.FolderName,
                MiddleEntityServiceName = d.MiddleEntity!.ServiceName,
                PrimaryPropertyEntityFolderName = d.PrimaryProperty.Entity.FolderName,
                PrimaryPropertyEntityPluralTitle = d.PrimaryProperty.Entity.PluralName,
                PrimaryPropertyEntityTitle = d.PrimaryProperty.Entity.SingularName,
                PrimaryPropertyId = d.PrimaryPropertyId,
                PrimaryPropertyTitle = d.PrimaryProperty.Name,
                PrimaryPropertyDataType = d.PrimaryProperty.DataType,
                RelationType = d.RelationType,
                SecondaryEntityId = d.SecondaryEntityId,
                PrimaryPropertyEntityId = d.PrimaryProperty.EntityId,
                SecondaryEntity = new EntityListDto()
                {
                    AddToMenu = d.SecondaryEntity.AddToMenu,
                    AuthorId = d.SecondaryEntity.AuthorId,
                    CreateDate = d.SecondaryEntity.CreateDate,
                    FolderName = d.SecondaryEntity.FolderName,
                    Id = d.SecondaryEntity.Id,
                    IdType = d.SecondaryEntity.IdType,
                    InheritedEntityId = d.SecondaryEntity.InheritedEntityId,
                    IsExcluded = d.SecondaryEntity.IsExcluded,
                    PluralName = d.SecondaryEntity.PluralName,
                    SingularName = d.SecondaryEntity.SingularName,
                    ProjectId = d.SecondaryEntity.ProjectId,
                    ProjectTitle = d.SecondaryEntity.Project.Title,
                    ServiceName = d.SecondaryEntity.ServiceName,
                    LatestEditDate = d.SecondaryEntity.LatestEditDate,
                    InheritedEntityPluralName = d.SecondaryEntity.InheritedEntity!.SingularName
                },
                SecondaryEntityTitleProperty =
            d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault() == null ? new PropertyListDto() :
            new PropertyListDto()
            {
                CreateDate = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .CreateDate,
                EditCounts = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .EditCounts,
                Id = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .Id,
                LatestEditDate = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .LatestEditDate,
                DataAnnotationDataType = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .DataAnnotationDataType,
                DataType = d.SecondaryEntity
                .Properties
               .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .DataType,
                EntityId = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .EntityId,
                Name = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .Name,
                EntityTitle = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .Entity!.SingularName,
                ExcludeFromListDto = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .ExcludeFromListDto,
                IsFilterContain = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!.IsFilterContain,
                ForceMapperCode = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!.ForceMapperCode,
                IsRequired = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .IsRequired,
                IsFilterEqual = d.SecondaryEntity
                .Properties
               .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .IsFilterEqual,
                IsUnique = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .IsUnique,
                IsUpdatable = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .IsUpdatable,
                MaxLength = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .MaxLength,
                MinLength = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .MinLength,
                NameInDb = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .Name,
                Order = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .Order,
                ProjectEnumId = d.SecondaryEntity
                .Properties
               .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .ProjectEnumId,
                ProjectEnumEnglishName = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!
                .ProjectEnum!.EnglishName,
                UseEditor = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!.UseEditor,
                ShowInList = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!.ShowInList,
                RangeTo = d.SecondaryEntity
                .Properties
                .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
                .ThenBy(a => a.Order)
                .ThenByDescending(a => a.DataType == DataTypeEnum.String)
                .FirstOrDefault()!.RangeTo,
                RangeFrom = d.SecondaryEntity
                .Properties
               .OrderByDescending(a => a.DataAnnotationDataType == DataAnnotationsDataType.Title)
               .ThenBy(a => a.Order)
               .ThenByDescending(a => a.DataType == DataTypeEnum.String)
               .FirstOrDefault()!.RangeFrom
            }
            }).ToList(),
            ProjectEnum = r.ProjectEnum == null ? null : new ProjectEnumListDto()
            {
                EnglishName = r.ProjectEnum.EnglishName,
                AuthorId = r.ProjectEnum.AuthorId,
                AuthorName = r.ProjectEnum.Author.PhoneNumber,
                CreateDate = r.ProjectEnum.CreateDate,
                EditCounts = r.ProjectEnum.EditCounts,
                FolderName = r.ProjectEnum.FolderName,
                Id = r.ProjectEnum.Id,
                LatestEditDate = r.ProjectEnum.LatestEditDate,
                ProjectId = r.ProjectEnum.ProjectId,
                ProjectTitle = r.ProjectEnum.Project!.EnglishName,
            },
            PropertySelectedLanguagesList = r.PropertySelectedLanguages
            .Select(d => new DevCopilot2.Domain.DTOs.Properties.PropertySelectedLanguageListDto()
            {
                CreateDate = d.CreateDate,
                EditCounts = d.EditCounts,
                Id = d.Id,
                LatestEditDate = d.LatestEditDate,
                LanguageId = d.LanguageId,
                LanguageName = d.Language.Name,
                PropertyId = d.PropertyId,
                PropertyName = d.Property.Name,
                Title = d.Title
            }).ToList(),
            EntitySelectedProjectAreaSelectedFilters = r.EntitySelectedProjectAreaSelectedFilters
            .Select(d => new EntitySelectedProjectAreaSelectedFilterListDto()
            {
                CreateDate = d.CreateDate,
                EditCounts = d.EditCounts,
                Id = d.Id,
                LatestEditDate = d.LatestEditDate,
                EntitySelectedProjectAreaId = d.EntitySelectedProjectAreaId,
                PropertyId = d.PropertyId,
                EntitySelectedProjectAreaName = d.EntitySelectedProjectArea.ProjectArea.Title,
                Value = d.Value,
                PropertyName = d.Property.Name
            }).ToList(),
            PropertyImageResizeInformationList = r.PropertyImageResizeInformation
            .Select(d => new PropertyImageResizeInformationListDto()
            {
                CreateDate = d.CreateDate,
                EditCounts = d.EditCounts,
                Id = d.Id,
                LatestEditDate = d.LatestEditDate,
                Name = d.Name,
                PropertyName = d.Property.Name,
                Width = d.Width,
                PropertyId = d.PropertyId,
                Height = d.Height
            }).ToList()
        }).ToList()
    });

        #endregion

        #region to dto

        public static IQueryable<EntityListDto> ToBaseDto(this IQueryable<Entity> query)
            => query.Select(entity => new EntityListDto()
            {
                Id = entity.Id,
                LatestEditDate = entity.LatestEditDate,
                CreateDate = entity.CreateDate,
                EditCounts = entity.EditCounts,

                SingularName = entity.SingularName,
                PluralName = entity.PluralName,
                FolderName = entity.FolderName,
                InheritedEntityPluralName = entity.InheritedEntity == null ? "-" : entity.InheritedEntity!.PluralName,
                InheritedEntityId = entity.InheritedEntityId,
                IdType = entity.IdType,
                ServiceName = entity.ServiceName,
                AuthorPhoneNumber = entity.Author == null ? "-" : entity.Author!.PhoneNumber,
                AuthorId = entity.AuthorId,
                ProjectTitle = entity.Project.Title,
                ProjectId = entity.ProjectId,
                IsExcluded = entity.IsExcluded,
                AddToMenu = entity.AddToMenu,
            }).AsQueryable();

        public static IQueryable<EntityListDto> ToDto(this IQueryable<Entity> query)
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

        public static IQueryable<UpdateEntityDto> ToUpdateDto(this IQueryable<Entity> query)
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
                    InheritedEntityId = create.InheritedEntityId > 0 ? create.InheritedEntityId : null,
                    IdType = create.IdType,
                    ServiceName = create.ServiceName.SanitizeText()!,
                    AuthorId = create.AuthorId > 0 ? create.AuthorId : null,
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
            entity.InheritedEntityId = update.InheritedEntityId > 0 ? update.InheritedEntityId : null;
            entity.IdType = update.IdType;
            entity.ServiceName = update.ServiceName.SanitizeText()!;
            entity.AuthorId = update.AuthorId > 0 ? update.AuthorId : null;
            entity.ProjectId = update.ProjectId;
            entity.IsExcluded = update.IsExcluded;
            entity.AddToMenu = update.AddToMenu;
            return entity;
        }

        #endregion

    }
}
