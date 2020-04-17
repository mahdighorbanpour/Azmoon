using Abp.Application.Services;
using Abp.Domain.Repositories;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Application.Quiz.Categories.Dto;

namespace Azmoon.Application.Quiz.Categories
{
    public class CategoryAppService : AsyncCrudAppService<Category, CategoryDto, int, PagedCategoryResultRequestDto, CreateCategoryDto, CategoryDto>, ICategoryAppService
    {
        public CategoryAppService(IRepository<Category, int> repository): base(repository)
        {
        }      
    }
}
