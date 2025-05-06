using System;
using DevCopilot2.Domain.Entities.Entities;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;

namespace DevCopilot2.Core.Mappers.Entities
{
    public static class EntitySelectedLanguageMappers
    {
        #region to dto

        public static IQueryable<EntitySelectedLanguageListDto>ToDto(this IQueryable<EntitySelectedLanguage> query)
                    => query.Select(entitySelectedLanguage => new EntitySelectedLanguageListDto()
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

                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateEntitySelectedLanguageDto>ToUpdateDto(this IQueryable<EntitySelectedLanguage> query)
                    => query.Select(entitySelectedLanguage => new UpdateEntitySelectedLanguageDto()
                    {

                        Id = entitySelectedLanguage.Id,

                        EntityId = entitySelectedLanguage.EntityId,
                        LanguageId = entitySelectedLanguage.LanguageId,
                        SingularTitle = entitySelectedLanguage.SingularTitle,
                        PluralTitle = entitySelectedLanguage.PluralTitle,

                    });

        #endregion

        #region to create dto

        public static List<CreateEntitySelectedLanguageDto>ToCreateDto(this IEnumerable<UpdateEntitySelectedLanguageDto> entitySelectedLanguages)
                    =>  entitySelectedLanguages.Select(entitySelectedLanguage => new CreateEntitySelectedLanguageDto()
                    {

                        EntityId = entitySelectedLanguage.EntityId,
                        LanguageId = entitySelectedLanguage.LanguageId,
                        SingularTitle = entitySelectedLanguage.SingularTitle,
                        PluralTitle = entitySelectedLanguage.PluralTitle,

                    }).ToList();

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<EntitySelectedLanguage> query)
			    => query.Select(entitySelectedLanguage => new ComboDto()
			{
            Title = entitySelectedLanguage.SingularTitle,
            Value = entitySelectedLanguage.Id.ToString()
            });

        #endregion

        #region to create model

        public static EntitySelectedLanguage ToModel(this CreateEntitySelectedLanguageDto create)
				=> new EntitySelectedLanguage()
				{
                    EntityId = create.EntityId,
                    LanguageId = create.LanguageId,
                    SingularTitle = create.SingularTitle.ToTitle()!,
                    PluralTitle = create.PluralTitle.ToTitle()!,
				};

        #endregion

        #region to update model

        public static EntitySelectedLanguage ToModel(this EntitySelectedLanguage entitySelectedLanguage, UpdateEntitySelectedLanguageDto update)
        {
            entitySelectedLanguage.EntityId = update.EntityId;
            entitySelectedLanguage.LanguageId = update.LanguageId;
            entitySelectedLanguage.SingularTitle = update.SingularTitle.ToTitle()!;
            entitySelectedLanguage.PluralTitle = update.PluralTitle.ToTitle()!;
            return entitySelectedLanguage;
        }

        #endregion

    }
}
