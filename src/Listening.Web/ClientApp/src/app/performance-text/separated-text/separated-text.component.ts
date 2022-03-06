// tslint:disable:curly
import {
  Component, ChangeDetectionStrategy, ChangeDetectorRef, AfterContentInit,
  HostListener, OnDestroy
} from '@angular/core';
import { WordClient } from '../../../apiDefinitions';
import { BuildWordService } from '../../shared/services/build-word.service';
import { AbstractTextComponent } from '../abstract-text/abstract-text.component';
import { Word, Position, WordEnhanced, Letter } from '../models/locators';
import { LetterCssTypeName } from '../models/cssClasses';
import { HotkeysService } from '../hotkeys.service';
import { CheckGuessingService } from '../check-guessing.service';
import { SeparatedTextService } from '../separated-text.service';
import { ChartEventsService } from '../../shared/services/chart-events.service';
import { DiagramElementType } from '../models/diagram';
import { BuildResultService } from '../build-result.service';
import { ActivatedRoute } from '@angular/router';
import { ActionStrEventCombination, ActionStrEventCombinationDictionary, KeyStrCombinations } from '../models/keyStrCombinations';
import * as _ from 'underscore';
import * as $ from 'jquery';

@Component({
  selector: 'appc-separated-text',
  templateUrl: './separated-text.component.html',
  styleUrls: [
    './separated-text.component.scss',
    '../abstract-text/abstract-text.component.scss',
    '../../shared/styles/common-btns.scss'
  ],
  providers: [WordClient],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SeparatedTextComponent extends AbstractTextComponent implements OnDestroy, AfterContentInit {
  private currentWord = '';
  private inputClassNames: string[];
  private componentName = 's';
  public currentPosition: Position;

  constructor(
    private hotkeysService: HotkeysService,
    private separatedTextService: SeparatedTextService,
    public wordClient: WordClient,
    public ref: ChangeDetectorRef,
    public buildWordService: BuildWordService,
    public checkGuessingService: CheckGuessingService,
    public chartEventsService: ChartEventsService,
    public buildResultService: BuildResultService,
    public route: ActivatedRoute
  ) {
    super(wordClient, ref, buildWordService, chartEventsService, buildResultService, route);

    this.subscriptions.add(
      checkGuessingService.finishResult$.subscribe(this.calculateResults.bind(this))
    );
  }

  ngAfterContentInit() {
    this.currentPosition = this.separatedTextService.findFirstPosition(this.paragraphs);
    this.buildInputClassNames();

    const actionsEvents = [
      new ActionStrEventCombination(KeyStrCombinations.tab, () => this._goNext()),
      new ActionStrEventCombination(KeyStrCombinations.shiftTab, () => this._goPrev())
    ];

    const actionsEventsDictionary = new ActionStrEventCombinationDictionary(
      this.componentName, actionsEvents);

    this.hotkeysService.addEvents(actionsEventsDictionary);
  }

  ngOnDestroy(): void {
    this.hotkeysService.releaseAll();
    this.hotkeysService.removeEvents(this.componentName);

    super.ngOnDestroy();
  }

  private getInputClass(word: Word): string {
    return `edit-box-word edit-box-length-${word.letters.length}`;
  }

  private buildInputClassNames() {
    const { paragraphIndex, wordIndex } = this.currentPosition;
    this.inputClassNames = [this.getInputClass(this.paragraphs[paragraphIndex].words[wordIndex])];
  }

  discardChanges() {
    this.currentWord = '';
  }

  discardBtnDisabled() {
    return this.currentWord.length === 0;
  }

  isOkBtnDisabled(word: Word): boolean {
    return word.letters.length !== this.currentWord.length;
  }

  keyPress(word: WordEnhanced, iParagraph: number, iWord: number, key: string) {
    if (!this.isOkBtnDisabled(word) && key === 'Enter')
      this.checkWord(word, iParagraph, iWord);
  }

  checkWord(word: WordEnhanced, iParagraph: number, iWord: number) {

    if (word.failedAttempts.indexOf(this.currentWord) === -1) {
      const self = this;

      this.wordClient.getWordCorrectness(this.textDescription.textId, iParagraph,
        iWord, this.currentWord)
        .subscribe(data => {
          if (!data) {
            self.inputClassNames.push('red-bg-color');
            self.ref.markForCheck();
            setTimeout(() => { self.inputClassNames.push('transition-to-white-bg'); self.ref.markForCheck(); }, 50);
            setTimeout(() => { self.inputClassNames.pop(); self.inputClassNames.pop(); self.ref.markForCheck(); }, 1000);

            word.failedAttempts.push(self.currentWord);
            self.chartEventsService.announceErrorCountIncrement(1);
            // self.failedAttempts.push(this.word);
            if (word.failedAttempts.length <= 3) {
              if (word.failedAttempts.length !== 1)
                word.tooltipClassNames.pop();

              word.tooltipClassNames.push(`margin-top-${word.failedAttempts.length}`);
              self.ref.markForCheck();
            }
            return;
          }

          word.isGuessed = true;
          let guessedCount = 0;

          word.letters.forEach((letter, index) => {
            if (letter.className === LetterCssTypeName.hiddenSymbolClass) {
              letter.symbol = self.currentWord[index];
              letter.className = LetterCssTypeName.guessedSymbolClass;
              guessedCount++;
            }
          });

          self.currentWord = '';
          self.currentPosition = self.separatedTextService.findNext(self.paragraphs, self.currentPosition);
          self.chartEventsService.announceCountsIncrement(DiagramElementType.Guessed, guessedCount);
        });
    }
  }

  setInputVisibility(iParagraph: number, iWord: number) {
    const word = this.paragraphs[iParagraph].words[iWord] as WordEnhanced;

    if (!word.isGuessed && !word.isSign) {
      this.currentPosition = new Position(iParagraph, iWord);
      this.currentWord = '';
      this.buildInputClassNames();
    }

    this.ref.markForCheck();
  }

  checkInputVisibility(iParagraph: number, iWord: number, shouldMakeFocus: boolean = false) {
    if (this.currentPosition == null)
      return false;

    const { paragraphIndex, wordIndex } = this.currentPosition;
    const isVisible = paragraphIndex === iParagraph && wordIndex === iWord;

    if (isVisible && shouldMakeFocus) {
      const self = this;
      setTimeout(() => {
        $(`#${iParagraph}_${iWord}`).focus();
        self.ref.markForCheck();
      }, 0);
    }

    return isVisible;
  }

  @HostListener('window:keydown', ['$event'])
  press(event: KeyboardEvent) {
    if (event.key === 'Tab')
      event.preventDefault();

    this.hotkeysService.press(event.key, this.componentName);
  }

  @HostListener('window:keyup', ['$event'])
  release(event: KeyboardEvent) {
    this.hotkeysService.release(event.key);
  }

  @HostListener('window:focus')
  releaseAll() {
    this.hotkeysService.releaseAll();
  }

  letterHinted(paragraphIndex: number, wordIndex: number) {
    const word = this.paragraphs[paragraphIndex].words[wordIndex] as WordEnhanced;
    const isFinishedGuessing = _.all(word.letters,
      (e: Letter) => e.className !== LetterCssTypeName.hiddenSymbolClass);

    if (isFinishedGuessing) {
      word.isGuessed = true;
      this._goNext();
    }
  }

  private _goNext() {
    const next = this.separatedTextService.findNext(this.paragraphs, this.currentPosition.deepClone());
    if (next == null) {
      // this.checkGuessingService.announceFinish(true);
      return;
    }

    this.currentPosition = next;
    this.currentWord = '';
    this.buildInputClassNames();
    this.ref.markForCheck();
  }

  private _goPrev() {
    const prev = this.separatedTextService.findPrev(this.paragraphs, this.currentPosition.deepClone());
    this.currentPosition = prev;
    this.currentWord = '';
    this.buildInputClassNames();
    this.ref.markForCheck();
  }

}
