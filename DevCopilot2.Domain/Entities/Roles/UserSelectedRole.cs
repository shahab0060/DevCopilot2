
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Entities.Roles;
using DevCopilot2.Domain.Entities.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevCopilot2.Domain.Entities.Roles
{
    public class UserSelectedRole : EntityId<long>
    {
        #region properties

        public long UserId { get; set; }

        public long RoleId { get; set; }

        #endregion

        #region relations

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        #endregion

    }
}
