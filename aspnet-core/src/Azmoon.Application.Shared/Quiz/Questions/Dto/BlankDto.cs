using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Azmoon.Core.Quiz.Entities;
using System;

namespace Azmoon.Application.Shared.Quiz.Questions.Dto
{
    [AutoMap(typeof(Blank))]
    public class BlankDto : EntityDto<Guid>
    {
        public Guid ChoiceId { get; set; }
        public string Answer { get; set; }
        public int Index { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }
    }
}
