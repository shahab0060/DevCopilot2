using System;
using DevCopilot2.Domain.Entities.Projects;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;

namespace DevCopilot2.Core.Mappers.Projects
{
    public static class ProjectEnumPropertyMappers
    {
        #region to dto

        public static IQueryable<ProjectEnumPropertyListDto>ToDto(this IQueryable<ProjectEnumProperty> query)
                    => query.Select(projectEnumProperty => new ProjectEnumPropertyListDto()
                    {

                        Id = projectEnumProperty.Id,
                        LatestEditDate = projectEnumProperty.LatestEditDate,
                        CreateDate = projectEnumProperty.CreateDate,
                        EditCounts = projectEnumProperty.EditCounts,

                        ProjectEnumEnglishName = projectEnumProperty.ProjectEnum.EnglishName,
                        ProjectEnumId = projectEnumProperty.ProjectEnumId,
                        Name = projectEnumProperty.Name,
                        Order = projectEnumProperty.Order,

                        ProjectEnumPropertySelectedLanguagesList = projectEnumProperty.ProjectEnumPropertySelectedLanguages
                        .Select(projectEnumPropertySelectedLanguage => new ProjectEnumPropertySelectedLanguageListDto()
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

                        })
                        .ToList(),
                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateProjectEnumPropertyDto>ToUpdateDto(this IQueryable<ProjectEnumProperty> query)
                    => query.Select(projectEnumProperty => new UpdateProjectEnumPropertyDto()
                    {

                        Id = projectEnumProperty.Id,

                        ProjectEnumId = projectEnumProperty.ProjectEnumId,
                        Name = projectEnumProperty.Name,
                        Order = projectEnumProperty.Order,

                        ProjectEnumPropertySelectedLanguagesList = projectEnumProperty.ProjectEnumPropertySelectedLanguages
                        .Select(projectEnumPropertySelectedLanguage => new UpdateProjectEnumPropertySelectedLanguageDto()
                        {

                        Id = projectEnumPropertySelectedLanguage.Id,

                        LanguageId = projectEnumPropertySelectedLanguage.LanguageId,
                        ProjectEnumPropertyId = projectEnumPropertySelectedLanguage.ProjectEnumPropertyId,
                        Title = projectEnumPropertySelectedLanguage.Title,

                        })
                        .ToList(),
                    });

        #endregion

        #region to create dto

        public static List<CreateProjectEnumPropertyDto>ToCreateDto(this IEnumerable<UpdateProjectEnumPropertyDto> projectEnumProperties)
                    =>  projectEnumProperties.Select(projectEnumProperty => new CreateProjectEnumPropertyDto()
                    {

                        ProjectEnumId = projectEnumProperty.ProjectEnumId,
                        Name = projectEnumProperty.Name,
                        Order = projectEnumProperty.Order,

                        ProjectEnumPropertySelectedLanguagesList = projectEnumProperty.ProjectEnumPropertySelectedLanguagesList
                        .Select(projectEnumPropertySelectedLanguage => new CreateProjectEnumPropertySelectedLanguageDto()
                        {

                        LanguageId = projectEnumPropertySelectedLanguage.LanguageId,
                        ProjectEnumPropertyId = projectEnumPropertySelectedLanguage.ProjectEnumPropertyId,
                        Title = projectEnumPropertySelectedLanguage.Title,

                        })
                        .ToList(),
                    }).ToList();

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<ProjectEnumProperty> query)
			    => query.Select(projectEnumProperty => new ComboDto()
			{
            Title = projectEnumProperty.Name,
            Value = projectEnumProperty.Id.ToString()
            });

        #endregion

        #region to create model

        public static ProjectEnumProperty ToModel(this CreateProjectEnumPropertyDto create)
				=> new ProjectEnumProperty()
				{
                    ProjectEnumId = create.ProjectEnumId,
                    Name = create.Name.ToTitle()!,
                    Order = create.Order,
				};

        #endregion

        #region to update model

        public static ProjectEnumProperty ToModel(this ProjectEnumProperty projectEnumProperty, UpdateProjectEnumPropertyDto update)
        {
            projectEnumProperty.ProjectEnumId = update.ProjectEnumId;
            projectEnumProperty.Name = update.Name.ToTitle()!;
            projectEnumProperty.Order = update.Order;
            return projectEnumProperty;
        }

        #endregion

    }
}
