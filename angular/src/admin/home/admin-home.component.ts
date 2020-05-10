import { Component, Injector, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
    templateUrl: './admin-home.component.html',
    animations: [appModuleAnimation()]
})
export class AdminHomeComponent extends AppComponentBase implements AfterViewInit {

    constructor(injector: Injector) {
        super(injector);
        this.setTitle(this.l("AdminPage"))
    }

    ngAfterViewInit(): void {

    }
}
