import { Component, OnInit, Injector } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
    PagedListingComponentBase,
    PagedRequestDto
} from 'shared/paged-listing-component-base';
import { QuizDto, AdminQuizServiceProxy, QuizDtoPagedResultDto, DictionaryDto, AdminCategoryServiceProxy, CategoryDto } from '@shared/service-proxies/service-proxies';
import { CreateOrUpdateQuizDialogComponent } from './create-update/create-update-quiz-dialog.component';

class PagedQuizzesRequestDto extends PagedRequestDto {
    filter: string;
    isActive: boolean;
    categoryId: number;
    isPublic: boolean;
}

@Component({
    animations: [appModuleAnimation()],
    templateUrl: './quiz.component.html'
})
export class AdminQuizzesComponent extends PagedListingComponentBase<QuizDto> {

    entityList: QuizDto[] = [];
    filter = '';
    isActive: boolean;
    categoryId: number;
    isPublic: boolean;
    categories: DictionaryDto[] = undefined;

    constructor(injector: Injector,
        private _service: AdminQuizServiceProxy,
        private _categoryService: AdminCategoryServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
        this.setTitle(this.l('Quizzes'));
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
        request: PagedQuizzesRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.filter = this.filter;
        request.categoryId = this.categoryId;
        request.isActive = this.isActive;
        request.isPublic = this.isPublic;
        this._service
            .getAll(request.filter, request.isActive, request.categoryId, request.isPublic, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: QuizDtoPagedResultDto) => {
                this.entityList = result.items;
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
