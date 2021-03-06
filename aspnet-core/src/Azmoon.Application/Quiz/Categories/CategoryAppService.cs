﻿using Abp.Application.Services;
using Abp.Domain.Repositories;
using Azmoon.Core.Quiz.Entities;
using Azmoon.Application.Shared.Quiz.Categories.Dto;
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
               .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), c =>
                 c.Title.Contains(input.Filter) ||
                 c.ShortDescription.Contains(input.Filter) ||
                 c.LongDescription.Contains(input.Filter)
               );
        }
    }
}
