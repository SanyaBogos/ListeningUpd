import {
  Component, ChangeDetectionStrategy, ChangeDetectorRef, ElementRef, ViewChild, AfterViewInit, Input
} from '@angular/core';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { OCRClient, ImageParamsViewModel } from 'apiDefinitions';
import { Status } from '@app/core/models/status';
import { CountriesListService } from '@app/shared/services/countries-list.service';
import { VideoDescription } from '@app/appshared/models/videoDescrption';
import { BaseUseCaptchaComponent } from '@app/appshared/components/base-use-captcha/base-use-captcha.component';

@Component({
  selector: 'appc-own-optical-recognition',
  templateUrl: './own-optical-recognition.component.html',
  styleUrls: ['./own-optical-recognition.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class OwnOpticalRecognitionComponent extends BaseUseCaptchaComponent implements AfterViewInit {

  @ViewChild('txtArea', { static: false }) fileInput: ElementRef<HTMLTextAreaElement>;
  public txtAreaHTML: HTMLTextAreaElement;

  @Input() isCaptured: boolean;

  public imageChangedEvent = '';
  public fileFormat: string = '';
  public croppedImage = '';
  public recognizedResult = '';
  public langList: string[];
  public selectedLanguages: string[];

  public videoDescriptions: VideoDescription[];

  constructor(
    private _ref: ChangeDetectorRef,
    private _notificationsService: MyNotificationsService,
    private _ocrClient: OCRClient,
    public countriesListService: CountriesListService
  ) {
    super();
    const basePath = 'intro-video/ocr/';

    this.videoDescriptions = [
      { name: 'ocr', src: `${basePath}1-tess`, isAllowed: true, type: 'webm' },
    ];

    this.langList = countriesListService.getTesseractLanguagesList();
    this.selectedLanguages = ['eng'];
    this._ref.markForCheck();
  }

  ngAfterViewInit(): void {
    this.txtAreaHTML = this.fileInput.nativeElement;
  }

  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
    var file = event.target.files[0] as File;
    const { name } = file;
    this.fileFormat = name;
    this._ref.markForCheck();
  }

  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
    this._ref.markForCheck();
  }
  imageLoaded() {
    // show cropper
  }
  cropperReady() {
    // cropper ready
  }
  loadImageFailed() {
    // show message
  }


  recognize() {
    const languages = this.selectedLanguages.join('+');

    if (!this.isCaptured)
      this._ocrClient.getRecognized(languages, this.croppedImage)
        .subscribe(this._handleRecognizedResult.bind(this));
    else
      this._ocrClient.getRecognizedImage(languages,
        { base64: this.croppedImage, captcha: this.captcha } as ImageParamsViewModel)
        .subscribe(this._handleRecognizedResult.bind(this));
  }

  copied() {
    this._notificationsService.notify('copied_to_buffer', Status.Success);
  }

  private _handleRecognizedResult(result: string) {
    this.recognizedResult = result;
    this._ref.markForCheck();
  }

  /* TODO: replace to higher level
  pushToText() {
    if (this.txtAreaHTML.selectionStart !== this.txtAreaHTML.selectionEnd) {
      this.adminService.announcePushOcrText(
        this.txtAreaHTML.value.substring(this.txtAreaHTML.selectionStart, this.txtAreaHTML.selectionEnd));
    } else
      this.adminService.announcePushOcrText(this.txtAreaHTML.value);
  }*/

}
