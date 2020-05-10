import { Component, OnInit, Injector, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { AppAuthService } from '@shared/auth/app-auth.service';

@Component({
    templateUrl: './admin-navbar-user-area.component.html',
    selector: '[admin-navbar-user-area]',
    encapsulation: ViewEncapsulation.None
})
export class AdminNavBarUserAreaComponent extends AppComponentBase implements OnInit {

    shownLoginName = '';

    constructor(
        injector: Injector,
        private _authService: AppAuthService
    ) {
        super(injector);
    }

    ngOnInit() {
        this.shownLoginName = this.appSession.getShownLoginName();
    }

    logout(): void {
        this._authService.logout();
    }
}
