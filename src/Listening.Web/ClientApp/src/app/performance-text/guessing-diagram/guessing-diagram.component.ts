// tslint:disable:curly
import { Component, OnInit, OnDestroy, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { ChartEventsService } from '../../shared/services/chart-events.service';
import { DiagramElement, DiagramElementType } from '../models/diagram';
import { CheckGuessingService } from '../check-guessing.service';
import { Counts } from '../models/counts';
import { TranslateTextService } from '../translate-text.service';
import { IncrementCount } from '../models/incrementCount';
import { BaseSubscriptionsComponent } from '@app/shared/components/base-subscriptions/base-subscriptions.component';

@Component({
  selector: 'appc-guessing-diagram',
  templateUrl: './guessing-diagram.component.html',
  styleUrls: ['./guessing-diagram.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GuessingDiagramComponent extends BaseSubscriptionsComponent implements OnInit {

  public isVisible = false;

  private data: DiagramElement[];
  public view: number[] = [200, 150];
  public showLegend = false;
  public showLabels = false;
  public explodeSlices = false;
  public doughnut = false;
  public colorScheme = {
    domain: ['#e0e0e0', '#eb9316', '#419641', '#2d6ca2']
  };

  public maxErrorValue: number;
  public errorCount: number;

  constructor(
    private _ref: ChangeDetectorRef,
    private _translateService: TranslateTextService,
    private _chartEventsService: ChartEventsService,
    private _checkGuessingService: CheckGuessingService
  ) {
    super();
    const self = this;

    this._subscriptions.add(
      this._chartEventsService.countsCalculated$.subscribe(this.onCountsCalculated.bind(this)),
      this._chartEventsService.errorCountIncrement$.subscribe(this._handleErrorCountIncrement.bind(this)),
      this._chartEventsService.countsIncrement$.subscribe(this._handleCountsIncrement.bind(this))
    );
  }

  ngOnInit() { }

  private onCountsCalculated(counts: Counts) {

    const hidden = this._translateService.getDiagramElement(DiagramElementType.Hidden, counts.hiddenCount);
    const hinted = this._translateService.getDiagramElement(DiagramElementType.Hinted, counts.hintedCount);
    const guessed = this._translateService.getDiagramElement(DiagramElementType.Guessed, counts.guessedCount);
    const sign = this._translateService.getDiagramElement(DiagramElementType.Sign, counts.signCount);

    this.data = [hidden, hinted, guessed, sign];

    this.isVisible = true;

    const possibleMax = counts.wordsCount * 3;
    this.maxErrorValue = possibleMax < 100 ? possibleMax : 100;
    this.errorCount = counts.errorCount % this.maxErrorValue;

    this._ref.detectChanges();
  }

  private _handleErrorCountIncrement(): void {
    this.errorCount = (this.errorCount + 1) % this.maxErrorValue;
    this._ref.markForCheck();
  }

  private _handleCountsIncrement(incrementCount: IncrementCount): void {
    const { count, fieldForIncrement } = incrementCount;
    let hidden: DiagramElement;
    let hinted: DiagramElement;
    let guessed: DiagramElement;

    if (fieldForIncrement === DiagramElementType.Hinted) {
      hidden = this._translateService.getDiagramElement(DiagramElementType.Hidden, this.data[0].value - count);
      hinted = this._translateService.getDiagramElement(DiagramElementType.Hinted, this.data[1].value + count);
      guessed = this._translateService.getDiagramElement(DiagramElementType.Guessed, this.data[2].value);
    } else if (fieldForIncrement === DiagramElementType.Guessed) {
      hidden = this._translateService.getDiagramElement(DiagramElementType.Hidden, this.data[0].value - count);
      hinted = this._translateService.getDiagramElement(DiagramElementType.Hinted, this.data[1].value);
      guessed = this._translateService.getDiagramElement(DiagramElementType.Guessed, this.data[2].value + count);
    }

    this.data = [hidden, hinted, guessed, this.data[3]];

    if (this.data[0].value === 0)
      this._checkGuessingService.announceFinish(this.data);

    this._ref.detectChanges();
  }

}
