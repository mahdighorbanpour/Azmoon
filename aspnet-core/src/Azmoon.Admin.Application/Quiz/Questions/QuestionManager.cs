﻿using Abp.Domain.Repositories;
using Azmoon.Core.Quiz.Entities;
using System;
using System.Threading.Tasks;

namespace Azmoon.Admin.Application.Questions
{
    public class QuestionManager : IQuestionManager
    {
        private readonly IRepository<Question, Guid> _repository;
        private readonly IRepository<Choice, Guid> _choicesRepository;
        private readonly IQuestionPolicyFactory _questionPlociyFacory;

        public QuestionManager(
            IRepository<Question, Guid> repository,
            IRepository<Choice, Guid> choicesRepository,
            IQuestionPolicyFactory questionPlociyFacory)
        {
            _repository = repository;
            _choicesRepository = choicesRepository;
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