using Abp.Domain.Repositories;
using Azmoon.Core.Quiz.Entities;
using System;
using System.Threading.Tasks;

namespace Azmoon.Core.Quiz.Questions
{
    public class QuestionManager : IQuestionManager
    {
        private readonly IRepository<Question, Guid> _repository;
        private readonly IQuestionPolicyFactory _questionPlociyFacory;

        public QuestionManager(
            IRepository<Question, Guid> repository,
            IQuestionPolicyFactory questionPlociyFacory)
        {
            _repository = repository;
            _questionPlociyFacory = questionPlociyFacory;
        }

        public async Task<Question> CreateAsync(Question question)
        {
            var policy = _questionPlociyFacory.CreatePolicy(question);
            policy.CheckPolicies();

           return await _repository.InsertAsync(question);
        }

        public async Task<Question> UpdateAsync(Question question)
        {
            var policy = _questionPlociyFacory.CreatePolicy(question);
            policy.CheckPolicies();

            return await _repository.UpdateAsync(question);
        }
    }
}
