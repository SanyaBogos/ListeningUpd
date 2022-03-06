// tslint:disable:curly
import { Component, ChangeDetectionStrategy, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { WordClient } from '../../../apiDefinitions';
import { CheckGuessingService } from '../check-guessing.service';
import { BuildWordService } from '../../shared/services/build-word.service';
import { LetterCssTypeName } from '../models/cssClasses';
import { AbstractTextComponent } from '../abstract-text/abstract-text.component';
import { ChartEventsService } from '../../shared/services/chart-events.service';
import { DiagramElementType } from '../models/diagram';
import { BuildResultService } from '../build-result.service';
import { ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';

@Component({
  selector: 'appc-joined-text',
  templateUrl: './joined-text.component.html',
  styleUrls: [
    './joined-text.component.scss',
    '../abstract-text/abstract-text.component.scss',
    '../../shared/styles/common-btns.scss'
  ],
  providers: [WordClient],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class JoinedTextComponent extends AbstractTextComponent implements OnDestroy {

  constructor(
    public wordClient: WordClient,
    public ref: ChangeDetectorRef,
    public checkGuessingService: CheckGuessingService,
    public buildWordService: BuildWordService,
    public chartEventsService: ChartEventsService,
    public buildResultService: BuildResultService,
    public route: ActivatedRoute
  ) {
    super(wordClient, ref, buildWordService, chartEventsService, buildResultService, route);
    this.subscriptions.add(
      checkGuessingService.checkWordsStart$.subscribe(this._checkWords.bind(this)),
      checkGuessingService.finishResult$.subscribe(this.calculateResults.bind(this))
    );
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  private _checkWords(wordsToSend: string[]) {
    const self = this;
    const wordsToCheck = _.difference(wordsToSend, this.buildWordService.failedAttempts);

    if (wordsToCheck.length === 0)
      return;

    this.wordClient.postCheckWords(this.textDescription.textId, wordsToCheck)
      .subscribe(correctWords => {
        let guessedCount = 0;

        correctWords.forEach(correctWord => {
          correctWord.locators.forEach(locator => {
            self.paragraphs[locator.paragraphIndex].words[locator.wordIndex]
              .letters.forEach(letter => {
                if (letter.className === LetterCssTypeName.hiddenSymbolClass) {
                  const index = Number(letter.symbol);
                  letter.symbol = index === 1 && locator.isCapital
                    ? correctWord.word[index - 1].toUpperCase() : correctWord.word[index - 1];
                  letter.className = LetterCssTypeName.guessedSymbolClass;
                  guessedCount++;
                }
              });
          });
        });
        self.checkGuessingService.announceGettingCheckWordsResult(
          correctWords.map(w => w.word));

        self.chartEventsService.announceCountsIncrement(DiagramElementType.Guessed, guessedCount);

        const errorsCount = wordsToCheck.length - guessedCount;

        if (errorsCount > 0)
          self.chartEventsService.announceErrorCountIncrement(errorsCount);

        self.ref.detectChanges();
      });
  }

}
