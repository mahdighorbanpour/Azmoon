using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;

namespace Azmoon.Admin.Application.Questions
{
    public class QuestionPolicyFactory : IQuestionPolicyFactory
    {
        public QuestionPolicyBase CreatePolicy(Question question)
        {
            switch (question.QuestionType)
            {
                case QuestionType.TrueFalse:
                    return new TrueFalseQuestionPolicy(question);
                case QuestionType.MultipleChoice:
                    return new MultipleChoiceQuestionPolicy(question);
                case QuestionType.Ordering:
                    return new OrderingQuestionPolicy(question);
                case QuestionType.ShortAnswer:
                    break;
                case QuestionType.FillInTheBlank:
                    break;
                case QuestionType.Matching:
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}