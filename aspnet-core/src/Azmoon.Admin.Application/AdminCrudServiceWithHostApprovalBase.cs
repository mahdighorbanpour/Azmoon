using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Azmoon.Core.Quiz.Interfaces;
using System;
using System.Threading.Tasks;

namespace Azmoon.Admin.Application
{

    public abstract class AdminCrudServiceWithHostApprovalBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>
        : AdminCrudServiceWithHostApprovalBase<TEntity, TEntityDto, TPrimaryKey, TEntityDto, TGetAllInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        public AdminCrudServiceWithHostApprovalBase(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
        }

    }
    public abstract class AdminCrudServiceWithHostApprovalBase<TEntity, TEntityDto, TPrimaryKey, TListDto, TGetAllInput, TCreateInput, TUpdateInput>
        : AdminCrudServiceBase<TEntity, TEntityDto, TPrimaryKey, TListDto, TGetAllInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TListDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        public AdminCrudServiceWithHostApprovalBase(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
        }

        [AbpAuthorize("ApproveIsPublic")]
        public virtual async Task ApproveIsPublic(TPrimaryKey id)
        {
            var entityAsObj = await GetEntityByIdAsync(id);

            if (!(entityAsObj is INeedHostApproval))
            {
                throw new UserFriendlyException(L("EntityCannotBeApproved"));
            }

            var entity = entityAsObj.As<INeedHostApproval>();
            entity.IsApproved = true;

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize("ApproveIsPublic")]
        public virtual async Task RejectIsPublic(TPrimaryKey id)
        {
            var entityAsObj = await GetEntityByIdAsync(id);

            if (!(entityAsObj is INeedHostApproval))
            {
                throw new UserFriendlyException(L("EntityCannotBeApproved"));
            }

            var entity = entityAsObj.As<INeedHostApproval>();
            entity.IsApproved = false;

            await CurrentUnitOfWork.SaveChangesAsync();
        }


        [AbpAuthorize("ApproveIsPublic")]
        public virtual async Task ResetIsPublic(TPrimaryKey id)
        {
            var entityAsObj = await GetEntityByIdAsync(id);

            if (!(entityAsObj is INeedHostApproval))
            {
                throw new UserFriendlyException(L("EntityCannotBeApproved"));
            }

            var entity = entityAsObj.As<INeedHostApproval>();
            entity.IsApproved = null;

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public override async Task<TEntityDto> UpdateAsync(TUpdateInput input)
        {
            await AuthorizeIMayBePublicEntity(input.Id);

            return await base.UpdateAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<TPrimaryKey> input)
        {
            await AuthorizeIMayBePublicEntity(input.Id);

            await base.DeleteAsync(input);
        }

        protected async Task AuthorizeIMayBePublicEntity(TPrimaryKey id)
        {
            TEntity entityAsObj = await GetEntityByIdAsync(id);
            if (!(entityAsObj is IMayHaveTenant) || !(entityAsObj is IMayBePublic))
            {
                return;
            }
            var entity = entityAsObj.As<IMayHaveTenant>();
            if (entity.TenantId != AbpSession.TenantId)
                throw new UserFriendlyException(L("NotAuthorized"));
        }
    }
}
