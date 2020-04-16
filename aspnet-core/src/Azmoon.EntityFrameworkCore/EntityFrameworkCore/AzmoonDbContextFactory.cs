using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Azmoon.Configuration;
using Azmoon.Web;

namespace Azmoon.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class AzmoonDbContextFactory : IDesignTimeDbContextFactory<AzmoonDbContext>
    {
        public AzmoonDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AzmoonDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            AzmoonDbContextConfigurer.Configure(builder, configuration.GetConnectionString(AzmoonConsts.ConnectionStringName));

            return new AzmoonDbContext(builder.Options);
        }
    }
}
