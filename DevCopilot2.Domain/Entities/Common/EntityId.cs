using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.Entities.Common
{
    [Index(nameof(CreateDate))]
    [Index(nameof(LatestEditDate))]
    public class EntityId<T> : SoftDelete where T : struct
    {
        [Key]
        public T Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LatestEditDate { get; set; }

        public int EditCounts { get; set; }
    }

}
