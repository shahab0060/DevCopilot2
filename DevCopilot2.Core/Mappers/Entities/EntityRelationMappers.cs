using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.Entities.Entities;

namespace DevCopilot2.Core.Mappers.Entities
{
    public static class EntityRelationMappers
    {
        #region to dto

        public static IQueryable<EntityRelationListDto> ToDto(this IQueryable<EntityRelation> query)
                    => query.Select(entityRelation => new EntityRelationListDto()
                    {

                        Id = entityRelation.Id,
                        LatestEditDate = entityRelation.LatestEditDate,
                        CreateDate = entityRelation.CreateDate,
                        EditCounts = entityRelation.EditCounts,
                        PrimaryPropertyTitle = entityRelation.PrimaryProperty.Name,
                        PrimaryPropertyId = entityRelation.PrimaryPropertyId,
                        SecondaryEntityPluralName = entityRelation.SecondaryEntity.PluralName,
                        SecondaryEntityId = entityRelation.SecondaryEntityId,
                        MiddleEntityPluralName = entityRelation.MiddleEntity!.PluralName,
                        MiddleEntityId = entityRelation.MiddleEntityId,
                        RelationType = entityRelation.RelationType,
                        InputType = entityRelation.InputType,
                        FillingType = entityRelation.FillingType,
                        FillingCode = entityRelation.FillingCode,
                        ProjectId = entityRelation.SecondaryEntity.ProjectId,
                        PrimaryPropertyDataType = entityRelation.PrimaryProperty.DataType,
                        PrimaryPropertyEntityFolderName = entityRelation.PrimaryProperty.Entity.FolderName,
                        PrimaryPropertyEntityId = entityRelation.PrimaryProperty.EntityId,
                        PrimaryPropertyEntityPluralTitle = entityRelation.PrimaryProperty.Entity.PluralName,
                        PrimaryPropertyEntityTitle = entityRelation.PrimaryProperty.Entity.SingularName,
                        MiddleEntityTitle = entityRelation.MiddleEntity!.SingularName,
                        MiddleEntityFolderName = entityRelation.MiddleEntity!.FolderName
                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateEntityRelationDto> ToUpdateDto(this IQueryable<EntityRelation> query)
                    => query.Select(entityRelation => new UpdateEntityRelationDto()
                    {

                        Id = entityRelation.Id,

                        PrimaryPropertyId = entityRelation.PrimaryPropertyId,
                        SecondaryEntityId = entityRelation.SecondaryEntityId,
                        MiddleEntityId = entityRelation.MiddleEntityId,
                        RelationType = entityRelation.RelationType,
                        InputType = entityRelation.InputType,
                        FillingType = entityRelation.FillingType,
                        FillingCode = entityRelation.FillingCode,

                    });

        #endregion

        #region to create dto

        public static List<CreateEntityRelationDto> ToCreateDto(this IEnumerable<UpdateEntityRelationDto> entityRelations)
                    => entityRelations.Select(entityRelation => new CreateEntityRelationDto()
                    {

                        PrimaryPropertyId = entityRelation.PrimaryPropertyId,
                        SecondaryEntityId = entityRelation.SecondaryEntityId,
                        MiddleEntityId = entityRelation.MiddleEntityId,
                        RelationType = entityRelation.RelationType,
                        InputType = entityRelation.InputType,
                        FillingType = entityRelation.FillingType,
                        FillingCode = entityRelation.FillingCode,

                    }).ToList();

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<EntityRelation> query)
                => query.Select(entityRelation => new ComboDto()
                {
                    Title = entityRelation.FillingCode,
                    Value = entityRelation.Id.ToString()
                });

        #endregion

        #region to create model

        public static EntityRelation ToModel(this CreateEntityRelationDto create)
                => new EntityRelation()
                {
                    PrimaryPropertyId = create.PrimaryPropertyId,
                    SecondaryEntityId = create.SecondaryEntityId,
                    MiddleEntityId = create.MiddleEntityId > 0 ? create.MiddleEntityId : null,
                    RelationType = create.RelationType,
                    InputType = create.InputType,
                    FillingType = create.FillingType,
                    FillingCode = create.FillingCode.ToTitle()!,
                };

        #endregion

        #region to update model

        public static EntityRelation ToModel(this EntityRelation entityRelation, UpdateEntityRelationDto update)
        {
            entityRelation.PrimaryPropertyId = update.PrimaryPropertyId;
            entityRelation.SecondaryEntityId = update.SecondaryEntityId;
            entityRelation.MiddleEntityId = update.MiddleEntityId > 0 ? update.MiddleEntityId : null;
            entityRelation.RelationType = update.RelationType;
            entityRelation.InputType = update.InputType;
            entityRelation.FillingType = update.FillingType;
            entityRelation.FillingCode = update.FillingCode.ToTitle()!;
            return entityRelation;
        }

        #endregion

    }
}
