import { Component, Injector, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';

@Component({
    templateUrl: './admin-sidebar-nav.component.html',
    selector: 'admin-sidebar-nav',
    encapsulation: ViewEncapsulation.None
})
export class AdminSideBarNavComponent extends AppComponentBase {

    menuItems: MenuItem[] = [
        new MenuItem(this.l('AdminPage'), '', 'home', '/app/home'),
        new MenuItem(this.l('Categories'), 'Pages.Categories', 'business', '/admin/categories'),
        new MenuItem(this.l('Quizzes'), 'Pages.Quiz', 'business', '/admin/quiz'),
        new MenuItem(this.l('Tenants'), 'Pages.Tenants', 'business', '/admin/tenants'),
        new MenuItem(this.l('Users'), 'Pages.Users', 'people', '/admin/users'),
        new MenuItem(this.l('Roles'), 'Pages.Roles', 'local_offer', '/admin/roles'),
        new MenuItem(this.l('HomePage'), '', 'home', '/app/home'),
    ];

    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    showMenuItem(menuItem): boolean {
        if (menuItem.permissionName) {
            return this.permission.isGranted(menuItem.permissionName);
        }

        return true;
    }
}
