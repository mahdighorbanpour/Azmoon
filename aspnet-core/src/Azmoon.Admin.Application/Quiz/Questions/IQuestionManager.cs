using Azmoon.Core.Quiz.Entities;
using System.Threading.Tasks;

namespace Azmoon.Admin.Application.Questions
{
    public interface IQuestionManager
    {
        Task<Question> CreateAsync(Question question);
        Task<Question> UpdateAsync(Question entity);
    }
}
