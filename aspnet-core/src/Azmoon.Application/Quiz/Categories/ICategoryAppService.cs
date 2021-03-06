﻿using Abp.Application.Services;
using Azmoon.Application.Shared.Quiz.Categories.Dto;

namespace Azmoon.Application.Quiz.Categories
{
    public interface ICategoryAppService : IAsyncCrudAppService<CategoryDto, int, PagedCategoryResultRequestDto, CategoryDto, CategoryDto>
    {
    }
}
