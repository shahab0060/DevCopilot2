using DevCopilot2.Domain.Entities.Common;

namespace DevCopilot2.Domain.Entities.Roles
{
    public class Role : EntityId<long>
    {
        #region properties

        public string Title { get; set; }

        #endregion

        #region relations

        public ICollection<UserSelectedRole> UserSelectedRoles { get; set; }

        public ICollection<RoleSelectedPermission> RoleSelectedPermissions { get; set; }
        #endregion

    }
}
