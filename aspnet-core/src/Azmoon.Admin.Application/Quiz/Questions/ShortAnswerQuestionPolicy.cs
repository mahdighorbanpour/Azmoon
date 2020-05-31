using Abp.UI;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;

namespace Azmoon.Admin.Application.Questions
{
    public class ShortAnswerQuestionPolicy : QuestionPolicyBase
    {
        public ShortAnswerQuestionPolicy(Question question) : base(question)
        {
        }

        public override void CheckPolicies()
        {
            CheckHasOnlyOneChoice();
        }

        protected override void CheckType()
        {
            if (Question.QuestionType != QuestionType.ShortAnswer)
                throw new UserFriendlyException("Incompatible policy cheker is selected!");
        }

        private void CheckHasOnlyOneChoice()
        {
            if (Question.AllChoicesCount != 1 || Question.Choices[0].IsCorrect == false)
                throw new UserFriendlyException("Short answer question must have only 1 choice which should be correct!");
        }
    }
}
