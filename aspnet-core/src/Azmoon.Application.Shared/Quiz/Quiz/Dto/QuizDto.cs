using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace Azmoon.Application.Shared.Quiz.Quiz.Dto
{
    [AutoMapFrom(typeof(Core.Quiz.Entities.Quiz))]
    public class QuizDto : EntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan? Duration { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public int QuestionsCount { get; set; }
    }
}
