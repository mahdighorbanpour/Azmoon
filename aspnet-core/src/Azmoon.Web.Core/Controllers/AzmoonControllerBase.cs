using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Azmoon.Controllers
{
    public abstract class AzmoonControllerBase: AbpController
    {
        protected AzmoonControllerBase()
        {
            LocalizationSourceName = AzmoonConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
