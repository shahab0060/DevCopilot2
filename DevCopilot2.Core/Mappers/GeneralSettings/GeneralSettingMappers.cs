using System;
using DevCopilot2.Domain.Entities.GeneralSettings;
using DevCopilot2.Domain.DTOs.GeneralSettings;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;

namespace DevCopilot2.Core.Mappers.GeneralSettings
{
    public static class GeneralSettingMappers
    {
        #region to dto

        public static IQueryable<GeneralSettingListDto>ToDto(this IQueryable<GeneralSetting> query)
                    => query.Select(generalSetting => new GeneralSettingListDto()
                    {

                        Id = generalSetting.Id,
                        LatestEditDate = generalSetting.LatestEditDate,
                        CreateDate = generalSetting.CreateDate,
                        EditCounts = generalSetting.EditCounts,

                        DefaultSolutionName = generalSetting.DefaultSolutionName,
                        DefaultSolutionLocation = generalSetting.DefaultSolutionLocation,
                        DefaultReactJsSolutionName = generalSetting.DefaultReactJsSolutionName,
                        DefaultReactSolutionLocation = generalSetting.DefaultReactSolutionLocation,

                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateGeneralSettingDto>ToUpdateDto(this IQueryable<GeneralSetting> query)
                    => query.Select(generalSetting => new UpdateGeneralSettingDto()
                    {

                        Id = generalSetting.Id,

                        DefaultSolutionName = generalSetting.DefaultSolutionName,
                        DefaultSolutionLocation = generalSetting.DefaultSolutionLocation,
                        DefaultReactJsSolutionName = generalSetting.DefaultReactJsSolutionName,
                        DefaultReactSolutionLocation = generalSetting.DefaultReactSolutionLocation,

                    });

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<GeneralSetting> query)
			    => query.Select(generalSetting => new ComboDto()
			{
            Title = generalSetting.DefaultSolutionName,
            Value = generalSetting.Id.ToString()
            });

        #endregion

        #region to create model

        public static GeneralSetting ToModel(this CreateGeneralSettingDto create)
				=> new GeneralSetting()
				{
                    DefaultSolutionName = create.DefaultSolutionName.SanitizeText()!,
                    DefaultSolutionLocation = create.DefaultSolutionLocation.SanitizeText()!,
                    DefaultReactJsSolutionName = create.DefaultReactJsSolutionName.SanitizeText()!,
                    DefaultReactSolutionLocation = create.DefaultReactSolutionLocation.SanitizeText()!,
				};

        #endregion

        #region to update model

        public static GeneralSetting ToModel(this GeneralSetting generalSetting, UpdateGeneralSettingDto update)
        {
            generalSetting.DefaultSolutionName = update.DefaultSolutionName.SanitizeText()!;
            generalSetting.DefaultSolutionLocation = update.DefaultSolutionLocation.SanitizeText()!;
            generalSetting.DefaultReactJsSolutionName = update.DefaultReactJsSolutionName.SanitizeText()!;
            generalSetting.DefaultReactSolutionLocation = update.DefaultReactSolutionLocation.SanitizeText()!;
            return generalSetting;
        }

        #endregion

    }
}
