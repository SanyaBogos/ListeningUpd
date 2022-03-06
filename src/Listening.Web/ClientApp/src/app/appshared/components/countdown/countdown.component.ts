// tslint:disable:curly
import {
  Component, OnInit, Input, EventEmitter, Output,
  ChangeDetectorRef, ChangeDetectionStrategy
} from '@angular/core';

@Component({
  selector: 'appc-countdown',
  templateUrl: './countdown.component.html',
  styleUrls: ['./countdown.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CountdownComponent implements OnInit {

  @Input() time: number;
  @Output() timeOver = new EventEmitter();

  private seconds: number;
  private minutes: number;

  constructor(
    private ref: ChangeDetectorRef,
  ) { }

  ngOnInit() {
    if (!this.time)
      return;

    this.minutes = Math.floor(this.time / 60);
    this.seconds = this.time % 60;

    setInterval(() => {
      this.seconds--;

      if (this.seconds === 0 && this.minutes === 0)
        this.timeOver.emit(true);

      if (this.seconds < 0) {
        this.minutes--;
        this.seconds += 60;
      }

      this.ref.markForCheck();
    }, 1000);
  }

}
