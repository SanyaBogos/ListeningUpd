import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Input, Output, EventEmitter } from '@angular/core';
import { AppService } from '@app/app.service';
import _ from 'underscore';

@Component({
  selector: 'appc-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmailComponent implements OnInit {
  private _incorrectEmail: string;

  @Input() email: string;
  @Output() emailChanged = new EventEmitter<string>();

  public emailError = '';

  constructor(
    private _cdRef: ChangeDetectorRef,
    private _appService: AppService
  ) { }

  ngOnInit() {
    this._incorrectEmail = this._appService.appData.content['incorrect_email'];
  }

  changed() {
    const isValid = !/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      .test(this.email);

    if (!!this.email && this.email.length !== 0 && isValid)
      this.emailError = this._incorrectEmail;
    else
      this.emailError = '';

    if (!this.emailError)
      this.emailChanged.emit(this.email);
  }

}
