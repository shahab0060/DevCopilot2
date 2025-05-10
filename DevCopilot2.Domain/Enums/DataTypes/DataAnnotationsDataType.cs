using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.DataTypes;

namespace DevCopilot2.Domain.Enums.DataTypes
{
    public enum DataAnnotationsDataType
    {
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.Title))]
        Title = 0,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.Text))]
        Text = 1,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.Url))]
        Url = 2,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.Email))]
        Email = 3,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.Password))]
        Password = 4,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.PhoneNumber))]
        PhoneNumber = 5,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.NationalCode))]
        NationalCode = 6,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.PersianDate))]
        PersianDate = 7,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.Price))]
        Price = 8,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.Color))]
        Color = 9,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.Image))]
        Image = 10,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.Video))]
        Video = 11,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.File))]
        File = 12,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.Others))]
        Others = 13,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.List))]
        List = 14,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.DtoList))]
        DtoList = 15,
        [Display(ResourceType = typeof(DataAnnotationsDataTypeResources), Name = nameof(DataAnnotationsDataTypeResources.Hidden))]
        Hidden = 16,
    }
}
