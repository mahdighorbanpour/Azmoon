using Azmoon.Core.Quiz.Entities;

namespace Azmoon.Core.Quiz.Questions
{
    public abstract class QuestionPolicyBase
    {
        public Question Question { get; }

        public QuestionPolicyBase(Question question)
        {
            Question = question;
            CheckType();
        }

        protected abstract void CheckType();
        public abstract void CheckPolicies();
    }
}
