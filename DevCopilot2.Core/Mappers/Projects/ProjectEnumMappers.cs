using System;
using DevCopilot2.Domain.Entities.Projects;
using DevCopilot2.Domain.DTOs.Projects;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;

namespace DevCopilot2.Core.Mappers.Projects
{
    public static class ProjectEnumMappers
    {
        #region to dto

        public static IQueryable<ProjectEnumListDto>ToDto(this IQueryable<ProjectEnum> query)
                    => query.Select(projectEnum => new ProjectEnumListDto()
                    {

                        Id = projectEnum.Id,
                        LatestEditDate = projectEnum.LatestEditDate,
                        CreateDate = projectEnum.CreateDate,
                        EditCounts = projectEnum.EditCounts,

                        ProjectTitle = projectEnum.Project.Title,
                        ProjectId = projectEnum.ProjectId,
                        EnglishName = projectEnum.EnglishName,
                        FolderName = projectEnum.FolderName,
                        AuthorPhoneNumber = projectEnum.Author.PhoneNumber,
                        AuthorId = projectEnum.AuthorId,

                        ProjectEnumPropertiesList = projectEnum.ProjectEnumProperties
                        .Select(projectEnumProperty => new ProjectEnumPropertyListDto()
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
                        })
                        .ToList(),
                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdateProjectEnumDto>ToUpdateDto(this IQueryable<ProjectEnum> query)
                    => query.Select(projectEnum => new UpdateProjectEnumDto()
                    {

                        Id = projectEnum.Id,

                        ProjectId = projectEnum.ProjectId,
                        EnglishName = projectEnum.EnglishName,
                        FolderName = projectEnum.FolderName,
                        AuthorId = projectEnum.AuthorId,

                        ProjectEnumPropertiesList = projectEnum.ProjectEnumProperties
                        .Select(projectEnumProperty => new UpdateProjectEnumPropertyDto()
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
                        })
                        .ToList(),
                    });

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<ProjectEnum> query)
			    => query.Select(projectEnum => new ComboDto()
			{
            Title = projectEnum.EnglishName,
            Value = projectEnum.Id.ToString()
            });

        #endregion

        #region to create model

        public static ProjectEnum ToModel(this CreateProjectEnumDto create)
				=> new ProjectEnum()
				{
                    ProjectId = create.ProjectId > 0 ? create.ProjectId: null,
                    EnglishName = create.EnglishName.ToTitle()!,
                    FolderName = create.FolderName.ToTitle()!,
                    AuthorId = create.AuthorId,
				};

        #endregion

        #region to update model

        public static ProjectEnum ToModel(this ProjectEnum projectEnum, UpdateProjectEnumDto update)
        {
            projectEnum.ProjectId = update.ProjectId > 0 ? update.ProjectId: null;
            projectEnum.EnglishName = update.EnglishName.ToTitle()!;
            projectEnum.FolderName = update.FolderName.ToTitle()!;
            projectEnum.AuthorId = update.AuthorId;
            return projectEnum;
        }

        #endregion

    }
}
