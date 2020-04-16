using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Azmoon.Authorization.Roles;
using Azmoon.Authorization.Users;
using Azmoon.MultiTenancy;

namespace Azmoon.EntityFrameworkCore
{
    public class AzmoonDbContext : AbpZeroDbContext<Tenant, Role, User, AzmoonDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public AzmoonDbContext(DbContextOptions<AzmoonDbContext> options)
            : base(options)
        {
        }
    }
}
