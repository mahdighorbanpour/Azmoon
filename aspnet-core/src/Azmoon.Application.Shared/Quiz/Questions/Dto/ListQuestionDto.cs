﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Azmoon.Core.Quiz.Entities;
using System;

namespace Azmoon.Application.Shared.Quiz.Questions.Dto
{
    [AutoMapFrom(typeof(Question))]
    public class ListQuestionDto : EntityDto<Guid>
    {
        public string Title { get; set; }
        public int Marks { get; set; }
        public string QuestionType { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }

        public int ChoicesCount { get; set; }
        public int QuizzsCount { get; set; }
    }
}
