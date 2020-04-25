using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Azmoon.Authorization;

namespace Azmoon
{
    [DependsOn(
        typeof(AzmoonCoreModule), 
        typeof(AzmoonApplicationSharedModule), 
        typeof(AzmoonApplicationModule), 
        typeof(AbpAutoMapperModule))]
    public class AzmoonAdminApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Configuration.Authorization.Providers.Add<AzmoonAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(AzmoonAdminApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
