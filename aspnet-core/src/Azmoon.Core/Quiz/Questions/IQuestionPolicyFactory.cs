using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;

namespace Azmoon.Core.Quiz.Questions
{
    public interface IQuestionPolicyFactory
    {
        QuestionPolicyBase CreatePolicy(Question question);
    }

    public class QuestionPolicyFactory : IQuestionPolicyFactory
    {
        public QuestionPolicyBase CreatePolicy(Question question)
        {
            switch (question.QuestionType)
            {
                case QuestionType.TrueFalse:
                    return new TrueFalseQuestionPolicy(question);
                case QuestionType.MultipleChoice:
                    break;
                case QuestionType.Ordering:
                    break;
                case QuestionType.ShortAnswer:
                    break;
                case QuestionType.FillInTtheBlank:
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