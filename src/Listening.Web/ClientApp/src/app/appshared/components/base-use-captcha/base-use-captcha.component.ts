import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { CaptchaCheckDto } from 'apiDefinitions';

@Component({
  selector: 'appc-base-use-captcha',
  templateUrl: './base-use-captcha.component.html',
  styleUrls: ['./base-use-captcha.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class  BaseUseCaptchaComponent implements OnInit {

  public captcha: CaptchaCheckDto;

  constructor() { }

  ngOnInit() {
  }

  updateCaptchaObj(captcha: CaptchaCheckDto) {
    this.captcha = captcha;
  }

}
