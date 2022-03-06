import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnChanges, OnInit, Output } from '@angular/core';
import { BukvitsaBaseComponent } from '../bukvitsa-base/bukvitsa-base.component';
import { Bukvitsa } from '../models/bukvitsa';

@Component({
  selector: 'appc-bukvitsa',
  templateUrl: './bukvitsa.component.html',
  styleUrls: ['./bukvitsa.component.scss', '../bukvitsa-base/bukvitsa-base.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BukvitsaComponent extends BukvitsaBaseComponent implements OnInit, OnChanges {

  @Input() bukvitsa: Bukvitsa;
  @Output() selectedBukvitsa = new EventEmitter();

  public className: string;
  private _borderClassName: string = 'border border-primary rounded bg-light-blue';

  constructor() {
    super();
  }

  ngOnInit() {
    const { width, height, scale1, scale2 } = this.bukvitsa;
    this.bukvitsa.viewBox = `0 0 ${width} ${height}`;
    this.bukvitsa.transform = `translate(0.000000, ${height}) scale(${scale1}00000,-${scale2}00000)`;
    this.className = `size-${this.bukvitsa.size}`;
  }

  ngDoCheck(){
    
  }

  ngOnChanges() {
    this._setClassName();
  }

  bukvitsaSelected() {
    this.bukvitsa.isSelected = true;
    this._setClassName();
    this.selectedBukvitsa.emit(this.bukvitsa);
  }

  private _setClassName() {
    this.className = this.bukvitsa.isSelected ? `size-${this.bukvitsa.size} ${this._borderClassName}` : `size-${this.bukvitsa.size}`;
  }
}
