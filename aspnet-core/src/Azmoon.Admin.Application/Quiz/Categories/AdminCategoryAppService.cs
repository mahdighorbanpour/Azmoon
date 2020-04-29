using Abp.Domain.Repositories;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Application.Shared.Quiz.Categories.Dto;
using Abp.Collections.Extensions;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Abp.Authorization;
using Azmoon.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Azmoon.Admin.Application.Quiz.Categories
{
    [AbpAuthorize(PermissionNames.Pages_Categories)]
    public class AdminCategoryAppService : AdminCrudServiceWithHostApprovalBase<Category, CategoryDto, int, PagedCategoryResultRequestDto, CreateUpdateCategoryDto, CreateUpdateCategoryDto>, 
        IAdminCategoryAppService
    {
        public AdminCategoryAppService(IRepository<Category, int> repository): base(repository)
        {
        }

        public async override Task<PagedResultDto<CategoryDto>> GetAllAsync(PagedCategoryResultRequestDto input)
        {
            CheckGetAllPermission();

            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await ObjectMapper.ProjectTo<CategoryDto>(query).ToListAsync();

            return new PagedResultDto<CategoryDto>(
                totalCount,
                entities
            );
        }

        protected override IQueryable<Category> CreateFilteredQuery(PagedCategoryResultRequestDto input)
        {
            return Repository.GetAll()
               .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), c =>
                 c.Title.Contains(input.Filter) ||
                 c.ShortDescription.Contains(input.Filter) ||
                 c.LongDescription.Contains(input.Filter)
               );
        }

    }
}
