using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Azmoon.Core.Quiz.Entities;
using System;
using System.Collections.Generic;

namespace Azmoon.Application.Shared.Quiz.Questions.Dto
{
    [AutoMapFrom(typeof(Choice))]
    public class ChoiceDto: EntityDto<Guid>
    {
        public ChoiceDto()
        {
            Blanks = new List<BlankDto>();
        }
        public Guid QuestionId { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; private set; } = false;
        public int? OrderNo { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }
        public List<BlankDto> Blanks { get; set; }
        public MatchSetDto MatchSet { get; set; }
    }
}
