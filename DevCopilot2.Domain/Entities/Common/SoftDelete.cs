using Microsoft.EntityFrameworkCore;

namespace DevCopilot2.Domain.Entities.Common
{
    [Index(nameof(IsDelete))]

    public class SoftDelete
    {
        public bool IsDelete { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
