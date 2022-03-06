import { NgModule } from '@angular/core';

import { NgxSelectModule, INgxSelectOptions } from 'ngx-select-ex';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule } from '@angular/forms';
import { TranslationModule } from './translation/translation.module';
import { DownloadModule } from './download/download.module';
import { TimeStampComponent } from './components/time-stamp/time-stamp.component';
import { HowToUseComponent } from './components/how-to-use/how-to-use.component';
import { CommonModule } from '@angular/common';
import { HowToUseEnhComponent } from './components/how-to-use-enh/how-to-use-enh.component';
import { VideoSharedModule } from '@app/video-shared/video-shared.module';
import { BaseUseCaptchaComponent } from './components/base-use-captcha/base-use-captcha.component';
import { CaptchaComponent } from '@app/appshared/components/captcha/captcha.component';
import { CountdownComponent } from './components/countdown/countdown.component';

const CustomSelectOptions: INgxSelectOptions = { // Check the interface for more options
  optionValueField: 'id',
  keepSelectedItems: false,
  autoSelectSingleOption: true
};

@NgModule({
  imports: [
    CommonModule,
    TranslationModule,
    NgxSelectModule.forRoot(CustomSelectOptions),
    NgxPaginationModule, FormsModule,
    DownloadModule, VideoSharedModule
  ],
  exports: [
    TranslationModule, NgxSelectModule, NgxPaginationModule, FormsModule, DownloadModule,
    TimeStampComponent, HowToUseComponent, HowToUseEnhComponent, CountdownComponent, CaptchaComponent, BaseUseCaptchaComponent
  ],
  declarations: [TimeStampComponent, HowToUseComponent, HowToUseEnhComponent, CountdownComponent, CaptchaComponent, BaseUseCaptchaComponent],
  providers: [],
})
export class AppSharedModule { }

