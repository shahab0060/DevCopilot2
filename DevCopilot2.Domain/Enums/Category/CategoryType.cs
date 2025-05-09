using System;
using System.ComponentModel.DataAnnotations;
using DevCopilot2.Domain.Resources.Enums.Category;

namespace DevCopilot2.Domain.Enums.Category
{
    public enum CategoryType
    {
        [Display(ResourceType = typeof(CategoryTypeResources), Name = nameof(CategoryTypeResources.Product))]
        Product = 0,
        [Display(ResourceType = typeof(CategoryTypeResources), Name = nameof(CategoryTypeResources.Article))]
        Article = 1,
        [Display(ResourceType = typeof(CategoryTypeResources), Name = nameof(CategoryTypeResources.FAQ))]
        FAQ = 2,
    }
}
