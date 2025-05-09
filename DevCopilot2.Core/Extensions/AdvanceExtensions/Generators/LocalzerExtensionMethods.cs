using DevCopilot2.Domain.DTOs.Localizer;
using DevCopilot2.Domain.Entities.Entities;

namespace DevCopilot2.Core.Extensions.AdvanceExtensions.Generators
{
    public static class LocalzerExtensionMethods
    {
        public static ListViewLocalizersDto GetListViewLocalizer(this Entity entity)
            => new ListViewLocalizersDto()
            {
                AddNew = $"Create New {entity.SingularName}",
                Title = $"{entity.PluralName}",
                MetaTitle = $"Manage {entity.PluralName}"
            };
    }
}
