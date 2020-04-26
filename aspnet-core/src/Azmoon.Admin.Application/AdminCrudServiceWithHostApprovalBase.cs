using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Azmoon.Core.Quiz.Interfaces;
using System.Threading.Tasks;

namespace Azmoon.Admin.Application
{
    public class AdminCrudServiceWithHostApprovalBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput> : AdminCrudServiceBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        public AdminCrudServiceWithHostApprovalBase(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
        }

        [AbpAuthorize("ApproveIsPublic")]
        public async Task ApproveIsPublic(TPrimaryKey id)
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
        public async Task RejectIsPublic(TPrimaryKey id)
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
        public async Task ResetIsPublic(TPrimaryKey id)
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


    }
}
