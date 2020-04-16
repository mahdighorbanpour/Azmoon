using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Azmoon.Configuration.Dto;

namespace Azmoon.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : AzmoonAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
