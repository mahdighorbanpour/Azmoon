using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Azmoon.Core.Quiz.Entities;
using System;

namespace Azmoon.Application.Shared.Quiz.Questions.Dto
{
    [AutoMapFrom(typeof(Choice))]
    public class ChoiceDto: EntityDto<Guid>
    {
        public Guid QuestionId { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; private set; } = false;
    }


}
