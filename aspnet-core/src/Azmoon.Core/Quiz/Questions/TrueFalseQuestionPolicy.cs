using Abp.UI;
using Azmoon.Core.Quiz.Entities;

namespace Azmoon.Core.Quiz.Questions
{
    public class TrueFalseQuestionPolicy: QuestionPolicyBase
    {
        public TrueFalseQuestionPolicy(Question question) : base(question)
        {
        }
        public override void CheckPolicies()
        {
            CheckHasOnlyTwoChoices();
            CheckHasOnlyOnCorrectChoice();
        }

        protected override void CheckType()
        {
            if(Question.QuestionType != Enums.QuestionType.TrueFalse)
                throw new UserFriendlyException("Incompatible policy cheker is selected!");
        }

        private void CheckHasOnlyOnCorrectChoice()
        {
            if (Question.CorrectChoicesCount != 1)
                throw new UserFriendlyException("True/False question must have 1 correct choice!");
        }

        private void CheckHasOnlyTwoChoices()
        {
            if (Question.AllChoicesCount != 2)
                throw new UserFriendlyException("True/False question is allowed to have only 2 choices!");
        }
    }
}
