using System;
using DevCopilot2.Domain.Entities.Languages;
using DevCopilot2.Domain.DTOs.Languages;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;

namespace DevCopilot2.Core.Mappers.Languages
{
    public static class LanguageMappers
    {
        #region to dto

        public static IQueryable<LanguageListDto>ToDto(this IQueryable<Language> query)
                    => query.Select(language => new LanguageListDto()
                    {

                        Id = language.Id,
                        LatestEditDate = language.LatestEditDate,
                        CreateDate = language.CreateDate,
                        EditCounts = language.EditCounts,

                        Name = language.Name,
                        Culture = language.Culture,
                        DefaultPluralSuffix = language.DefaultPluralSuffix,

                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateLanguageDto>ToUpdateDto(this IQueryable<Language> query)
                    => query.Select(language => new UpdateLanguageDto()
                    {

                        Id = language.Id,

                        Name = language.Name,
                        Culture = language.Culture,
                        DefaultPluralSuffix = language.DefaultPluralSuffix,

                    });

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<Language> query)
			    => query.Select(language => new ComboDto()
			{
            Title = language.Name,
            Value = language.Id.ToString()
            });

        #endregion

        #region to create model

        public static Language ToModel(this CreateLanguageDto create)
				=> new Language()
				{
                    Name = create.Name.ToTitle()!,
                    Culture = create.Culture.SanitizeText()!,
                    DefaultPluralSuffix = create.DefaultPluralSuffix.SanitizeText()!,
				};

        #endregion

        #region to update model

        public static Language ToModel(this Language language, UpdateLanguageDto update)
        {
            language.Name = update.Name.ToTitle()!;
            language.Culture = update.Culture.SanitizeText()!;
            language.DefaultPluralSuffix = update.DefaultPluralSuffix.SanitizeText()!;
            return language;
        }

        #endregion

    }
}
