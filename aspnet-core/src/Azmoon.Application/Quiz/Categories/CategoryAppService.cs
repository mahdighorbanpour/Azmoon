using Abp.Application.Services;
using Abp.Domain.Repositories;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Application.Quiz.Categories.Dto;
using Abp.Collections.Extensions;
using System.Linq;
using Abp.Linq.Extensions;

namespace Azmoon.Application.Quiz.Categories
{
    public class CategoryAppService : AsyncCrudAppService<Category, CategoryDto, int, PagedCategoryResultRequestDto, CategoryDto, CategoryDto>, ICategoryAppService
    {
        public CategoryAppService(IRepository<Category, int> repository): base(repository)
        {
        }

        protected override IQueryable<Category> CreateFilteredQuery(PagedCategoryResultRequestDto input)
        {
            return Repository.GetAll()
               .WhereIf(!string.IsNullOrWhiteSpace(input.filter), c =>
                 c.Title.Contains(input.filter) ||
                 c.ShortDescription.Contains(input.filter) ||
                 c.LongDescription.Contains(input.filter)
               );
        }
    }
}
