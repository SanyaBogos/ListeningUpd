import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { AdminComponent } from './admin.component';
import { routing } from './admin.routes';
import { ListTextsComponent } from './list-texts/list-texts.component';
import { EditTextComponent } from './edit-text/edit-text.component';
import { FileUploadModule } from 'ng2-file-upload';
import { AudioSettingComponent } from './audio-setting/audio-setting.component';
import { VideoSettingComponent } from './video-setting/video-setting.component';
// import { TimeStampComponent } from './time-stamp/time-stamp.component';
import { AdminService } from './admin.service';
import { AbstractChangeSettingsComponent } from './abstract-change-settings/abstract-change-settings.component';
import { OpticalRecognitionComponent } from './optical-recognition/optical-recognition.component';
import { PreviewTextComponent } from './preview-text/preview-text.component';
import { VideoSharedModule } from '@app/video-shared/video-shared.module';
import { AdditionalModule } from '@app/additional/additional.module';
import { RolesComponent } from './roles/roles.component';
import { Ng2TableModule } from 'ng2-table';
import { PaginationModule } from 'ngx-bootstrap';
import { YoutubeVideoSettingComponent } from './youtube-video-setting/youtube-video-setting.component';
import { BackupComponent } from './backup/backup.component';

@NgModule({
    imports: [routing, SharedModule, FileUploadModule,
        VideoSharedModule, AdditionalModule, PaginationModule.forRoot(), Ng2TableModule
    ],
    declarations: [AdminComponent, ListTextsComponent, EditTextComponent, AudioSettingComponent,
        VideoSettingComponent, AbstractChangeSettingsComponent, OpticalRecognitionComponent,
        PreviewTextComponent, BackupComponent,
        RolesComponent, YoutubeVideoSettingComponent],
    providers: [AdminService]
})
export class AdminModule { }
