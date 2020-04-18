import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { CategoriesComponent } from './quiz/categories/categories.component';
import { AdminHomeComponent } from './home/admin-home.component';
import { AdminComponent } from './admin.component';
import { RolesComponent } from './roles/roles.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AdminComponent,
                children: [
                    { path: 'home', component: AdminHomeComponent,  canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'update-password', component: ChangePasswordComponent },
                    { path: 'categories', component: CategoriesComponent }
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AdminRoutingModule { }
