import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
    PagedListingComponentBase,
    PagedRequestDto
} from 'shared/paged-listing-component-base';
import { CategoryDto, AdminCategoryServiceProxy, CategoryDtoPagedResultDto, CreateUpdateCategoryDto } from '@shared/service-proxies/service-proxies';
import { CreateOrUpdateCategoryDialogComponent } from './create-update/create-update-category-dialog.component';
import { IMayBePublicApproval } from 'admin/shared/IMayBePublicApproval';

class PagedCategoriesRequestDto extends PagedRequestDto {
    filter: string;
}

@Component({
    animations: [appModuleAnimation()],
    templateUrl: './categories.component.html'
})
export class CategoriesComponent extends PagedListingComponentBase<CategoryDto> implements IMayBePublicApproval{

    entityList: CategoryDto[] = [];
    filter = '';

    constructor(injector: Injector,
        private _service: AdminCategoryServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
        this.setTitle(this.l('Categories'));
    }

    list(
        request: PagedCategoriesRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {

        request.filter = this.filter;

        this._service
            .getAll(request.filter, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: CategoryDtoPagedResultDto) => {
                this.entityList = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    delete(entity: CategoryDto): void {
        abp.message.confirm(
            this.l('CategoryDeleteWarningMessage', entity.title),
            undefined,
            (result: boolean) => {
                if (result) {
                    this._service
                        .delete(entity.id)
                        .pipe(
                            finalize(() => {
                                abp.notify.success(this.l('SuccessfullyDeleted'));
                                this.refresh();
                            })
                        )
                        .subscribe(() => { });
                }
            }
        );
    }

    create(): void {
        this.showCreateOrEditDialog();
    }

    edit(entity: CreateUpdateCategoryDto): void {
        this.showCreateOrEditDialog(entity.id);
    }

    showCreateOrEditDialog(id?: number): void {
        let createOrEditDialog = this._dialog.open(CreateOrUpdateCategoryDialogComponent, {
            width: '600px',
            data: id
        });
        createOrEditDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }

    approveIsPublic(id: any) {
        this._service.approveIsPublic(id)
        .subscribe(
          res => {
              abp.notify.success(this.l('SuccessfullyApproved'));
              this.refresh();
          },
          err => {}
        );
    }

    rejectIsPublic(id: any) {
        this._service.rejectIsPublic(id)
          .subscribe(
            res => {
                abp.notify.success(this.l('SuccessfullyRejected'));
                this.refresh();
            },
            err => {}
          );
    }

    resetIsPublic(id: any) {
        this._service.resetIsPublic(id)
          .subscribe(
            res => {
                abp.notify.success(this.l('SuccessfullyReseted'));
                this.refresh();
            },
            err => {}
          );
    }
}
