using System;
using DevCopilot2.Domain.IRepository;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Security;
using DevCopilot2.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Enums.SiteSettings;
using DevCopilot2.Core.Mappers.SiteSettings;
using DevCopilot2.Domain.Entities.SiteSettings;
using DevCopilot2.Domain.DTOs.SiteSettings;
using DevCopilot2.Domain.Enums.Languages;
using DevCopilot2.Core.Mappers.Languages;
using DevCopilot2.Domain.Entities.Languages;
using DevCopilot2.Domain.DTOs.Languages;
using DevCopilot2.Domain.Enums.GeneralSettings;
using DevCopilot2.Core.Mappers.GeneralSettings;
using DevCopilot2.Domain.Entities.GeneralSettings;
using DevCopilot2.Domain.DTOs.GeneralSettings;

namespace DevCopilot2.Core.Services.Classes
{
    public class SiteService : ISiteService
    {
        #region constructor

        private readonly ICrudRepository<SiteSetting, long> _siteSettingRepository;
        private readonly ICrudRepository<Language, int> _languageRepository;
        private readonly ICrudRepository<GeneralSetting, int> _generalSettingRepository;
        public SiteService(
                           ICrudRepository<SiteSetting, long> siteSettingRepository,
                           ICrudRepository<Language, int> languageRepository,
                           ICrudRepository<GeneralSetting, int> generalSettingRepository 
                                      )
        {
            this._siteSettingRepository = siteSettingRepository;
            this._languageRepository = languageRepository;
            this._generalSettingRepository = generalSettingRepository;
        }

        #endregion

        #region SiteSettings

        #region site settings

        IQueryable<SiteSetting> GetSiteSettingsWithFilterAndSort(FilterSiteSettingsDto filter)
        {
            IQueryable<SiteSetting> query = _siteSettingRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                                SortSiteSettingType.SMSApiKey => query.OrderBy(a => a.SMSApiKey),
                SortSiteSettingType.SMSTemplateName => query.OrderBy(a => a.SMSTemplateName),
                SortSiteSettingType.FavIconName => query.OrderBy(a => a.FavIconName),
                SortSiteSettingType.SiteName => query.OrderBy(a => a.SiteName),
                SortSiteSettingType.LogoImageName => query.OrderBy(a => a.LogoImageName),
                _ => query
            };

            #endregion

            #region base sort

            query = filter.BaseSortEntityType switch
            {
                BaseSortEntityType.Default => query,
                BaseSortEntityType.Newest => query.OrderByDescending(q => q.CreateDate),
                BaseSortEntityType.LatestUpdate => query.OrderByDescending(q => q.LatestEditDate),
                _ => query
            };

            query = filter.SortType switch
            {
                SortType.Ascending => query.Reverse(),
                SortType.Descending => query,
                _ => query
            };

            #endregion

            return query;
        }

        public async Task<FilterSiteSettingsDto> FilterSiteSettings(FilterSiteSettingsDto filter)
        {
            IQueryable<SiteSetting> query = 
                GetSiteSettingsWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId, 
                await query.CountAsync(),
                filter.TakeEntity, 
                filter.HowManyShowPageAfterAndBefore);

            filter.SiteSettings = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetSiteSettingsAsCombo(FilterSiteSettingsDto filter)
            => await GetSiteSettingsWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<SiteSettingListDto?> GetSingleSiteSettingInformation(long siteSettingId)
            => await _siteSettingRepository
            .GetQueryable()
            .Where(a => a.Id == siteSettingId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<UpdateSiteSettingDto?> GetSiteSettingInformation(long siteSettingId)
        =>  await _siteSettingRepository
            .GetQueryable()
            .Where(a => a.Id == siteSettingId)
            .ToUpdateDto()
            .FirstOrDefaultAsync();
public async Task<BaseChangeEntityResult> UpdateSiteSetting(UpdateSiteSettingDto update)
        { 
            SiteSetting? siteSetting = await _siteSettingRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (siteSetting is null) return BaseChangeEntityResult.NotFound;

            siteSetting = await siteSetting.ToModel(update);
            _siteSettingRepository.Update(siteSetting);
            await _siteSettingRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
public async Task<BaseChangeEntityResult> DeleteSiteSetting(long siteSettingId)
        {
            SiteSetting? siteSetting = await _siteSettingRepository.GetAsTracking(siteSettingId);
            if (siteSetting is null) return BaseChangeEntityResult.NotFound;

            _siteSettingRepository.SoftDelete(siteSetting);

            await _siteSettingRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
public async Task DeleteSiteSetting(List<long> siteSettingIds)
        {
            foreach (long siteSettingId in siteSettingIds)
            { 
                SiteSetting? siteSetting = await _siteSettingRepository.GetAsTracking(siteSettingId);
                if (siteSetting is not null)
                    _siteSettingRepository.SoftDelete(siteSetting);
            }
            await _siteSettingRepository.SaveChanges();
        }

        #endregion

        #endregion

        #region Languages

        #region languages

        IQueryable<Language> GetLanguagesWithFilterAndSort(FilterLanguagesDto filter)
        {
            IQueryable<Language> query = _languageRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.Name, $"%{filter.Search}%") ||

EF.Functions.Like(q.Culture, $"%{filter.Search}%") ||

EF.Functions.Like(q.DefaultPluralSuffix, $"%{filter.Search}%")   
                                         );

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                                SortLanguageType.Name => query.OrderBy(a => a.Name),
                SortLanguageType.Culture => query.OrderBy(a => a.Culture),
                SortLanguageType.DefaultPluralSuffix => query.OrderBy(a => a.DefaultPluralSuffix),
                _ => query
            };

            #endregion

            #region base sort

            query = filter.BaseSortEntityType switch
            {
                BaseSortEntityType.Default => query,
                BaseSortEntityType.Newest => query.OrderByDescending(q => q.CreateDate),
                BaseSortEntityType.LatestUpdate => query.OrderByDescending(q => q.LatestEditDate),
                _ => query
            };

            query = filter.SortType switch
            {
                SortType.Ascending => query.Reverse(),
                SortType.Descending => query,
                _ => query
            };

            #endregion

            return query;
        }

        public async Task<FilterLanguagesDto> FilterLanguages(FilterLanguagesDto filter)
        {
            IQueryable<Language> query = 
                GetLanguagesWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId, 
                await query.CountAsync(),
                filter.TakeEntity, 
                filter.HowManyShowPageAfterAndBefore);

            filter.Languages = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetLanguagesAsCombo(FilterLanguagesDto filter)
            => await GetLanguagesWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<LanguageListDto?> GetSingleLanguageInformation(int languageId)
            => await _languageRepository
            .GetQueryable()
            .Where(a => a.Id == languageId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<BaseChangeEntityResult> CreateLanguage(CreateLanguageDto create)
        { 

            #region validate unique properties

            if (await _languageRepository
                .GetQueryable()
                .AnyAsync(a => a.Name == create.Name.ToTitle()!))
                return BaseChangeEntityResult.Exists;

            #endregion

            Language language =  create.ToModel();
            await _languageRepository.Add(language);
            await _languageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
public async Task<UpdateLanguageDto?> GetLanguageInformation(int languageId)
        =>  await _languageRepository
            .GetQueryable()
            .Where(a => a.Id == languageId)
            .ToUpdateDto()
            .FirstOrDefaultAsync();
public async Task<BaseChangeEntityResult> UpdateLanguage(UpdateLanguageDto update)
        { 
            Language? language = await _languageRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (language is null) return BaseChangeEntityResult.NotFound;

            #region validate unique properties

            if (await _languageRepository
                .GetQueryable()
                .AnyAsync(a => a.Name == update.Name.ToTitle()!
                               && a.Id!=update.Id))
                return BaseChangeEntityResult.Exists;

            #endregion

            language =  language.ToModel(update);
            _languageRepository.Update(language);
            await _languageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
public async Task<BaseChangeEntityResult> DeleteLanguage(int languageId)
        {
            Language? language = await _languageRepository.GetAsTracking(languageId);
            if (language is null) return BaseChangeEntityResult.NotFound;

            _languageRepository.SoftDelete(language);

            await _languageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
public async Task DeleteLanguage(List<int> languageIds)
        {
            foreach (int languageId in languageIds)
            { 
                Language? language = await _languageRepository.GetAsTracking(languageId);
                if (language is not null)
                    _languageRepository.SoftDelete(language);
            }
            await _languageRepository.SaveChanges();
        }

        #endregion

        #endregion

        #region GeneralSettings

        #region general settings

        IQueryable<GeneralSetting> GetGeneralSettingsWithFilterAndSort(FilterGeneralSettingsDto filter)
        {
            IQueryable<GeneralSetting> query = _generalSettingRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.DefaultSolutionName, $"%{filter.Search}%") ||

EF.Functions.Like(q.DefaultSolutionLocation, $"%{filter.Search}%") ||

EF.Functions.Like(q.DefaultReactJsSolutionName, $"%{filter.Search}%") ||

EF.Functions.Like(q.DefaultReactSolutionLocation, $"%{filter.Search}%")   
                                         );

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                                SortGeneralSettingType.DefaultSolutionName => query.OrderBy(a => a.DefaultSolutionName),
                SortGeneralSettingType.DefaultSolutionLocation => query.OrderBy(a => a.DefaultSolutionLocation),
                SortGeneralSettingType.DefaultReactJsSolutionName => query.OrderBy(a => a.DefaultReactJsSolutionName),
                SortGeneralSettingType.DefaultReactSolutionLocation => query.OrderBy(a => a.DefaultReactSolutionLocation),
                _ => query
            };

            #endregion

            #region base sort

            query = filter.BaseSortEntityType switch
            {
                BaseSortEntityType.Default => query,
                BaseSortEntityType.Newest => query.OrderByDescending(q => q.CreateDate),
                BaseSortEntityType.LatestUpdate => query.OrderByDescending(q => q.LatestEditDate),
                _ => query
            };

            query = filter.SortType switch
            {
                SortType.Ascending => query.Reverse(),
                SortType.Descending => query,
                _ => query
            };

            #endregion

            return query;
        }

        public async Task<FilterGeneralSettingsDto> FilterGeneralSettings(FilterGeneralSettingsDto filter)
        {
            IQueryable<GeneralSetting> query = 
                GetGeneralSettingsWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId, 
                await query.CountAsync(),
                filter.TakeEntity, 
                filter.HowManyShowPageAfterAndBefore);

            filter.GeneralSettings = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetGeneralSettingsAsCombo(FilterGeneralSettingsDto filter)
            => await GetGeneralSettingsWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<GeneralSettingListDto?> GetSingleGeneralSettingInformation(int generalSettingId)
            => await _generalSettingRepository
            .GetQueryable()
            .Where(a => a.Id == generalSettingId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<UpdateGeneralSettingDto?> GetGeneralSettingInformation(int generalSettingId)
        =>  await _generalSettingRepository
            .GetQueryable()
            .Where(a => a.Id == generalSettingId)
            .ToUpdateDto()
            .FirstOrDefaultAsync();
public async Task<BaseChangeEntityResult> UpdateGeneralSetting(UpdateGeneralSettingDto update)
        { 
            GeneralSetting? generalSetting = await _generalSettingRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (generalSetting is null) return BaseChangeEntityResult.NotFound;

            generalSetting =  generalSetting.ToModel(update);
            _generalSettingRepository.Update(generalSetting);
            await _generalSettingRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
public async Task<BaseChangeEntityResult> DeleteGeneralSetting(int generalSettingId)
        {
            GeneralSetting? generalSetting = await _generalSettingRepository.GetAsTracking(generalSettingId);
            if (generalSetting is null) return BaseChangeEntityResult.NotFound;

            _generalSettingRepository.SoftDelete(generalSetting);

            await _generalSettingRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
public async Task DeleteGeneralSetting(List<int> generalSettingIds)
        {
            foreach (int generalSettingId in generalSettingIds)
            { 
                GeneralSetting? generalSetting = await _generalSettingRepository.GetAsTracking(generalSettingId);
                if (generalSetting is not null)
                    _generalSettingRepository.SoftDelete(generalSetting);
            }
            await _generalSettingRepository.SaveChanges();
        }

        #endregion

        #endregion
    }
}
