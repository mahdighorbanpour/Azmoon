import { Component, Injector, OnInit, Optional, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  AdminQuestionServiceProxy,
  QuestionDto,
  CreateUpdateQuestionDto,
  AdminCategoryServiceProxy,
  DictionaryDto,
  CreateUpdateChoiceDto,
  QuestionType
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-update-question-dialog.component.html',
  styles: [
    `
      mat-form-field {
        width: 100%;
      }
      mat-checkbox {
        padding-bottom: 5px;
      }
    `
  ]
})
export class CreateOrUpdateQuestionDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  entity: CreateUpdateQuestionDto = new CreateUpdateQuestionDto();
  dialogTitle: string = '';
  categories: DictionaryDto[] = undefined;
  questionTypes: DictionaryDto[] = undefined;

  constructor(
    injector: Injector,
    private _categoryService: AdminCategoryServiceProxy,
    public _service: AdminQuestionServiceProxy,
    private _dialogRef: MatDialogRef<CreateOrUpdateQuestionDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: string
  ) {
    super(injector);
  }

  ngOnInit(): void {
    if (this._id == undefined || this._id == '') {
      this.dialogTitle = this.l("CreateNewQuestion");
    }
    else {
      this.dialogTitle = this.l("EditQuestion");
      this._service.get(this._id).subscribe((result: QuestionDto) => {
        this.entity = result;
        console.log(this.entity)
      });
    }

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

  getQuestionTypesDictionary() {
    if (this.questionTypes == undefined) {
      this.setSelectIsLoading('questionTypes');
      this._service.getQuestionTypesDictionary()
        .pipe(
          finalize(() => {
            this.clearSelectIsLoading();
          })
        )
        .subscribe((result: DictionaryDto[]) => {
          this.questionTypes = result;
        });
    }
  }

  save(): void {
    this.saving = true;

    let createOrUpdate = this.entity.id == undefined || this.entity.id == '' ?
      this._service.create(this.entity) :
      this._service.update(this.entity);

    createOrUpdate.pipe(
      finalize(() => {
        this.saving = false;
      })
    )
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close(true);
      });
  }

  addChoice() {
    if (this.entity.choices == undefined)
      this.entity.choices = [];
    let newChoice = new CreateUpdateChoiceDto();
    this.entity.choices.push(newChoice);
    if (this.entity.questionType == QuestionType._3) // Ordering
    {
      this.setOrderNo();
    }
  }

  removeChoice(choice: CreateUpdateChoiceDto) {
    let idx = this.entity.choices.findIndex(c => c == choice);
    this.entity.choices.splice(idx, 1);
  }

  questionTypeChanged() {
    if (this.entity.questionType == QuestionType._3) // Ordering
    {
      this.entity.randomizeChoices = true;
      this.setOrderNo();
    }
  }

  private setOrderNo() {
    if (this.entity.choices)
    {
      for (let i = 0; i < this.entity.choices.length; i++)
        this.entity.choices[i].orderNo = i;
    }
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }
}
