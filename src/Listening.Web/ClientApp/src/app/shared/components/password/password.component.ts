import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Input, Output, EventEmitter } from '@angular/core';
import { AppService } from '@app/app.service';
import _ from 'underscore';

@Component({
  selector: 'appc-password',
  templateUrl: './password.component.html',
  styleUrls: ['./password.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PasswordComponent implements OnInit {
  private _passwordSymbolError: string;
  private _passwordLengthError: string;
  private _symbolTranslate: string;
  private _minPasswordLength = 6;

  @Input() password: string;
  @Output() passwordChanged = new EventEmitter<string>();

  public passwordError = '';
  public showPasswd = true;

  constructor(
    private _cdRef: ChangeDetectorRef,
    private _appService: AppService
  ) { }

  ngOnInit() {
    this._passwordSymbolError = this._appService.appData.content['password_should_contain'];
    this._passwordLengthError = this._appService.appData.content['password_should_be'];
    this._symbolTranslate = this._appService.appData.content['password_symbols'];
  }

  public changeState() {
    this.showPasswd = !this.showPasswd;
    this._cdRef.markForCheck();
  }

  changed() {
    this._checkPassword(this.password);

    if (!this.passwordError)
      this.passwordChanged.emit(this.password);
  }

  private _checkPassword(password: string) {
    var constraints = [
      { name: 'digit', regexp: '[0-9]' },
      { name: 'big letter', regexp: '[A-Z]' },
      { name: 'little letter', regexp: '[a-z]' },
      { name: 'special character', regexp: '[!@#$%^&*(){}\_+-]' }
    ];

    if (!!password && password.length !== 0) {
      var constraint = _.find(constraints, element => new RegExp(element.regexp).exec(password) === null);

      if (constraint)
        this.passwordError = `${this._passwordSymbolError} ${constraint.name}`;
      else if (password.length < this._minPasswordLength)
        this.passwordError = `${this._passwordLengthError} ${this._minPasswordLength} ${this._symbolTranslate}`;
      else
        this.passwordError = '';
    }
  }
}
