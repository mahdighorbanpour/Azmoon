using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Azmoon.Application.Shared.Mappings.Converters;
using Azmoon.Application.Shared.Quiz;
using Azmoon.Application.Shared.Quiz.Questions.Dto;
using Azmoon.Core.Quiz.Entities;

namespace Azmoon.Application.Shared.Mappings
{
    public static class AdminServicesDtoMapper
    {
        public static void ApplyMappings(IAbpStartupConfiguration configuration)
        {
            configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<Category, DictionaryDto>()
                      .ForMember(u => u.GuidValue, options => options.Ignore())
                      .ForMember(u => u.IntValue, options => options.MapFrom(input => input.Id))
                      .ForMember(u => u.Text, options => options.MapFrom(input => input.Title));

                config.CreateMap<Question, ListQuestionDto>()
                    .ForMember(q => q.QuestionTypeString, options => options.MapFrom(input => input.QuestionType.ToString()));
                config.CreateMap<Question, QuestionDto>()
                    .ForMember(q => q.QuestionTypeString, options => options.MapFrom(input => input.QuestionType.ToString()));

                config.CreateMap<CreateUpdateQuestionDto, Question>()
                    .ConvertUsing<CreateUpdateQuestionConverter>();
            });
        }
    }
}
