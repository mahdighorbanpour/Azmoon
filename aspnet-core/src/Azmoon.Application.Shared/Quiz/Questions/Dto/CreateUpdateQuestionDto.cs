using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Azmoon.Core.Quiz.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Azmoon.Application.Shared.Quiz.Questions.Dto
{
    [AutoMapTo(typeof(Core.Quiz.Entities.Question))]
    public class CreateUpdateQuestionDto : EntityDto<Guid>
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(10000)]
        public string Description { get; set; }

        [StringLength(1000)]
        public string Hint { get; set; }
        public int Marks { get; set; }
        public QuestionType QuestionType { get; set; }

        //public List<QuizQuestion> Quizzes { get; set; }

        //private readonly List<Choice> _choices = new List<Choice>();
        //public IReadOnlyList<Choice> Choices => _choices.AsReadOnly();
        public bool? RandomizeChoices { get; set; }
        public bool IsPublic { get; set; }
    }
}
