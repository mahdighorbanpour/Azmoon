using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Azmoon.Authorization;

namespace Azmoon
{
    [DependsOn(
        typeof(AzmoonCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class AzmoonApplicationSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Configuration.Authorization.Providers.Add<AzmoonAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(AzmoonApplicationSharedModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
