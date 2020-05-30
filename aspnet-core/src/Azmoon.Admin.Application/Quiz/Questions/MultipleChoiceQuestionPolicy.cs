using Abp.UI;
using Azmoon.Core.Quiz.Entities;

namespace Azmoon.Core.Quiz.Questions
{
    public class MultipleChoiceQuestionPolicy : QuestionPolicyBase
    {
        public MultipleChoiceQuestionPolicy(Question question) : base(question)
        {
        }
        public override void CheckPolicies()
        {
            CheckHasAtLeastOnCorrectChoice();
            CheckHasMoreThanOneChoice();
        }

        protected override void CheckType()
        {
            if (Question.QuestionType != Enums.QuestionType.MultipleChoice)
                throw new UserFriendlyException("Incompatible policy cheker is selected!");
        }

        private void CheckHasMoreThanOneChoice()
        {
            if (Question.AllChoicesCount <= 1)
                throw new UserFriendlyException("Multiple choice question must have 2 choices or more!");
        }
    }
}
