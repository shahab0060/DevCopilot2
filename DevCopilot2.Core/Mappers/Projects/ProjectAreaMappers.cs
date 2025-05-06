using System;
using DevCopilot2.Domain.Entities.Projects;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;

namespace DevCopilot2.Core.Mappers.Projects
{
    public static class ProjectAreaMappers
    {
        #region to dto

        public static IQueryable<ProjectAreaListDto>ToDto(this IQueryable<ProjectArea> query)
                    => query.Select(projectArea => new ProjectAreaListDto()
                    {

                        Id = projectArea.Id,
                        LatestEditDate = projectArea.LatestEditDate,
                        CreateDate = projectArea.CreateDate,
                        EditCounts = projectArea.EditCounts,

                        ProjectTitle = projectArea.Project.Title,
                        ProjectId = projectArea.ProjectId,
                        EnglishName = projectArea.EnglishName,
                        Title = projectArea.Title,
                        TemplateTitle = projectArea.Template.Title,
                        TemplateId = projectArea.TemplateId,

                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateProjectAreaDto>ToUpdateDto(this IQueryable<ProjectArea> query)
                    => query.Select(projectArea => new UpdateProjectAreaDto()
                    {

                        Id = projectArea.Id,

                        ProjectId = projectArea.ProjectId,
                        EnglishName = projectArea.EnglishName,
                        Title = projectArea.Title,
                        TemplateId = projectArea.TemplateId,

                    });

        #endregion

        #region to create dto

        public static List<CreateProjectAreaDto>ToCreateDto(this IEnumerable<UpdateProjectAreaDto> projectAreas)
                    =>  projectAreas.Select(projectArea => new CreateProjectAreaDto()
                    {

                        ProjectId = projectArea.ProjectId,
                        EnglishName = projectArea.EnglishName,
                        Title = projectArea.Title,
                        TemplateId = projectArea.TemplateId,

                    }).ToList();

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<ProjectArea> query)
			    => query.Select(projectArea => new ComboDto()
			{
            Title = projectArea.Title,
            Value = projectArea.Id.ToString()
            });

        #endregion

        #region to create model

        public static ProjectArea ToModel(this CreateProjectAreaDto create)
				=> new ProjectArea()
				{
                    ProjectId = create.ProjectId,
                    EnglishName = create.EnglishName.SanitizeText()!,
                    Title = create.Title.ToTitle()!,
                    TemplateId = create.TemplateId,
				};

        #endregion

        #region to update model

        public static ProjectArea ToModel(this ProjectArea projectArea, UpdateProjectAreaDto update)
        {
            projectArea.ProjectId = update.ProjectId;
            projectArea.EnglishName = update.EnglishName.SanitizeText()!;
            projectArea.Title = update.Title.ToTitle()!;
            projectArea.TemplateId = update.TemplateId;
            return projectArea;
        }

        #endregion

    }
}
