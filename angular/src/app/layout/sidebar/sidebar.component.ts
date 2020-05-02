import { Component, OnInit, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';

// declare interface RouteInfo {
//     path: string;
//     title: string;
//     icon: string;
//     class: string;
// }
// export const ROUTES: RouteInfo[] = [
//     { path: '/dashboard', title: 'Dashboard',  icon: 'ni-tv-2 text-primary', class: '' },
//     { path: '/icons', title: 'Icons',  icon:'ni-planet text-blue', class: '' },
//     { path: '/maps', title: 'Maps',  icon:'ni-pin-3 text-orange', class: '' },
//     { path: '/user-profile', title: 'User profile',  icon:'ni-single-02 text-yellow', class: '' },
//     { path: '/tables', title: 'Tables',  icon:'ni-bullet-list-67 text-red', class: '' },
//     { path: '/login', title: 'Login',  icon:'ni-key-25 text-info', class: '' },
//     { path: '/register', title: 'Register',  icon:'ni-circle-08 text-pink', class: '' }
// ];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent extends AppComponentBase {

  menuItems: MenuItem[] = [
      new MenuItem(this.l('HomePage'), '', 'home', '/app/home', 'text-primary'),
      new MenuItem(this.l('AdminPage'), 'Pages.Admin', 'settings', '/admin/home', 'text-blue'),
      new MenuItem(this.l('About'), '', 'info', '/app/about', 'text-orange'),

      new MenuItem(this.l('MultiLevelMenu'), '', 'menu', '', 'text-yellow', [
          new MenuItem('ASP.NET Boilerplate', '', '', '', '', [
              new MenuItem('Home', '', '', 'https://aspnetboilerplate.com/?ref=abptmpl'),
              new MenuItem('Templates', '', '', 'https://aspnetboilerplate.com/Templates?ref=abptmpl'),
              new MenuItem('Samples', '', '', 'https://aspnetboilerplate.com/Samples?ref=abptmpl'),
              new MenuItem('Documents', '', '', 'https://aspnetboilerplate.com/Pages/Documents?ref=abptmpl')
          ]),
          new MenuItem('ASP.NET Zero', '', '', '', '', [
              new MenuItem('Home', '', '', 'https://aspnetzero.com?ref=abptmpl'),
              new MenuItem('Description', '', '', 'https://aspnetzero.com/?ref=abptmpl#description'),
              new MenuItem('Features', '', '', 'https://aspnetzero.com/?ref=abptmpl#features'),
              new MenuItem('Pricing', '', '', 'https://aspnetzero.com/?ref=abptmpl#pricing'),
              new MenuItem('Faq', '', '', 'https://aspnetzero.com/Faq?ref=abptmpl'),
              new MenuItem('Documents', '', '', 'https://aspnetzero.com/Documents?ref=abptmpl')
          ])
      ])
  ];

  constructor(
      injector: Injector,
      private router: Router
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
