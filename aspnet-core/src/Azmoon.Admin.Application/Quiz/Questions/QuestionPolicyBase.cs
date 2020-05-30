using Abp.UI;
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
        
        protected void CheckHasAtLeastOnCorrectChoice()
        {
            if (Question.CorrectChoicesCount == 0)
                throw new UserFriendlyException("Question must have at least 1 correct choice!");
        }

        protected void CheckHasAtLeastOnChoice()
        {
            if (Question.AllChoicesCount == 0)
                throw new UserFriendlyException("Question must have at least 1 choice!");
        }
    }
}
