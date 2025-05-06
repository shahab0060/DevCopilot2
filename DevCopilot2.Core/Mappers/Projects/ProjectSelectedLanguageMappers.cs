using System;
using DevCopilot2.Domain.Entities.Projects;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;

namespace DevCopilot2.Core.Mappers.Projects
{
    public static class ProjectSelectedLanguageMappers
    {
        #region to dto

        public static IQueryable<ProjectSelectedLanguageListDto>ToDto(this IQueryable<ProjectSelectedLanguage> query)
                    => query.Select(projectSelectedLanguage => new ProjectSelectedLanguageListDto()
                    {

                        Id = projectSelectedLanguage.Id,
                        LatestEditDate = projectSelectedLanguage.LatestEditDate,
                        CreateDate = projectSelectedLanguage.CreateDate,
                        EditCounts = projectSelectedLanguage.EditCounts,

                        ProjectTitle = projectSelectedLanguage.Project.Title,
                        ProjectId = projectSelectedLanguage.ProjectId,
                        LanguageName = projectSelectedLanguage.Language.Name,
                        LanguageId = projectSelectedLanguage.LanguageId,

                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateProjectSelectedLanguageDto>ToUpdateDto(this IQueryable<ProjectSelectedLanguage> query)
                    => query.Select(projectSelectedLanguage => new UpdateProjectSelectedLanguageDto()
                    {

                        Id = projectSelectedLanguage.Id,

                        ProjectId = projectSelectedLanguage.ProjectId,
                        LanguageId = projectSelectedLanguage.LanguageId,

                    });

        #endregion

        #region to create dto

        public static List<CreateProjectSelectedLanguageDto>ToCreateDto(this IEnumerable<UpdateProjectSelectedLanguageDto> projectSelectedLanguages)
                    =>  projectSelectedLanguages.Select(projectSelectedLanguage => new CreateProjectSelectedLanguageDto()
                    {

                        ProjectId = projectSelectedLanguage.ProjectId,
                        LanguageId = projectSelectedLanguage.LanguageId,

                    }).ToList();

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<ProjectSelectedLanguage> query)
			    => query.Select(projectSelectedLanguage => new ComboDto()
			{
            Title = projectSelectedLanguage.ProjectId.ToString(),
            Value = projectSelectedLanguage.Id.ToString()
            });

        #endregion

        #region to create model

        public static ProjectSelectedLanguage ToModel(this CreateProjectSelectedLanguageDto create)
				=> new ProjectSelectedLanguage()
				{
                    ProjectId = create.ProjectId,
                    LanguageId = create.LanguageId,
				};

        #endregion

        #region to update model

        public static ProjectSelectedLanguage ToModel(this ProjectSelectedLanguage projectSelectedLanguage, UpdateProjectSelectedLanguageDto update)
        {
            projectSelectedLanguage.ProjectId = update.ProjectId;
            projectSelectedLanguage.LanguageId = update.LanguageId;
            return projectSelectedLanguage;
        }

        #endregion

    }
}
