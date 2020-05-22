using Azmoon.Core.Quiz.Entities;
using System.Threading.Tasks;

namespace Azmoon.Core.Quiz.Questions
{
    public interface IQuestionManager
    {
        Task<Question> CreateAsync(Question question);
        Task<Question> UpdateAsync(Question entity);
    }
}
