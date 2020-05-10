using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Abp.Collections.Extensions;
using Abp.Linq.Extensions;
using Abp.Authorization;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Authorization;
using Azmoon.Admin.Application.Quiz.Interfaces;
using Azmoon.Application.Shared.Quiz.Categories.Dto;
using System.Collections.Generic;
using Azmoon.Application.Shared.Quiz;

namespace Azmoon.Admin.Application.Quiz.Categories
{
    [AbpAuthorize(PermissionNames.Pages_Categories)]
    public class AdminCategoryAppService : AdminCrudServiceWithHostApprovalBase<Category, CategoryDto, int, PagedCategoryResultRequestDto, CreateUpdateCategoryDto, CreateUpdateCategoryDto>, 
        IAdminCategoryAppService,
        IHaveDictionary
    {
        public AdminCategoryAppService(IRepository<Category, int> repository): base(repository)
        {
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

        public async Task<List<DictionaryDto>> GetDictionary()
        {
            CheckGetAllPermission();

            var query = this.Repository.GetAll();

            return await ObjectMapper.ProjectTo<DictionaryDto>(query).ToListAsync();
        }
    }
}
