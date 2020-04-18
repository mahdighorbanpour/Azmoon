import { Component, Injector, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
    templateUrl: './admin-topbar.component.html',
    selector: 'admin-top-bar',
    encapsulation: ViewEncapsulation.None
})
export class AdminTopBarComponent extends AppComponentBase {

    constructor(
        injector: Injector
    ) {
        super(injector);
    }
}
