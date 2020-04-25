using Abp.Application.Services;
using Abp.Domain.Repositories;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Application.Shared.Quiz.Categories.Dto;
using Abp.Collections.Extensions;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.Authorization;
using Azmoon.Authorization;
using System.Threading.Tasks;

namespace Azmoon.Admin.Application.Quiz.Categories
{
    [AbpAuthorize(PermissionNames.Pages_Categories)]
    public class AdminCategoryAppService : AzmoonAdminBaseCrudService<Category, CategoryDto, int, PagedCategoryResultRequestDto, CreateUpdateCategoryDto, CreateUpdateCategoryDto>, IAdminCategoryAppService
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

        //public override Task<CategoryDto> CreateAsync(CategoryDto input)
        //{
        //    var category = MapToEntity(input);
        //    if(AbpSession.)
        //    return base.CreateAsync(input);
        //}
    }
}
