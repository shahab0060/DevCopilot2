
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Entities.Permissions;
using DevCopilot2.Domain.Entities.Roles;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevCopilot2.Domain.Entities.Roles
{
    public class RoleSelectedPermission : EntityId<long>
    {
        #region properties

        public long RoleId { get; set; }

        public long PermissionId { get; set; }

        #endregion

        #region relations

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        [ForeignKey(nameof(PermissionId))]
        public Permission Permission { get; set; }

        #endregion

    }
}
