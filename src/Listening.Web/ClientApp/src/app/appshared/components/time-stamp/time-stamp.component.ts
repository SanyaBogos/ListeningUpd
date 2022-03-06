// tslint:disable:curly
import { Component, OnInit, Input, EventEmitter, Output, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { TimeStamp } from '../../models/timeStamp';

@Component({
  selector: 'appc-time-stamp',
  templateUrl: './time-stamp.component.html',
  styleUrls: ['./time-stamp.component.scss', '../../../shared/styles/common-iniline.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TimeStampComponent implements OnInit {

  @Input() duration: number;
  @Input() type: string;
  @Input() isVertical: boolean = false;

  @Input() set timeFromPlayer(value: number) {
    this.playerTime = value;
  }

  @Output() resultTime = new EventEmitter<TimeStamp>();
  @Output() setTime = new EventEmitter<number>();
  @Output() getTime = new EventEmitter<number>();

  private timeInSeconds: number;
  private playerTime: number;
  private typesDictionary: { [type: string]: string } = {
    'f': 'from',
    't': 'to',
    'u': 'time'
  };

  public time: string;
  public error: string;
  public placeholder: string;
  public isSetEnable = false;

  constructor(
    private cdRef: ChangeDetectorRef,
  ) { }

  ngOnInit() {
    this.placeholder = this.typesDictionary[this.type];
    this.cdRef.markForCheck();
  }

  arrowName(isGetter: boolean) {
    const name = this.isVertical
      ? (isGetter ? 'down' : 'up')
      : (isGetter ? 'left' : 'right');

    return name;
  }

  setTimeToPlayer() {
    this.setTime.emit(this.timeInSeconds);
    this.cdRef.markForCheck();
  }

  getPlayerTime() {
    this.timeInSeconds = this.playerTime;
    this.time = `${Math.floor(this.playerTime / 60)}:${Math.round(this.playerTime % 60)}`;
    this.resultTime.emit(new TimeStamp(this.timeInSeconds, this.type));
    this.getTimeValue();
    this.cdRef.markForCheck();
    this.cdRef.detectChanges();
  }

  getTimeValue() {
    if (!this.time) {
      this.error = 'no_empty';
      return;
    }

    const timeParts = this.time.split(':');
    if (timeParts.length > 2 || !/^[0-9]+$/.test(timeParts[0])) {
      this.error = 'incorrect_format';
      return;
    }

    if (timeParts.length === 1) {
      const seconds = Number.parseFloat(timeParts[0]);
      if (!this.isDurationCorect(seconds))
        return;

      this.error = '';
      this.timeInSeconds = Number.parseFloat(timeParts[0]);
      this.isSetEnable = true;
      this.resultTime.emit(new TimeStamp(this.timeInSeconds, this.type));
      return;
    }

    if (!/^[0-9]+$/.test(timeParts[1])) {
      this.error = 'incorrect_format';
      return;
    }

    const common = Number(timeParts[0]) * 60 + Number.parseFloat(timeParts[1]);
    if (!this.isDurationCorect(common))
      return;

    this.error = '';
    this.timeInSeconds = common;
    this.isSetEnable = true;
    this.resultTime.emit(new TimeStamp(common, this.type));
  }

  private isDurationCorect(current: number): boolean {
    if (current > this.duration) {
      this.error = 'value_not_null';
      return false;
    }

    return true;
  }

}
