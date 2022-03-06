import { Component, OnInit, ChangeDetectionStrategy, ViewChild, ElementRef, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { BaseUseCaptchaComponent } from '@app/appshared/components/base-use-captcha/base-use-captcha.component';
import { VideoDescription } from '@app/appshared/models/videoDescrption';
import { AccountService } from '@app/core/services';
import { CaptchaCheckDto, StegSettingsDto } from 'apiDefinitions';
import { DataOperationsService } from '../data-operations.service';

@Component({
  selector: 'appc-steg-image-base',
  templateUrl: './steg-image-base.component.html',
  styleUrls: ['./steg-image-base.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class StegImageBaseComponent extends BaseUseCaptchaComponent  implements OnInit, AfterViewInit {

  @ViewChild('canvasWithMsg', { static: false }) canvasWithMsg: ElementRef<HTMLCanvasElement>;
  protected canvasWithMsgHTML: HTMLCanvasElement;

  public videoDescriptions: VideoDescription[];

  public imageData: ImageData;
  public imgSrc: string;
  public settings: StegSettingsDto;
  public isCaptured: boolean;

  public showCaptcha: boolean = false;
  protected pictureFile: File;

  protected maxMessageLength: number;
  protected colorsCount: number = 4;

  constructor(
    public ref: ChangeDetectorRef,
    public dataOperationsService: DataOperationsService,
    public accountService: AccountService
  ) {
    super();
    const basePath = 'intro-video/steg/';

    this.videoDescriptions = [
      { name: 'inj', src: `${basePath}0-inject`, type: 'webm', isAllowed: true },
      { name: 'ej', src: `${basePath}1-eject`, type: 'webm', isAllowed: true }
    ];
  }

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.canvasWithMsgHTML = this.canvasWithMsg.nativeElement;
    this.isCaptured = !(this.accountService.isAdmin() || this.accountService.isSuper());
  }

  protected fileChanged(event: any) {
    const file = event.target.files[0] as File;
    this.pictureFile = file;
    this.imgSrc = '';

    const reader = new FileReader();

    reader.onload = (e: any) => {
      this.imgSrc = e.target.result;
      this.buildImageData(this.imgSrc);
      this.ref.markForCheck();
    };

    reader.readAsDataURL(file);
  }

  applySettings(settings: StegSettingsDto) {
    this.settings = settings;
  }

  updateCaptchaObj(captcha: CaptchaCheckDto) {
    this.captcha = captcha;
  }

  protected _areCorrectSettings(): boolean {
    if (!this.settings)
      return false;

    if (!this.captcha) {
      this.showCaptcha = true;
      this.ref.markForCheck();
      return false;
    }

    return true;
  }

  private buildImageData(base64EncodedImage: string) {
    const image = new Image();
    const self = this;

    image.onload = () => {
      this.canvasWithMsgHTML.height = image.height;
      this.canvasWithMsgHTML.width = image.width;

      var context = this.canvasWithMsgHTML.getContext('2d');
      context.drawImage(image, 0, 0);

      self.imageData = context.getImageData(0, 0, image.width, image.height);
      self.maxMessageLength = self.dataOperationsService.getMaxLength(image.width, image.height, self.colorsCount);

      this.ref.markForCheck();
    };

    image.src = base64EncodedImage;
  }

}
