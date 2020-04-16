using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Azmoon.Configuration;

namespace Azmoon.Web.Host.Startup
{
    [DependsOn(
       typeof(AzmoonWebCoreModule))]
    public class AzmoonWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AzmoonWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AzmoonWebHostModule).GetAssembly());
        }
    }
}
