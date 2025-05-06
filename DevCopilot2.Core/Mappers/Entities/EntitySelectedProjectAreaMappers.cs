using System;
using DevCopilot2.Domain.Entities.Entities;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;

namespace DevCopilot2.Core.Mappers.Entities
{
    public static class EntitySelectedProjectAreaMappers
    {
        #region to dto

        public static IQueryable<EntitySelectedProjectAreaListDto>ToDto(this IQueryable<EntitySelectedProjectArea> query)
                    => query.Select(entitySelectedProjectArea => new EntitySelectedProjectAreaListDto()
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
                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateEntitySelectedProjectAreaDto>ToUpdateDto(this IQueryable<EntitySelectedProjectArea> query)
                    => query.Select(entitySelectedProjectArea => new UpdateEntitySelectedProjectAreaDto()
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
                    });

        #endregion

        #region to create dto

        public static List<CreateEntitySelectedProjectAreaDto>ToCreateDto(this IEnumerable<UpdateEntitySelectedProjectAreaDto> entitySelectedProjectAreas)
                    =>  entitySelectedProjectAreas.Select(entitySelectedProjectArea => new CreateEntitySelectedProjectAreaDto()
                    {

                        EntityId = entitySelectedProjectArea.EntityId,
                        ProjectAreaId = entitySelectedProjectArea.ProjectAreaId,
                        HasIndex = entitySelectedProjectArea.HasIndex,
                        HasCreate = entitySelectedProjectArea.HasCreate,
                        HasUpdate = entitySelectedProjectArea.HasUpdate,
                        HasDelete = entitySelectedProjectArea.HasDelete,
                        HasApi = entitySelectedProjectArea.HasApi,
                        HasWeb = entitySelectedProjectArea.HasWeb,

                        EntitySelectedProjectAreaSelectedFiltersList = entitySelectedProjectArea.EntitySelectedProjectAreaSelectedFiltersList
                        .Select(entitySelectedProjectAreaSelectedFilter => new CreateEntitySelectedProjectAreaSelectedFilterDto()
                        {

                        EntitySelectedProjectAreaId = entitySelectedProjectAreaSelectedFilter.EntitySelectedProjectAreaId,
                        PropertyId = entitySelectedProjectAreaSelectedFilter.PropertyId,
                        Value = entitySelectedProjectAreaSelectedFilter.Value,

                        })
                        .ToList(),
                    }).ToList();

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<EntitySelectedProjectArea> query)
			    => query.Select(entitySelectedProjectArea => new ComboDto()
			{
            Title = entitySelectedProjectArea.HasWeb.ToString(),
            Value = entitySelectedProjectArea.Id.ToString()
            });

        #endregion

        #region to create model

        public static EntitySelectedProjectArea ToModel(this CreateEntitySelectedProjectAreaDto create)
				=> new EntitySelectedProjectArea()
				{
                    EntityId = create.EntityId,
                    ProjectAreaId = create.ProjectAreaId,
                    HasIndex = create.HasIndex,
                    HasCreate = create.HasCreate,
                    HasUpdate = create.HasUpdate,
                    HasDelete = create.HasDelete,
                    HasApi = create.HasApi,
                    HasWeb = create.HasWeb,
				};

        #endregion

        #region to update model

        public static EntitySelectedProjectArea ToModel(this EntitySelectedProjectArea entitySelectedProjectArea, UpdateEntitySelectedProjectAreaDto update)
        {
            entitySelectedProjectArea.EntityId = update.EntityId;
            entitySelectedProjectArea.ProjectAreaId = update.ProjectAreaId;
            entitySelectedProjectArea.HasIndex = update.HasIndex;
            entitySelectedProjectArea.HasCreate = update.HasCreate;
            entitySelectedProjectArea.HasUpdate = update.HasUpdate;
            entitySelectedProjectArea.HasDelete = update.HasDelete;
            entitySelectedProjectArea.HasApi = update.HasApi;
            entitySelectedProjectArea.HasWeb = update.HasWeb;
            return entitySelectedProjectArea;
        }

        #endregion

    }
}
