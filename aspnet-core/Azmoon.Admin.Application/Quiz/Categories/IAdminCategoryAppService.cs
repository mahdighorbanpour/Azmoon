using Abp.Application.Services;
using Azmoon.Application.Shared.Quiz.Categories.Dto;

namespace Azmoon.Admin.Application.Quiz.Categories
{
    public interface IAdminCategoryAppService : IAsyncCrudAppService<CategoryDto, int, PagedCategoryResultRequestDto, CategoryDto, CategoryDto>
    {
    }
}
