using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Azmoon.Authorization.Roles;
using Azmoon.Authorization.Users;
using Azmoon.MultiTenancy;
using Azmoon.Core.Quiz.Entities;
using System.Reflection;
using Abp.Domain.Entities;
using Abp.Extensions;
using System.Linq.Expressions;
using System;
using Azmoon.Core.Quiz.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Abp.Events.Bus.Entities;

namespace Azmoon.Persistence.EntityFrameworkCore{
    public class AzmoonDbContext : AbpZeroDbContext<Tenant, Role, User, AzmoonDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Choice>  Choices { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public AzmoonDbContext(DbContextOptions<AzmoonDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void CheckAndSetMayHaveTenantIdProperty(object entityAsObj)
        {
            // code copied from abp github
            if (SuppressAutoSetTenantId)
            {
                return;
            }
            //skipped by Azmoon
            //Only works for single tenant applications
            //if (MultiTenancyConfig?.IsEnabled ?? false)
            //{
            //    return;
            //}

            //Only set IMayHaveTenant entities
            if (!(entityAsObj is IMayHaveTenant))
            {
                return;
            }

            var entity = entityAsObj.As<IMayHaveTenant>();

            //Don't set if it's already set
            if (entity.TenantId != null)
            {
                return;
            }

            entity.TenantId = GetCurrentTenantIdOrNull();
        }

        protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
        {
            // code copied from abp github
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> softDeleteFilter = e => !IsSoftDeleteFilterEnabled || !((ISoftDelete)e).IsDeleted;
                expression = expression == null ? softDeleteFilter : CombineExpressions(expression, softDeleteFilter);
            }
            
            if (typeof(IMayHaveTenant).IsAssignableFrom(typeof(TEntity)))
            {
                // here is the custom code
                if (typeof(IMayBePublic).IsAssignableFrom(typeof(TEntity)))
                {
                    Expression<Func<TEntity, bool>> mayBePublicFilter = e =>
                        // retrieve only records belonging to current tenant or the approved public records
                        (CurrentTenantId.HasValue &&
                            (((IMayHaveTenant)e).TenantId == CurrentTenantId ||
                           (((IMayBePublic)e).IsPublic && ((INeedHostApproval)e).IsApproved))
                           ) ||
                        // retrieve only records belonging to host or marked as public records
                        (!CurrentTenantId.HasValue &&
                            (((IMayHaveTenant)e).TenantId == null || ((IMayBePublic)e).IsPublic)
                            );
                    expression = expression == null ? mayBePublicFilter : CombineExpressions(expression, mayBePublicFilter);
                }
                else
                {
                    Expression<Func<TEntity, bool>> mayHaveTenantFilter = e => !IsMayHaveTenantFilterEnabled || ((IMayHaveTenant)e).TenantId == CurrentTenantId;
                    expression = expression == null ? mayHaveTenantFilter : CombineExpressions(expression, mayHaveTenantFilter);
                }
            }

            if (typeof(IMustHaveTenant).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> mustHaveTenantFilter = e => !IsMustHaveTenantFilterEnabled || ((IMustHaveTenant)e).TenantId == CurrentTenantId;
                expression = expression == null ? mustHaveTenantFilter : CombineExpressions(expression, mustHaveTenantFilter);
            }

            return expression;
        }

        protected override void ApplyAbpConcepts(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            base.ApplyAbpConcepts(entry, userId, changeReport);

            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyAzmoonConceptsForAddedEntity(entry, userId, changeReport);
                    break;
                case EntityState.Modified:
                    ApplyAzmoonConceptsForModifiedEntity(entry, userId, changeReport);
                    break;
                case EntityState.Deleted:
                    ApplyAzmoonConceptsForDeletedEntity(entry, userId, changeReport);
                    break;
            }

        }
        private void ApplyAzmoonConceptsForAddedEntity(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            CheckAndSetINeedHostApprovalProperty(entry.Entity);
        }
        
        private void ApplyAzmoonConceptsForModifiedEntity(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
        }
        private void ApplyAzmoonConceptsForDeletedEntity(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
        }

        private void CheckAndSetINeedHostApprovalProperty(object entityAsObj)
        {
            if (!(entityAsObj is INeedHostApproval))
            {
                return;
            }

            var entity = entityAsObj.As<INeedHostApproval>();

            //If it's not a host user, Set value to false
            if (CurrentTenantId.HasValue)
            {
                entity.IsApproved = false;
            }

        }

    }
}
