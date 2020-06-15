using Abp.Domain.Entities;
using Azmoon.Core.Quiz.Interfaces;
using System;
using System.Collections.Generic;

namespace Azmoon.Core.Quiz.Entities
{
    public class MatchSet : Entity<Guid>, IMayHaveTenant, IMayBePublic, INeedHostApproval
    {
        public int? TenantId { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public string Value { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsApproved { get; set; }
        public List<Choice> Choices { get; set; }
    }
}