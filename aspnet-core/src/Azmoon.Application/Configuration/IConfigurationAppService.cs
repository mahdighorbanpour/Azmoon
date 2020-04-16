using System.Threading.Tasks;
using Azmoon.Configuration.Dto;

namespace Azmoon.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
