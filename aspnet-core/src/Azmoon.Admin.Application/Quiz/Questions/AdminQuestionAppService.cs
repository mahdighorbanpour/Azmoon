﻿using Abp.Application.Services.Dto;
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
using Azmoon.Core.Quiz.Questions;
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
        private readonly IQuestionManager _questionManager;

        public AdminQuestionAppService(
            IRepository<Question, Guid> repository,
            IRepository<Choice, Guid> choicesRepository,

            IQuestionManager questionManager
            ) : base(repository)
        {
            _choicesRepository = choicesRepository;
            _questionManager = questionManager;
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
                    entity.AddChoice(choice.Value, choice.IsCorrect);

                entity = await _questionManager.CreateAsync(entity);
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
                    entity.AddChoice(choice.Value, choice.IsCorrect);

                entity = await _questionManager.UpdateAsync(entity);
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

            var choices = await _choicesRepository.GetAll()
                .Where(c => c.QuestionId.Equals(input.Id))
                .ToListAsync();
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
    }
}
