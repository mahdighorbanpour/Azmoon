using Abp.Authorization;
using Azmoon.Authorization.Roles;
using Azmoon.Authorization.Users;

namespace Azmoon.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
