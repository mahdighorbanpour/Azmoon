using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Azmoon.Application.Shared.Quiz.Questions.Dto;
using Azmoon.Authorization;
using Azmoon.Core.Quiz.Entities;
using System;
using System.Linq;

namespace Azmoon.Admin.Application.Quiz.Questions
{
    [AbpAuthorize(PermissionNames.Pages_Questions)]
    public class AdminQuestionAppService : AdminCrudServiceWithHostApprovalBase<Question, QuestionDto, Guid, PagedQuestionResultRequestDto, CreateUpdateQuestionDto, CreateUpdateQuestionDto>, IAdminQuestionAppService
    {
        public AdminQuestionAppService(IRepository<Question, Guid> repository) : base(repository)
        {
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

        
    }
}
