using System;
using DevCopilot2.Domain.IRepository;
using DevCopilot2.Domain.DTOs.Paging;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Domain.Enums.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Enums.Projects;
using DevCopilot2.Core.Mappers.Projects;
using DevCopilot2.Domain.DTOs.Projects;

namespace DevCopilot2.Core.Services.Interfaces
{
    public interface IProjectService : IService
    {

        #region Projects

        #region project selected languages

		Task<FilterProjectSelectedLanguagesDto> FilterProjectSelectedLanguages(FilterProjectSelectedLanguagesDto filter);
        Task<List<ComboDto>> GetProjectSelectedLanguagesAsCombo(FilterProjectSelectedLanguagesDto filter);
		Task<ProjectSelectedLanguageListDto?> GetSingleProjectSelectedLanguageInformation(int projectSelectedLanguageId);

        		Task<BaseChangeEntityResult> CreateProjectSelectedLanguage(CreateProjectSelectedLanguageDto create);
		Task<UpdateProjectSelectedLanguageDto?> GetProjectSelectedLanguageInformation(int projectSelectedLanguageId);
		Task<BaseChangeEntityResult> UpdateProjectSelectedLanguage(UpdateProjectSelectedLanguageDto update);
		Task<BaseChangeEntityResult> DeleteProjectSelectedLanguage(int projectSelectedLanguageId);
		Task DeleteProjectSelectedLanguage(List<int> projectSelectedLanguagesId);

        #endregion

        #region project areas

		Task<FilterProjectAreasDto> FilterProjectAreas(FilterProjectAreasDto filter);
        Task<List<ComboDto>> GetProjectAreasAsCombo(FilterProjectAreasDto filter);
		Task<ProjectAreaListDto?> GetSingleProjectAreaInformation(int projectAreaId);

        		Task<ChangeProjectAreaResult> CreateProjectArea(CreateProjectAreaDto create);
		Task<UpdateProjectAreaDto?> GetProjectAreaInformation(int projectAreaId);
		Task<ChangeProjectAreaResult> UpdateProjectArea(UpdateProjectAreaDto update);
		Task<BaseChangeEntityResult> DeleteProjectArea(int projectAreaId);
		Task DeleteProjectArea(List<int> projectAreasId);

        #endregion

        #region project enum properties

		Task<FilterProjectEnumPropertiesDto> FilterProjectEnumProperties(FilterProjectEnumPropertiesDto filter);
        Task<List<ComboDto>> GetProjectEnumPropertiesAsCombo(FilterProjectEnumPropertiesDto filter);
		Task<ProjectEnumPropertyListDto?> GetSingleProjectEnumPropertyInformation(int projectEnumPropertyId);

        		Task<BaseChangeEntityResult> CreateProjectEnumProperty(CreateProjectEnumPropertyDto create);
		Task<UpdateProjectEnumPropertyDto?> GetProjectEnumPropertyInformation(int projectEnumPropertyId);
		Task<BaseChangeEntityResult> UpdateProjectEnumProperty(UpdateProjectEnumPropertyDto update);
		Task<BaseChangeEntityResult> DeleteProjectEnumProperty(int projectEnumPropertyId);
		Task DeleteProjectEnumProperty(List<int> projectEnumPropertiesId);

        #endregion

        #region project enums

		Task<FilterProjectEnumsDto> FilterProjectEnums(FilterProjectEnumsDto filter);
        Task<List<ComboDto>> GetProjectEnumsAsCombo(FilterProjectEnumsDto filter);
		Task<ProjectEnumListDto?> GetSingleProjectEnumInformation(int projectEnumId);

        		Task<BaseChangeEntityResult> CreateProjectEnum(CreateProjectEnumDto create);
		Task<UpdateProjectEnumDto?> GetProjectEnumInformation(int projectEnumId);
		Task<BaseChangeEntityResult> UpdateProjectEnum(UpdateProjectEnumDto update);
		Task<BaseChangeEntityResult> DeleteProjectEnum(int projectEnumId);
		Task DeleteProjectEnum(List<int> projectEnumsId);

        #endregion

        #region projects

		Task<FilterProjectsDto> FilterProjects(FilterProjectsDto filter);
        Task<List<ComboDto>> GetProjectsAsCombo(FilterProjectsDto filter);
		Task<ProjectListDto?> GetSingleProjectInformation(int projectId);

        		Task<ChangeProjectResult> CreateProject(CreateProjectDto create);
		Task<UpdateProjectDto?> GetProjectInformation(int projectId);
		Task<ChangeProjectResult> UpdateProject(UpdateProjectDto update);
		Task<BaseChangeEntityResult> DeleteProject(int projectId);
		Task DeleteProject(List<int> projectsId);

        #endregion

        #region project enum property selected languages

		Task<FilterProjectEnumPropertySelectedLanguagesDto> FilterProjectEnumPropertySelectedLanguages(FilterProjectEnumPropertySelectedLanguagesDto filter);
        Task<List<ComboDto>> GetProjectEnumPropertySelectedLanguagesAsCombo(FilterProjectEnumPropertySelectedLanguagesDto filter);
		Task<ProjectEnumPropertySelectedLanguageListDto?> GetSingleProjectEnumPropertySelectedLanguageInformation(int projectEnumPropertySelectedLanguageId);

        		Task<BaseChangeEntityResult> CreateProjectEnumPropertySelectedLanguage(CreateProjectEnumPropertySelectedLanguageDto create);
		Task<UpdateProjectEnumPropertySelectedLanguageDto?> GetProjectEnumPropertySelectedLanguageInformation(int projectEnumPropertySelectedLanguageId);
		Task<BaseChangeEntityResult> UpdateProjectEnumPropertySelectedLanguage(UpdateProjectEnumPropertySelectedLanguageDto update);
		Task<BaseChangeEntityResult> DeleteProjectEnumPropertySelectedLanguage(int projectEnumPropertySelectedLanguageId);
		Task DeleteProjectEnumPropertySelectedLanguage(List<int> projectEnumPropertySelectedLanguagesId);

        #endregion

        #endregion
    }
}
