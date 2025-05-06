using System;
using DevCopilot2.Domain.Entities.Properties;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;

namespace DevCopilot2.Core.Mappers.Properties
{
    public static class PropertySelectedLanguageMappers
    {
        #region to dto

        public static IQueryable<PropertySelectedLanguageListDto>ToDto(this IQueryable<PropertySelectedLanguage> query)
                    => query.Select(propertySelectedLanguage => new PropertySelectedLanguageListDto()
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

                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdatePropertySelectedLanguageDto>ToUpdateDto(this IQueryable<PropertySelectedLanguage> query)
                    => query.Select(propertySelectedLanguage => new UpdatePropertySelectedLanguageDto()
                    {

                        Id = propertySelectedLanguage.Id,

                        PropertyId = propertySelectedLanguage.PropertyId,
                        LanguageId = propertySelectedLanguage.LanguageId,
                        Title = propertySelectedLanguage.Title,

                    });

        #endregion

        #region to create dto

        public static List<CreatePropertySelectedLanguageDto>ToCreateDto(this IEnumerable<UpdatePropertySelectedLanguageDto> propertySelectedLanguages)
                    =>  propertySelectedLanguages.Select(propertySelectedLanguage => new CreatePropertySelectedLanguageDto()
                    {

                        PropertyId = propertySelectedLanguage.PropertyId,
                        LanguageId = propertySelectedLanguage.LanguageId,
                        Title = propertySelectedLanguage.Title,

                    }).ToList();

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<PropertySelectedLanguage> query)
			    => query.Select(propertySelectedLanguage => new ComboDto()
			{
            Title = propertySelectedLanguage.Title,
            Value = propertySelectedLanguage.Id.ToString()
            });

        #endregion

        #region to create model

        public static PropertySelectedLanguage ToModel(this CreatePropertySelectedLanguageDto create)
				=> new PropertySelectedLanguage()
				{
                    PropertyId = create.PropertyId,
                    LanguageId = create.LanguageId,
                    Title = create.Title.ToTitle()!,
				};

        #endregion

        #region to update model

        public static PropertySelectedLanguage ToModel(this PropertySelectedLanguage propertySelectedLanguage, UpdatePropertySelectedLanguageDto update)
        {
            propertySelectedLanguage.PropertyId = update.PropertyId;
            propertySelectedLanguage.LanguageId = update.LanguageId;
            propertySelectedLanguage.Title = update.Title.ToTitle()!;
            return propertySelectedLanguage;
        }

        #endregion

    }
}
