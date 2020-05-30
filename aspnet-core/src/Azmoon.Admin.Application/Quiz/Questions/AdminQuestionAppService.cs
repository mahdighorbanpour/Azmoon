using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Azmoon.Application.Shared.Quiz;
using Azmoon.Application.Shared.Quiz.Questions.Dto;
using Azmoon.Authorization;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Core.Quiz.Enums;
using Azmoon.Admin.Application.Questions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azmoon.Admin.Application.Quiz.Questions
{
    [AbpAuthorize(PermissionNames.Pages_Questions)]
    public class AdminQuestionAppService : AdminCrudServiceWithHostApprovalBase<Question, QuestionDto, Guid, ListQuestionDto, PagedQuestionResultRequestDto, CreateUpdateQuestionDto, CreateUpdateQuestionDto>, IAdminQuestionAppService
    {
        private readonly IRepository<Choice, Guid> _choicesRepository;
        private readonly IQuestionPolicyFactory _questionPlociyFacory;

        public AdminQuestionAppService(
            IRepository<Question, Guid> repository,
            IRepository<Choice, Guid> choicesRepository,
            IQuestionPolicyFactory questionPlociyFacory
            ) : base(repository)
        {
            _choicesRepository = choicesRepository;
            _questionPlociyFacory = questionPlociyFacory;
        }

        protected override IQueryable<Question> CreateFilteredQuery(PagedQuestionResultRequestDto input)
        {
            return Repository
                .GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), q =>
                  q.Title.Contains(input.Filter) ||
                  q.Description.Contains(input.Filter))
                .WhereIf(input.QuestionType.HasValue, q => q.QuestionType == input.QuestionType.Value)
                .WhereIf(input.IsPublic.HasValue, q => q.IsPublic == input.IsPublic)
                .WhereIf(input.CategoryId.HasValue, q => q.CategoryId == input.CategoryId);
        }

        public override async Task<QuestionDto> CreateAsync(CreateUpdateQuestionDto input)
        {
            CheckCreatePermission();

            var entity = MapToEntity(input);
            try
            {
                foreach (var choice in input.Choices)
                    entity.AddChoice(choice.Value, choice.IsCorrect, choice.OrderNo);

                var policy = _questionPlociyFacory.CreatePolicy(entity);
                policy.CheckPolicies();

                entity = await Repository.InsertAsync(entity);
                return MapToEntityDto(entity);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(L(ex.Message));
            }
        }

        public override async Task<QuestionDto> UpdateAsync(CreateUpdateQuestionDto input)
        {
            CheckUpdatePermission();
            await AuthorizeIMayBePublicEntity(input.Id);

            var entity = await GetEntityByIdAsync(input.Id);

            MapToEntity(input, entity);
            try
            {
                entity.ClearChoices();
                foreach (var choice in input.Choices)
                    entity.AddChoice(choice.Value, choice.IsCorrect, choice.OrderNo);

                entity = await Repository.UpdateAsync(entity);
                return MapToEntityDto(entity);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(L(ex.Message));
            }
        }

        public override async Task DeleteAsync(EntityDto<Guid> input)
        {
            CheckDeletePermission();
            await AuthorizeIMayBePublicEntity(input.Id);

            var choices = await GetChoicesForQuestion(input.Id);
            if (choices.Count > 0)
            {
                foreach (var choice in choices)
                    await _choicesRepository.DeleteAsync(choice);
            }

            await Repository.DeleteAsync(input.Id);
        }

        public Task<List<DictionaryDto>> GetQuestionTypesDictionary()
        {
            List<DictionaryDto> list = new List<DictionaryDto>();
            foreach (var item in Enum.GetValues(typeof(QuestionType)))
            {
                list.Add(new DictionaryDto()
                {
                    IntValue = (int)item,
                    Text = L(item.ToString())
                });
            }
            return Task.FromResult(list);
        }

        public override async Task ApproveIsPublic(Guid id)
        {
            await base.ApproveIsPublic(id);
            var choices = await GetChoicesForQuestion(id);
            if (choices.Count > 0)
            {
                foreach (var choice in choices)
                {
                    choice.IsApproved = true;
                    await _choicesRepository.UpdateAsync(choice);
                }
            }
        }

        private async Task<List<Choice>> GetChoicesForQuestion(Guid questionId)
        {
            return await _choicesRepository.GetAll()
                .Where(c => c.QuestionId.Equals(questionId))
                .ToListAsync();
        }
    }
}
