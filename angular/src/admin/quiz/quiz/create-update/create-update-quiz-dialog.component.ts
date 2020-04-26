import { Component, Injector, OnInit, Optional, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  AdminQuizServiceProxy,
  QuizDto,
  CreateUpdateQuizDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-update-quiz-dialog.component.html',
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
export class CreateOrUpdateQuizDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  entity: CreateUpdateQuizDto = new CreateUpdateQuizDto();
  dialogTitle: string = '';

  constructor(
    injector: Injector,
    public _service: AdminQuizServiceProxy,
    private _dialogRef: MatDialogRef<CreateOrUpdateQuizDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: string
  ) {
    super(injector);
  }

  ngOnInit(): void {
    if (this._id == undefined || this._id == '') {
      this.dialogTitle = this.l("CreateNewQuiz");
    }
    else {
      this.dialogTitle = this.l("EditQuiz");
      this._service.get(this._id).subscribe((result: QuizDto) => {
        this.entity = result;
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

  close(result: any): void {
    this._dialogRef.close(result);
  }
}
