import { Component, ViewContainerRef, OnInit, ViewEncapsulation, Injector } from '@angular/core';
import { LoginService } from './login/login.service';
import { AppComponentBase } from '@shared/app-component-base';
import { Router } from '@angular/router';

@Component({
    templateUrl: './account.component.html',
    styleUrls: [
        './account.component.less'
    ],
    encapsulation: ViewEncapsulation.None
})
export class AccountComponent extends AppComponentBase implements OnInit {

    versionText: string;
    currentYear: number;
    pageTitle: string;

    private viewContainerRef: ViewContainerRef;

    public constructor(
        injector: Injector,
        private _loginService: LoginService,
        router: Router
    ) {
        super(injector);
        switch (router.url) {
            case '/account/login':
                this.pageTitle = this.l('LoginWelcomeMessage');
                break;
            case '/account/register':
                this.pageTitle = this.l('RegisterWelcomeMessage');
                break;
        }
        this.currentYear = new Date().getFullYear();
        this.versionText = this.appSession.application.version + ' [' + this.appSession.application.releaseDate.format('YYYYDDMM') + ']';
    }

    showTenantChange(): boolean {
        return abp.multiTenancy.isEnabled;
    }

    ngOnInit(): void {
        $('body').addClass('login-page');
    }
}
