import { Component, OnInit, ElementRef, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent extends AppComponentBase {
  public focus;
  constructor(injector: Injector) {
    super(injector);
  }

}