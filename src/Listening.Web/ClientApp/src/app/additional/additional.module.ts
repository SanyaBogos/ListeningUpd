import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OwnOpticalRecognitionComponent } from './optical-recognition/own-optical-recognition/own-optical-recognition.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import { ClipboardModule } from 'ngx-clipboard';
import { TranslationModule } from '@app/appshared/translation/translation.module';
import { SharedModule } from '@app/shared/shared.module';
import { ImgRecognitionComponent } from './optical-recognition/img-recognition/img-recognition.component';
import { routing } from './additional.routes';
import { YoutubeDownloadComponent } from './youtube-download/youtube-download.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [OwnOpticalRecognitionComponent, ImgRecognitionComponent, YoutubeDownloadComponent],
  imports: [routing, HttpClientModule, CommonModule, ImageCropperModule, ClipboardModule, TranslationModule, SharedModule,
    ],
  exports: [OwnOpticalRecognitionComponent]
})
export class AdditionalModule { }
