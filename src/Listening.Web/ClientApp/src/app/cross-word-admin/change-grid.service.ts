import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { SizeSettings, WordSettings } from './models/settings';
import { Position } from './models/position';
import { QuestionAndWordDescriptionDto } from 'apiDefinitions';

@Injectable()
export class ChangeGridService {

  private changeGridSizeSource = new Subject<SizeSettings>();
  private addWordSource = new Subject<WordSettings>();
  private wordAddedToPositionSource = new Subject<Position>();
  private saveQuestionAndAnswerSource = new Subject<QuestionAndWordDescriptionDto>();

  changeGridSize$ = this.changeGridSizeSource.asObservable();
  addWord$ = this.addWordSource.asObservable();
  wordAddedToPosition$ = this.wordAddedToPositionSource.asObservable();
  saveQuestionAndAnswer$ = this.saveQuestionAndAnswerSource.asObservable();

  constructor() { }

  announceChangeGridSize(sizeSettings: SizeSettings) {
    this.changeGridSizeSource.next(sizeSettings);
  }

  announceAddWord(wordSettings: WordSettings) {
    this.addWordSource.next(wordSettings);
  }

  announceWordAddedToPositionSource(position: Position) {
    this.wordAddedToPositionSource.next(position);
  }

  announceSaveQuestionAndAnswerWord(questionAndWordDescriptionDto: QuestionAndWordDescriptionDto) {
    this.saveQuestionAndAnswerSource.next(questionAndWordDescriptionDto);
  }

}
