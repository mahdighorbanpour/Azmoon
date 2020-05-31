using Abp.UI;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace Azmoon.Admin.Application.Questions
{
    public class OrderingQuestionPolicy : QuestionPolicyBase
    {
        public OrderingQuestionPolicy(Question question) : base(question)
        {
        }

        public override void CheckPolicies()
        {
            CheckHasMoreThanOneChoice();
            CheckAllChoicesHaveOrderNo();
            CheckOrderNumbersAreInSequence();
            CheckRandomizeChoicesIsEnabled();
        }

        protected override void CheckType()
        {
            if (Question.QuestionType != QuestionType.Ordering)
                throw new UserFriendlyException("Incompatible policy cheker is selected!");
        }

        private void CheckHasMoreThanOneChoice()
        {
            if (Question.AllChoicesCount <= 1)
                throw new UserFriendlyException("Ordering question must have 2 choices or more!");
        }

        private void CheckAllChoicesHaveOrderNo()
        {
            if (Question.Choices.Any(c=> c.OrderNo == null))
                throw new UserFriendlyException("Ordering question all choices must have an order number!");
        }

        private void CheckOrderNumbersAreInSequence()
        {
            for (int i = 0; i < Question.AllChoicesCount; i++)
                if (Question.Choices.ElementAt(i).OrderNo != i)
                    throw new UserFriendlyException("Order numbers must be in a sequence starting from 0!");
        }

        private void CheckRandomizeChoicesIsEnabled()
        {
            if (Question.RandomizeChoices == null || Question.RandomizeChoices.Value == false)
                throw new UserFriendlyException("Randomize choices must be checked for ordering question!");
        }
    }
}
