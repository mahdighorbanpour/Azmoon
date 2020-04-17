using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Azmoon.Core.Quiz.Entities
{
    public class Quiz: FullAuditedEntity<Guid>, IMayHaveTenant
    {
        public Quiz()
        {

        }
        public int? TenantId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan? Duration { get; set; }
        public List<QuizQuestion> Questions { get; set; }

        public int TotalQuestion { get { return Questions.Count; } }
        public int TotalMarks { get { return Questions.Sum(q => q.Marks); } }
    }
}
