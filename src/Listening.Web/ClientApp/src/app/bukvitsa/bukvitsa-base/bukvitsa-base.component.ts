import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Bukvitsa } from '../models/bukvitsa';

@Component({
  selector: 'appc-bukvitsa-base',
  templateUrl: './bukvitsa-base.component.html',
  styleUrls: ['./bukvitsa-base.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BukvitsaBaseComponent implements OnInit {

  @Input() bukvitsa: Bukvitsa;
  // @Output() selectedBukvitsa = new EventEmitter();

  constructor() { }

  ngOnInit() {
    const { width, height, scale1, scale2 } = this.bukvitsa;
    this.bukvitsa.viewBox = `0 0 ${width} ${height}`;
    this.bukvitsa.transform = `translate(0.000000, ${height}) scale(${scale1}00000,-${scale2}00000)`;
  }

  // bukvitsaSelected() {
  //   console.log(this.bukvitsa)
  //   this.selectedBukvitsa.emit(this.bukvitsa);
  // }
}
