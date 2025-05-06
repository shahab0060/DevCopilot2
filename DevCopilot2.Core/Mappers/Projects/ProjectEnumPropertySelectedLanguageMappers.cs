using System;
using DevCopilot2.Domain.Entities.Projects;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;

namespace DevCopilot2.Core.Mappers.Projects
{
    public static class ProjectEnumPropertySelectedLanguageMappers
    {
        #region to dto

        public static IQueryable<ProjectEnumPropertySelectedLanguageListDto>ToDto(this IQueryable<ProjectEnumPropertySelectedLanguage> query)
                    => query.Select(projectEnumPropertySelectedLanguage => new ProjectEnumPropertySelectedLanguageListDto()
                    {

                        Id = projectEnumPropertySelectedLanguage.Id,
                        LatestEditDate = projectEnumPropertySelectedLanguage.LatestEditDate,
                        CreateDate = projectEnumPropertySelectedLanguage.CreateDate,
                        EditCounts = projectEnumPropertySelectedLanguage.EditCounts,

                        LanguageName = projectEnumPropertySelectedLanguage.Language.Name,
                        LanguageId = projectEnumPropertySelectedLanguage.LanguageId,
                        ProjectEnumPropertyName = projectEnumPropertySelectedLanguage.ProjectEnumProperty.Name,
                        ProjectEnumPropertyId = projectEnumPropertySelectedLanguage.ProjectEnumPropertyId,
                        Title = projectEnumPropertySelectedLanguage.Title,

                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateProjectEnumPropertySelectedLanguageDto>ToUpdateDto(this IQueryable<ProjectEnumPropertySelectedLanguage> query)
                    => query.Select(projectEnumPropertySelectedLanguage => new UpdateProjectEnumPropertySelectedLanguageDto()
                    {

                        Id = projectEnumPropertySelectedLanguage.Id,

                        LanguageId = projectEnumPropertySelectedLanguage.LanguageId,
                        ProjectEnumPropertyId = projectEnumPropertySelectedLanguage.ProjectEnumPropertyId,
                        Title = projectEnumPropertySelectedLanguage.Title,

                    });

        #endregion

        #region to create dto

        public static List<CreateProjectEnumPropertySelectedLanguageDto>ToCreateDto(this IEnumerable<UpdateProjectEnumPropertySelectedLanguageDto> projectEnumPropertySelectedLanguages)
                    =>  projectEnumPropertySelectedLanguages.Select(projectEnumPropertySelectedLanguage => new CreateProjectEnumPropertySelectedLanguageDto()
                    {

                        LanguageId = projectEnumPropertySelectedLanguage.LanguageId,
                        ProjectEnumPropertyId = projectEnumPropertySelectedLanguage.ProjectEnumPropertyId,
                        Title = projectEnumPropertySelectedLanguage.Title,

                    }).ToList();

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<ProjectEnumPropertySelectedLanguage> query)
			    => query.Select(projectEnumPropertySelectedLanguage => new ComboDto()
			{
            Title = projectEnumPropertySelectedLanguage.Title,
            Value = projectEnumPropertySelectedLanguage.Id.ToString()
            });

        #endregion

        #region to create model

        public static ProjectEnumPropertySelectedLanguage ToModel(this CreateProjectEnumPropertySelectedLanguageDto create)
				=> new ProjectEnumPropertySelectedLanguage()
				{
                    LanguageId = create.LanguageId,
                    ProjectEnumPropertyId = create.ProjectEnumPropertyId,
                    Title = create.Title.ToTitle()!,
				};

        #endregion

        #region to update model

        public static ProjectEnumPropertySelectedLanguage ToModel(this ProjectEnumPropertySelectedLanguage projectEnumPropertySelectedLanguage, UpdateProjectEnumPropertySelectedLanguageDto update)
        {
            projectEnumPropertySelectedLanguage.LanguageId = update.LanguageId;
            projectEnumPropertySelectedLanguage.ProjectEnumPropertyId = update.ProjectEnumPropertyId;
            projectEnumPropertySelectedLanguage.Title = update.Title.ToTitle()!;
            return projectEnumPropertySelectedLanguage;
        }

        #endregion

    }
}
