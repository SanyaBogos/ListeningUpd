import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { StegImageBaseComponent } from '../steg-image-base/steg-image-base.component';
import { ImgTransformService } from '../img-transform.service';
import { DataOperationsService } from '../data-operations.service';
import { FileClient, FileNameViewModel, FileParameter, StegClient } from 'apiDefinitions';
import { AccountService } from '@app/core/services';

@Component({
  selector: 'appc-steg-image-eject',
  templateUrl: './steg-image-eject.component.html',
  styleUrls: ['./steg-image-eject.component.scss', '../../shared/styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class StegImageEjectComponent extends StegImageBaseComponent implements OnInit {

  public extractedMessage: string;

  constructor(
    public ref: ChangeDetectorRef,
    public dataOperationsService: DataOperationsService,
    public accountService: AccountService,
    private _imgTransformService: ImgTransformService,
    private _stegClient: StegClient,
    private _fileClient: FileClient
  ) {
    super(ref, dataOperationsService, accountService);
  }

  ngOnInit() {

  }

  fileChangeEvent(event: any): void {

    this.fileChanged(event);

  }

  eject() {
    let message = this._imgTransformService.simpleEjectMessage(this.imageData, this.settings);
    this.extractedMessage = message;
    this.ref.markForCheck();
  }

  ejectServer() {
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

    this._stegClient.simplePictureEject(this.settings)
      .subscribe(message => {
        this.extractedMessage = message;
        this.ref.markForCheck();
      });
  }

}
