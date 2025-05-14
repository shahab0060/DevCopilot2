using System;
using DevCopilot2.Domain.IRepository;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Security;
using DevCopilot2.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Enums.Properties;
using DevCopilot2.Core.Mappers.Properties;
using DevCopilot2.Domain.Entities.Properties;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.Entities.Languages;
using DevCopilot2.Domain.Enums.Entities;
using DevCopilot2.Core.Mappers.Entities;
using DevCopilot2.Domain.Entities.Entities;
using DevCopilot2.Domain.DTOs.Entities;
using DevCopilot2.Domain.Entities.Projects;
using DevCopilot2.Domain.Entities.Users;
using DevCopilot2.Domain.Enums.Projects;
using DevCopilot2.Domain.DTOs.Imports;
using DevCopilot2.Domain.Enums.DataTypes;
using Microsoft.Data.SqlClient;
using DevCopilot2.Core.Extensions.AdvanceExtensions.Data;

namespace DevCopilot2.Core.Services.Classes
{
    public class EntityService : IEntityService
    {
        #region constructor

        private readonly ICrudRepository<Property, int> _propertyRepository;
        private readonly ICrudRepository<Language, int> _languageRepository;
        private readonly ICrudRepository<Entity, int> _entityRepository;
        private readonly ICrudRepository<EntitySelectedProjectArea, int> _entitySelectedProjectAreaRepository;
        private readonly ICrudRepository<ProjectArea, int> _projectAreaRepository;
        private readonly ICrudRepository<ProjectEnum, int> _projectEnumRepository;
        private readonly ICrudRepository<User, long> _userRepository;
        private readonly ICrudRepository<Project, int> _projectRepository;
        private readonly ICrudRepository<PropertySelectedLanguage, int> _propertySelectedLanguageRepository;
        private readonly ICrudRepository<EntitySelectedLanguage, int> _entitySelectedLanguageRepository;
        private readonly ICrudRepository<EntitySelectedProjectAreaSelectedFilter, int> _entitySelectedProjectAreaSelectedFilterRepository;
        private readonly ICrudRepository<PropertyImageResizeInformation, int> _propertyImageResizeInformationRepository;
        private readonly ICrudRepository<EntityRelation, int> _entityRelationRepository;
        public EntityService(
                           ICrudRepository<Property, int> propertyRepository,
                           ICrudRepository<Language, int> languageRepository,
                           ICrudRepository<Entity, int> entityRepository,
                           ICrudRepository<EntitySelectedProjectArea, int> entitySelectedProjectAreaRepository,
                           ICrudRepository<ProjectArea, int> projectAreaRepository,
                           ICrudRepository<ProjectEnum, int> projectEnumRepository,
                           ICrudRepository<User, long> userRepository,
                           ICrudRepository<Project, int> projectRepository,
                           ICrudRepository<PropertySelectedLanguage, int> propertySelectedLanguageRepository,
                           ICrudRepository<EntitySelectedLanguage, int> entitySelectedLanguageRepository,
                           ICrudRepository<EntitySelectedProjectAreaSelectedFilter, int> entitySelectedProjectAreaSelectedFilterRepository,
                           ICrudRepository<PropertyImageResizeInformation, int> propertyImageResizeInformationRepository,
                           ICrudRepository<EntityRelation, int> entityRelationRepository
                                      )
        {
            this._propertyRepository = propertyRepository;
            this._languageRepository = languageRepository;
            this._entityRepository = entityRepository;
            this._entitySelectedProjectAreaRepository = entitySelectedProjectAreaRepository;
            this._projectAreaRepository = projectAreaRepository;
            this._projectEnumRepository = projectEnumRepository;
            this._userRepository = userRepository;
            this._projectRepository = projectRepository;
            this._propertySelectedLanguageRepository = propertySelectedLanguageRepository;
            this._entitySelectedLanguageRepository = entitySelectedLanguageRepository;
            this._entitySelectedProjectAreaSelectedFilterRepository = entitySelectedProjectAreaSelectedFilterRepository;
            this._propertyImageResizeInformationRepository = propertyImageResizeInformationRepository;
            this._entityRelationRepository = entityRelationRepository;
        }

        #endregion

        #region Properties

        #region property selected languages

        IQueryable<PropertySelectedLanguage> GetPropertySelectedLanguagesWithFilterAndSort(FilterPropertySelectedLanguagesDto filter)
        {
            IQueryable<PropertySelectedLanguage> query = _propertySelectedLanguageRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.Title, $"%{filter.Search}%")
                                         );

            if (filter.PropertyId > 0)
                query = query.Where(q => q.PropertyId == filter.PropertyId);

            if (filter.LanguageId > 0)
                query = query.Where(q => q.LanguageId == filter.LanguageId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortPropertySelectedLanguageType.PropertyName => query.OrderBy(a => a.Property.Name),
                SortPropertySelectedLanguageType.PropertyId => query.OrderBy(a => a.PropertyId),
                SortPropertySelectedLanguageType.LanguageName => query.OrderBy(a => a.Language.Name),
                SortPropertySelectedLanguageType.LanguageId => query.OrderBy(a => a.LanguageId),
                SortPropertySelectedLanguageType.Title => query.OrderBy(a => a.Title),
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

        public async Task<FilterPropertySelectedLanguagesDto> FilterPropertySelectedLanguages(FilterPropertySelectedLanguagesDto filter)
        {
            IQueryable<PropertySelectedLanguage> query =
                GetPropertySelectedLanguagesWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.PropertySelectedLanguages = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetPropertySelectedLanguagesAsCombo(FilterPropertySelectedLanguagesDto filter)
            => await GetPropertySelectedLanguagesWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<PropertySelectedLanguageListDto?> GetSinglePropertySelectedLanguageInformation(int propertySelectedLanguageId)
            => await _propertySelectedLanguageRepository
            .GetQueryable()
            .Where(a => a.Id == propertySelectedLanguageId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<BaseChangeEntityResult> CreatePropertySelectedLanguage(CreatePropertySelectedLanguageDto create)
        {

            #region validate unique properties

            if (await _propertySelectedLanguageRepository
                .GetQueryable()
                .AnyAsync(a => a.Title == create.Title.ToTitle()!))
                return BaseChangeEntityResult.Exists;

            #endregion

            #region validate relation ids

            if (!await _propertyRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.PropertyId))
                return BaseChangeEntityResult.NotFound;

            if (!await _languageRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.LanguageId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            PropertySelectedLanguage propertySelectedLanguage = create.ToModel();
            await _propertySelectedLanguageRepository.Add(propertySelectedLanguage);
            await _propertySelectedLanguageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdatePropertySelectedLanguageDto?> GetPropertySelectedLanguageInformation(int propertySelectedLanguageId)
                => await _propertySelectedLanguageRepository
                    .GetQueryable()
                    .Where(a => a.Id == propertySelectedLanguageId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();
        public async Task<BaseChangeEntityResult> UpdatePropertySelectedLanguage(UpdatePropertySelectedLanguageDto update)
        {
            PropertySelectedLanguage? propertySelectedLanguage = await _propertySelectedLanguageRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (propertySelectedLanguage is null) return BaseChangeEntityResult.NotFound;

            #region validate unique properties

            if (await _propertySelectedLanguageRepository
                .GetQueryable()
                .AnyAsync(a => a.Title == update.Title.ToTitle()!
                               && a.Id != update.Id))
                return BaseChangeEntityResult.Exists;

            #endregion

            #region validate relation ids

            if (!await _propertyRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.PropertyId))
                return BaseChangeEntityResult.NotFound;

            if (!await _languageRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.LanguageId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            propertySelectedLanguage = propertySelectedLanguage.ToModel(update);
            _propertySelectedLanguageRepository.Update(propertySelectedLanguage);
            await _propertySelectedLanguageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeletePropertySelectedLanguage(int propertySelectedLanguageId)
        {
            PropertySelectedLanguage? propertySelectedLanguage = await _propertySelectedLanguageRepository.GetAsTracking(propertySelectedLanguageId);
            if (propertySelectedLanguage is null) return BaseChangeEntityResult.NotFound;

            _propertySelectedLanguageRepository.SoftDelete(propertySelectedLanguage);

            await _propertySelectedLanguageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeletePropertySelectedLanguage(List<int> propertySelectedLanguageIds)
        {
            foreach (int propertySelectedLanguageId in propertySelectedLanguageIds)
            {
                PropertySelectedLanguage? propertySelectedLanguage = await _propertySelectedLanguageRepository.GetAsTracking(propertySelectedLanguageId);
                if (propertySelectedLanguage is not null)
                    _propertySelectedLanguageRepository.SoftDelete(propertySelectedLanguage);
            }
            await _propertySelectedLanguageRepository.SaveChanges();
        }

        #endregion

        #region property image resize information

        IQueryable<PropertyImageResizeInformation> GetPropertyImageResizeInformationWithFilterAndSort(FilterPropertyImageResizeInformationDto filter)
        {
            IQueryable<PropertyImageResizeInformation> query = _propertyImageResizeInformationRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.Name, $"%{filter.Search}%")
                                         );

            if (filter.PropertyId > 0)
                query = query.Where(q => q.PropertyId == filter.PropertyId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortPropertyImageResizeInformationType.PropertyName => query.OrderBy(a => a.Property.Name),
                SortPropertyImageResizeInformationType.PropertyId => query.OrderBy(a => a.PropertyId),
                SortPropertyImageResizeInformationType.Name => query.OrderBy(a => a.Name),
                SortPropertyImageResizeInformationType.Width => query.OrderBy(a => a.Width),
                SortPropertyImageResizeInformationType.Height => query.OrderBy(a => a.Height),
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

        public async Task<FilterPropertyImageResizeInformationDto> FilterPropertyImageResizeInformation(FilterPropertyImageResizeInformationDto filter)
        {
            IQueryable<PropertyImageResizeInformation> query =
                GetPropertyImageResizeInformationWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.PropertyImageResizeInformation = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetPropertyImageResizeInformationAsCombo(FilterPropertyImageResizeInformationDto filter)
            => await GetPropertyImageResizeInformationWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<PropertyImageResizeInformationListDto?> GetSinglePropertyImageResizeInformationInformation(int propertyImageResizeInformationId)
            => await _propertyImageResizeInformationRepository
            .GetQueryable()
            .Where(a => a.Id == propertyImageResizeInformationId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<BaseChangeEntityResult> CreatePropertyImageResizeInformation(CreatePropertyImageResizeInformationDto create)
        {

            #region validate unique properties

            if (await _propertyImageResizeInformationRepository
                .GetQueryable()
                .AnyAsync(a => a.Name == create.Name.ToTitle()!))
                return BaseChangeEntityResult.Exists;

            #endregion

            #region validate relation ids

            if (!await _propertyRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.PropertyId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            PropertyImageResizeInformation propertyImageResizeInformation = create.ToModel();
            await _propertyImageResizeInformationRepository.Add(propertyImageResizeInformation);
            await _propertyImageResizeInformationRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdatePropertyImageResizeInformationDto?> GetPropertyImageResizeInformationInformation(int propertyImageResizeInformationId)
                => await _propertyImageResizeInformationRepository
                    .GetQueryable()
                    .Where(a => a.Id == propertyImageResizeInformationId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();
        public async Task<BaseChangeEntityResult> UpdatePropertyImageResizeInformation(UpdatePropertyImageResizeInformationDto update)
        {
            PropertyImageResizeInformation? propertyImageResizeInformation = await _propertyImageResizeInformationRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (propertyImageResizeInformation is null) return BaseChangeEntityResult.NotFound;

            #region validate unique properties

            if (await _propertyImageResizeInformationRepository
                .GetQueryable()
                .AnyAsync(a => a.Name == update.Name.ToTitle()!
                               && a.Id != update.Id))
                return BaseChangeEntityResult.Exists;

            #endregion

            #region validate relation ids

            if (!await _propertyRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.PropertyId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            propertyImageResizeInformation = propertyImageResizeInformation.ToModel(update);
            _propertyImageResizeInformationRepository.Update(propertyImageResizeInformation);
            await _propertyImageResizeInformationRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeletePropertyImageResizeInformation(int propertyImageResizeInformationId)
        {
            PropertyImageResizeInformation? propertyImageResizeInformation = await _propertyImageResizeInformationRepository.GetAsTracking(propertyImageResizeInformationId);
            if (propertyImageResizeInformation is null) return BaseChangeEntityResult.NotFound;

            _propertyImageResizeInformationRepository.SoftDelete(propertyImageResizeInformation);

            await _propertyImageResizeInformationRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeletePropertyImageResizeInformation(List<int> propertyImageResizeInformationIds)
        {
            foreach (int propertyImageResizeInformationId in propertyImageResizeInformationIds)
            {
                PropertyImageResizeInformation? propertyImageResizeInformation = await _propertyImageResizeInformationRepository.GetAsTracking(propertyImageResizeInformationId);
                if (propertyImageResizeInformation is not null)
                    _propertyImageResizeInformationRepository.SoftDelete(propertyImageResizeInformation);
            }
            await _propertyImageResizeInformationRepository.SaveChanges();
        }

        #endregion

        #region properties

        IQueryable<Property> GetPropertiesWithFilterAndSort(FilterPropertiesDto filter)
        {
            IQueryable<Property> query = _propertyRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.Name, $"%{filter.Search}%") ||

EF.Functions.Like(q.ForceMapperCode, $"%{filter.Search}%")
                                         );

            if (filter.ProjectEnumId > 0)
                query = query.Where(q => q.ProjectEnumId == filter.ProjectEnumId);

            if (filter.EntityId > 0)
                query = query.Where(q => q.EntityId == filter.EntityId);

            if (filter.ProjectId > 0)
                query = query.Where(q => q.Entity.ProjectId == filter.ProjectId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortPropertyType.Name => query.OrderBy(a => a.Name),
                SortPropertyType.DataType => query.OrderBy(a => a.DataType),
                SortPropertyType.MaxLength => query.OrderBy(a => a.MaxLength),
                SortPropertyType.MinLength => query.OrderBy(a => a.MinLength),
                SortPropertyType.RangeFrom => query.OrderBy(a => a.RangeFrom),
                SortPropertyType.RangeTo => query.OrderBy(a => a.RangeTo),
                SortPropertyType.IsRequired => query.OrderBy(a => a.IsRequired),
                SortPropertyType.ProjectEnumEnglishName => query.OrderBy(a => a.ProjectEnum.EnglishName),
                SortPropertyType.ProjectEnumId => query.OrderBy(a => a.ProjectEnumId),
                SortPropertyType.DataAnnotationDataType => query.OrderBy(a => a.DataAnnotationDataType),
                SortPropertyType.IsUnique => query.OrderBy(a => a.IsUnique),
                SortPropertyType.IsUpdatable => query.OrderBy(a => a.IsUpdatable),
                SortPropertyType.ShowInList => query.OrderBy(a => a.ShowInList),
                SortPropertyType.IsFilterContain => query.OrderBy(a => a.IsFilterContain),
                SortPropertyType.IsFilterEqual => query.OrderBy(a => a.IsFilterEqual),
                SortPropertyType.Order => query.OrderBy(a => a.Order),
                SortPropertyType.UseEditor => query.OrderBy(a => a.UseEditor),
                SortPropertyType.EntityPluralName => query.OrderBy(a => a.Entity.PluralName),
                SortPropertyType.EntityId => query.OrderBy(a => a.EntityId),
                SortPropertyType.ForceMapperCode => query.OrderBy(a => a.ForceMapperCode),
                SortPropertyType.ExcludeFromListDto => query.OrderBy(a => a.ExcludeFromListDto),
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

        public async Task<FilterPropertiesDto> FilterProperties(FilterPropertiesDto filter)
        {
            IQueryable<Property> query =
                GetPropertiesWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.Properties = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetPropertiesAsCombo(FilterPropertiesDto filter)
            => await GetPropertiesWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<PropertyListDto?> GetSinglePropertyInformation(int propertyId)
            => await _propertyRepository
            .GetQueryable()
            .Where(a => a.Id == propertyId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<BaseChangeEntityResult> CreateProperty(CreatePropertyDto create)
        {
            #region validate unique properties

            if (await _propertyRepository
                .GetQueryable()
                .AnyAsync(a => a.Name == create.Name.SanitizeText()! && a.EntityId == create.EntityId))
                return BaseChangeEntityResult.Exists;

            #endregion

            #region validate relation ids

            if (create.ProjectEnumId > 0)
                if (!await _projectEnumRepository
                    .GetQueryable()
                    .AnyAsync(a => a.Id == create.ProjectEnumId))
                    return BaseChangeEntityResult.NotFound;

            if (!await _entityRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.EntityId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            Property property = create.ToModel();
            await _propertyRepository.Add(property);
            await _propertyRepository.SaveChanges();

            #region add relations

            #region add entity relations

            if (create.EntityRelationsList is not null && create.EntityRelationsList.Any())
            {
                foreach (var entityRelation in create.EntityRelationsList)
                {
                    entityRelation.PrimaryPropertyId = property.Id;
                    await CreateEntityRelation(entityRelation);
                }

            }

            #endregion

            #region add property image resize information

            if (create.PropertyImageResizeInformationList is not null && create.PropertyImageResizeInformationList.Any())
            {
                foreach (var propertyImageResizeInformation in create.PropertyImageResizeInformationList)
                {
                    propertyImageResizeInformation.PropertyId = property.Id;
                    await CreatePropertyImageResizeInformation(propertyImageResizeInformation);
                }

            }

            #endregion

            #region add property selected languages

            if (create.PropertySelectedLanguagesList is not null && create.PropertySelectedLanguagesList.Any())
            {
                foreach (var propertySelectedLanguage in create.PropertySelectedLanguagesList)
                {
                    propertySelectedLanguage.PropertyId = property.Id;
                    await CreatePropertySelectedLanguage(propertySelectedLanguage);
                }

            }

            #endregion

            #endregion

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdatePropertyDto?> GetPropertyInformation(int propertyId)
                => await _propertyRepository
                    .GetQueryable()
                    .Where(a => a.Id == propertyId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();
        public async Task<BaseChangeEntityResult> UpdateProperty(UpdatePropertyDto update)
        {
            Property? property = await _propertyRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (property is null) return BaseChangeEntityResult.NotFound;

            #region validate unique properties

            if (await _propertyRepository
                .GetQueryable()
                .AnyAsync(a => a.Name == update.Name.SanitizeText()! && a.Id != update.Id && a.EntityId == update.EntityId))
                return BaseChangeEntityResult.Exists;

            #endregion

            #region validate relation ids

            if (update.ProjectEnumId > 0)
                if (!await _projectEnumRepository
                    .GetQueryable()
                    .AnyAsync(a => a.Id == update.ProjectEnumId))
                    return BaseChangeEntityResult.NotFound;

            if (!await _entityRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.EntityId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            property = property.ToModel(update);
            _propertyRepository.Update(property);
            await _propertyRepository.SaveChanges();

            #region update relations

            #region handle entity relations operations

            List<EntityRelation> currentEntityRelations = await _entityRelationRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.PrimaryPropertyId == property.Id)
                .ToListAsync();

            #region add new entity relations

            List<CreateEntityRelationDto> newEntityRelations = update.EntityRelationsList
                                .Where(a => !currentEntityRelations.Any(b => b.Id == a.Id))
                                .ToCreateDto()
                                .ToList();

            foreach (CreateEntityRelationDto entityRelation in newEntityRelations)
            {
                entityRelation.PrimaryPropertyId = property.Id;
                await CreateEntityRelation(entityRelation);
            }

            #endregion

            #region update entity relations

            List<UpdateEntityRelationDto> updatedEntityRelations =
                    update.EntityRelationsList
                    .Where(a => currentEntityRelations.Any(b => b.Id == a.Id))
                    .ToList();

            foreach (UpdateEntityRelationDto entityRelation in updatedEntityRelations)
            {
                await UpdateEntityRelation(entityRelation);
            }

            #endregion

            #region delete entity relations

            List<int> removedEntityRelationsIds =
                    currentEntityRelations
                    .Where(a => !update.EntityRelationsList.Any(b => b.Id == a.Id))
                    .Select(a => a.Id)
                    .ToList();

            await DeleteEntityRelation(removedEntityRelationsIds);

            #endregion

            #endregion

            #region handle property image resize information operations

            List<PropertyImageResizeInformation> currentPropertyImageResizeInformation = await _propertyImageResizeInformationRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.PropertyId == property.Id)
                .ToListAsync();

            #region add new property image resize information

            List<CreatePropertyImageResizeInformationDto> newPropertyImageResizeInformation = update.PropertyImageResizeInformationList
                                .Where(a => !currentPropertyImageResizeInformation.Any(b => b.Id == a.Id))
                                .ToCreateDto()
                                .ToList();

            foreach (CreatePropertyImageResizeInformationDto propertyImageResizeInformation in newPropertyImageResizeInformation)
            {
                propertyImageResizeInformation.PropertyId = property.Id;
                await CreatePropertyImageResizeInformation(propertyImageResizeInformation);
            }

            #endregion

            #region update property image resize information

            List<UpdatePropertyImageResizeInformationDto> updatedPropertyImageResizeInformation =
                    update.PropertyImageResizeInformationList
                    .Where(a => currentPropertyImageResizeInformation.Any(b => b.Id == a.Id))
                    .ToList();

            foreach (UpdatePropertyImageResizeInformationDto propertyImageResizeInformation in updatedPropertyImageResizeInformation)
            {
                await UpdatePropertyImageResizeInformation(propertyImageResizeInformation);
            }

            #endregion

            #region delete property image resize information

            List<int> removedPropertyImageResizeInformationIds =
                    currentPropertyImageResizeInformation
                    .Where(a => !update.PropertyImageResizeInformationList.Any(b => b.Id == a.Id))
                    .Select(a => a.Id)
                    .ToList();

            await DeletePropertyImageResizeInformation(removedPropertyImageResizeInformationIds);

            #endregion

            #endregion

            #region handle property selected languages operations

            List<PropertySelectedLanguage> currentPropertySelectedLanguages = await _propertySelectedLanguageRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.PropertyId == property.Id)
                .ToListAsync();

            #region add new property selected languages

            List<CreatePropertySelectedLanguageDto> newPropertySelectedLanguages = update.PropertySelectedLanguagesList
                                .Where(a => !currentPropertySelectedLanguages.Any(b => b.Id == a.Id))
                                .ToCreateDto()
                                .ToList();

            foreach (CreatePropertySelectedLanguageDto propertySelectedLanguage in newPropertySelectedLanguages)
            {
                propertySelectedLanguage.PropertyId = property.Id;
                await CreatePropertySelectedLanguage(propertySelectedLanguage);
            }

            #endregion

            #region update property selected languages

            List<UpdatePropertySelectedLanguageDto> updatedPropertySelectedLanguages =
                    update.PropertySelectedLanguagesList
                    .Where(a => currentPropertySelectedLanguages.Any(b => b.Id == a.Id))
                    .ToList();

            foreach (UpdatePropertySelectedLanguageDto propertySelectedLanguage in updatedPropertySelectedLanguages)
            {
                await UpdatePropertySelectedLanguage(propertySelectedLanguage);
            }

            #endregion

            #region delete property selected languages

            List<int> removedPropertySelectedLanguagesIds =
                    currentPropertySelectedLanguages
                    .Where(a => !update.PropertySelectedLanguagesList.Any(b => b.Id == a.Id))
                    .Select(a => a.Id)
                    .ToList();

            await DeletePropertySelectedLanguage(removedPropertySelectedLanguagesIds);

            #endregion

            #endregion

            #endregion

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeleteProperty(int propertyId)
        {
            Property? property = await _propertyRepository.GetAsTracking(propertyId);
            if (property is null) return BaseChangeEntityResult.NotFound;

            _propertyRepository.SoftDelete(property);

            await _propertyRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteProperty(List<int> propertyIds)
        {
            foreach (int propertyId in propertyIds)
            {
                Property? property = await _propertyRepository.GetAsTracking(propertyId);
                if (property is not null)
                    _propertyRepository.SoftDelete(property);
            }
            await _propertyRepository.SaveChanges();
        }

        #endregion

        #endregion

        #region Entities

        #region entity selected languages

        IQueryable<EntitySelectedLanguage> GetEntitySelectedLanguagesWithFilterAndSort(FilterEntitySelectedLanguagesDto filter)
        {
            IQueryable<EntitySelectedLanguage> query = _entitySelectedLanguageRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.SingularTitle, $"%{filter.Search}%") ||

EF.Functions.Like(q.PluralTitle, $"%{filter.Search}%")
                                         );

            if (filter.EntityId > 0)
                query = query.Where(q => q.EntityId == filter.EntityId);

            if (filter.LanguageId > 0)
                query = query.Where(q => q.LanguageId == filter.LanguageId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortEntitySelectedLanguageType.EntityPluralName => query.OrderBy(a => a.Entity.PluralName),
                SortEntitySelectedLanguageType.EntityId => query.OrderBy(a => a.EntityId),
                SortEntitySelectedLanguageType.LanguageName => query.OrderBy(a => a.Language.Name),
                SortEntitySelectedLanguageType.LanguageId => query.OrderBy(a => a.LanguageId),
                SortEntitySelectedLanguageType.SingularTitle => query.OrderBy(a => a.SingularTitle),
                SortEntitySelectedLanguageType.PluralTitle => query.OrderBy(a => a.PluralTitle),
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

        public async Task<FilterEntitySelectedLanguagesDto> FilterEntitySelectedLanguages(FilterEntitySelectedLanguagesDto filter)
        {
            IQueryable<EntitySelectedLanguage> query =
                GetEntitySelectedLanguagesWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.EntitySelectedLanguages = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetEntitySelectedLanguagesAsCombo(FilterEntitySelectedLanguagesDto filter)
            => await GetEntitySelectedLanguagesWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<EntitySelectedLanguageListDto?> GetSingleEntitySelectedLanguageInformation(int entitySelectedLanguageId)
            => await _entitySelectedLanguageRepository
            .GetQueryable()
            .Where(a => a.Id == entitySelectedLanguageId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<BaseChangeEntityResult> CreateEntitySelectedLanguage(CreateEntitySelectedLanguageDto create)
        {

            #region validate relation ids

            if (!await _entityRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.EntityId))
                return BaseChangeEntityResult.NotFound;

            if (!await _languageRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.LanguageId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            EntitySelectedLanguage entitySelectedLanguage = create.ToModel();
            await _entitySelectedLanguageRepository.Add(entitySelectedLanguage);
            await _entitySelectedLanguageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdateEntitySelectedLanguageDto?> GetEntitySelectedLanguageInformation(int entitySelectedLanguageId)
                => await _entitySelectedLanguageRepository
                    .GetQueryable()
                    .Where(a => a.Id == entitySelectedLanguageId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();
        public async Task<BaseChangeEntityResult> UpdateEntitySelectedLanguage(UpdateEntitySelectedLanguageDto update)
        {
            EntitySelectedLanguage? entitySelectedLanguage = await _entitySelectedLanguageRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (entitySelectedLanguage is null) return BaseChangeEntityResult.NotFound;

            #region validate relation ids

            if (!await _entityRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.EntityId))
                return BaseChangeEntityResult.NotFound;

            if (!await _languageRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.LanguageId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            entitySelectedLanguage = entitySelectedLanguage.ToModel(update);
            _entitySelectedLanguageRepository.Update(entitySelectedLanguage);
            await _entitySelectedLanguageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeleteEntitySelectedLanguage(int entitySelectedLanguageId)
        {
            EntitySelectedLanguage? entitySelectedLanguage = await _entitySelectedLanguageRepository.GetAsTracking(entitySelectedLanguageId);
            if (entitySelectedLanguage is null) return BaseChangeEntityResult.NotFound;

            _entitySelectedLanguageRepository.SoftDelete(entitySelectedLanguage);

            await _entitySelectedLanguageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteEntitySelectedLanguage(List<int> entitySelectedLanguageIds)
        {
            foreach (int entitySelectedLanguageId in entitySelectedLanguageIds)
            {
                EntitySelectedLanguage? entitySelectedLanguage = await _entitySelectedLanguageRepository.GetAsTracking(entitySelectedLanguageId);
                if (entitySelectedLanguage is not null)
                    _entitySelectedLanguageRepository.SoftDelete(entitySelectedLanguage);
            }
            await _entitySelectedLanguageRepository.SaveChanges();
        }

        #endregion

        #region entity selected project area selected filters

        IQueryable<EntitySelectedProjectAreaSelectedFilter> GetEntitySelectedProjectAreaSelectedFiltersWithFilterAndSort(FilterEntitySelectedProjectAreaSelectedFiltersDto filter)
        {
            IQueryable<EntitySelectedProjectAreaSelectedFilter> query = _entitySelectedProjectAreaSelectedFilterRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.Value, $"%{filter.Search}%")
                                         );

            if (filter.EntitySelectedProjectAreaId > 0)
                query = query.Where(q => q.EntitySelectedProjectAreaId == filter.EntitySelectedProjectAreaId);

            if (filter.PropertyId > 0)
                query = query.Where(q => q.PropertyId == filter.PropertyId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortEntitySelectedProjectAreaSelectedFilterType.EntitySelectedProjectAreaHasWeb => query.OrderBy(a => a.EntitySelectedProjectArea.HasWeb),
                SortEntitySelectedProjectAreaSelectedFilterType.EntitySelectedProjectAreaId => query.OrderBy(a => a.EntitySelectedProjectAreaId),
                SortEntitySelectedProjectAreaSelectedFilterType.PropertyName => query.OrderBy(a => a.Property.Name),
                SortEntitySelectedProjectAreaSelectedFilterType.PropertyId => query.OrderBy(a => a.PropertyId),
                SortEntitySelectedProjectAreaSelectedFilterType.Value => query.OrderBy(a => a.Value),
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

        public async Task<FilterEntitySelectedProjectAreaSelectedFiltersDto> FilterEntitySelectedProjectAreaSelectedFilters(FilterEntitySelectedProjectAreaSelectedFiltersDto filter)
        {
            IQueryable<EntitySelectedProjectAreaSelectedFilter> query =
                GetEntitySelectedProjectAreaSelectedFiltersWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.EntitySelectedProjectAreaSelectedFilters = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetEntitySelectedProjectAreaSelectedFiltersAsCombo(FilterEntitySelectedProjectAreaSelectedFiltersDto filter)
            => await GetEntitySelectedProjectAreaSelectedFiltersWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<EntitySelectedProjectAreaSelectedFilterListDto?> GetSingleEntitySelectedProjectAreaSelectedFilterInformation(int entitySelectedProjectAreaSelectedFilterId)
            => await _entitySelectedProjectAreaSelectedFilterRepository
            .GetQueryable()
            .Where(a => a.Id == entitySelectedProjectAreaSelectedFilterId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<BaseChangeEntityResult> CreateEntitySelectedProjectAreaSelectedFilter(CreateEntitySelectedProjectAreaSelectedFilterDto create)
        {

            #region validate relation ids

            if (!await _entitySelectedProjectAreaRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.EntitySelectedProjectAreaId))
                return BaseChangeEntityResult.NotFound;

            if (!await _propertyRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.PropertyId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            EntitySelectedProjectAreaSelectedFilter entitySelectedProjectAreaSelectedFilter = create.ToModel();
            await _entitySelectedProjectAreaSelectedFilterRepository.Add(entitySelectedProjectAreaSelectedFilter);
            await _entitySelectedProjectAreaSelectedFilterRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdateEntitySelectedProjectAreaSelectedFilterDto?> GetEntitySelectedProjectAreaSelectedFilterInformation(int entitySelectedProjectAreaSelectedFilterId)
                => await _entitySelectedProjectAreaSelectedFilterRepository
                    .GetQueryable()
                    .Where(a => a.Id == entitySelectedProjectAreaSelectedFilterId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();
        public async Task<BaseChangeEntityResult> UpdateEntitySelectedProjectAreaSelectedFilter(UpdateEntitySelectedProjectAreaSelectedFilterDto update)
        {
            EntitySelectedProjectAreaSelectedFilter? entitySelectedProjectAreaSelectedFilter = await _entitySelectedProjectAreaSelectedFilterRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (entitySelectedProjectAreaSelectedFilter is null) return BaseChangeEntityResult.NotFound;

            #region validate relation ids

            if (!await _entitySelectedProjectAreaRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.EntitySelectedProjectAreaId))
                return BaseChangeEntityResult.NotFound;

            if (!await _propertyRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.PropertyId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            entitySelectedProjectAreaSelectedFilter = entitySelectedProjectAreaSelectedFilter.ToModel(update);
            _entitySelectedProjectAreaSelectedFilterRepository.Update(entitySelectedProjectAreaSelectedFilter);
            await _entitySelectedProjectAreaSelectedFilterRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeleteEntitySelectedProjectAreaSelectedFilter(int entitySelectedProjectAreaSelectedFilterId)
        {
            EntitySelectedProjectAreaSelectedFilter? entitySelectedProjectAreaSelectedFilter = await _entitySelectedProjectAreaSelectedFilterRepository.GetAsTracking(entitySelectedProjectAreaSelectedFilterId);
            if (entitySelectedProjectAreaSelectedFilter is null) return BaseChangeEntityResult.NotFound;

            _entitySelectedProjectAreaSelectedFilterRepository.SoftDelete(entitySelectedProjectAreaSelectedFilter);

            await _entitySelectedProjectAreaSelectedFilterRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteEntitySelectedProjectAreaSelectedFilter(List<int> entitySelectedProjectAreaSelectedFilterIds)
        {
            foreach (int entitySelectedProjectAreaSelectedFilterId in entitySelectedProjectAreaSelectedFilterIds)
            {
                EntitySelectedProjectAreaSelectedFilter? entitySelectedProjectAreaSelectedFilter = await _entitySelectedProjectAreaSelectedFilterRepository.GetAsTracking(entitySelectedProjectAreaSelectedFilterId);
                if (entitySelectedProjectAreaSelectedFilter is not null)
                    _entitySelectedProjectAreaSelectedFilterRepository.SoftDelete(entitySelectedProjectAreaSelectedFilter);
            }
            await _entitySelectedProjectAreaSelectedFilterRepository.SaveChanges();
        }

        #endregion

        #region entity selected project areas

        IQueryable<EntitySelectedProjectArea> GetEntitySelectedProjectAreasWithFilterAndSort(FilterEntitySelectedProjectAreasDto filter)
        {
            IQueryable<EntitySelectedProjectArea> query = _entitySelectedProjectAreaRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (filter.EntityId > 0)
                query = query.Where(q => q.EntityId == filter.EntityId);

            if (filter.ProjectAreaId > 0)
                query = query.Where(q => q.ProjectAreaId == filter.ProjectAreaId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortEntitySelectedProjectAreaType.EntityPluralName => query.OrderBy(a => a.Entity.PluralName),
                SortEntitySelectedProjectAreaType.EntityId => query.OrderBy(a => a.EntityId),
                SortEntitySelectedProjectAreaType.ProjectAreaTitle => query.OrderBy(a => a.ProjectArea.Title),
                SortEntitySelectedProjectAreaType.ProjectAreaId => query.OrderBy(a => a.ProjectAreaId),
                SortEntitySelectedProjectAreaType.HasIndex => query.OrderBy(a => a.HasIndex),
                SortEntitySelectedProjectAreaType.HasCreate => query.OrderBy(a => a.HasCreate),
                SortEntitySelectedProjectAreaType.HasUpdate => query.OrderBy(a => a.HasUpdate),
                SortEntitySelectedProjectAreaType.HasDelete => query.OrderBy(a => a.HasDelete),
                SortEntitySelectedProjectAreaType.HasApi => query.OrderBy(a => a.HasApi),
                SortEntitySelectedProjectAreaType.HasWeb => query.OrderBy(a => a.HasWeb),
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

        public async Task<FilterEntitySelectedProjectAreasDto> FilterEntitySelectedProjectAreas(FilterEntitySelectedProjectAreasDto filter)
        {
            IQueryable<EntitySelectedProjectArea> query =
                GetEntitySelectedProjectAreasWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.EntitySelectedProjectAreas = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetEntitySelectedProjectAreasAsCombo(FilterEntitySelectedProjectAreasDto filter)
            => await GetEntitySelectedProjectAreasWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<EntitySelectedProjectAreaListDto?> GetSingleEntitySelectedProjectAreaInformation(int entitySelectedProjectAreaId)
            => await _entitySelectedProjectAreaRepository
            .GetQueryable()
            .Where(a => a.Id == entitySelectedProjectAreaId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<BaseChangeEntityResult> CreateEntitySelectedProjectArea(CreateEntitySelectedProjectAreaDto create)
        {

            #region validate relation ids

            if (!await _entityRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.EntityId))
                return BaseChangeEntityResult.NotFound;

            if (!await _projectAreaRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.ProjectAreaId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            EntitySelectedProjectArea entitySelectedProjectArea = create.ToModel();
            await _entitySelectedProjectAreaRepository.Add(entitySelectedProjectArea);
            await _entitySelectedProjectAreaRepository.SaveChanges();

            #region add relations

            #region add entity selected project area selected filters

            if (create.EntitySelectedProjectAreaSelectedFiltersList is not null && create.EntitySelectedProjectAreaSelectedFiltersList.Any())
            {
                foreach (var entitySelectedProjectAreaSelectedFilter in create.EntitySelectedProjectAreaSelectedFiltersList)
                {
                    entitySelectedProjectAreaSelectedFilter.EntitySelectedProjectAreaId = entitySelectedProjectArea.Id;
                    await CreateEntitySelectedProjectAreaSelectedFilter(entitySelectedProjectAreaSelectedFilter);
                }

            }

            #endregion

            #endregion

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdateEntitySelectedProjectAreaDto?> GetEntitySelectedProjectAreaInformation(int entitySelectedProjectAreaId)
                => await _entitySelectedProjectAreaRepository
                    .GetQueryable()
                    .Where(a => a.Id == entitySelectedProjectAreaId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();
        public async Task<BaseChangeEntityResult> UpdateEntitySelectedProjectArea(UpdateEntitySelectedProjectAreaDto update)
        {
            EntitySelectedProjectArea? entitySelectedProjectArea = await _entitySelectedProjectAreaRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (entitySelectedProjectArea is null) return BaseChangeEntityResult.NotFound;

            #region validate relation ids

            if (!await _entityRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.EntityId))
                return BaseChangeEntityResult.NotFound;

            if (!await _projectAreaRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.ProjectAreaId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            entitySelectedProjectArea = entitySelectedProjectArea.ToModel(update);
            _entitySelectedProjectAreaRepository.Update(entitySelectedProjectArea);
            await _entitySelectedProjectAreaRepository.SaveChanges();

            #region update relations

            #region handle entity selected project area selected filters operations

            List<EntitySelectedProjectAreaSelectedFilter> currentEntitySelectedProjectAreaSelectedFilters = await _entitySelectedProjectAreaSelectedFilterRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.EntitySelectedProjectAreaId == entitySelectedProjectArea.Id)
                .ToListAsync();

            #region add new entity selected project area selected filters

            List<CreateEntitySelectedProjectAreaSelectedFilterDto> newEntitySelectedProjectAreaSelectedFilters = update.EntitySelectedProjectAreaSelectedFiltersList
                                .Where(a => !currentEntitySelectedProjectAreaSelectedFilters.Any(b => b.Id == a.Id))
                                .ToCreateDto()
                                .ToList();

            foreach (CreateEntitySelectedProjectAreaSelectedFilterDto entitySelectedProjectAreaSelectedFilter in newEntitySelectedProjectAreaSelectedFilters)
            {
                entitySelectedProjectAreaSelectedFilter.EntitySelectedProjectAreaId = entitySelectedProjectArea.Id;
                await CreateEntitySelectedProjectAreaSelectedFilter(entitySelectedProjectAreaSelectedFilter);
            }

            #endregion

            #region update entity selected project area selected filters

            List<UpdateEntitySelectedProjectAreaSelectedFilterDto> updatedEntitySelectedProjectAreaSelectedFilters =
                    update.EntitySelectedProjectAreaSelectedFiltersList
                    .Where(a => currentEntitySelectedProjectAreaSelectedFilters.Any(b => b.Id == a.Id))
                    .ToList();

            foreach (UpdateEntitySelectedProjectAreaSelectedFilterDto entitySelectedProjectAreaSelectedFilter in updatedEntitySelectedProjectAreaSelectedFilters)
            {
                await UpdateEntitySelectedProjectAreaSelectedFilter(entitySelectedProjectAreaSelectedFilter);
            }

            #endregion

            #region delete entity selected project area selected filters

            List<int> removedEntitySelectedProjectAreaSelectedFiltersIds =
                    currentEntitySelectedProjectAreaSelectedFilters
                    .Where(a => !update.EntitySelectedProjectAreaSelectedFiltersList.Any(b => b.Id == a.Id))
                    .Select(a => a.Id)
                    .ToList();

            await DeleteEntitySelectedProjectAreaSelectedFilter(removedEntitySelectedProjectAreaSelectedFiltersIds);

            #endregion

            #endregion

            #endregion

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeleteEntitySelectedProjectArea(int entitySelectedProjectAreaId)
        {
            EntitySelectedProjectArea? entitySelectedProjectArea = await _entitySelectedProjectAreaRepository.GetAsTracking(entitySelectedProjectAreaId);
            if (entitySelectedProjectArea is null) return BaseChangeEntityResult.NotFound;

            _entitySelectedProjectAreaRepository.SoftDelete(entitySelectedProjectArea);

            await _entitySelectedProjectAreaRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteEntitySelectedProjectArea(List<int> entitySelectedProjectAreaIds)
        {
            foreach (int entitySelectedProjectAreaId in entitySelectedProjectAreaIds)
            {
                EntitySelectedProjectArea? entitySelectedProjectArea = await _entitySelectedProjectAreaRepository.GetAsTracking(entitySelectedProjectAreaId);
                if (entitySelectedProjectArea is not null)
                    _entitySelectedProjectAreaRepository.SoftDelete(entitySelectedProjectArea);
            }
            await _entitySelectedProjectAreaRepository.SaveChanges();
        }

        #endregion

        #region entity relations

        IQueryable<EntityRelation> GetEntityRelationsWithFilterAndSort(FilterEntityRelationsDto filter)
        {
            IQueryable<EntityRelation> query = _entityRelationRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.FillingCode, $"%{filter.Search}%")
                                         );

            if (filter.PrimaryPropertyId > 0)
                query = query.Where(q => q.PrimaryPropertyId == filter.PrimaryPropertyId);

            if (filter.SecondaryEntityId > 0)
                query = query.Where(q => q.SecondaryEntityId == filter.SecondaryEntityId);

            if (filter.MiddleEntityId > 0)
                query = query.Where(q => q.MiddleEntityId == filter.MiddleEntityId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortEntityRelationType.PrimaryPropertyName => query.OrderBy(a => a.PrimaryProperty.Name),
                SortEntityRelationType.PrimaryPropertyId => query.OrderBy(a => a.PrimaryPropertyId),
                SortEntityRelationType.SecondaryEntityPluralName => query.OrderBy(a => a.SecondaryEntity.PluralName),
                SortEntityRelationType.SecondaryEntityId => query.OrderBy(a => a.SecondaryEntityId),
                SortEntityRelationType.MiddleEntityPluralName => query.OrderBy(a => a.MiddleEntity.PluralName),
                SortEntityRelationType.MiddleEntityId => query.OrderBy(a => a.MiddleEntityId),
                SortEntityRelationType.RelationType => query.OrderBy(a => a.RelationType),
                SortEntityRelationType.InputType => query.OrderBy(a => a.InputType),
                SortEntityRelationType.FillingType => query.OrderBy(a => a.FillingType),
                SortEntityRelationType.FillingCode => query.OrderBy(a => a.FillingCode),
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

        public async Task<FilterEntityRelationsDto> FilterEntityRelations(FilterEntityRelationsDto filter)
        {
            IQueryable<EntityRelation> query =
                GetEntityRelationsWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.EntityRelations = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetEntityRelationsAsCombo(FilterEntityRelationsDto filter)
            => await GetEntityRelationsWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<EntityRelationListDto?> GetSingleEntityRelationInformation(int entityRelationId)
            => await _entityRelationRepository
            .GetQueryable()
            .Where(a => a.Id == entityRelationId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<BaseChangeEntityResult> CreateEntityRelation(CreateEntityRelationDto create)
        {

            #region validate relation ids

            if (!await _propertyRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.PrimaryPropertyId))
                return BaseChangeEntityResult.NotFound;

            if (!await _entityRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.SecondaryEntityId))
                return BaseChangeEntityResult.NotFound;

            if (create.MiddleEntityId > 0)
                if (!await _entityRepository
                    .GetQueryable()
                    .AnyAsync(a => a.Id == create.MiddleEntityId))
                    return BaseChangeEntityResult.NotFound;

            #endregion

            EntityRelation entityRelation = create.ToModel();
            await _entityRelationRepository.Add(entityRelation);
            await _entityRelationRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }


        async Task<List<BaseChangeEntityResult>> AddPropertyRelationsByName(List<CreateEntityRelationDto> createRelations, long projectId)
        {
            List<BaseChangeEntityResult> results = new List<BaseChangeEntityResult>();
            foreach (var create in createRelations)
            {
                create.SecondaryEntityId = await _entityRepository
                    .GetQueryable()
                    .Where(a => a.ProjectId == projectId && (a.SingularName == create.SecondaryEntityName || a.PluralName == create.SecondaryEntityName))
                    .Select(a => a.Id)
                    .FirstOrDefaultAsync();
                if (create.SecondaryEntityId is not > 0) continue;
                results.Add(await CreateEntityRelation(create));
            }
            return results;
        }
        public async Task<UpdateEntityRelationDto?> GetEntityRelationInformation(int entityRelationId)
                => await _entityRelationRepository
                    .GetQueryable()
                    .Where(a => a.Id == entityRelationId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();
        public async Task<BaseChangeEntityResult> UpdateEntityRelation(UpdateEntityRelationDto update)
        {
            EntityRelation? entityRelation = await _entityRelationRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (entityRelation is null) return BaseChangeEntityResult.NotFound;

            #region validate relation ids

            if (!await _propertyRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.PrimaryPropertyId))
                return BaseChangeEntityResult.NotFound;

            if (!await _entityRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.SecondaryEntityId))
                return BaseChangeEntityResult.NotFound;

            if (update.MiddleEntityId > 0)
                if (!await _entityRepository
                    .GetQueryable()
                    .AnyAsync(a => a.Id == update.MiddleEntityId))
                    return BaseChangeEntityResult.NotFound;

            #endregion

            entityRelation = entityRelation.ToModel(update);
            _entityRelationRepository.Update(entityRelation);
            await _entityRelationRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeleteEntityRelation(int entityRelationId)
        {
            EntityRelation? entityRelation = await _entityRelationRepository.GetAsTracking(entityRelationId);
            if (entityRelation is null) return BaseChangeEntityResult.NotFound;

            _entityRelationRepository.SoftDelete(entityRelation);

            await _entityRelationRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteEntityRelation(List<int> entityRelationIds)
        {
            foreach (int entityRelationId in entityRelationIds)
            {
                EntityRelation? entityRelation = await _entityRelationRepository.GetAsTracking(entityRelationId);
                if (entityRelation is not null)
                    _entityRelationRepository.SoftDelete(entityRelation);
            }
            await _entityRelationRepository.SaveChanges();
        }

        #endregion

        #region entities

        IQueryable<Entity> GetEntitiesWithFilterAndSort(FilterEntitiesDto filter)
        {
            IQueryable<Entity> query = _entityRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.SingularName, $"%{filter.Search}%") ||

EF.Functions.Like(q.PluralName, $"%{filter.Search}%") ||

EF.Functions.Like(q.FolderName, $"%{filter.Search}%") ||

EF.Functions.Like(q.ServiceName, $"%{filter.Search}%")
                                         );
            if (filter.IdType is not null)
                query = query.Where(q => q.IdType == filter.IdType);

            if (filter.InheritedEntityId > 0)
                query = query.Where(q => q.InheritedEntityId == filter.InheritedEntityId);

            if (filter.AuthorId > 0)
                query = query.Where(q => q.AuthorId == filter.AuthorId);

            if (filter.ProjectId > 0)
                query = query.Where(q => q.ProjectId == filter.ProjectId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortEntityType.SingularName => query.OrderBy(a => a.SingularName),
                SortEntityType.PluralName => query.OrderBy(a => a.PluralName),
                SortEntityType.FolderName => query.OrderBy(a => a.FolderName),
                SortEntityType.InheritedEntityPluralName => query.OrderBy(a => a.InheritedEntity.PluralName),
                SortEntityType.InheritedEntityId => query.OrderBy(a => a.InheritedEntityId),
                SortEntityType.IdType => query.OrderBy(a => a.IdType),
                SortEntityType.ServiceName => query.OrderBy(a => a.ServiceName),
                SortEntityType.AuthorPhoneNumber => query.OrderBy(a => a.Author.PhoneNumber),
                SortEntityType.AuthorId => query.OrderBy(a => a.AuthorId),
                SortEntityType.ProjectTitle => query.OrderBy(a => a.Project.Title),
                SortEntityType.ProjectId => query.OrderBy(a => a.ProjectId),
                SortEntityType.IsExcluded => query.OrderBy(a => a.IsExcluded),
                SortEntityType.AddToMenu => query.OrderBy(a => a.AddToMenu),
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

        public async Task<FilterEntitiesDto> FilterEntities(FilterEntitiesDto filter)
        {
            IQueryable<Entity> query =
                GetEntitiesWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.Entities = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetEntitiesAsCombo(FilterEntitiesDto filter)
            => await GetEntitiesWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<EntityListDto?> GetSingleEntityInformation(int entityId)
            => await _entityRepository
            .GetQueryable()
            .Where(a => a.Id == entityId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<ChangeEntityResult> CreateEntity(CreateEntityDto create)
        {

            #region validate unique properties

            if (await _entityRepository
                .GetQueryable()
                .AnyAsync(a => a.SingularName == create.SingularName.ToTitle()! && a.ProjectId == create.ProjectId))
                return ChangeEntityResult.SingularNameExists;

            if (await _entityRepository
                .GetQueryable()
                .AnyAsync(a => a.PluralName == create.PluralName.ToTitle()! && a.ProjectId == create.ProjectId))
                return ChangeEntityResult.PluralNameExists;

            #endregion

            #region validate relation ids

            if (create.InheritedEntityId > 0)
                if (!await _entityRepository
                    .GetQueryable()
                    .AnyAsync(a => a.Id == create.InheritedEntityId))
                    return ChangeEntityResult.NotFound;

            if (create.AuthorId > 0)
                if (!await _userRepository
                    .GetQueryable()
                    .AnyAsync(a => a.Id == create.AuthorId))
                    return ChangeEntityResult.NotFound;

            if (!await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.ProjectId))
                return ChangeEntityResult.NotFound;

            #endregion

            Entity entity = create.ToModel();
            await _entityRepository.Add(entity);
            await _entityRepository.SaveChanges();

            #region add relations

            #region add properties

            if (create.PropertiesList is not null && create.PropertiesList.Any())
            {
                foreach (var property in create.PropertiesList)
                {
                    property.EntityId = entity.Id;
                    await CreateProperty(property);
                }

            }

            #endregion

            #region add entity selected project areas

            if (create.EntitySelectedProjectAreasList is not null && create.EntitySelectedProjectAreasList.Any())
            {
                foreach (var entitySelectedProjectArea in create.EntitySelectedProjectAreasList)
                {
                    entitySelectedProjectArea.EntityId = entity.Id;
                    await CreateEntitySelectedProjectArea(entitySelectedProjectArea);
                }

            }

            #endregion

            #region add entity selected languages

            if (create.EntitySelectedLanguagesList is not null && create.EntitySelectedLanguagesList.Any())
            {
                foreach (var entitySelectedLanguage in create.EntitySelectedLanguagesList)
                {
                    entitySelectedLanguage.EntityId = entity.Id;
                    await CreateEntitySelectedLanguage(entitySelectedLanguage);
                }

            }

            #endregion

            #endregion

            return ChangeEntityResult.Success;
        }
        public async Task<UpdateEntityDto?> GetEntityInformation(int entityId)
                => await _entityRepository
                    .GetQueryable()
                    .Where(a => a.Id == entityId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();

        public List<CreateEntityDto>? GetCreateEntitiesFromDb(ImportEntitiesFromDbDto import)
        {
            string connectionString = import.ConnectionString.GetConnectionString();
            var fastCreateEntities = new List<CreateEntityDto>();

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    return null;
                }
                // Query to retrieve table and column information
                string tableQuery = @"
        SELECT TABLE_NAME
        FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_TYPE = 'BASE TABLE'";

                string columnQuery = @"
        SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE DATA_TYPE != 'uniqueidentifier'";
                string relationshipQuery = @"
        SELECT
            FK.TABLE_NAME AS ReferencingTable,
            FK.COLUMN_NAME AS ReferencingColumn,
            PK.TABLE_NAME AS ReferencedTable
        FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC
        JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE FK
            ON RC.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
        JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE PK
            ON RC.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME";

                // Load tables
                var tables = new List<string>();
                using (var command = new SqlCommand(tableQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader["TABLE_NAME"].ToString());
                    }
                }

                // Load relationships
                var relationships = new Dictionary<string, string>(); // Key: ReferencingColumn, Value: ReferencedTable
                using (var command = new SqlCommand(relationshipQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var referencingColumn = reader["ReferencingColumn"].ToString();
                        var referencedTable = reader["ReferencedTable"].ToString();
                        relationships[referencingColumn] = referencedTable;
                    }
                }

                // Load columns and create entities
                foreach (var table in tables)
                {
                    var fastCreateEntity = new CreateEntityDto
                    {
                        PluralName = table,
                        SingularName = table.MakeSingular(),
                        ServiceName = $"{table.MakeSingular()}Service",
                        FolderName = table,
                        AddToMenu = true,
                        ProjectId = import.ProjectId,
                        PropertiesList = new List<CreatePropertyDto>(),
                        EntitySelectedProjectAreasList = import.DefaultAreasList
                    };

                    using (var command = new SqlCommand(columnQuery, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["TABLE_NAME"].ToString() == table)
                            {
                                var columnName = reader["COLUMN_NAME"].ToString();
                                if (import.ExcludePropertiesNamesList.Any(a => a == columnName)) continue;
                                fastCreateEntity.PropertiesList.Add(new CreatePropertyDto
                                {
                                    Name = columnName,
                                    DataType = reader["DATA_TYPE"].ToString().GetDataType(),
                                    IsRequired = reader["IS_NULLABLE"].ToString() == "NO",
                                    MaxLength = reader["CHARACTER_MAXIMUM_LENGTH"] as int? > 0 ? reader["CHARACTER_MAXIMUM_LENGTH"] as int? : 0,
                                    ShowInList = true,
                                    IsUpdatable = true,
                                    IsFilterContain = reader["DATA_TYPE"].ToString().GetDataType() == DataTypeEnum.String,
                                    EntityRelationsList = relationships.ContainsKey(columnName)
                                        ? new List<CreateEntityRelationDto>() {
                                            new CreateEntityRelationDto()
                                            {
                                                SecondaryEntityName = relationships[columnName],
                                                SecondaryEntityId = 1
                                            }
                                        }
                                        : new List<CreateEntityRelationDto>()
                                });
                            }
                        }
                    }

                    fastCreateEntities.Add(fastCreateEntity);
                }
            }

            return fastCreateEntities;
        }

        public async Task<List<ChangeEntityResult>> CreateEntities(List<CreateEntityDto> create)
        {
            List<ChangeEntityResult> results = new List<ChangeEntityResult>();
            if (!create.Any()) return results;
            foreach (var singleCreate in create)
            {
                results.Add(await CreateEntity(singleCreate));
            }
            List<CreateEntityRelationDto> createEntityRelations = create
                .SelectMany(a => a.PropertiesList
                .Where(a => a.EntityRelationsList.FirstOrDefault() != null &&
                            !string.IsNullOrEmpty(a.EntityRelationsList.FirstOrDefault()!.SecondaryEntityName))
                .Select(a => a.EntityRelationsList.FirstOrDefault()!))
                .ToList();
            await AddPropertyRelationsByName(createEntityRelations, create.First().ProjectId);
            return results;
        }

        public async Task<ChangeEntityResult> UpdateEntity(UpdateEntityDto update)
        {
            Entity? entity = await _entityRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (entity is null) return ChangeEntityResult.NotFound;

            #region validate unique properties

            if (await _entityRepository
                .GetQueryable()
                .AnyAsync(a => a.SingularName == update.SingularName.ToTitle()!
                               && a.Id != update.Id && a.ProjectId == update.ProjectId))
                return ChangeEntityResult.SingularNameExists;

            if (await _entityRepository
                .GetQueryable()
                .AnyAsync(a => a.PluralName == update.PluralName.ToTitle()!
                               && a.Id != update.Id && a.ProjectId == update.ProjectId))
                return ChangeEntityResult.PluralNameExists;

            #endregion

            #region validate relation ids

            if (update.InheritedEntityId > 0)
                if (!await _entityRepository
                    .GetQueryable()
                    .AnyAsync(a => a.Id == update.InheritedEntityId))
                    return ChangeEntityResult.NotFound;

            if (update.AuthorId > 0)
                if (!await _userRepository
                    .GetQueryable()
                    .AnyAsync(a => a.Id == update.AuthorId))
                    return ChangeEntityResult.NotFound;

            if (!await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.ProjectId))
                return ChangeEntityResult.NotFound;

            #endregion

            entity = entity.ToModel(update);
            _entityRepository.Update(entity);
            await _entityRepository.SaveChanges();

            #region update relations

            #region handle properties operations

            List<Property> currentProperties = await _propertyRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.EntityId == entity.Id)
                .ToListAsync();

            #region add new properties

            List<CreatePropertyDto> newProperties = update.PropertiesList
                                .Where(a => !currentProperties.Any(b => b.Id == a.Id))
                                .ToCreateDto()
                                .ToList();

            foreach (CreatePropertyDto property in newProperties)
            {
                property.EntityId = entity.Id;
                await CreateProperty(property);
            }

            #endregion

            #region update properties

            List<UpdatePropertyDto> updatedProperties =
                    update.PropertiesList
                    .Where(a => currentProperties.Any(b => b.Id == a.Id))
                    .ToList();

            foreach (UpdatePropertyDto property in updatedProperties)
            {
                await UpdateProperty(property);
            }

            #endregion

            #region delete properties

            List<int> removedPropertiesIds =
                    currentProperties
                    .Where(a => !update.PropertiesList.Any(b => b.Id == a.Id))
                    .Select(a => a.Id)
                    .ToList();

            await DeleteProperty(removedPropertiesIds);

            #endregion

            #endregion

            #region handle entity selected project areas operations

            List<EntitySelectedProjectArea> currentEntitySelectedProjectAreas = await _entitySelectedProjectAreaRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.EntityId == entity.Id)
                .ToListAsync();

            #region add new entity selected project areas

            List<CreateEntitySelectedProjectAreaDto> newEntitySelectedProjectAreas = update.EntitySelectedProjectAreasList
                                .Where(a => !currentEntitySelectedProjectAreas.Any(b => b.Id == a.Id))
                                .ToCreateDto()
                                .ToList();

            foreach (CreateEntitySelectedProjectAreaDto entitySelectedProjectArea in newEntitySelectedProjectAreas)
            {
                entitySelectedProjectArea.EntityId = entity.Id;
                await CreateEntitySelectedProjectArea(entitySelectedProjectArea);
            }

            #endregion

            #region update entity selected project areas

            List<UpdateEntitySelectedProjectAreaDto> updatedEntitySelectedProjectAreas =
                    update.EntitySelectedProjectAreasList
                    .Where(a => currentEntitySelectedProjectAreas.Any(b => b.Id == a.Id))
                    .ToList();

            foreach (UpdateEntitySelectedProjectAreaDto entitySelectedProjectArea in updatedEntitySelectedProjectAreas)
            {
                await UpdateEntitySelectedProjectArea(entitySelectedProjectArea);
            }

            #endregion

            #region delete entity selected project areas

            List<int> removedEntitySelectedProjectAreasIds =
                    currentEntitySelectedProjectAreas
                    .Where(a => !update.EntitySelectedProjectAreasList.Any(b => b.Id == a.Id))
                    .Select(a => a.Id)
                    .ToList();

            await DeleteEntitySelectedProjectArea(removedEntitySelectedProjectAreasIds);

            #endregion

            #endregion

            #region handle entity selected languages operations

            List<EntitySelectedLanguage> currentEntitySelectedLanguages = await _entitySelectedLanguageRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.EntityId == entity.Id)
                .ToListAsync();

            #region add new entity selected languages

            List<CreateEntitySelectedLanguageDto> newEntitySelectedLanguages = update.EntitySelectedLanguagesList
                                .Where(a => !currentEntitySelectedLanguages.Any(b => b.Id == a.Id))
                                .ToCreateDto()
                                .ToList();

            foreach (CreateEntitySelectedLanguageDto entitySelectedLanguage in newEntitySelectedLanguages)
            {
                entitySelectedLanguage.EntityId = entity.Id;
                await CreateEntitySelectedLanguage(entitySelectedLanguage);
            }

            #endregion

            #region update entity selected languages

            List<UpdateEntitySelectedLanguageDto> updatedEntitySelectedLanguages =
                    update.EntitySelectedLanguagesList
                    .Where(a => currentEntitySelectedLanguages.Any(b => b.Id == a.Id))
                    .ToList();

            foreach (UpdateEntitySelectedLanguageDto entitySelectedLanguage in updatedEntitySelectedLanguages)
            {
                await UpdateEntitySelectedLanguage(entitySelectedLanguage);
            }

            #endregion

            #region delete entity selected languages

            List<int> removedEntitySelectedLanguagesIds =
                    currentEntitySelectedLanguages
                    .Where(a => !update.EntitySelectedLanguagesList.Any(b => b.Id == a.Id))
                    .Select(a => a.Id)
                    .ToList();

            await DeleteEntitySelectedLanguage(removedEntitySelectedLanguagesIds);

            #endregion

            #endregion

            #endregion

            return ChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeleteEntity(int entityId)
        {
            Entity? entity = await _entityRepository.GetAsTracking(entityId);
            if (entity is null) return BaseChangeEntityResult.NotFound;

            _entityRepository.SoftDelete(entity);

            await _entityRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteEntity(List<int> entityIds)
        {
            foreach (int entityId in entityIds)
            {
                Entity? entity = await _entityRepository.GetAsTracking(entityId);
                if (entity is not null)
                    _entityRepository.SoftDelete(entity);
            }
            await _entityRepository.SaveChanges();
        }

        #endregion

        #endregion
    }
}
