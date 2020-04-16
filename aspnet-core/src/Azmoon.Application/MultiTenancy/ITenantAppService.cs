using Abp.Application.Services;
using Azmoon.MultiTenancy.Dto;

namespace Azmoon.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

