import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { BukvitsaService } from '../bukvitsa.service';
import { Bukvitsa } from '../models/bukvitsa';
import * as _ from 'lodash';
import { SettingsBukvitsa } from '../models/settings-bukvitsa';

@Component({
  selector: 'appc-bukvitsa-page',
  templateUrl: './bukvitsa-page.component.html',
  styleUrls: ['./bukvitsa-page.component.scss', '../bukvitsy-table/bukvitsy-table.component.scss', '../../shared/styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BukvitsaPageComponent implements OnInit {
  private _borderClassName: string = 'border border-primary rounded bg-light-blue';

  public showHideBtnName: string = 'hide_bkv';
  public wordOfBukvitsy: Bukvitsa[] = [];
  public partWordOfBukvitsy: string[] = [];
  public isSelectedBukvitsy: boolean[] = [];

  public prevWordSimplified: string;
  public wordSimplified: string;

  public color: string;
  public selectionColor: string;
  public size: number;
  public colSize: number;
  public isBukvitsaVisible: boolean;
  public isSettingsVisible: boolean;
  public isAutoConvert: boolean;

  public settings: SettingsBukvitsa;

  public selectedWordBukvitsaIndex: number = -1;


  constructor(
    private _cdRef: ChangeDetectorRef,
    private _bukvitsaService: BukvitsaService,
  ) {
    this.color = '#FF7518';
    this.selectionColor = '#d7e5f9';
    this.size = 10;
    this.isBukvitsaVisible = true;

    this.settings = new SettingsBukvitsa(this.color, this.selectionColor, this.size, this.isBukvitsaVisible);
    this._cdRef.markForCheck();
  }

  ngOnInit() {
  }

  changeBukvitsaVisibility() {
    this.isBukvitsaVisible = !this.isBukvitsaVisible;
    this.showHideBtnName = this.isBukvitsaVisible ? 'hide_bkv' : 'show_bkv';
  }

  changeAutoConvert() {
    this.isAutoConvert = !this.isAutoConvert;

    if (this.isAutoConvert)
      this.convertText();
  }

  textChanged(event: any) {
    if (event.data == null && event.inputType.includes('delete')) {
      let startRemove: number = -1;
      let finishRemove: number = -1;

      for (let i = 0; i < this.prevWordSimplified.length; i++) {
        if (this.prevWordSimplified[i] != this.wordSimplified[i] && startRemove == -1)
          startRemove = i;
        else if (this.prevWordSimplified[i] == this.wordSimplified[startRemove] && startRemove != -1)
          finishRemove = i;
      }

      // TODO: fix (or just check) position
      if (finishRemove == -1)
        this.wordOfBukvitsy.splice(startRemove);
      else if (startRemove != -1 && finishRemove != -1)
        this.wordOfBukvitsy.splice(this._findPosition(startRemove), finishRemove - startRemove);

      this.prevWordSimplified = this.wordSimplified;

      return;
    }

    let letter = event.data;

    if (letter !== '_' && this.partWordOfBukvitsy.length === 0) {
      var result = letter === ' ' ? null : this._bukvitsaService.rusToBukvitsa(letter);
      const position = this._findPosition(event.target.selectionStart - 1);
      this.wordOfBukvitsy.splice(position, 0, ...result);
    } else {
      this.partWordOfBukvitsy.push(letter);

      if (this.partWordOfBukvitsy.length > 2 && this.partWordOfBukvitsy[0] === '_' && this.partWordOfBukvitsy[this.partWordOfBukvitsy.length - 1] === '_') {
        var result = this._bukvitsaService.rusToBukvitsa(letter);
        // TODO: fix position
        this.wordOfBukvitsy.splice(event.target.selectionStart - 1, 0, ...result);
        this.partWordOfBukvitsy.length = 0;
      }
    }

    this.prevWordSimplified = this.wordSimplified;
  }

  private _findPosition(index: number) {
    // letter 'я' takes 2 symbols, therefore need to find shift including this
    let yaCount = 0;

    for (let i = 0; i < index; i++) {
      if (this.wordSimplified[i] == 'я')
        yaCount++;
    }

    //TODO : also check letter such like _ерь_ , _гервь_ 

    return index + yaCount;
  }


  apply() {
    this.settings = new SettingsBukvitsa(this.color, this.selectionColor, this.size, this.isBukvitsaVisible);
    this._bukvitsaService.announceChangeSettings(this.settings);
  }

  addBukvitsa(bukvitsa: Bukvitsa) {
    if (this.selectedWordBukvitsaIndex === -1) {
      this.isSelectedBukvitsy.push(false);
      this.wordOfBukvitsy.push(bukvitsa);
    }
    else {
      const oldBukvitsa = this.wordOfBukvitsy.splice(this.selectedWordBukvitsaIndex, 1, bukvitsa);
      const otherBukvitsa = this.wordOfBukvitsy.find(x => oldBukvitsa[0] === x);

      if (!otherBukvitsa)
        this._bukvitsaService.announceRemoveSelection(oldBukvitsa[0].id);

      this.isSelectedBukvitsy[this.selectedWordBukvitsaIndex] = false;
      this.selectedWordBukvitsaIndex = -1;
    }

    this._cdRef.markForCheck();
  }

  selectedWordBukvitsa(index: number) {
    if (this.selectedWordBukvitsaIndex === index) {
      this.isSelectedBukvitsy[index] = false;
      this.selectedWordBukvitsaIndex = -1;
      this._cdRef.markForCheck();
      return;
    } else if (this.selectedWordBukvitsaIndex > -1) {
      this.isSelectedBukvitsy[this.selectedWordBukvitsaIndex] = false;
    }

    this.selectedWordBukvitsaIndex = index;
    this.isSelectedBukvitsy[index] = true;
    this._cdRef.markForCheck();
  }

  removeWordBukvitsa(index: number) {
    const bukvitsa = this.wordOfBukvitsy.splice(index, 1);
    const otherBukvitsa = this.wordOfBukvitsy.find(x => bukvitsa[0] === x);

    if (!otherBukvitsa)
      this._bukvitsaService.announceRemoveSelection(bukvitsa[0].id);
  }

  convertText() {
    this.wordOfBukvitsy = this._bukvitsaService.rusToBukvitsa(this.wordSimplified);
  }

  clear() {
    this.wordSimplified = '';
    this.wordOfBukvitsy.length = 0;
    this._bukvitsaService.announceClear();
    this._cdRef.markForCheck();
  }

  getClassName(index: number) {
    const className = this.isSelectedBukvitsy[index] ? this._borderClassName : '';
    return className;
  }

}
