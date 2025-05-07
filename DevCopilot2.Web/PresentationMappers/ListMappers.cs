using DevCopilot2.Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevCopilot2.Web.PresentationMappers
{
    public static class ListMappers
    {
        public static SelectListItem ToSelectListItem(this ComboDto combo)
            => new SelectListItem()
            {
                Text = combo.Title,
                Value = combo.Value
            };

        public static SelectListItem GetDefault()
            => GetDefaultCombo().ToSelectListItem();

        public static ComboDto GetDefaultCombo()
            => new ComboDto()
            {
                Title = "لطفا انتخاب کنید",
                Value = ""
            };

        public static List<SelectListItem> ToSelectListItem(this List<ComboDto> combos, bool selectAll = false, bool addDefault = false)
        {
            if (combos.Any())
            {
                if (addDefault)
                {
                    combos.Add(GetDefaultCombo());
                }
                if (selectAll)
                {
                    ComboDto selectAllCombo = new ComboDto()
                    {
                        Title = "انتخاب همه",
                        Value = ""
                    };
                    combos.Add(selectAllCombo);
                }
            }
            else
            {
                ComboDto emptyCombo = new ComboDto()
                {
                    Title = "هیچ موردی یافت نشد",
                    Value = ""
                };
                combos.Add(emptyCombo);
            }
            return combos
                .OrderBy(a => a.Value)
                .Select(c => c.ToSelectListItem())
                .ToList();
        }
    }
}
