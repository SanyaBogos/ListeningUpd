// tslint:disable:curly
import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Input, OnDestroy } from '@angular/core';
import { WordClient, TextForGuessingDto } from '../../../apiDefinitions';
import { BuildWordService } from '../../shared/services/build-word.service';
import { TextDescription } from '../models/textDescription';
import { Paragraph, Letter, Identicable } from '../models/locators';
import { LetterCssTypeName } from '../models/cssClasses';
import { MergeTextObject } from '../models/mergeTextObject';
import { ChartEventsService } from '../../shared/services/chart-events.service';
import { DiagramElement, DiagramElementType } from '../models/diagram';
import { BuildResultService } from '../build-result.service';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { MySubscriptions } from '@app/shared/models/mySubscription';

@Component({
  selector: 'appc-abstract-text',
  templateUrl: './abstract-text.component.html',
  styleUrls: ['./abstract-text.component.scss', 
  '../../shared/styles/common.scss',
  
  '../../shared/styles/common-btns.scss'],
  providers: [WordClient],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AbstractTextComponent implements OnInit, OnDestroy {

  @Input() textForGuessing: TextForGuessingDto;
  @Input() textDescription: TextDescription;

  protected resultsData: DiagramElement[];
  protected resultsWordsData: DiagramElement[];
  protected subscriptions = new MySubscriptions();

  public paragraphs: Paragraph[] = [];
  public isFinished = false;

  constructor(
    public wordClient: WordClient,
    public ref: ChangeDetectorRef,
    public buildWordService: BuildWordService,
    public chartEventsService: ChartEventsService,
    public buildResultService: BuildResultService,
    public route: ActivatedRoute
  ) {

    this.subscriptions.add(
      buildWordService.startOver$.subscribe(this._handleStartOver.bind(this))      
    );
  }

  ngOnInit() {
    if (!this.textForGuessing.isStarted) {
      this.paragraphs = this.buildWordService.getHiddenWords(
        this.textForGuessing, this.textDescription.textFormType);
    } else {
      this.paragraphs = this.buildWordService.getMergedWords(
        new MergeTextObject(this.textForGuessing.mergedText,
          this.textForGuessing.resultsEncodedString,
          this.textForGuessing.errorsForJoined,
          this.textForGuessing.errorsForSeparated,
          this.textDescription.textFormType));
    }

    this.ref.markForCheck();
  }

  ngOnDestroy(): void {
    this.subscriptions.remove();
  }

  hintLetter(letter: Letter, paragraphIndex: number, wordIndex: number) {
    if (letter.className.indexOf(LetterCssTypeName.hiddenSymbolClass) < 0)
      return;

    const { textId, textFormType } = this.textDescription;
    const self = this;

    this.wordClient.getLetter(textId, paragraphIndex,
      wordIndex, (Number(letter.symbol) - 1), textFormType)
      .subscribe(data => {
        letter.symbol = data;
        letter.className = LetterCssTypeName.hintedSymbolClass;
        self.letterHinted(paragraphIndex, wordIndex);
        self.chartEventsService.announceCountsIncrement(DiagramElementType.Hinted, 1);
        self.ref.markForCheck();
      });
  }

  calculateResults(resultsDiagramData: DiagramElement[]) {
    this.resultsData = resultsDiagramData;
    this.resultsWordsData = this.buildResultService.buildWordResults(this.paragraphs);
    this.isFinished = true;
    this.ref.markForCheck();
  }

  trackByFn(index: number, obj: Identicable) {
    return obj.id;
  }

  letterHinted(paragraphIndex: number, wordIndex: number) { }

  private _handleStartOver(): void {
    this.route.params.pipe(
      switchMap((params: TextDescription) => {
        return this.wordClient.getWordsCountInParagraphs(params.textId, params.textFormType);
      }))
      .subscribe(data => {
        this.textForGuessing = data;

        if (!this.textForGuessing.errorsForJoined)
          this.textForGuessing.errorsForJoined = [];

        this.ngOnInit();
      });
  }

}
