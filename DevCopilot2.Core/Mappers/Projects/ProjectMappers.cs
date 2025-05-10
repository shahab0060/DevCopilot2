using System;
using DevCopilot2.Domain.Entities.Projects;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;
using DevCopilot2.Domain.Enums.Project;

namespace DevCopilot2.Core.Mappers.Projects
{
    public static class ProjectMappers
    {
        #region to dto

        public static IQueryable<ProjectListDto>ToDto(this IQueryable<Project> query)
                    => query.Select(project => new ProjectListDto()
                    {

                        Id = project.Id,
                        LatestEditDate = project.LatestEditDate,
                        CreateDate = project.CreateDate,
                        EditCounts = project.EditCounts,

                        EnglishName = project.EnglishName,
                        Title = project.Title,
                        Location = project.Location,
                        Architecture = project.Architecture,
                        AuthorPhoneNumber = project.Author.PhoneNumber,
                        AuthorId = project.AuthorId,
                        ReactProjectLocation = project.ReactProjectLocation,

                        ProjectAreasList = project.ProjectAreas
                        .Select(projectArea => new ProjectAreaListDto()
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
                        
                        })
                        .ToList(),
                        ProjectSelectedLanguagesList = project.ProjectSelectedLanguages
                        .Select(projectSelectedLanguage => new ProjectSelectedLanguageListDto()
                        {

                        Id = projectSelectedLanguage.Id,
                        LatestEditDate = projectSelectedLanguage.LatestEditDate,
                        CreateDate = projectSelectedLanguage.CreateDate,
                        EditCounts = projectSelectedLanguage.EditCounts,

                        ProjectTitle = projectSelectedLanguage.Project.Title,
                        ProjectId = projectSelectedLanguage.ProjectId,
                        LanguageName = projectSelectedLanguage.Language.Name,
                        LanguageId = projectSelectedLanguage.LanguageId,
                        LanguageCulture = projectSelectedLanguage.Language.Culture
                        })
                        .ToList(),
                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateProjectDto>ToUpdateDto(this IQueryable<Project> query)
                    => query.Select(project => new UpdateProjectDto()
                    {

                        Id = project.Id,

                        EnglishName = project.EnglishName,
                        Title = project.Title,
                        Location = project.Location,
                        Architecture = project.Architecture,
                        AuthorId = project.AuthorId,
                        ReactProjectLocation = project.ReactProjectLocation,

                        ProjectAreasList = project.ProjectAreas
                        .Select(projectArea => new UpdateProjectAreaDto()
                        {

                        Id = projectArea.Id,

                        ProjectId = projectArea.ProjectId,
                        EnglishName = projectArea.EnglishName,
                        Title = projectArea.Title,
                        TemplateId = projectArea.TemplateId,

                        })
                        .ToList(),
                        ProjectSelectedLanguagesList = project.ProjectSelectedLanguages
                        .Select(projectSelectedLanguage => new UpdateProjectSelectedLanguageDto()
                        {

                        Id = projectSelectedLanguage.Id,

                        ProjectId = projectSelectedLanguage.ProjectId,
                        LanguageId = projectSelectedLanguage.LanguageId,

                        })
                        .ToList(),
                    });

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<Project> query)
			    => query.Select(project => new ComboDto()
			{
            Title = project.EnglishName,
            Value = project.Id.ToString()
            });

        #endregion

        #region to create model

        public static Project ToModel(this CreateProjectDto create)
				=> new Project()
				{
                    EnglishName = create.EnglishName.ToTitle()!,
                    Title = create.Title.ToTitle()!,
                    Location = create.Location.SanitizeText()!,
                    Architecture = create.Architecture,
                    AuthorId = create.AuthorId,
                    ReactProjectLocation = create.ReactProjectLocation.SanitizeText()!,
				};

        #endregion

        #region to update model

        public static Project ToModel(this Project project, UpdateProjectDto update)
        {
            project.EnglishName = update.EnglishName.ToTitle()!;
            project.Title = update.Title.ToTitle()!;
            project.Location = update.Location.SanitizeText()!;
            project.Architecture = update.Architecture;
            project.AuthorId = update.AuthorId;
            project.ReactProjectLocation = update.ReactProjectLocation.SanitizeText()!;
            return project;
        }

        #endregion

    }
}
