using Abp.Application.Services.Dto;
using Azmoon.Core.Quiz.Enums;

namespace Azmoon.Application.Shared.Quiz.Questions.Dto
{
    public class PagedQuestionResultRequestDto : PagedResultRequestDto
    {
        public string Filter { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsPublic { get; set; }
        public QuestionType? QuestionType { get; set; }
    }
}
