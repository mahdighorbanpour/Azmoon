using Abp.UI;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace Azmoon.Admin.Application.Questions
{
    public class FillInTheBlankQuestionPolicy : QuestionPolicyBase
    {
        public FillInTheBlankQuestionPolicy(Question question) : base(question)
        {
        }
        public override void CheckPolicies()
        {
            CheckHasAtleastOneChoiceWithOneBlank();
            CheckAllChoicesHaveAtLeastOneBlank();
            CheckBlanksForChoicesHaveDifferentIndexes();
            CheckBlanksForChoicesHaveIndexesLessThanChoiceLength();
        }

        protected override void CheckType()
        {
            if(Question.QuestionType != QuestionType.FillInTheBlank)
                throw new UserFriendlyException("Incompatible policy cheker is selected!");
        }

        private void CheckHasAtleastOneChoiceWithOneBlank()
        {
            if (Question.AllChoicesCount ==0 || Question.Choices[0].Blanks.Count == 0)
                throw new UserFriendlyException("FillInTheBlank question must have at least 1 choice with 1 blank!");
        }

        private void CheckAllChoicesHaveAtLeastOneBlank()
        {
            if (Question.Choices.Any(c=>c.Blanks.Count == 0))
                throw new UserFriendlyException("FillInTheBlank question all choices must have at least 1 blank!");
        }

        private void CheckBlanksForChoicesHaveDifferentIndexes()
        {
            if (Question.Choices.Any(c => 
                c.Blanks.GroupBy(b => b.Index)
                    .Any(g => g.Count() > 1)
                ))
                throw new UserFriendlyException("FillInTheBlank a choice cannot have repeated blank indexes!");
        }
        private void CheckBlanksForChoicesHaveIndexesLessThanChoiceLength()
        {
            if (Question.Choices.Any(c => c.Blanks.Max(b => b.Index) > c.Value.Length -1 ))
                throw new UserFriendlyException("FillInTheBlank a choice cannot have a blank index greater than it's lenght!");
        }
    }
}
