import { Component, OnInit, AfterViewInit, ChangeDetectorRef, ChangeDetectionStrategy } from '@angular/core';
import { DataOperationsService } from '../data-operations.service';
import { ImgTransformService } from '../img-transform.service';
import { StegImageBaseComponent } from '../steg-image-base/steg-image-base.component';
import { DownloadService } from '@app/appshared/download/download.service';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { Status } from '@app/core/models/status';
import { FileClient, FileNameViewModel, FileParameter, StegClient } from 'apiDefinitions';
import { saveAs } from 'file-saver';
import { AccountService } from '@app/core/services';

@Component({
  selector: 'appc-steg-image-inject',
  templateUrl: './steg-image-inject.component.html',
  styleUrls: ['./steg-image-inject.component.scss', '../../shared/styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class StegImageInjectComponent extends StegImageBaseComponent implements OnInit, AfterViewInit {

  public resultImageData: ImageData;
  public msgToHide: string;

  public sliderInputVal: number = 50;
  // public sliderResultVal: number = 100;

  public showInput: boolean = false;
  public showResult: boolean = false;
  public showDiff: boolean = false;

  public isInjected: boolean = false;



  constructor(
    public ref: ChangeDetectorRef,
    public dataOperationsService: DataOperationsService,
    public accountService: AccountService,
    private _imgTransformService: ImgTransformService,
    private _downloadService: DownloadService,
    private _notificationsService: MyNotificationsService,
    private _stegClient: StegClient,
    private _fileClient: FileClient
  ) {
    super(ref, dataOperationsService, accountService);
  }

  ngOnInit() {
  }

  injectMessage() {
    this.resultImageData = this._imgTransformService.simpleInjectMessage(this.imageData, this.msgToHide, this.settings);

    let ctx = this.canvasWithMsgHTML.getContext('2d');
    ctx.putImageData(this.resultImageData, 0, 0);

    this.isInjected = true;
    this.ref.markForCheck();

    this._notificationsService.notify(
      'msg_ingctd', Status.Success, 'success_inject');
  }

  fileChangeEvent(event: any): void {
    this.fileChanged(event);
  }

  savePicture() {
    const url = this.canvasWithMsgHTML.toDataURL('image/png');
    this._downloadService.run(url, 'result_picture.png');
  }

  computeServer() {
    if (!this._areCorrectSettings())
      return;

    const fileParam = { data: this.pictureFile } as FileParameter;

    if (this.isCaptured)
      this._fileClient.saveStegPictureAno(this.captcha.captcha, this.captcha.hash, this.pictureFile.name, fileParam)
        .subscribe(this._handleSaveStegPicture.bind(this));
    else
      this._fileClient.saveStegPicture(this.pictureFile.name, fileParam)
        .subscribe(this._handleSaveStegPicture.bind(this));
  }

  private _handleSaveStegPicture(fileNameVM: FileNameViewModel) {
    this.settings.fileName = fileNameVM.name;
    this.settings.message = this.msgToHide;

    this._stegClient.simplePictureInject(this.settings)
      .subscribe(fileDescription => {
        saveAs(fileDescription.data, fileDescription.fileName);
      });
  }

}
