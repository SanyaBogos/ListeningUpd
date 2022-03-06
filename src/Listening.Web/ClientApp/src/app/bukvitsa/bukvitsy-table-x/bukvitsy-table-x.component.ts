import { ChangeDetectionStrategy, ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BukvitsaService } from '../bukvitsa.service';
import { Bukvitsa } from '../models/bukvitsa';
import { SettingsBukvitsa } from '../models/settings-bukvitsa';

@Component({
  selector: 'appc-bukvitsy-table-x',
  templateUrl: './bukvitsy-table-x.component.html',
  styleUrls: ['./bukvitsy-table-x.component.scss', '../../shared/styles/common-iniline.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BukvitsyTableXComponent implements OnInit {
  private _borderClassName: string = 'border border-primary rounded';

  @Input() settings: SettingsBukvitsa;
  @Output() selectedBukvitsa = new EventEmitter();

  public bukvitsy: Bukvitsa[][];
  public colSize: number;
  public bgColor: string;

  constructor(
    private _cdRef: ChangeDetectorRef,
    private _bukvitsaService: BukvitsaService,
  ) {

    this._bukvitsaService.clear$
      .subscribe(this._clear.bind(this));

    this._bukvitsaService.changeSettings$
      .subscribe(this._updateSettings.bind(this));

    this._bukvitsaService.removeSelection$
      .subscribe(this._deselect.bind(this));
  }

  ngOnInit() {
    this._initBukvitsa();
  }

  selected(bukvitsa: Bukvitsa) {
    bukvitsa.isSelected = true;
    bukvitsa.className = `size-${bukvitsa.size} ${this._borderClassName}`;
    bukvitsa.bgcolor = this.bgColor;

    this.selectedBukvitsa.emit(bukvitsa);
  }

  private _initBukvitsa() {
    const { color, size, bgColor } = this.settings;
    this.bgColor = bgColor;
    this.colSize = this.settings.size + 2;
    this.bukvitsy = this._bukvitsaService.getBukvitsa(color, size);
  }

  private _updateSettings(settings: SettingsBukvitsa) {
    this.colSize = settings.size + 2;
    this.bgColor = settings.bgColor;

    for (const bukvitsyArray of this.bukvitsy) {
      for (const bukvitsa of bukvitsyArray) {
        bukvitsa.color = settings.color;
        bukvitsa.size = settings.size;

        const oldPart = bukvitsa.className.split(' ').filter(x => !x.includes('size-'));
        bukvitsa.className = `size-${bukvitsa.size} ${oldPart.join(' ')}`;

        if (bukvitsa.isSelected)
          bukvitsa.bgcolor = settings.bgColor;
      }
    }

    this._cdRef.markForCheck();
  }

  private _deselect(id: number) {
    const rowIndex = Math.floor(id / 7);
    const colIndex = id % 7;
    const bukvitsaToChange = this.bukvitsy[rowIndex][colIndex];

    bukvitsaToChange.isSelected = false;
    bukvitsaToChange.resetToDefault();

    this._cdRef.markForCheck();
  }

  private _clear() {
    for (const bukvitsyArray of this.bukvitsy) {
      for (const bukvitsa of bukvitsyArray) {
        if (bukvitsa.isSelected) {
          bukvitsa.isSelected = false;
          bukvitsa.resetToDefault();
        }
      }
    }

    this._cdRef.markForCheck();
  }


}
