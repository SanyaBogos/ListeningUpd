// tslint:disable:curly
import {
  Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef,
  OnDestroy,
  HostListener
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WordClient, TextForGuessingDto, ResultUpdateTimeDto, ResultClient } from '../../../apiDefinitions';
import { TextDescription } from '../models/textDescription';
import { switchMap } from 'rxjs/operators';
import { BuildWordService } from '../../shared/services/build-word.service';
import { CheckGuessingService } from '../check-guessing.service';
import { BaseSubscriptionsComponent } from '@app/shared/components/base-subscriptions/base-subscriptions.component';

@Component({
  selector: 'appc-main-text',
  templateUrl: './main-text.component.html',
  styleUrls: ['./main-text.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [WordClient]
})
export class MainTextComponent extends BaseSubscriptionsComponent implements OnInit, OnDestroy {

  private _time: Date;
  private _result: ResultUpdateTimeDto;
  private _isFinished: boolean = false;
  
  public shiftFromTop = '0';
  public type: string;
  public textDescription: TextDescription;

  public textForGuessing: TextForGuessingDto;

  constructor(
    private _ref: ChangeDetectorRef,
    private _route: ActivatedRoute,
    private _wordClient: WordClient,
    private _resultClient: ResultClient,
    private _checkGuessingService: CheckGuessingService,
    private _buildWordService: BuildWordService
  ) {
    super();
    this._subscriptions.add(
      this._checkGuessingService.finishResult$.subscribe(this._handleFinish.bind(this))
    );
  }

  ngOnInit() {
    const self = this;

    this._route.params.pipe(
      switchMap((params: TextDescription) => {
        self.textDescription = params;
        self.type = params.textFormType;
        self._result = <ResultUpdateTimeDto>{ textId: params.textId, mode: params.textFormType };
        return self._wordClient.getWordsCountInParagraphs(params.textId, self.type);
      }))
      .subscribe(data => {
        self.textForGuessing = data;

        if (!self.textForGuessing.errorsForJoined)
          self.textForGuessing.errorsForJoined = [];

        self._buildWordService.failedAttempts = self.textForGuessing.errorsForJoined;
        self._time = new Date();
        self._ref.markForCheck();
      });
  }

  ngOnDestroy(): void {
    this._blured();

    super.ngOnDestroy();
    this.textDescription = null;
    this.textForGuessing = null;
  }

  @HostListener('window:blur', ['$event'])
  onBlur(): void {
    this._blured();
  }

  @HostListener('window:focus', ['$event'])
  onFocus(): void {
    this._focused();
  }

  setShiftFromTop(height: number) {
    this.shiftFromTop = `${height}px`;
    this._ref.detectChanges();
  }

  private _handleFinish(): void {
    this._isFinished = true;
    this._blured();
  }

  private _focused(): void {
    if (this._isFinished)
      return;

    this._time = new Date();
  }

  private _blured(): void {
    if (this._isFinished)
      return;

    const newTime = new Date();
    const diff = newTime.getTime() - this._time.getTime();

    this._result.timeSpent = diff;
    this._resultClient.updateLeaveTime(this._result)
      .subscribe(() => { });
  }
}
