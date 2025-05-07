using System;
using DevCopilot2.Domain.IRepository;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Security;
using DevCopilot2.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Enums.Projects;
using DevCopilot2.Core.Mappers.Projects;
using DevCopilot2.Domain.Entities.Projects;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.Entities.Languages;
using DevCopilot2.Domain.Entities.Templates;
using DevCopilot2.Domain.Entities.Users;

namespace DevCopilot2.Core.Services.Classes
{
    public class ProjectService : IProjectService
    {
        #region constructor

        private readonly ICrudRepository<Project, int> _projectRepository;
        private readonly ICrudRepository<Language, int> _languageRepository;
        private readonly ICrudRepository<Template, int> _templateRepository;
        private readonly ICrudRepository<ProjectEnum, int> _projectEnumRepository;
        private readonly ICrudRepository<ProjectEnumProperty, int> _projectEnumPropertyRepository;
        private readonly ICrudRepository<User, long> _userRepository;
        private readonly ICrudRepository<ProjectSelectedLanguage, int> _projectSelectedLanguageRepository;
        private readonly ICrudRepository<ProjectArea, int> _projectAreaRepository;
        private readonly ICrudRepository<ProjectEnumPropertySelectedLanguage, int> _projectEnumPropertySelectedLanguageRepository;
        public ProjectService(
                           ICrudRepository<Project, int> projectRepository,
                           ICrudRepository<Language, int> languageRepository,
                           ICrudRepository<Template, int> templateRepository,
                           ICrudRepository<ProjectEnum, int> projectEnumRepository,
                           ICrudRepository<ProjectEnumProperty, int> projectEnumPropertyRepository,
                           ICrudRepository<User, long> userRepository,
                           ICrudRepository<ProjectSelectedLanguage, int> projectSelectedLanguageRepository,
                           ICrudRepository<ProjectArea, int> projectAreaRepository,
                           ICrudRepository<ProjectEnumPropertySelectedLanguage, int> projectEnumPropertySelectedLanguageRepository
                                      )
        {
            this._projectRepository = projectRepository;
            this._languageRepository = languageRepository;
            this._templateRepository = templateRepository;
            this._projectEnumRepository = projectEnumRepository;
            this._projectEnumPropertyRepository = projectEnumPropertyRepository;
            this._userRepository = userRepository;
            this._projectSelectedLanguageRepository = projectSelectedLanguageRepository;
            this._projectAreaRepository = projectAreaRepository;
            this._projectEnumPropertySelectedLanguageRepository = projectEnumPropertySelectedLanguageRepository;
        }

        #endregion

        #region Projects

        #region project selected languages

        IQueryable<ProjectSelectedLanguage> GetProjectSelectedLanguagesWithFilterAndSort(FilterProjectSelectedLanguagesDto filter)
        {
            IQueryable<ProjectSelectedLanguage> query = _projectSelectedLanguageRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (filter.ProjectId > 0)
                query = query.Where(q => q.ProjectId == filter.ProjectId);

            if (filter.LanguageId > 0)
                query = query.Where(q => q.LanguageId == filter.LanguageId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortProjectSelectedLanguageType.ProjectTitle => query.OrderBy(a => a.Project.Title),
                SortProjectSelectedLanguageType.ProjectId => query.OrderBy(a => a.ProjectId),
                SortProjectSelectedLanguageType.LanguageName => query.OrderBy(a => a.Language.Name),
                SortProjectSelectedLanguageType.LanguageId => query.OrderBy(a => a.LanguageId),
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

        public async Task<FilterProjectSelectedLanguagesDto> FilterProjectSelectedLanguages(FilterProjectSelectedLanguagesDto filter)
        {
            IQueryable<ProjectSelectedLanguage> query =
                GetProjectSelectedLanguagesWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.ProjectSelectedLanguages = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetProjectSelectedLanguagesAsCombo(FilterProjectSelectedLanguagesDto filter)
            => await GetProjectSelectedLanguagesWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<ProjectSelectedLanguageListDto?> GetSingleProjectSelectedLanguageInformation(int projectSelectedLanguageId)
            => await _projectSelectedLanguageRepository
            .GetQueryable()
            .Where(a => a.Id == projectSelectedLanguageId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<BaseChangeEntityResult> CreateProjectSelectedLanguage(CreateProjectSelectedLanguageDto create)
        {

            #region validate relation ids

            if (!await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.ProjectId))
                return BaseChangeEntityResult.NotFound;

            if (!await _languageRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.LanguageId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            ProjectSelectedLanguage projectSelectedLanguage = create.ToModel();
            await _projectSelectedLanguageRepository.Add(projectSelectedLanguage);
            await _projectSelectedLanguageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdateProjectSelectedLanguageDto?> GetProjectSelectedLanguageInformation(int projectSelectedLanguageId)
                => await _projectSelectedLanguageRepository
                    .GetQueryable()
                    .Where(a => a.Id == projectSelectedLanguageId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();
        public async Task<BaseChangeEntityResult> UpdateProjectSelectedLanguage(UpdateProjectSelectedLanguageDto update)
        {
            ProjectSelectedLanguage? projectSelectedLanguage = await _projectSelectedLanguageRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (projectSelectedLanguage is null) return BaseChangeEntityResult.NotFound;

            #region validate relation ids

            if (!await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.ProjectId))
                return BaseChangeEntityResult.NotFound;

            if (!await _languageRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.LanguageId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            projectSelectedLanguage = projectSelectedLanguage.ToModel(update);
            _projectSelectedLanguageRepository.Update(projectSelectedLanguage);
            await _projectSelectedLanguageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeleteProjectSelectedLanguage(int projectSelectedLanguageId)
        {
            ProjectSelectedLanguage? projectSelectedLanguage = await _projectSelectedLanguageRepository.GetAsTracking(projectSelectedLanguageId);
            if (projectSelectedLanguage is null) return BaseChangeEntityResult.NotFound;

            _projectSelectedLanguageRepository.SoftDelete(projectSelectedLanguage);

            await _projectSelectedLanguageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteProjectSelectedLanguage(List<int> projectSelectedLanguageIds)
        {
            foreach (int projectSelectedLanguageId in projectSelectedLanguageIds)
            {
                ProjectSelectedLanguage? projectSelectedLanguage = await _projectSelectedLanguageRepository.GetAsTracking(projectSelectedLanguageId);
                if (projectSelectedLanguage is not null)
                    _projectSelectedLanguageRepository.SoftDelete(projectSelectedLanguage);
            }
            await _projectSelectedLanguageRepository.SaveChanges();
        }

        #endregion

        #region project areas

        IQueryable<ProjectArea> GetProjectAreasWithFilterAndSort(FilterProjectAreasDto filter)
        {
            IQueryable<ProjectArea> query = _projectAreaRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.EnglishName, $"%{filter.Search}%") ||

EF.Functions.Like(q.Title, $"%{filter.Search}%")
                                         );

            if (filter.ProjectId > 0)
                query = query.Where(q => q.ProjectId == filter.ProjectId);

            if (filter.TemplateId > 0)
                query = query.Where(q => q.TemplateId == filter.TemplateId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortProjectAreaType.ProjectTitle => query.OrderBy(a => a.Project.Title),
                SortProjectAreaType.ProjectId => query.OrderBy(a => a.ProjectId),
                SortProjectAreaType.EnglishName => query.OrderBy(a => a.EnglishName),
                SortProjectAreaType.Title => query.OrderBy(a => a.Title),
                SortProjectAreaType.TemplateTitle => query.OrderBy(a => a.Template.Title),
                SortProjectAreaType.TemplateId => query.OrderBy(a => a.TemplateId),
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

        public async Task<FilterProjectAreasDto> FilterProjectAreas(FilterProjectAreasDto filter)
        {
            IQueryable<ProjectArea> query =
                GetProjectAreasWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.ProjectAreas = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetProjectAreasAsCombo(FilterProjectAreasDto filter)
            => await GetProjectAreasWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<ProjectAreaListDto?> GetSingleProjectAreaInformation(int projectAreaId)
            => await _projectAreaRepository
            .GetQueryable()
            .Where(a => a.Id == projectAreaId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<ChangeProjectAreaResult> CreateProjectArea(CreateProjectAreaDto create)
        {

            #region validate unique properties

            if (await _projectAreaRepository
                .GetQueryable()
                .AnyAsync(a => a.EnglishName == create.EnglishName.SanitizeText()!))
                return ChangeProjectAreaResult.EnglishNameExists;

            if (await _projectAreaRepository
                .GetQueryable()
                .AnyAsync(a => a.Title == create.Title.ToTitle()!))
                return ChangeProjectAreaResult.TitleExists;

            #endregion

            #region validate relation ids

            if (!await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.ProjectId))
                return ChangeProjectAreaResult.NotFound;

            if (!await _templateRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.TemplateId))
                return ChangeProjectAreaResult.NotFound;

            #endregion

            ProjectArea projectArea = create.ToModel();
            await _projectAreaRepository.Add(projectArea);
            await _projectAreaRepository.SaveChanges();

            return ChangeProjectAreaResult.Success;
        }
        public async Task<UpdateProjectAreaDto?> GetProjectAreaInformation(int projectAreaId)
                => await _projectAreaRepository
                    .GetQueryable()
                    .Where(a => a.Id == projectAreaId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();
        public async Task<ChangeProjectAreaResult> UpdateProjectArea(UpdateProjectAreaDto update)
        {
            ProjectArea? projectArea = await _projectAreaRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (projectArea is null) return ChangeProjectAreaResult.NotFound;

            #region validate unique properties

            if (await _projectAreaRepository
                .GetQueryable()
                .AnyAsync(a => a.EnglishName == update.EnglishName.SanitizeText()!
                               && a.Id != update.Id))
                return ChangeProjectAreaResult.EnglishNameExists;

            if (await _projectAreaRepository
                .GetQueryable()
                .AnyAsync(a => a.Title == update.Title.ToTitle()!
                               && a.Id != update.Id))
                return ChangeProjectAreaResult.TitleExists;

            #endregion

            #region validate relation ids

            if (!await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.ProjectId))
                return ChangeProjectAreaResult.NotFound;

            if (!await _templateRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.TemplateId))
                return ChangeProjectAreaResult.NotFound;

            #endregion

            projectArea = projectArea.ToModel(update);
            _projectAreaRepository.Update(projectArea);
            await _projectAreaRepository.SaveChanges();

            return ChangeProjectAreaResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeleteProjectArea(int projectAreaId)
        {
            ProjectArea? projectArea = await _projectAreaRepository.GetAsTracking(projectAreaId);
            if (projectArea is null) return BaseChangeEntityResult.NotFound;

            _projectAreaRepository.SoftDelete(projectArea);

            await _projectAreaRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteProjectArea(List<int> projectAreaIds)
        {
            foreach (int projectAreaId in projectAreaIds)
            {
                ProjectArea? projectArea = await _projectAreaRepository.GetAsTracking(projectAreaId);
                if (projectArea is not null)
                    _projectAreaRepository.SoftDelete(projectArea);
            }
            await _projectAreaRepository.SaveChanges();
        }

        #endregion

        #region project enum properties

        IQueryable<ProjectEnumProperty> GetProjectEnumPropertiesWithFilterAndSort(FilterProjectEnumPropertiesDto filter)
        {
            IQueryable<ProjectEnumProperty> query = _projectEnumPropertyRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.Name, $"%{filter.Search}%")
                                         );

            if (filter.ProjectEnumId > 0)
                query = query.Where(q => q.ProjectEnumId == filter.ProjectEnumId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortProjectEnumPropertyType.ProjectEnumEnglishName => query.OrderBy(a => a.ProjectEnum.EnglishName),
                SortProjectEnumPropertyType.ProjectEnumId => query.OrderBy(a => a.ProjectEnumId),
                SortProjectEnumPropertyType.Name => query.OrderBy(a => a.Name),
                SortProjectEnumPropertyType.Order => query.OrderBy(a => a.Order),
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

        public async Task<FilterProjectEnumPropertiesDto> FilterProjectEnumProperties(FilterProjectEnumPropertiesDto filter)
        {
            IQueryable<ProjectEnumProperty> query =
                GetProjectEnumPropertiesWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.ProjectEnumProperties = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetProjectEnumPropertiesAsCombo(FilterProjectEnumPropertiesDto filter)
            => await GetProjectEnumPropertiesWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<ProjectEnumPropertyListDto?> GetSingleProjectEnumPropertyInformation(int projectEnumPropertyId)
            => await _projectEnumPropertyRepository
            .GetQueryable()
            .Where(a => a.Id == projectEnumPropertyId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<BaseChangeEntityResult> CreateProjectEnumProperty(CreateProjectEnumPropertyDto create)
        {

            #region validate relation ids

            if (!await _projectEnumRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.ProjectEnumId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            ProjectEnumProperty projectEnumProperty = create.ToModel();
            await _projectEnumPropertyRepository.Add(projectEnumProperty);
            await _projectEnumPropertyRepository.SaveChanges();

            #region add relations

            #region add project enum property selected languages

            if (create.ProjectEnumPropertySelectedLanguagesList is not null && create.ProjectEnumPropertySelectedLanguagesList.Any())
            {
                foreach (var projectEnumPropertySelectedLanguage in create.ProjectEnumPropertySelectedLanguagesList)
                {
                    projectEnumPropertySelectedLanguage.ProjectEnumPropertyId = projectEnumProperty.Id;
                    await CreateProjectEnumPropertySelectedLanguage(projectEnumPropertySelectedLanguage);
                }

            }

            #endregion

            #endregion

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdateProjectEnumPropertyDto?> GetProjectEnumPropertyInformation(int projectEnumPropertyId)
                => await _projectEnumPropertyRepository
                    .GetQueryable()
                    .Where(a => a.Id == projectEnumPropertyId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();
        public async Task<BaseChangeEntityResult> UpdateProjectEnumProperty(UpdateProjectEnumPropertyDto update)
        {
            ProjectEnumProperty? projectEnumProperty = await _projectEnumPropertyRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (projectEnumProperty is null) return BaseChangeEntityResult.NotFound;

            #region validate relation ids

            if (!await _projectEnumRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.ProjectEnumId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            projectEnumProperty = projectEnumProperty.ToModel(update);
            _projectEnumPropertyRepository.Update(projectEnumProperty);
            await _projectEnumPropertyRepository.SaveChanges();

            #region update relations

            #region handle project enum property selected languages operations

            List<ProjectEnumPropertySelectedLanguage> currentProjectEnumPropertySelectedLanguages = await _projectEnumPropertySelectedLanguageRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.ProjectEnumPropertyId == projectEnumProperty.Id)
                .ToListAsync();

            #region add new project enum property selected languages

            List<CreateProjectEnumPropertySelectedLanguageDto> newProjectEnumPropertySelectedLanguages = update.ProjectEnumPropertySelectedLanguagesList
                                .Where(a => !currentProjectEnumPropertySelectedLanguages.Any(b => b.Id == a.Id))
                                .ToCreateDto()
                                .ToList();

            foreach (CreateProjectEnumPropertySelectedLanguageDto projectEnumPropertySelectedLanguage in newProjectEnumPropertySelectedLanguages)
            {
                projectEnumPropertySelectedLanguage.ProjectEnumPropertyId = projectEnumProperty.Id;
                await CreateProjectEnumPropertySelectedLanguage(projectEnumPropertySelectedLanguage);
            }

            #endregion

            #region update project enum property selected languages

            List<UpdateProjectEnumPropertySelectedLanguageDto> updatedProjectEnumPropertySelectedLanguages =
                    update.ProjectEnumPropertySelectedLanguagesList
                    .Where(a => currentProjectEnumPropertySelectedLanguages.Any(b => b.Id == a.Id))
                    .ToList();

            foreach (UpdateProjectEnumPropertySelectedLanguageDto projectEnumPropertySelectedLanguage in updatedProjectEnumPropertySelectedLanguages)
            {
                await UpdateProjectEnumPropertySelectedLanguage(projectEnumPropertySelectedLanguage);
            }

            #endregion

            #region delete project enum property selected languages

            List<int> removedProjectEnumPropertySelectedLanguagesIds =
                    currentProjectEnumPropertySelectedLanguages
                    .Where(a => !update.ProjectEnumPropertySelectedLanguagesList.Any(b => b.Id == a.Id))
                    .Select(a => a.Id)
                    .ToList();

            await DeleteProjectEnumPropertySelectedLanguage(removedProjectEnumPropertySelectedLanguagesIds);

            #endregion

            #endregion

            #endregion

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeleteProjectEnumProperty(int projectEnumPropertyId)
        {
            ProjectEnumProperty? projectEnumProperty = await _projectEnumPropertyRepository.GetAsTracking(projectEnumPropertyId);
            if (projectEnumProperty is null) return BaseChangeEntityResult.NotFound;

            _projectEnumPropertyRepository.SoftDelete(projectEnumProperty);

            await _projectEnumPropertyRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteProjectEnumProperty(List<int> projectEnumPropertyIds)
        {
            foreach (int projectEnumPropertyId in projectEnumPropertyIds)
            {
                ProjectEnumProperty? projectEnumProperty = await _projectEnumPropertyRepository.GetAsTracking(projectEnumPropertyId);
                if (projectEnumProperty is not null)
                    _projectEnumPropertyRepository.SoftDelete(projectEnumProperty);
            }
            await _projectEnumPropertyRepository.SaveChanges();
        }

        #endregion

        #region project enums

        IQueryable<ProjectEnum> GetProjectEnumsWithFilterAndSort(FilterProjectEnumsDto filter)
        {
            IQueryable<ProjectEnum> query = _projectEnumRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.EnglishName, $"%{filter.Search}%") ||

EF.Functions.Like(q.FolderName, $"%{filter.Search}%")
                                         );
            if (filter.EnglishName is not null)
                query = query.Where(q => q.EnglishName == filter.EnglishName);

            if (filter.FolderName is not null)
                query = query.Where(q => q.FolderName == filter.FolderName);

            if (filter.ProjectId > 0)
                query = query.Where(q => q.ProjectId == filter.ProjectId || q.ProjectId == null);

            if (filter.AuthorId > 0)
                query = query.Where(q => q.AuthorId == filter.AuthorId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortProjectEnumType.ProjectTitle => query.OrderBy(a => a.Project.Title),
                SortProjectEnumType.ProjectId => query.OrderBy(a => a.ProjectId),
                SortProjectEnumType.EnglishName => query.OrderBy(a => a.EnglishName),
                SortProjectEnumType.FolderName => query.OrderBy(a => a.FolderName),
                SortProjectEnumType.AuthorPhoneNumber => query.OrderBy(a => a.Author.PhoneNumber),
                SortProjectEnumType.AuthorId => query.OrderBy(a => a.AuthorId),
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

        public async Task<FilterProjectEnumsDto> FilterProjectEnums(FilterProjectEnumsDto filter)
        {
            IQueryable<ProjectEnum> query =
                GetProjectEnumsWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.ProjectEnums = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetProjectEnumsAsCombo(FilterProjectEnumsDto filter)
            => await GetProjectEnumsWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<ProjectEnumListDto?> GetSingleProjectEnumInformation(int projectEnumId)
            => await _projectEnumRepository
            .GetQueryable()
            .Where(a => a.Id == projectEnumId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<BaseChangeEntityResult> CreateProjectEnum(CreateProjectEnumDto create)
        {

            #region validate unique properties

            if (await _projectEnumRepository
                .GetQueryable()
                .AnyAsync(a => a.EnglishName == create.EnglishName.ToTitle()!))
                return BaseChangeEntityResult.Exists;

            #endregion

            #region validate relation ids

            if (create.ProjectId > 0)
                if (!await _projectRepository
                    .GetQueryable()
                    .AnyAsync(a => a.Id == create.ProjectId))
                    return BaseChangeEntityResult.NotFound;

            if (!await _userRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.AuthorId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            ProjectEnum projectEnum = create.ToModel();
            await _projectEnumRepository.Add(projectEnum);
            await _projectEnumRepository.SaveChanges();

            #region add relations

            #region add project enum properties

            if (create.ProjectEnumPropertiesList is not null && create.ProjectEnumPropertiesList.Any())
            {
                foreach (var projectEnumProperty in create.ProjectEnumPropertiesList)
                {
                    projectEnumProperty.ProjectEnumId = projectEnum.Id;
                    await CreateProjectEnumProperty(projectEnumProperty);
                }

            }

            #endregion

            #endregion

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdateProjectEnumDto?> GetProjectEnumInformation(int projectEnumId)
                => await _projectEnumRepository
                    .GetQueryable()
                    .Where(a => a.Id == projectEnumId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();
        public async Task<BaseChangeEntityResult> UpdateProjectEnum(UpdateProjectEnumDto update)
        {
            ProjectEnum? projectEnum = await _projectEnumRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (projectEnum is null) return BaseChangeEntityResult.NotFound;

            #region validate unique properties

            if (await _projectEnumRepository
                .GetQueryable()
                .AnyAsync(a => a.EnglishName == update.EnglishName.ToTitle()!
                               && a.Id != update.Id))
                return BaseChangeEntityResult.Exists;

            #endregion

            #region validate relation ids

            if (update.ProjectId > 0)
                if (!await _projectRepository
                    .GetQueryable()
                    .AnyAsync(a => a.Id == update.ProjectId))
                    return BaseChangeEntityResult.NotFound;

            if (!await _userRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.AuthorId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            projectEnum = projectEnum.ToModel(update);
            _projectEnumRepository.Update(projectEnum);
            await _projectEnumRepository.SaveChanges();

            #region update relations

            #region handle project enum properties operations

            List<ProjectEnumProperty> currentProjectEnumProperties = await _projectEnumPropertyRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.ProjectEnumId == projectEnum.Id)
                .ToListAsync();

            #region add new project enum properties

            List<CreateProjectEnumPropertyDto> newProjectEnumProperties = update.ProjectEnumPropertiesList
                                .Where(a => !currentProjectEnumProperties.Any(b => b.Id == a.Id))
                                .ToCreateDto()
                                .ToList();

            foreach (CreateProjectEnumPropertyDto projectEnumProperty in newProjectEnumProperties)
            {
                projectEnumProperty.ProjectEnumId = projectEnum.Id;
                await CreateProjectEnumProperty(projectEnumProperty);
            }

            #endregion

            #region update project enum properties

            List<UpdateProjectEnumPropertyDto> updatedProjectEnumProperties =
                    update.ProjectEnumPropertiesList
                    .Where(a => currentProjectEnumProperties.Any(b => b.Id == a.Id))
                    .ToList();

            foreach (UpdateProjectEnumPropertyDto projectEnumProperty in updatedProjectEnumProperties)
            {
                await UpdateProjectEnumProperty(projectEnumProperty);
            }

            #endregion

            #region delete project enum properties

            List<int> removedProjectEnumPropertiesIds =
                    currentProjectEnumProperties
                    .Where(a => !update.ProjectEnumPropertiesList.Any(b => b.Id == a.Id))
                    .Select(a => a.Id)
                    .ToList();

            await DeleteProjectEnumProperty(removedProjectEnumPropertiesIds);

            #endregion

            #endregion

            #endregion

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeleteProjectEnum(int projectEnumId)
        {
            ProjectEnum? projectEnum = await _projectEnumRepository.GetAsTracking(projectEnumId);
            if (projectEnum is null) return BaseChangeEntityResult.NotFound;

            _projectEnumRepository.SoftDelete(projectEnum);

            await _projectEnumRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteProjectEnum(List<int> projectEnumIds)
        {
            foreach (int projectEnumId in projectEnumIds)
            {
                ProjectEnum? projectEnum = await _projectEnumRepository.GetAsTracking(projectEnumId);
                if (projectEnum is not null)
                    _projectEnumRepository.SoftDelete(projectEnum);
            }
            await _projectEnumRepository.SaveChanges();
        }

        #endregion

        #region projects

        IQueryable<Project> GetProjectsWithFilterAndSort(FilterProjectsDto filter)
        {
            IQueryable<Project> query = _projectRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.EnglishName, $"%{filter.Search}%") ||

EF.Functions.Like(q.Title, $"%{filter.Search}%") ||

EF.Functions.Like(q.Location, $"%{filter.Search}%") ||

EF.Functions.Like(q.ReactProjectLocation, $"%{filter.Search}%")
                                         );

            if (filter.AuthorId > 0)
                query = query.Where(q => q.AuthorId == filter.AuthorId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortProjectType.EnglishName => query.OrderBy(a => a.EnglishName),
                SortProjectType.Title => query.OrderBy(a => a.Title),
                SortProjectType.Location => query.OrderBy(a => a.Location),
                SortProjectType.Architecture => query.OrderBy(a => a.Architecture),
                SortProjectType.AuthorPhoneNumber => query.OrderBy(a => a.Author.PhoneNumber),
                SortProjectType.AuthorId => query.OrderBy(a => a.AuthorId),
                SortProjectType.ReactProjectLocation => query.OrderBy(a => a.ReactProjectLocation),
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

        public async Task<FilterProjectsDto> FilterProjects(FilterProjectsDto filter)
        {
            IQueryable<Project> query =
                GetProjectsWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.Projects = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetProjectsAsCombo(FilterProjectsDto filter)
            => await GetProjectsWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<ProjectListDto?> GetSingleProjectInformation(int projectId)
            => await _projectRepository
            .GetQueryable()
            .Where(a => a.Id == projectId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<ChangeProjectResult> CreateProject(CreateProjectDto create)
        {

            #region validate unique properties

            if (await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.EnglishName == create.EnglishName.ToTitle()!))
                return ChangeProjectResult.EnglishNameExists;

            if (await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.Title == create.Title.ToTitle()!))
                return ChangeProjectResult.TitleExists;

            if (await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.Location == create.Location.SanitizeText()!))
                return ChangeProjectResult.LocationExists;

            #endregion

            #region validate relation ids

            if (!await _userRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.AuthorId))
                return ChangeProjectResult.NotFound;

            #endregion

            Project project = create.ToModel();
            await _projectRepository.Add(project);
            await _projectRepository.SaveChanges();

            #region add relations

            #region add project areas

            if (create.ProjectAreasList is not null && create.ProjectAreasList.Any())
            {
                foreach (var projectArea in create.ProjectAreasList)
                {
                    projectArea.ProjectId = project.Id;
                    await CreateProjectArea(projectArea);
                }

            }

            #endregion

            #region add project selected languages

            if (create.ProjectSelectedLanguagesList is not null && create.ProjectSelectedLanguagesList.Any())
            {
                foreach (var projectSelectedLanguage in create.ProjectSelectedLanguagesList)
                {
                    projectSelectedLanguage.ProjectId = project.Id;
                    await CreateProjectSelectedLanguage(projectSelectedLanguage);
                }

            }

            #endregion

            #endregion

            return ChangeProjectResult.Success;
        }
        public async Task<UpdateProjectDto?> GetProjectInformation(int projectId)
                => await _projectRepository
                    .GetQueryable()
                    .Where(a => a.Id == projectId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();
        public async Task<ChangeProjectResult> UpdateProject(UpdateProjectDto update)
        {
            Project? project = await _projectRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (project is null) return ChangeProjectResult.NotFound;

            #region validate unique properties

            if (await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.EnglishName == update.EnglishName.ToTitle()!
                               && a.Id != update.Id))
                return ChangeProjectResult.EnglishNameExists;

            if (await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.Title == update.Title.ToTitle()!
                               && a.Id != update.Id))
                return ChangeProjectResult.TitleExists;

            if (await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.Location == update.Location.SanitizeText()!
                               && a.Id != update.Id))
                return ChangeProjectResult.LocationExists;

            #endregion

            #region validate relation ids

            if (!await _userRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.AuthorId))
                return ChangeProjectResult.NotFound;

            #endregion

            project = project.ToModel(update);
            _projectRepository.Update(project);
            await _projectRepository.SaveChanges();

            #region update relations

            #region handle project areas operations

            List<ProjectArea> currentProjectAreas = await _projectAreaRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.ProjectId == project.Id)
                .ToListAsync();

            #region add new project areas

            List<CreateProjectAreaDto> newProjectAreas = update.ProjectAreasList
                                .Where(a => !currentProjectAreas.Any(b => b.Id == a.Id))
                                .ToCreateDto()
                                .ToList();

            foreach (CreateProjectAreaDto projectArea in newProjectAreas)
            {
                projectArea.ProjectId = project.Id;
                await CreateProjectArea(projectArea);
            }

            #endregion

            #region update project areas

            List<UpdateProjectAreaDto> updatedProjectAreas =
                    update.ProjectAreasList
                    .Where(a => currentProjectAreas.Any(b => b.Id == a.Id))
                    .ToList();

            foreach (UpdateProjectAreaDto projectArea in updatedProjectAreas)
            {
                await UpdateProjectArea(projectArea);
            }

            #endregion

            #region delete project areas

            List<int> removedProjectAreasIds =
                    currentProjectAreas
                    .Where(a => !update.ProjectAreasList.Any(b => b.Id == a.Id))
                    .Select(a => a.Id)
                    .ToList();

            await DeleteProjectArea(removedProjectAreasIds);

            #endregion

            #endregion

            #region handle project selected languages operations

            List<ProjectSelectedLanguage> currentProjectSelectedLanguages = await _projectSelectedLanguageRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.ProjectId == project.Id)
                .ToListAsync();

            #region add new project selected languages

            List<CreateProjectSelectedLanguageDto> newProjectSelectedLanguages = update.ProjectSelectedLanguagesList
                                .Where(a => !currentProjectSelectedLanguages.Any(b => b.Id == a.Id))
                                .ToCreateDto()
                                .ToList();

            foreach (CreateProjectSelectedLanguageDto projectSelectedLanguage in newProjectSelectedLanguages)
            {
                projectSelectedLanguage.ProjectId = project.Id;
                await CreateProjectSelectedLanguage(projectSelectedLanguage);
            }

            #endregion

            #region update project selected languages

            List<UpdateProjectSelectedLanguageDto> updatedProjectSelectedLanguages =
                    update.ProjectSelectedLanguagesList
                    .Where(a => currentProjectSelectedLanguages.Any(b => b.Id == a.Id))
                    .ToList();

            foreach (UpdateProjectSelectedLanguageDto projectSelectedLanguage in updatedProjectSelectedLanguages)
            {
                await UpdateProjectSelectedLanguage(projectSelectedLanguage);
            }

            #endregion

            #region delete project selected languages

            List<int> removedProjectSelectedLanguagesIds =
                    currentProjectSelectedLanguages
                    .Where(a => !update.ProjectSelectedLanguagesList.Any(b => b.Id == a.Id))
                    .Select(a => a.Id)
                    .ToList();

            await DeleteProjectSelectedLanguage(removedProjectSelectedLanguagesIds);

            #endregion

            #endregion

            #endregion

            return ChangeProjectResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeleteProject(int projectId)
        {
            Project? project = await _projectRepository.GetAsTracking(projectId);
            if (project is null) return BaseChangeEntityResult.NotFound;

            _projectRepository.SoftDelete(project);

            await _projectRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteProject(List<int> projectIds)
        {
            foreach (int projectId in projectIds)
            {
                Project? project = await _projectRepository.GetAsTracking(projectId);
                if (project is not null)
                    _projectRepository.SoftDelete(project);
            }
            await _projectRepository.SaveChanges();
        }

        #endregion

        #region project enum property selected languages

        IQueryable<ProjectEnumPropertySelectedLanguage> GetProjectEnumPropertySelectedLanguagesWithFilterAndSort(FilterProjectEnumPropertySelectedLanguagesDto filter)
        {
            IQueryable<ProjectEnumPropertySelectedLanguage> query = _projectEnumPropertySelectedLanguageRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.Title, $"%{filter.Search}%")
                                         );

            if (filter.LanguageId > 0)
                query = query.Where(q => q.LanguageId == filter.LanguageId);

            if (filter.ProjectEnumPropertyId > 0)
                query = query.Where(q => q.ProjectEnumPropertyId == filter.ProjectEnumPropertyId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                SortProjectEnumPropertySelectedLanguageType.LanguageName => query.OrderBy(a => a.Language.Name),
                SortProjectEnumPropertySelectedLanguageType.LanguageId => query.OrderBy(a => a.LanguageId),
                SortProjectEnumPropertySelectedLanguageType.ProjectEnumPropertyName => query.OrderBy(a => a.ProjectEnumProperty.Name),
                SortProjectEnumPropertySelectedLanguageType.ProjectEnumPropertyId => query.OrderBy(a => a.ProjectEnumPropertyId),
                SortProjectEnumPropertySelectedLanguageType.Title => query.OrderBy(a => a.Title),
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

        public async Task<FilterProjectEnumPropertySelectedLanguagesDto> FilterProjectEnumPropertySelectedLanguages(FilterProjectEnumPropertySelectedLanguagesDto filter)
        {
            IQueryable<ProjectEnumPropertySelectedLanguage> query =
                GetProjectEnumPropertySelectedLanguagesWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId,
                await query.CountAsync(),
                filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            filter.ProjectEnumPropertySelectedLanguages = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetProjectEnumPropertySelectedLanguagesAsCombo(FilterProjectEnumPropertySelectedLanguagesDto filter)
            => await GetProjectEnumPropertySelectedLanguagesWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<ProjectEnumPropertySelectedLanguageListDto?> GetSingleProjectEnumPropertySelectedLanguageInformation(int projectEnumPropertySelectedLanguageId)
            => await _projectEnumPropertySelectedLanguageRepository
            .GetQueryable()
            .Where(a => a.Id == projectEnumPropertySelectedLanguageId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<BaseChangeEntityResult> CreateProjectEnumPropertySelectedLanguage(CreateProjectEnumPropertySelectedLanguageDto create)
        {

            #region validate relation ids

            if (!await _languageRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.LanguageId))
                return BaseChangeEntityResult.NotFound;

            if (!await _projectEnumPropertyRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.ProjectEnumPropertyId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            ProjectEnumPropertySelectedLanguage projectEnumPropertySelectedLanguage = create.ToModel();
            await _projectEnumPropertySelectedLanguageRepository.Add(projectEnumPropertySelectedLanguage);
            await _projectEnumPropertySelectedLanguageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<UpdateProjectEnumPropertySelectedLanguageDto?> GetProjectEnumPropertySelectedLanguageInformation(int projectEnumPropertySelectedLanguageId)
                => await _projectEnumPropertySelectedLanguageRepository
                    .GetQueryable()
                    .Where(a => a.Id == projectEnumPropertySelectedLanguageId)
                    .ToUpdateDto()
                    .FirstOrDefaultAsync();
        public async Task<BaseChangeEntityResult> UpdateProjectEnumPropertySelectedLanguage(UpdateProjectEnumPropertySelectedLanguageDto update)
        {
            ProjectEnumPropertySelectedLanguage? projectEnumPropertySelectedLanguage = await _projectEnumPropertySelectedLanguageRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (projectEnumPropertySelectedLanguage is null) return BaseChangeEntityResult.NotFound;

            #region validate relation ids

            if (!await _languageRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.LanguageId))
                return BaseChangeEntityResult.NotFound;

            if (!await _projectEnumPropertyRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.ProjectEnumPropertyId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            projectEnumPropertySelectedLanguage = projectEnumPropertySelectedLanguage.ToModel(update);
            _projectEnumPropertySelectedLanguageRepository.Update(projectEnumPropertySelectedLanguage);
            await _projectEnumPropertySelectedLanguageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task<BaseChangeEntityResult> DeleteProjectEnumPropertySelectedLanguage(int projectEnumPropertySelectedLanguageId)
        {
            ProjectEnumPropertySelectedLanguage? projectEnumPropertySelectedLanguage = await _projectEnumPropertySelectedLanguageRepository.GetAsTracking(projectEnumPropertySelectedLanguageId);
            if (projectEnumPropertySelectedLanguage is null) return BaseChangeEntityResult.NotFound;

            _projectEnumPropertySelectedLanguageRepository.SoftDelete(projectEnumPropertySelectedLanguage);

            await _projectEnumPropertySelectedLanguageRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
        public async Task DeleteProjectEnumPropertySelectedLanguage(List<int> projectEnumPropertySelectedLanguageIds)
        {
            foreach (int projectEnumPropertySelectedLanguageId in projectEnumPropertySelectedLanguageIds)
            {
                ProjectEnumPropertySelectedLanguage? projectEnumPropertySelectedLanguage = await _projectEnumPropertySelectedLanguageRepository.GetAsTracking(projectEnumPropertySelectedLanguageId);
                if (projectEnumPropertySelectedLanguage is not null)
                    _projectEnumPropertySelectedLanguageRepository.SoftDelete(projectEnumPropertySelectedLanguage);
            }
            await _projectEnumPropertySelectedLanguageRepository.SaveChanges();
        }

        #endregion

        #endregion
    }
}
