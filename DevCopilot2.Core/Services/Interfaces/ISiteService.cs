using System;
using DevCopilot2.Domain.IRepository;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Enums.SiteSettings;
using DevCopilot2.Core.Mappers.SiteSettings;
using DevCopilot2.Domain.DTOs.SiteSettings;
using DevCopilot2.Domain.Enums.Languages;
using DevCopilot2.Core.Mappers.Languages;
using DevCopilot2.Domain.DTOs.Languages;
using DevCopilot2.Domain.Enums.GeneralSettings;
using DevCopilot2.Core.Mappers.GeneralSettings;
using DevCopilot2.Domain.DTOs.GeneralSettings;

namespace DevCopilot2.Core.Services.Interfaces
{
    public interface ISiteService : IService
    {

        #region SiteSettings

        #region site settings

		Task<FilterSiteSettingsDto> FilterSiteSettings(FilterSiteSettingsDto filter);
        Task<List<ComboDto>> GetSiteSettingsAsCombo(FilterSiteSettingsDto filter);
		Task<SiteSettingListDto?> GetSingleSiteSettingInformation(long siteSettingId);

        		Task<UpdateSiteSettingDto?> GetSiteSettingInformation(long siteSettingId);
		Task<BaseChangeEntityResult> UpdateSiteSetting(UpdateSiteSettingDto update);
		Task<BaseChangeEntityResult> DeleteSiteSetting(long siteSettingId);
		Task DeleteSiteSetting(List<long> siteSettingsId);

        #endregion

        #endregion

        #region Languages

        #region languages

		Task<FilterLanguagesDto> FilterLanguages(FilterLanguagesDto filter);
        Task<List<ComboDto>> GetLanguagesAsCombo(FilterLanguagesDto filter);
		Task<LanguageListDto?> GetSingleLanguageInformation(int languageId);

        		Task<BaseChangeEntityResult> CreateLanguage(CreateLanguageDto create);
		Task<UpdateLanguageDto?> GetLanguageInformation(int languageId);
		Task<BaseChangeEntityResult> UpdateLanguage(UpdateLanguageDto update);
		Task<BaseChangeEntityResult> DeleteLanguage(int languageId);
		Task DeleteLanguage(List<int> languagesId);

        #endregion

        #endregion

        #region GeneralSettings

        #region general settings

		Task<FilterGeneralSettingsDto> FilterGeneralSettings(FilterGeneralSettingsDto filter);
        Task<List<ComboDto>> GetGeneralSettingsAsCombo(FilterGeneralSettingsDto filter);
		Task<GeneralSettingListDto?> GetSingleGeneralSettingInformation(int generalSettingId);

        		Task<UpdateGeneralSettingDto?> GetGeneralSettingInformation(int generalSettingId);
		Task<BaseChangeEntityResult> UpdateGeneralSetting(UpdateGeneralSettingDto update);
		Task<BaseChangeEntityResult> DeleteGeneralSetting(int generalSettingId);
		Task DeleteGeneralSetting(List<int> generalSettingsId);

        #endregion

        #endregion
    }
}
