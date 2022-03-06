import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Output, EventEmitter } from '@angular/core';
import { AccountClient, CaptchaCheckDto } from 'apiDefinitions';

@Component({
  selector: 'appc-captcha',
  templateUrl: './captcha.component.html',
  styleUrls: ['./captcha.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CaptchaComponent implements OnInit {

  public captchaImage: string = null;
  public captchaCheckDto: CaptchaCheckDto = new CaptchaCheckDto();
  public captchaTTL: number = 30;
  public captcha!: string;

  @Output() captchaObject = new EventEmitter<CaptchaCheckDto>();

  constructor(
    private cdRef: ChangeDetectorRef,
    public accountClient: AccountClient) { }

  ngOnInit() {
    this.refresh();
  }

  updateCaptcha(newValue: string) {
    this.captchaCheckDto.captcha = newValue;
    this.captchaObject.emit(this.captchaCheckDto);
  }

  dropPicture() {
    this.captchaImage = null;
  }

  refresh() {
    this.captchaImage = null;
    this.captcha = null;

    this.accountClient.getCaptchaImage()
      .subscribe(data => {
        this.captchaImage = `captcha/${data.name}`;
        // console.log(this.captchaImage);
        this.captchaCheckDto.hash = data.hash;
        this.captchaTTL = 30;
        this.cdRef.markForCheck();
      });
  }
}
