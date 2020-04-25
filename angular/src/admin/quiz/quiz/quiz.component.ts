import { Component, OnInit, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
    PagedListingComponentBase,
    PagedRequestDto
} from 'shared/paged-listing-component-base';
import { QuizDto, AdminQuizServiceProxy, QuizDtoPagedResultDto } from '@shared/service-proxies/service-proxies';
import { CreateOrUpdateQuizDialogComponent } from './create-update/create-update-quiz-dialog.component';

class PagedQuizzesRequestDto extends PagedRequestDto {
    filter: string;
    isActive: boolean;
}

@Component({
    animations: [appModuleAnimation()],
    templateUrl: './quiz.component.html'
})
export class AdminQuizzesComponent extends PagedListingComponentBase<QuizDto> {

    entityList: QuizDto[] = [];
    filter = '';
    isActive: boolean;
    constructor(injector: Injector,
        private _service: AdminQuizServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }

    list(
        request: PagedQuizzesRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {

        request.filter = this.filter;

        this._service
            .getAll(request.filter, request.isActive, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: QuizDtoPagedResultDto) => {
                this.entityList = result.items;
                console.log(this.entityList);
                this.showPaging(result, pageNumber);
            });
    }

    delete(entity: QuizDto): void {
        abp.message.confirm(
            this.l('QuizDeleteWarningMessage', entity.title),
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

    edit(entity: QuizDto): void {
        this.showCreateOrEditDialog(entity.id);
    }

    showCreateOrEditDialog(id?: any): void {
        let createOrEditDialog = this._dialog.open(CreateOrUpdateQuizDialogComponent, {
            width: '600px',
            data: id
        });
        createOrEditDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}
