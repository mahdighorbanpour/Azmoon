using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
//using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Azmoon.Core.Quiz.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Azmoon.Admin.Application
{
    public class AzmoonAdminBaseCrudService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput> : AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        public AzmoonAdminBaseCrudService(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
        }

    }

    public static class AdminBaseSErviceExtensions
    {
    }
}
