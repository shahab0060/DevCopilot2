using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Entities.Entities;
using DevCopilot2.Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;

namespace DevCopilot2.Domain.Entities.Users
{
    [Index(nameof(FirstName))]
    [Index(nameof(LastName))]
    [Index(nameof(PhoneNumber), nameof(Password))]
    [Index(nameof(PhoneNumber),
           nameof(PhoneNumberActivationCode),
           nameof(PhoneNumberActivationCodeExpireTime))]
    public class User : EntityId<long>
    {
        #region properties

        public bool IsSuperAdmin { get; set; }

        public string? ImageName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string? Password { get; set; }

        public string PhoneNumberActivationCode { get; set; }

        public DateTime PhoneNumberActivationCodeExpireTime { get; set; }

        #endregion

        #region relations

        public ICollection<UserSelectedRole> UserSelectedRoles { get; set; } = new List<UserSelectedRole>();
        public ICollection<Entity> CreatedEntities { get; set; } = new List<Entity>();


        #endregion

    }
}
