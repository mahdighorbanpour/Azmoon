import { Component, Injector, OnInit, Optional, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  CategoryServiceProxy,
  CategoryDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-update-category-dialog.component.html',
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
export class CreateOrUpdateCategoryDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  entity: CategoryDto = new CategoryDto();
  dialogTitle: string = '';

  constructor(
    injector: Injector,
    public _service: CategoryServiceProxy,
    private _dialogRef: MatDialogRef<CreateOrUpdateCategoryDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
  ) {
    super(injector);
  }

  ngOnInit(): void {
    if (this._id == undefined || this._id <= 0) {
      this.dialogTitle = this.l("CreateNewCategory");
    }
    else {
      this.dialogTitle = this.l("EditCategory");
      this._service.get(this._id).subscribe((result: CategoryDto) => {
        this.entity = result;
      });
    }

  }

  save(): void {
    this.saving = true;

    let createOrUpdate = this.entity.id == undefined || this.entity.id <= 0 ?
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
