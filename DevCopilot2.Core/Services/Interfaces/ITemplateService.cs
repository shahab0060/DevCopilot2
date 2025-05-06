using System;
using DevCopilot2.Domain.IRepository;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Enums.Templates;
using DevCopilot2.Core.Mappers.Templates;
using DevCopilot2.Domain.DTOs.Templates;

namespace DevCopilot2.Core.Services.Interfaces
{
    public interface ITemplateService : IService
    {

        #region Templates

        #region templates

		Task<FilterTemplatesDto> FilterTemplates(FilterTemplatesDto filter);
        Task<List<ComboDto>> GetTemplatesAsCombo(FilterTemplatesDto filter);
		Task<TemplateListDto?> GetSingleTemplateInformation(int templateId);

        		Task<BaseChangeEntityResult> CreateTemplate(CreateTemplateDto create);
		Task<UpdateTemplateDto?> GetTemplateInformation(int templateId);
		Task<BaseChangeEntityResult> UpdateTemplate(UpdateTemplateDto update);
		Task<BaseChangeEntityResult> DeleteTemplate(int templateId);
		Task DeleteTemplate(List<int> templatesId);

        #endregion

        #endregion
    }
}
