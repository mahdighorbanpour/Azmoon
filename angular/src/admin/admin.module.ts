import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap';
import { NgxPaginationModule } from 'ngx-pagination';
import { AbpModule } from '@abp/abp.module';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';

import { AdminComponent } from './admin.component';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminHomeComponent } from './home/admin-home.component';
import { AdminTopBarComponent } from './layout/admin-topbar.component';
import { AdminTopBarLanguageSwitchComponent } from './layout/admin-topbar-languageswitch.component';
import { AdminSideBarUserAreaComponent } from './layout/admin-sidebar-user-area.component';
import { AdminSideBarNavComponent } from './layout/admin-sidebar-nav.component';
import { AdminSideBarFooterComponent } from './layout/admin-sidebar-footer.component';
import { AdminRightSideBarComponent } from './layout/admin-right-sidebar.component';
// tenants
import { TenantsComponent } from './tenants/tenants.component';
import { CreateTenantDialogComponent } from './tenants/create-tenant/create-tenant-dialog.component';
import { EditTenantDialogComponent } from './tenants/edit-tenant/edit-tenant-dialog.component';
// roles
import { RolesComponent } from './roles/roles.component';
import { CreateRoleDialogComponent } from './roles/create-role/create-role-dialog.component';
import { EditRoleDialogComponent } from './roles/edit-role/edit-role-dialog.component';
// users
import { UsersComponent } from './users/users.component';
import { CreateUserDialogComponent } from './users/create-user/create-user-dialog.component';
import { EditUserDialogComponent } from './users/edit-user/edit-user-dialog.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { ResetPasswordDialogComponent } from './users/reset-password/reset-password.component';
// categories
import { CategoriesComponent } from './quiz/categories/categories.component';
import { CreateOrUpdateCategoryDialogComponent } from './quiz/categories/create-update/create-update-category-dialog.component';
import { AdminQuizzesComponent } from './quiz/quiz/quiz.component';
import { CreateOrUpdateQuizDialogComponent } from './quiz/quiz/create-update/create-update-quiz-dialog.component';
import { AdminSidebarComponent } from './layout/sidebar/admin-sidebar.component';
import { AdminNavbarComponent } from './layout/navbar/admin-navbar.component';
import { AdminFooterComponent } from './layout/footer/admin-footer.component';

@NgModule({
  declarations: [
    AdminComponent,
    AdminHomeComponent,
    AdminTopBarComponent,
    AdminTopBarLanguageSwitchComponent,
    AdminSideBarUserAreaComponent,
    AdminSideBarNavComponent,
    AdminSideBarFooterComponent,
    AdminRightSideBarComponent,
    // tenants
    TenantsComponent,
    CreateTenantDialogComponent,
    EditTenantDialogComponent,
    // roles
    RolesComponent,
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    // users
    UsersComponent,
    CreateUserDialogComponent,
    EditUserDialogComponent,
    ChangePasswordComponent,
    ResetPasswordDialogComponent,
    // categories
    CategoriesComponent,
    CreateOrUpdateCategoryDialogComponent,
    // quizzes
    AdminQuizzesComponent,
    CreateOrUpdateQuizDialogComponent,
    // new components
    AdminFooterComponent,
    AdminNavbarComponent,
    AdminSidebarComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    ModalModule.forRoot(),
    AbpModule,
    AdminRoutingModule,
    ServiceProxyModule,
    SharedModule,
    NgxPaginationModule
  ],
  exports:[
    AdminTopBarComponent,
    AdminTopBarLanguageSwitchComponent,
    AdminSideBarUserAreaComponent,
    AdminSideBarNavComponent,
    AdminSideBarFooterComponent,
    AdminRightSideBarComponent,
  ],
  providers: [],
  entryComponents: [
    // tenants
    CreateTenantDialogComponent,
    EditTenantDialogComponent,
    // roles
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    // users
    CreateUserDialogComponent,
    EditUserDialogComponent,
    ResetPasswordDialogComponent,
    // categories
    CreateOrUpdateCategoryDialogComponent,
    // quizzes
    CreateOrUpdateQuizDialogComponent
  ]
})
export class AdminModule {}
