using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
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
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<Category, DictionaryDto>()
                      .ForMember(u => u.GuidValue, options => options.Ignore())
                      .ForMember(u => u.IntValue, options => options.MapFrom(input => input.Id))
                      .ForMember(u => u.Text, options => options.MapFrom(input => input.Title));

                config.CreateMap<Question, ListQuestionDto>()
                    .ForMember(q => q.QuestionTypeString, options => options.MapFrom(input => input.QuestionType.ToString()));
            });
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
