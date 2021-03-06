﻿using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Azmoon.Authorization
{
    public class AzmoonAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Admin, L("AdminPage"));
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Categories, L("Categories"));
            context.CreatePermission(PermissionNames.Pages_Quiz, L("Quizzes"));
            context.CreatePermission(PermissionNames.ApproveIsPublic, L("ApproveIsPublic"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Questions, L("Questions"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AzmoonConsts.LocalizationSourceName);
        }
    }
}
