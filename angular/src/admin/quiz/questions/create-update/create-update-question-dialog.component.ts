import { Component, Injector, OnInit, Optional, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  AdminQuestionServiceProxy,
  QuestionDto,
  CreateUpdateQuestionDto,
  AdminCategoryServiceProxy,
  DictionaryDto,
  CreateUpdateChoiceDto,
  ChoiceDto,
  BlankDto,
  MatchSetDto,
} from '@shared/service-proxies/service-proxies';
import { QuestionType } from '@shared/dtos/questionType';

const blankStartIndicator = '[(';
const blankEndIndicator = ')]';

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
  canAddNewChoice: boolean = true;
  canSetIsCorrect: boolean = true;
  hasMoreInputs: boolean = false;
  QuestionType = QuestionType;

  constructor(
    injector: Injector,
    private _categoryService: AdminCategoryServiceProxy,
    public _service: AdminQuestionServiceProxy,
    private _dialog: MatDialog,
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
        this.questionTypeChanged();
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

  async save() {
    let isPublicIssue = await this.checkForIncompatibleIsPublic();
    if (isPublicIssue && !confirm(this.l("IncompatibleIsPublicCategoryWarning"))) {
      return;
    }
    if (this.entity.questionType == QuestionType.FillInTheBlank) {
      this.checkBlanks();
    }
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
    newChoice.questionId = this.entity.id;
    newChoice.value = " ";

    this.entity.choices.push(newChoice);
    if (this.entity.questionType == QuestionType.Ordering) // Ordering
    {
      this.setOrderNo();
    }
  }

  removeChoice(choice: CreateUpdateChoiceDto) {
    let idx = this.entity.choices.findIndex(c => c == choice);
    this.entity.choices.splice(idx, 1);
  }

  addBlank(choice: CreateUpdateChoiceDto, input: any) {
    if (choice.value == undefined) {
      choice.value = '';
    }
    let position = input.selectionStart;
    let before = choice.value.substr(0, position);
    let after = choice.value.substr(position, choice.value.length - position);
    choice.value = before + blankStartIndicator + blankEndIndicator + after;

    // set new position of caret
    setTimeout(() => {
      this.setCaretPosition(input.id, position + blankStartIndicator.length);
    }, 100);
  }

  questionTypeChanged() {
    this.canAddNewChoice = true;
    this.canSetIsCorrect = true;
    this.hasMoreInputs = false;

    switch (this.entity.questionType) {
      case QuestionType.Ordering:
        this.entity.randomizeChoices = true;
        this.canSetIsCorrect = false;
        this.setOrderNo();
        break;
      case QuestionType.ShortAnswer:
        if (this.entity.choices == undefined || this.entity.choices.length == 0) {
          this.addChoice();
        }
        this.entity.choices[0].isCorrect = true;
        this.canAddNewChoice = false;
        this.canSetIsCorrect = false;
        break;
      case QuestionType.FillInTheBlank:
        this.canSetIsCorrect = false;
        break;
      case QuestionType.Matching:
          this.canSetIsCorrect = false;
          this.hasMoreInputs = true;
          break;
    }
  }

  private checkBlanks() {
    if (this.entity.questionType != QuestionType.FillInTheBlank || this.entity.choices.length == 0)
      return;
    this.entity.choices.forEach((choice) => {
      choice.blanks = [];
      let pos = 0;
      let blankStartPos = choice.value.indexOf(blankStartIndicator, pos);
      let blankEndPos = choice.value.indexOf(blankEndIndicator, pos);
      while (blankStartPos != -1 && blankEndPos != -1) {
        let blankValue = choice.value.substring((blankStartPos + blankStartIndicator.length), blankEndPos);
        if (blankValue != undefined && blankValue.length > 0) {
          let blank = new BlankDto();
          blank.answer = blankValue;
          blank.choiceId = choice.id;
          blank.index = blankStartPos;
          choice.blanks.push(blank);
        }
        else {
          // let's remove not useful blank indicators
          let before = choice.value.substring(0, blankStartPos);
          let after = choice.value.substring(blankStartPos + blankStartIndicator.length + blankEndIndicator.length);
          choice.value = before + after;
          pos -= blankStartIndicator.length + blankEndIndicator.length;
        }
        pos = blankEndPos + 1;
        blankStartPos = choice.value.indexOf(blankStartIndicator, pos);
        blankEndPos = choice.value.indexOf(blankEndIndicator, pos);
      }
    })
  }

  private setOrderNo() {
    if (this.entity.choices) {
      for (let i = 0; i < this.entity.choices.length; i++)
        this.entity.choices[i].orderNo = i;
    }
  }

  private async checkForIncompatibleIsPublic(): Promise<boolean> {
    if (this.entity.isPublic) {
      let category = await this._categoryService
        .get(this.entity.categoryId)
        .toPromise();
      return !(category.isPublic && category.isApproved);
    }
    return false;
  }

  setCaretPosition(elemId, caretPos) {
    let elem: HTMLInputElement = <HTMLInputElement>document.getElementById(elemId);
    if (elem != null) {
      if (elem['createTextRange']) {
        var range = elem['createTextRange']();
        range.move('character', caretPos);
        range.select();
      }
      else {
        if (elem.selectionStart) {
          elem.focus();
          elem.setSelectionRange(caretPos, caretPos);
        }
        else
          elem.focus();
      }
    }
  }

  addMatchSet() {
    if (this.entity.matchSets == undefined)
      this.entity.matchSets = [];
    let newMatchSet = new MatchSetDto();
    newMatchSet.questionId = this.entity.id;
    newMatchSet.value = " ";

    this.entity.matchSets.push(newMatchSet);
  }

  removeMatchSet(choice: MatchSetDto) {
    let idx = this.entity.matchSets.findIndex(c => c == choice);
    this.entity.matchSets.splice(idx, 1);
  }

  compareFn(m1: MatchSetDto, m2: MatchSetDto): boolean {
    return m1 && m2 ? m1.id === m2.id : m1 === m2;
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }
}
