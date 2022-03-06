// tslint:disable:curly
import {
  Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, ElementRef, ViewChild,
  AfterViewInit, EventEmitter, Output, OnDestroy
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TextDescription } from '../models/textDescription';
import { CheckGuessingService } from '../check-guessing.service';
import * as _ from 'underscore';
import { BuildWordService } from '../../shared/services/build-word.service';
import { BrowserDetectService } from '@app/shared/services/browser-detect.service';
import { BaseSubscriptionsComponent } from '@app/shared/components/base-subscriptions/base-subscriptions.component';

@Component({
  selector: 'appc-general-header',
  templateUrl: './general-header.component.html',
  styleUrls: ['./general-header.component.scss'],
  providers: [BrowserDetectService],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GeneralHeaderComponent extends BaseSubscriptionsComponent implements OnInit, AfterViewInit {

  @ViewChild('headerDescription', { static: false }) headerRef: ElementRef<HTMLElement>;
  @Output() heightUpdated = new EventEmitter();

  private text = '';
  private uniqueWords: string[] = [];

  public type: string;
  public failedAttemptsText = '';
  public isFinished = false;
  public isVideo = false;
  public textDescription: TextDescription;
  public isJoinedAndVideoType: boolean;

  constructor(
    private _ref: ChangeDetectorRef,
    private _route: ActivatedRoute,
    private _checkGuessingService: CheckGuessingService,
    private _browserDetectService: BrowserDetectService,
    private _buildWordService: BuildWordService
  ) {
    super();
    this._subscriptions.add(
      this._checkGuessingService.getCheckWordsResult$.subscribe(this.fillFailedAttemtsByCorrectWords.bind(this)),
      this._buildWordService.startOver$.subscribe(this.clean.bind(this)),
      this._checkGuessingService.finishResult$.subscribe(x => {
        this.isFinished = true;
        this._ref.markForCheck();
      })
    );
  }

  ngOnInit() {
    this._route.params
      .subscribe((params: TextDescription) => {
        this.type = params.textFormType;
        this.textDescription = params;
        this.isVideo = params.listeningType === 'v';
        this.isJoinedAndVideoType = this.type === 'j' && this.isVideo;
        this._ref.markForCheck();
      });

    this.failedAttemptsText = this._buildWordService.failedAttempts.join(' ');
  }

  ngAfterViewInit() {
    setTimeout(() => {
      const headerDescriptionHTML = this.headerRef.nativeElement;
      const shift = this._browserDetectService.isFirefox() ? 105 : 118;
      this.heightUpdated.emit((headerDescriptionHTML.clientHeight + shift));
    }, this.isVideo ? 3000 : 100);
  }

  keyPress(key: string) {
    if (key === 'Enter')
      this.startCheckWords();
  }

  startCheckWords() {
    const formattedText = this.text.replace(/[^a-zA-Zа-яА-Я0-9'\- ]/g, '')
      .replace(/ +(?= )/g, '');

    const wordsToSend = formattedText.replace('\'', '`').split(' ');

    this.uniqueWords = formattedText.split(' ')
      .filter(function (item, index, inputArray) {
        return inputArray.indexOf(item) === index;
      });

    this.text = '';
    this._checkGuessingService.announceCheckWordsStart(wordsToSend);
  }

  fillFailedAttemtsByCorrectWords(correctWords: string[]) {
    const diff = _.difference(this.uniqueWords, correctWords);
    const intersect = _.intersection(this._buildWordService.failedAttempts, diff);
    this._buildWordService.failedAttempts =
      this._buildWordService.failedAttempts.concat(_.difference(diff, intersect));
    this.failedAttemptsText = this._buildWordService.failedAttempts.join(' ');
    this.text = '';
    this._ref.markForCheck();
  }

  clean() {
    if (this.type === 'j') {
      this.text = '';
      this.failedAttemptsText = '';
      this._buildWordService.failedAttempts.length = 0;
    }
  }

}
