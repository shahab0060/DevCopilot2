
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Entities.Roles;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevCopilot2.Domain.Entities.Permissions
{
    public class Permission : EntityId<long>
    {
        #region properties

        public string Title { get; set; }

        public string KeyName { get; set; }

        #endregion

        #region relations

        public ICollection<RoleSelectedPermission> RoleSelectedPermissions { get; set; }
        #endregion

    }
}
