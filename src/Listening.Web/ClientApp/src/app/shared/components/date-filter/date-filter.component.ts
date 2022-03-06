import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Input, Output, EventEmitter } from '@angular/core';
import { IMyDateModel, IAngularMyDpOptions, IMyDate } from 'angular-mydatepicker';
import { DateFilter, DateName, DateType } from '@app/shared/models/date-filter';

@Component({
  selector: 'appc-date-filter',
  templateUrl: './date-filter.component.html',
  styleUrls: ['./date-filter.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DateFilterComponent implements OnInit {

  private _hasCreated: boolean;
  private _hasUpdated: boolean;

  public model: IMyDateModel = null;
  public myDpOptions: IAngularMyDpOptions = {
    // dateRange: true,
    dateFormat: 'dd mmm yyyy',
  };

  public btnStates: DateType[] = ['<', '>', '='];
  public allSelectors: DateName[] = [DateFilter.createdName, DateFilter.updatedName];
  public selectors: DateName[] = [];
  public currentBtnState: DateType;
  public currentSelectorState: DateName;
  public selectedDate: IMyDateModel;

  @Input() set hasCreated(val: boolean) {
    this._hasCreated = val;
    const index = this.selectors.indexOf(DateFilter.createdName);
    val ? this.selectors.push(DateFilter.createdName) : this.selectors.splice(index, 1);
    this.currentSelectorState = this.selectors[0];
    this.myDpOptions.dateRange = this._hasCreated && this._hasUpdated ? true : false;
    this.cdRef.markForCheck();
    this.emit();
  };
  get hasCreated(): boolean { return this._hasCreated; }

  @Input() set hasUpdated(val: boolean) {
    this._hasUpdated = val;
    const index = this.selectors.indexOf(DateFilter.updatedName);
    val ? this.selectors.push(DateFilter.updatedName) : this.selectors.splice(index, 1);
    this.currentSelectorState = this.selectors[0];
    this.myDpOptions.dateRange = this._hasCreated && this._hasUpdated ? true : false;
    this.cdRef.markForCheck();
    this.emit();
  };
  get hasUpdated(): boolean { return this._hasUpdated };

  @Output() dateSelected = new EventEmitter<DateFilter>();

  constructor(
    private cdRef: ChangeDetectorRef
  ) {
    let currentTime = new Date();
    this.myDpOptions.disableSince = <IMyDate>{
      year: currentTime.getFullYear(),
      month: currentTime.getMonth() + 1,
      day: currentTime.getDate() + 3
    };
    this.myDpOptions.disableUntil = <IMyDate>{ year: 2014, month: 1, day: 1 };
    this.currentBtnState = this.btnStates[0];
    this.currentSelectorState = this.selectors[0];
  }

  ngOnInit() { }

  changeBtnState() {
    this.currentBtnState = this.currentBtnState == this.btnStates[0]
      ? this.btnStates[1] : this.currentBtnState == this.btnStates[1]
        ? this.btnStates[2] : this.btnStates[0];

    this.myDpOptions.dateRange = this.currentBtnState == '=' && this._hasCreated && this._hasUpdated;
    this.emit();
  }

  onDateChanged(date: IMyDateModel): void {
    this.selectedDate = date;
    this.emit();
  }

  clearDate(): boolean {
    this.selectedDate = null;
    this.emit();
    return true;
  }

  selectName(name: DateName) {
    this.currentSelectorState = name;
    this.emit();
  }

  toggleDatePicker() {
    setTimeout(() => {
      this.cdRef.markForCheck();
    }, 10);
  }

  private emit() {
    if (!this.selectedDate || !this.selectedDate.singleDate.date || this.selectedDate.singleDate.date.year < 2012){
      this.dateSelected.emit(null);
      return;
    }

    const dateFilter = new DateFilter(this.currentSelectorState, this.currentBtnState, this.selectedDate);
    this.dateSelected.emit(dateFilter);
  }
}
