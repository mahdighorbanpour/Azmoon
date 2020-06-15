using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Azmoon.Core.Quiz.Entities;
using System;

namespace Azmoon.Application.Shared.Quiz.Questions.Dto
{
    [AutoMap(typeof(MatchSet))]
    public class MatchSetDto : EntityDto<Guid>
    {
        public MatchSetDto()
        {
        }
        public Guid QuestionId { get; set; }
        public string Value { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }
    }
}
