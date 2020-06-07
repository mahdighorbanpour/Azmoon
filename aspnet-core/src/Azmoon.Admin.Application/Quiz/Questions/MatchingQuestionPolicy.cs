using Abp.UI;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace Azmoon.Admin.Application.Questions
{
    public class MatchingQuestionPolicy : QuestionPolicyBase
    {
        public MatchingQuestionPolicy(Question question) : base(question)
        {
        }

        public override void CheckPolicies()
        {
            CheckHasMoreThanOneMatchSets();
            CheckHasMoreThanOneChoice();
            CheckAllChoicesHaveMatchSet();
        }

        protected override void CheckType()
        {
            if (Question.QuestionType != QuestionType.Matching)
                throw new UserFriendlyException("Incompatible policy cheker is selected!");
        }

        private void CheckHasMoreThanOneMatchSets()
        {
            if (Question.MatchSets.Count <= 1)
                throw new UserFriendlyException("Matching question must have 2 match sets or more!");
        }

        private void CheckHasMoreThanOneChoice()
        {
            if (Question.AllChoicesCount <= 1)
                throw new UserFriendlyException("Matching question must have 2 choices or more!");
        }

        private void CheckAllChoicesHaveMatchSet()
        {
            if (Question.Choices.Any(c=> c.MatchSet == null))
                throw new UserFriendlyException("Matching question all choices must have a match set!");
        }

    }
}
