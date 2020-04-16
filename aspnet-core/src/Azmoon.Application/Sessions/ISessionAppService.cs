using System.Threading.Tasks;
using Abp.Application.Services;
using Azmoon.Sessions.Dto;

namespace Azmoon.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
