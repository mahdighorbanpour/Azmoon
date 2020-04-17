using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace Azmoon.Core.Quiz.Entities
{
    public class QuizQuestion: FullAuditedEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public int Marks { get; set; }
        public bool? RandomizeChoices { get; set; }
    }
}
