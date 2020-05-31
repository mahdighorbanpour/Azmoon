import { Component, OnInit, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';

@Component({
    selector: 'admin-sidebar',
    templateUrl: './admin-sidebar.component.html',
    styleUrls: ['./admin-sidebar.component.scss']
})
export class AdminSidebarComponent extends AppComponentBase {
    versionText: string;
    currentYear: number;
    isCollapsed = true;
  menuItems: MenuItem[] = [
      new MenuItem(this.l('AdminPage'), 'Pages.Admin', 'settings', '/admin/home', 'text-blue'),
      new MenuItem(this.l('About'), '', 'info', '/app/about', 'text-orange'),
      new MenuItem(this.l('HomePage'), '', 'home', '/app/home', 'text-primary'),

        new MenuItem(this.l('Categories'), 'Pages.Categories', 'view_list', '/admin/categories'),
        new MenuItem(this.l('Questions'), 'Pages.Questions', 'question_answer', '/admin/questions'),
        new MenuItem(this.l('Quizzes'), 'Pages.Quiz', 'question_answer', '/admin/quiz'),
        new MenuItem(this.l('Tenants'), 'Pages.Tenants', 'business', '/admin/tenants'),
        new MenuItem(this.l('Users'), 'Pages.Users', 'people', '/admin/users'),
        new MenuItem(this.l('Roles'), 'Pages.Roles', 'local_offer', '/admin/roles'),
    ];

    constructor(
        injector: Injector,
        private router: Router
    ) {
        super(injector);
        this.currentYear = new Date().getFullYear();
        this.versionText = this.appSession.application.version + ' [' + this.appSession.application.releaseDate.format('YYYYDDMM') + ']';
    }

    showMenuItem(menuItem): boolean {
        if (menuItem.permissionName) {
            return this.permission.isGranted(menuItem.permissionName);
        }

        return true;
    }
    
    ngOnInit() {
        this.router.events.subscribe((event) => {
          this.isCollapsed = true;
       });
      }
}
