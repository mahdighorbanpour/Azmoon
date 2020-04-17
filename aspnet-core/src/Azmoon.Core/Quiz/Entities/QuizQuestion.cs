using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Azmoon.Core.Quiz.Entities
{
    public class QuizQuestion: FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int Marks { get; set; }
        public bool? RandomizeChoices { get; set; }
    }
}
