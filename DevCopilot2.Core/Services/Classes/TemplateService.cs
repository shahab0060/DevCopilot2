using System;
using DevCopilot2.Domain.IRepository;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Security;
using DevCopilot2.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Enums.Templates;
using DevCopilot2.Core.Mappers.Templates;
using DevCopilot2.Domain.Entities.Templates;
using DevCopilot2.Domain.DTOs.Templates;
using DevCopilot2.Domain.Entities.Projects;
using DevCopilot2.Domain.Entities.Users;

namespace DevCopilot2.Core.Services.Classes
{
    public class TemplateService : ITemplateService
    {
        #region constructor

        private readonly ICrudRepository<Project, int> _projectRepository;
        private readonly ICrudRepository<User, long> _userRepository;
        private readonly ICrudRepository<Template, int> _templateRepository;
        public TemplateService(
                           ICrudRepository<Project, int> projectRepository,
                           ICrudRepository<User, long> userRepository,
                           ICrudRepository<Template, int> templateRepository 
                                      )
        {
            this._projectRepository = projectRepository;
            this._userRepository = userRepository;
            this._templateRepository = templateRepository;
        }

        #endregion

        #region Templates

        #region templates

        IQueryable<Template> GetTemplatesWithFilterAndSort(FilterTemplatesDto filter)
        {
            IQueryable<Template> query = _templateRepository.GetQueryable();

            #region filter

            if (filter.FromDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate >= filter.FromDate!.ToMiladiDateTime());
            if (filter.ToDate!.ToMiladiDateTime() != null)
                query = query.Where(q => q.CreateDate <= filter.ToDate!.ToMiladiDateTime());

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(q => EF.Functions.Like(q.Title, $"%{filter.Search}%") ||

EF.Functions.Like(q.ListViewHtml, $"%{filter.Search}%") ||

EF.Functions.Like(q.ListFirstThCode, $"%{filter.Search}%") ||

EF.Functions.Like(q.ListOtherThCodes, $"%{filter.Search}%") ||

EF.Functions.Like(q.ListBoolTdHtml, $"%{filter.Search}%") ||

EF.Functions.Like(q.ListTextTdHtml, $"%{filter.Search}%") ||

EF.Functions.Like(q.ListImageTdHtml, $"%{filter.Search}%") ||

EF.Functions.Like(q.ListPriceTdHtml, $"%{filter.Search}%") ||

EF.Functions.Like(q.ListDefaultTdCode, $"%{filter.Search}%") ||

EF.Functions.Like(q.ListViewCardHtml, $"%{filter.Search}%") ||

EF.Functions.Like(q.CreatePageHtml, $"%{filter.Search}%") ||

EF.Functions.Like(q.CheckBoxInputCode, $"%{filter.Search}%") ||

EF.Functions.Like(q.FileInputCode, $"%{filter.Search}%") ||

EF.Functions.Like(q.TextInputHtml, $"%{filter.Search}%") ||

EF.Functions.Like(q.TextEditorInputHtml, $"%{filter.Search}%") ||

EF.Functions.Like(q.IntegerInputHtml, $"%{filter.Search}%") ||

EF.Functions.Like(q.SelectInputHtml, $"%{filter.Search}%") ||

EF.Functions.Like(q.SingleImageHtml, $"%{filter.Search}%") ||

EF.Functions.Like(q.ColorPickerInputCode, $"%{filter.Search}%") ||

EF.Functions.Like(q.BreadCrumbCode, $"%{filter.Search}%") ||

EF.Functions.Like(q.AnchorTagCode, $"%{filter.Search}%") ||

EF.Functions.Like(q.SubmitBtnCode, $"%{filter.Search}%")   
                                         );

                        if (filter.ProjectId > 0)
                query = query.Where(q => q.ProjectId == filter.ProjectId);

            if (filter.AuthorId > 0)
                query = query.Where(q => q.AuthorId == filter.AuthorId);

            #endregion

            #region advance sort

            query = filter.SortProperty switch
            {
                                SortTemplateType.Title => query.OrderBy(a => a.Title),
                SortTemplateType.ProjectTitle => query.OrderBy(a => a.Project.Title),
                SortTemplateType.ProjectId => query.OrderBy(a => a.ProjectId),
                SortTemplateType.ListViewHtml => query.OrderBy(a => a.ListViewHtml),
                SortTemplateType.ListFirstThCode => query.OrderBy(a => a.ListFirstThCode),
                SortTemplateType.ListOtherThCodes => query.OrderBy(a => a.ListOtherThCodes),
                SortTemplateType.ListBoolTdHtml => query.OrderBy(a => a.ListBoolTdHtml),
                SortTemplateType.ListTextTdHtml => query.OrderBy(a => a.ListTextTdHtml),
                SortTemplateType.ListImageTdHtml => query.OrderBy(a => a.ListImageTdHtml),
                SortTemplateType.ListPriceTdHtml => query.OrderBy(a => a.ListPriceTdHtml),
                SortTemplateType.ListDefaultTdCode => query.OrderBy(a => a.ListDefaultTdCode),
                SortTemplateType.ListViewCardHtml => query.OrderBy(a => a.ListViewCardHtml),
                SortTemplateType.CreatePageHtml => query.OrderBy(a => a.CreatePageHtml),
                SortTemplateType.CheckBoxInputCode => query.OrderBy(a => a.CheckBoxInputCode),
                SortTemplateType.FileInputCode => query.OrderBy(a => a.FileInputCode),
                SortTemplateType.TextInputHtml => query.OrderBy(a => a.TextInputHtml),
                SortTemplateType.TextEditorInputHtml => query.OrderBy(a => a.TextEditorInputHtml),
                SortTemplateType.IntegerInputHtml => query.OrderBy(a => a.IntegerInputHtml),
                SortTemplateType.SelectInputHtml => query.OrderBy(a => a.SelectInputHtml),
                SortTemplateType.AuthorPhoneNumber => query.OrderBy(a => a.Author.PhoneNumber),
                SortTemplateType.AuthorId => query.OrderBy(a => a.AuthorId),
                SortTemplateType.SingleImageHtml => query.OrderBy(a => a.SingleImageHtml),
                SortTemplateType.ColorPickerInputCode => query.OrderBy(a => a.ColorPickerInputCode),
                SortTemplateType.BreadCrumbCode => query.OrderBy(a => a.BreadCrumbCode),
                SortTemplateType.AnchorTagCode => query.OrderBy(a => a.AnchorTagCode),
                SortTemplateType.SubmitBtnCode => query.OrderBy(a => a.SubmitBtnCode),
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

        public async Task<FilterTemplatesDto> FilterTemplates(FilterTemplatesDto filter)
        {
            IQueryable<Template> query = 
                GetTemplatesWithFilterAndSort(filter);

            var pager = Pager.Build(
                filter.PageId, 
                await query.CountAsync(),
                filter.TakeEntity, 
                filter.HowManyShowPageAfterAndBefore);

            filter.Templates = await query
                .Paging(pager)
                .ToDto()
                .ToListAsync();

            return filter.SetPaging(pager);
        }

        public async Task<List<ComboDto>> GetTemplatesAsCombo(FilterTemplatesDto filter)
            => await GetTemplatesWithFilterAndSort(filter)
            .ToCombo()
            .ToListAsync();

        public async Task<TemplateListDto?> GetSingleTemplateInformation(int templateId)
            => await _templateRepository
            .GetQueryable()
            .Where(a => a.Id == templateId)
            .ToDto()
            .FirstOrDefaultAsync();

        public async Task<BaseChangeEntityResult> CreateTemplate(CreateTemplateDto create)
        { 

            #region validate unique properties

            if (await _templateRepository
                .GetQueryable()
                .AnyAsync(a => a.Title == create.Title.ToTitle()!))
                return BaseChangeEntityResult.Exists;

            #endregion

            #region validate relation ids

            if(create.ProjectId>0)
            if (!await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.ProjectId))
                return BaseChangeEntityResult.NotFound;

            if (!await _userRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == create.AuthorId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            Template template =  create.ToModel();
            await _templateRepository.Add(template);
            await _templateRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
public async Task<UpdateTemplateDto?> GetTemplateInformation(int templateId)
        =>  await _templateRepository
            .GetQueryable()
            .Where(a => a.Id == templateId)
            .ToUpdateDto()
            .FirstOrDefaultAsync();
public async Task<BaseChangeEntityResult> UpdateTemplate(UpdateTemplateDto update)
        { 
            Template? template = await _templateRepository
                .GetQueryable()
                .Where(a => a.Id == update.Id)
                .FirstOrDefaultAsync();
            if (template is null) return BaseChangeEntityResult.NotFound;

            #region validate unique properties

            if (await _templateRepository
                .GetQueryable()
                .AnyAsync(a => a.Title == update.Title.ToTitle()!
                               && a.Id!=update.Id))
                return BaseChangeEntityResult.Exists;

            #endregion

            #region validate relation ids

            if(update.ProjectId>0)
            if (!await _projectRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.ProjectId))
                return BaseChangeEntityResult.NotFound;

            if (!await _userRepository
                .GetQueryable()
                .AnyAsync(a => a.Id == update.AuthorId))
                return BaseChangeEntityResult.NotFound;

            #endregion

            template =  template.ToModel(update);
            _templateRepository.Update(template);
            await _templateRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
public async Task<BaseChangeEntityResult> DeleteTemplate(int templateId)
        {
            Template? template = await _templateRepository.GetAsTracking(templateId);
            if (template is null) return BaseChangeEntityResult.NotFound;

            _templateRepository.SoftDelete(template);

            await _templateRepository.SaveChanges();

            return BaseChangeEntityResult.Success;
        }
public async Task DeleteTemplate(List<int> templateIds)
        {
            foreach (int templateId in templateIds)
            { 
                Template? template = await _templateRepository.GetAsTracking(templateId);
                if (template is not null)
                    _templateRepository.SoftDelete(template);
            }
            await _templateRepository.SaveChanges();
        }

        #endregion

        #endregion
    }
}
