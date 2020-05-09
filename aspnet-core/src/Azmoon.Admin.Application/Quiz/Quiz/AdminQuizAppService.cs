using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Azmoon.Application.Shared.Quiz.Quiz.Dto;
using Azmoon.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Azmoon.Admin.Application.Quiz.Quiz
{
    [AbpAuthorize(PermissionNames.Pages_Quiz)]
    public class AdminQuizAppService : AdminCrudServiceWithHostApprovalBase<Core.Quiz.Entities.Quiz, QuizDto, Guid, PagedQuizResultRequestDto, CreateUpdateQuizDto, CreateUpdateQuizDto>, IAdminQuizAppService
    {
        public AdminQuizAppService(IRepository<Core.Quiz.Entities.Quiz, Guid> repository) : base(repository)
        {
        }

        protected override IQueryable<Core.Quiz.Entities.Quiz> CreateFilteredQuery(PagedQuizResultRequestDto input)
        {
            return Repository
                .GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), q =>
                  q.Title.Contains(input.Filter) ||
                  q.Description.Contains(input.Filter))
                .WhereIf(input.IsActive.HasValue, q => q.IsActive == input.IsActive.Value)
                .WhereIf(input.IsPublic.HasValue, q => q.IsPublic == input.IsPublic)
                .WhereIf(input.CategoryId.HasValue, q => q.CategoryId == input.CategoryId);
        }

        
    }
}
