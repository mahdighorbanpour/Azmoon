using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Azmoon.Core.Quiz.Entities
{
    public class Choice: FullAuditedEntity, IMayHaveTenant
    {
        public Choice(int questionId, string value, bool isCorrect)
        {
            QuestionId = questionId;
            Value = value;
            IsCorrect = isCorrect;
        }
        public int? TenantId { get; set; }

        public int QuestionId { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; private set; } = false;
    }
}