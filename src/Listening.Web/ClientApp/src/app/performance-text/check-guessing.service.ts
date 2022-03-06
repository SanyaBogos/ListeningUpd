import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { DiagramElement } from './models/diagram';

@Injectable()
export class CheckGuessingService {

  private checkWordsStartSource = new Subject<string[]>();
  private getCheckWordsResultSource = new Subject<string[]>();
  private finishResultSource = new Subject<DiagramElement[]>();

  finishResult$ = this.finishResultSource.asObservable();
  checkWordsStart$ = this.checkWordsStartSource.asObservable();
  getCheckWordsResult$ = this.getCheckWordsResultSource.asObservable();

  constructor() { }

  announceCheckWordsStart(wordsToSend: string[]) {
    this.checkWordsStartSource.next(wordsToSend);
  }

  announceGettingCheckWordsResult(correctWords: string[]) {
    this.getCheckWordsResultSource.next(correctWords);
  }

  // for now it`s just true, but later it should send calculated results
  announceFinish(diagramData: DiagramElement[]) {
    this.finishResultSource.next(diagramData);
  }
}
