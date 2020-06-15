using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Azmoon.Application.Shared.Mappings;
using Azmoon.Application.Shared.Quiz;
using Azmoon.Application.Shared.Quiz.Questions.Dto;
using Azmoon.Authorization;
using Azmoon.Core.Quiz.Entities;

namespace Azmoon
{
    [DependsOn(
        typeof(AzmoonCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class AzmoonApplicationSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(AzmoonApplicationSharedModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
            AdminServicesDtoMapper.ApplyMappings(Configuration);
        }
    }
}
