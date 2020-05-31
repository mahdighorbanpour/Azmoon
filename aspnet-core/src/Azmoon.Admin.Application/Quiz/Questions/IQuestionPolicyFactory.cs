using Azmoon.Core.Quiz.Entities;

namespace Azmoon.Admin.Application.Questions
{
    public interface IQuestionPolicyFactory
    {
        QuestionPolicyBase CreatePolicy(Question question);
    }
}