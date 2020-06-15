using Abp.AutoMapper;
using Azmoon.Core.Quiz.Entities;
using System.Collections.Generic;

namespace Azmoon.Application.Shared.Quiz.Questions.Dto
{
    [AutoMapFrom(typeof(Question))]
    public class QuestionDto : ListQuestionDto
    {
        public string Description { get; set; }
        public string Hint { get; set; }
        public bool? RandomizeChoices { get; set; }

        public List<ChoiceDto> Choices { get; set; }
        public List<MatchSetDto> MatchSets { get; set; }
    }
}
