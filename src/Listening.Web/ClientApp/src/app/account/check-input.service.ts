// tslint:disable:curly
import { Injectable } from '@angular/core';
import * as _ from 'underscore';

@Injectable({
  providedIn: 'root'
})
export class CheckInputService {
  private MinPasswordLength = 6;
  private CaptchaLength = 5;

  private constraints: any[] = [
    { name: 'digit', regexp: '[0-9]' },
    { name: 'big_letter', regexp: '[A-Z]' },
    { name: 'little_letter', regexp: '[a-z]' },
    { name: 'special_character', regexp: '[!@#$%^&*(){}\_+-]' }
  ];

  constructor() { }

  public emailCheck(email: string): string {
    if (!!email && email.length !== 0 &&
      !/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        .test(email)) {

      return 'incorrect_email';
    }

    return null;
  }

  public passwordCheck(password: string): string[] {
    if (!!password && password.length !== 0) {
      const constraint = _.find(this.constraints, function (el) {
        return new RegExp(el.regexp).exec(password) === null;
      });

      if (password.length < this.MinPasswordLength)
        return ['password_should_be', this.MinPasswordLength.toString()];
      else if (constraint)
        return ['password_should_contain', constraint.name];
    }
  }

  public confirmPasswordCheck(confirmPassword: string, password: string) {
    if (!!confirmPassword && confirmPassword.length !== 0 && confirmPassword !== password)
      return 'passwords_not_the_same';

    return null;
  }

  public captchaCheck(captcha: string) {
    if (captcha == null || captcha.length === 0)
      return 'captcha_is_not_filled';
    else if (captcha.length !== this.CaptchaLength)
      return 'wrong_captcha_length';
  }
}
