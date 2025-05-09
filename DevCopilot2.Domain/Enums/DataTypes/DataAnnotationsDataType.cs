using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.DataTypes;

namespace DevCopilot2.Domain.Enums.DataTypes
{
    public enum DataAnnotationsDataType
    {
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
            Name = nameof(DataAnnotationsDataTypeResources.Title))]
        Title,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
            Name = nameof(DataAnnotationsDataTypeResources.Text))]
        Text,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.Url))]
        Url,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.Email))]
        Email,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.Password))]
        Password,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.PhoneNumber))]
        PhoneNumber,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.NationalCode))]
        NationalCode,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.PersianDate))]
        PersianDate,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.Price))]
        Price,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.Color))]
        Color,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.Image))]
        Image,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.Video))]
        Video,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.File))]
        File,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.Others))]
        Others,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.List))]
        List,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.DtoList))]
        DtoList,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources),
                    Name = nameof(DataAnnotationsDataTypeResources.Hidden))]
        Hidden
    }
}
