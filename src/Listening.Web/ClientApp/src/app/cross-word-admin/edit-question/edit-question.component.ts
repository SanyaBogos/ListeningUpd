import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { BaseSubscriptionsComponent } from '@app/shared/components/base-subscriptions/base-subscriptions.component';
import { ModalComponent } from '@app/shared/directives/modal/modal.component';
import { QuestionAndWordDescriptionDto } from 'apiDefinitions';
import { ChangeGridService } from '../change-grid.service';
import { Direction } from '../models/direction';
import { Position } from '../models/position';

@Component({
  selector: 'appc-edit-question',
  templateUrl: './edit-question.component.html',
  styleUrls: ['./edit-question.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EditQuestionComponent extends BaseSubscriptionsComponent implements OnInit {

  @ViewChild(ModalComponent, { static: false })
  private readonly _confirmDeletingModal: ModalComponent;
  
  public directions: Direction[] = [
    { id: 'r', name: 'right' },
    { id: 'd', name: 'down' },
    { id: 'l', name: 'left' },
    { id: 'u', name: 'up' },
  ];

  public isNewElement = true;

  public selectedDirection: string;
  public questionAndWordDescriptionDto: QuestionAndWordDescriptionDto;

  constructor(
    private _ref: ChangeDetectorRef,
    private _changeGridService: ChangeGridService,
  ) {
    super();

    this.selectedDirection = this.directions[0].id;

    this._subscriptions.add(
      this._changeGridService.wordAddedToPosition$.subscribe(this._handleWordAdded.bind(this))

    );

    this._ref.markForCheck();
  }

  ngOnInit() {
    this.questionAndWordDescriptionDto = new QuestionAndWordDescriptionDto();

  }

  checkQuestion() {

  }

  addWordToGrid() {
    this._changeGridService.announceAddWord({
      name: this.questionAndWordDescriptionDto.answer,
      direction: this.selectedDirection
    });
  }

  isSaveDisabled() {
    const { question, answer, startPointX, startPointY } = this.questionAndWordDescriptionDto;
    return !(question && answer && startPointX && startPointY);
  }

  saveQuestion() {
    this._changeGridService.announceSaveQuestionAndAnswerWord(this.questionAndWordDescriptionDto);
    this.questionAndWordDescriptionDto = new QuestionAndWordDescriptionDto();
  }

  removeQuestion() {
    this._confirmDeletingModal.hide();

  }

  private _handleWordAdded(position: Position) {
    this.questionAndWordDescriptionDto.startPointX = position.row;
    this.questionAndWordDescriptionDto.startPointY = position.col;
  }

}
