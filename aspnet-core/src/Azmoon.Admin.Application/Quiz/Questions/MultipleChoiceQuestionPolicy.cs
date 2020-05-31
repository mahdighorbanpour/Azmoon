using Abp.UI;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;

namespace Azmoon.Admin.Application.Questions
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
            if (Question.QuestionType != QuestionType.MultipleChoice)
                throw new UserFriendlyException("Incompatible policy cheker is selected!");
        }

        private void CheckHasMoreThanOneChoice()
        {
            if (Question.AllChoicesCount <= 1)
                throw new UserFriendlyException("Multiple choice question must have 2 choices or more!");
        }
    }
}
