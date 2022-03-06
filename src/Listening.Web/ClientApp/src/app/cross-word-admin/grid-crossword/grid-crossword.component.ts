import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { BaseSubscriptionsComponent } from '@app/shared/components/base-subscriptions/base-subscriptions.component';
import { ChangeGridService } from '../change-grid.service';
import { LetterBtn } from '../models/letter';
import { Position } from '../models/position';
import { Settings, SizeSettings, WordSettings } from '../models/settings';

@Component({
  selector: 'appc-grid-crossword',
  templateUrl: './grid-crossword.component.html',
  styleUrls: ['./grid-crossword.component.scss', '../../shared/styles/common-btns.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GridCrosswordComponent extends BaseSubscriptionsComponent implements OnInit, OnDestroy {

  private _wordToAddSettings: WordSettings;
  private _currentWord: LetterBtn[] = [];
  private _words: LetterBtn[][] = [];

  private _generateWordActions: { [key: string]: Function; };
  private _generateCheckPositionActions: { [key: string]: Function; };

  @Input() settings: Settings;

  public cols: number[];
  public rows: number[];


  constructor(
    private _ref: ChangeDetectorRef,
    private _changeGridService: ChangeGridService
  ) {
    super();

    this._generateWordActions = {
      'r': (i: number, j: number, k: number, l: string) => this._currentWord.push(<LetterBtn>{ rowIndex: i, colIndex: j + k, letter: l }),
      'd': (i: number, j: number, k: number, l: string) => this._currentWord.push(<LetterBtn>{ rowIndex: i + k, colIndex: j, letter: l }),
      'l': (i: number, j: number, k: number, l: string) => this._currentWord.push(<LetterBtn>{ rowIndex: i, colIndex: j - k, letter: l }),
      'u': (i: number, j: number, k: number, l: string) => this._currentWord.push(<LetterBtn>{ rowIndex: i - k, colIndex: j, letter: l })
    };

    this._subscriptions.add(
      this._changeGridService.changeGridSize$.subscribe(this._handleChangeGridSize.bind(this)),
      this._changeGridService.addWord$.subscribe(this._handleAddWord.bind(this))
    );
  }

  ngOnInit() {
    const { height, width } = this.settings.size;
    this._init(height, width);
  }

  mouseEnter(i: number, j: number) {
    const { name, direction } = this._wordToAddSettings;

    if (!this._wordToAddSettings || !this._generateCheckPositionActions[direction](i, j, name.length))
      return;

    this._currentWord.length = 0;

    for (let k = 0; k < name.length; k++) {
      this._generateWordActions[direction](i, j, k, name[k]);
    }
  }

  // mouseLeave(i: number, j: number) {
  //   // console.log(event);
  //   console.log(`${i} ${j}`);
  // }

  getClass(i: number, j: number) {
    if (!this._currentWord || this._currentWord.length < 0)
      return '';

    for (let k = 0; k < this._currentWord.length; k++) {
      if (this._currentWord[k].rowIndex === i && this._currentWord[k].colIndex === j)
        return 'btn-lw-warn';
    }

    return '';
  }

  getLetter(i: number, j: number) {
    const spaceSymbol = '';

    if (!this._currentWord || this._currentWord.length < 0)
      return spaceSymbol;

    for (let k = 0; k < this._currentWord.length; k++) {
      if (this._currentWord[k].rowIndex === i && this._currentWord[k].colIndex === j)
        return this._currentWord[k].letter;
    }

    return spaceSymbol;
  }

  saveWordPosition(i: number, j: number) {
    this._words.push(this._currentWord);
    this._wordToAddSettings = null;
    this._changeGridService.announceWordAddedToPositionSource(<Position>{ row: i, col: j });
    this._ref.markForCheck();
  }

  private _handleChangeGridSize(sizeSettings: SizeSettings) {
    const { height, width } = sizeSettings;
    this._init(height, width);
  }


  private _handleAddWord(wordSettings: WordSettings) {
    this._wordToAddSettings = wordSettings;


    this._ref.markForCheck();
  }

  private _init(height: number, width: number) {
    this.rows = Array.from({ length: height++ }, (v, i) => i);
    this.cols = Array.from({ length: width++ }, (v, i) => i);

    this._generateCheckPositionActions = {
      'r': (i: number, j: number, length: number) => j + length < width,
      'd': (i: number, j: number, length: number) => i + length < height,
      'l': (i: number, j: number, length: number) => j - length > -2,
      'u': (i: number, j: number, length: number) => i - length > -2
    };

    this._ref.markForCheck();
  }

  private _isCorrectPosition() {

  }

}
