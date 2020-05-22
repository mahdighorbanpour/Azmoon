import { Component, OnInit, Injector } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
    PagedListingComponentBase,
    PagedRequestDto
} from 'shared/paged-listing-component-base';
import { QuestionDto, AdminQuestionServiceProxy, DictionaryDto, AdminCategoryServiceProxy, ListQuestionDtoPagedResultDto, ListQuestionDto, QuestionType } from '@shared/service-proxies/service-proxies';
import { CreateOrUpdateQuestionDialogComponent } from './create-update/create-update-question-dialog.component';

class PagedQuestionsRequestDto extends PagedRequestDto {
    filter: string;
    categoryId: number;
    isPublic: boolean;
    questionType: QuestionType;
}

@Component({
    animations: [appModuleAnimation()],
    templateUrl: './question.component.html'
})
export class AdminQuestionsComponent extends PagedListingComponentBase<QuestionDto> {

    entityList: ListQuestionDto[] = [];
    filter = '';
    questionType: QuestionType;
    categoryId: number;
    isPublic: boolean;
    categories: DictionaryDto[] = undefined;

    constructor(injector: Injector,
        private _service: AdminQuestionServiceProxy,
        private _categoryService: AdminCategoryServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
        this.setTitle(this.l('Questions'));
    }

    getCategoriesDictionary() {
        if (this.categories == undefined) {
            this.setSelectIsLoading('categories');
            this._categoryService.getDictionary()
                .pipe(
                    finalize(() => {
                        this.clearSelectIsLoading();
                    })
                )
                .subscribe((result: DictionaryDto[]) => {
                    this.categories = result;
                });
        }
    }

    list(
        request: PagedQuestionsRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.filter = this.filter;
        request.categoryId = this.categoryId;
        request.questionType = this.questionType;
        request.isPublic = this.isPublic;
        this._service
            .getAll(request.filter, request.categoryId, request.isPublic, request.questionType, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: ListQuestionDtoPagedResultDto) => {
                this.entityList = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    delete(entity: QuestionDto): void {
        abp.message.confirm(
            this.l('QuestionDeleteWarningMessage', entity.title),
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

    edit(entity: QuestionDto): void {
        this.showCreateOrEditDialog(entity.id);
    }

    showCreateOrEditDialog(id?: any): void {
        let createOrEditDialog = this._dialog.open(CreateOrUpdateQuestionDialogComponent, {
            width: '70vw',
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
                err => { }
            );
    }

    rejectIsPublic(id: any) {
        this._service.rejectIsPublic(id)
            .subscribe(
                res => {
                    abp.notify.success(this.l('SuccessfullyRejected'));
                    this.refresh();
                },
                err => { }
            );
    }

    resetIsPublic(id: any) {
        this._service.resetIsPublic(id)
            .subscribe(
                res => {
                    abp.notify.success(this.l('SuccessfullyReseted'));
                    this.refresh();
                },
                err => { }
            );
    }
}
