using Azmoon.Core.Quiz.Entities;

namespace Azmoon.Core.Quiz.Questions
{
    public interface IQuestionPolicyFactory
    {
        QuestionPolicyBase CreatePolicy(Question question);
    }
}