using System;
using DevCopilot2.Domain.Entities.Properties;
using DevCopilot2.Domain.DTOs.Properties;
using DevCopilot2.Domain.DTOs.Common;
using DevCopilot2.Core.Extensions.BasicExtensions;
using DevCopilot2.Core.Utils;
using DevCopilot2.Core.Security;

namespace DevCopilot2.Core.Mappers.Properties
{
    public static class PropertyImageResizeInformationMappers
    {
        #region to dto

        public static IQueryable<PropertyImageResizeInformationListDto>ToDto(this IQueryable<PropertyImageResizeInformation> query)
                    => query.Select(propertyImageResizeInformation => new PropertyImageResizeInformationListDto()
                    {

                        Id = propertyImageResizeInformation.Id,
                        LatestEditDate = propertyImageResizeInformation.LatestEditDate,
                        CreateDate = propertyImageResizeInformation.CreateDate,
                        EditCounts = propertyImageResizeInformation.EditCounts,

                        PropertyName = propertyImageResizeInformation.Property.Name,
                        PropertyId = propertyImageResizeInformation.PropertyId,
                        Name = propertyImageResizeInformation.Name,
                        Width = propertyImageResizeInformation.Width,
                        Height = propertyImageResizeInformation.Height,

                    });

        #endregion

        #region to update dto

        public static IQueryable<UpdatePropertyImageResizeInformationDto>ToUpdateDto(this IQueryable<PropertyImageResizeInformation> query)
                    => query.Select(propertyImageResizeInformation => new UpdatePropertyImageResizeInformationDto()
                    {

                        Id = propertyImageResizeInformation.Id,

                        PropertyId = propertyImageResizeInformation.PropertyId,
                        Name = propertyImageResizeInformation.Name,
                        Width = propertyImageResizeInformation.Width,
                        Height = propertyImageResizeInformation.Height,

                    });

        #endregion

        #region to create dto

        public static List<CreatePropertyImageResizeInformationDto>ToCreateDto(this IEnumerable<UpdatePropertyImageResizeInformationDto> propertyImageResizeInformation)
                    =>  propertyImageResizeInformation.Select(propertyImageResizeInformation => new CreatePropertyImageResizeInformationDto()
                    {

                        PropertyId = propertyImageResizeInformation.PropertyId,
                        Name = propertyImageResizeInformation.Name,
                        Width = propertyImageResizeInformation.Width,
                        Height = propertyImageResizeInformation.Height,

                    }).ToList();

        #endregion

        #region to combo

        public static IQueryable<ComboDto> ToCombo(this IQueryable<PropertyImageResizeInformation> query)
			    => query.Select(propertyImageResizeInformation => new ComboDto()
			{
            Title = propertyImageResizeInformation.Name,
            Value = propertyImageResizeInformation.Id.ToString()
            });

        #endregion

        #region to create model

        public static PropertyImageResizeInformation ToModel(this CreatePropertyImageResizeInformationDto create)
				=> new PropertyImageResizeInformation()
				{
                    PropertyId = create.PropertyId,
                    Name = create.Name.ToTitle()!,
                    Width = create.Width,
                    Height = create.Height,
				};

        #endregion

        #region to update model

        public static PropertyImageResizeInformation ToModel(this PropertyImageResizeInformation propertyImageResizeInformation, UpdatePropertyImageResizeInformationDto update)
        {
            propertyImageResizeInformation.PropertyId = update.PropertyId;
            propertyImageResizeInformation.Name = update.Name.ToTitle()!;
            propertyImageResizeInformation.Width = update.Width;
            propertyImageResizeInformation.Height = update.Height;
            return propertyImageResizeInformation;
        }

        #endregion

    }
}
