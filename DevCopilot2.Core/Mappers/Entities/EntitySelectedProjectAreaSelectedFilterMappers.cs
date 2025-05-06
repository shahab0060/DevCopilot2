using System;
using DevCopilot2.Domain.Entities.Entities;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;

namespace DevCopilot2.Core.Mappers.Entities
{
    public static class EntitySelectedProjectAreaSelectedFilterMappers
    {
        #region to dto

        public static IQueryable<EntitySelectedProjectAreaSelectedFilterListDto>ToDto(this IQueryable<EntitySelectedProjectAreaSelectedFilter> query)
                    => query.Select(entitySelectedProjectAreaSelectedFilter => new EntitySelectedProjectAreaSelectedFilterListDto()
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

                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateEntitySelectedProjectAreaSelectedFilterDto>ToUpdateDto(this IQueryable<EntitySelectedProjectAreaSelectedFilter> query)
                    => query.Select(entitySelectedProjectAreaSelectedFilter => new UpdateEntitySelectedProjectAreaSelectedFilterDto()
                    {

                        Id = entitySelectedProjectAreaSelectedFilter.Id,

                        EntitySelectedProjectAreaId = entitySelectedProjectAreaSelectedFilter.EntitySelectedProjectAreaId,
                        PropertyId = entitySelectedProjectAreaSelectedFilter.PropertyId,
                        Value = entitySelectedProjectAreaSelectedFilter.Value,

                    });

        #endregion

        #region to create dto

        public static List<CreateEntitySelectedProjectAreaSelectedFilterDto>ToCreateDto(this IEnumerable<UpdateEntitySelectedProjectAreaSelectedFilterDto> entitySelectedProjectAreaSelectedFilters)
                    =>  entitySelectedProjectAreaSelectedFilters.Select(entitySelectedProjectAreaSelectedFilter => new CreateEntitySelectedProjectAreaSelectedFilterDto()
                    {

                        EntitySelectedProjectAreaId = entitySelectedProjectAreaSelectedFilter.EntitySelectedProjectAreaId,
                        PropertyId = entitySelectedProjectAreaSelectedFilter.PropertyId,
                        Value = entitySelectedProjectAreaSelectedFilter.Value,

                    }).ToList();

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<EntitySelectedProjectAreaSelectedFilter> query)
			    => query.Select(entitySelectedProjectAreaSelectedFilter => new ComboDto()
			{
            Title = entitySelectedProjectAreaSelectedFilter.Value,
            Value = entitySelectedProjectAreaSelectedFilter.Id.ToString()
            });

        #endregion

        #region to create model

        public static EntitySelectedProjectAreaSelectedFilter ToModel(this CreateEntitySelectedProjectAreaSelectedFilterDto create)
				=> new EntitySelectedProjectAreaSelectedFilter()
				{
                    EntitySelectedProjectAreaId = create.EntitySelectedProjectAreaId,
                    PropertyId = create.PropertyId,
                    Value = create.Value.ToTitle()!,
				};

        #endregion

        #region to update model

        public static EntitySelectedProjectAreaSelectedFilter ToModel(this EntitySelectedProjectAreaSelectedFilter entitySelectedProjectAreaSelectedFilter, UpdateEntitySelectedProjectAreaSelectedFilterDto update)
        {
            entitySelectedProjectAreaSelectedFilter.EntitySelectedProjectAreaId = update.EntitySelectedProjectAreaId;
            entitySelectedProjectAreaSelectedFilter.PropertyId = update.PropertyId;
            entitySelectedProjectAreaSelectedFilter.Value = update.Value.ToTitle()!;
            return entitySelectedProjectAreaSelectedFilter;
        }

        #endregion

    }
}
