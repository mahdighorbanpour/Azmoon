import { Injector, ElementRef } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';
import {
    PermissionCheckerService,
    FeatureCheckerService,
    NotifyService,
    SettingService,
    MessageService,
    AbpMultiTenancyService
} from 'abp-ng2-module';
import { AppSessionService } from '@shared/session/app-session.service';
import { Title } from '@angular/platform-browser';
import { AzmoonLocalizationService } from './localization/azmoon.localization';

export abstract class AppComponentBase {

    localizationSourceName = AppConsts.localization.defaultLocalizationSourceName;

    localization: AzmoonLocalizationService;
    permission: PermissionCheckerService;
    feature: FeatureCheckerService;
    notify: NotifyService;
    setting: SettingService;
    message: MessageService;
    multiTenancy: AbpMultiTenancyService;
    appSession: AppSessionService;
    elementRef: ElementRef;
    titleService: Title;

    isSelectLoading = false;
    selectName = '';

    constructor(injector: Injector) {
        this.localization = injector.get(AzmoonLocalizationService);
        this.permission = injector.get(PermissionCheckerService);
        this.feature = injector.get(FeatureCheckerService);
        this.notify = injector.get(NotifyService);
        this.setting = injector.get(SettingService);
        this.message = injector.get(MessageService);
        this.multiTenancy = injector.get(AbpMultiTenancyService);
        this.appSession = injector.get(AppSessionService);
        this.elementRef = injector.get(ElementRef);
        this.titleService = injector.get(Title);
    }

    l(key: string, ...args: any[]): string {
        let localizedText = this.localization.localize(key, this.localizationSourceName);

        if (!localizedText) {
            localizedText = key;
        }

        if (!args || !args.length) {
            return localizedText;
        }

        args.unshift(localizedText);
        return abp.utils.formatString.apply(this, args);
    }

    isGranted(permissionName: string): boolean {
        return this.permission.isGranted(permissionName);
    }

    setTitle(newTitle: string) {
        this.titleService.setTitle(AppConsts.appName + '-'+ newTitle);
    }

    getTitle(): string {
        return this.titleService.getTitle()
        .replace(AppConsts.appName + '-', "");
    }

    setSelectIsLoading(name: string){
        this.isSelectLoading = true;
        this.selectName = name;
    }

    clearSelectIsLoading(){
        this.isSelectLoading = false;
        this.selectName = '';
    }
}
