// tslint:disable:curly
import {
  Component,
  ChangeDetectionStrategy, AfterViewInit, ChangeDetectorRef
} from '@angular/core';
import { AdminService } from '../admin.service';
import { AbstractChangeSettingsComponent } from '../abstract-change-settings/abstract-change-settings.component';
import { MyNotificationsService } from '../../core/services/my-notifications.service';
import { SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'appc-audio-setting',
  templateUrl: './audio-setting.component.html',
  styleUrls: ['./audio-setting.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AudioSettingComponent extends AbstractChangeSettingsComponent implements AfterViewInit {

  public type: string;
  public isFileChanged = false;
  public audioSrc: string;
  public safeAudioSrc: SafeUrl;

  constructor(
    public ref: ChangeDetectorRef,
    public adminService: AdminService,
    public notificationsService: MyNotificationsService
  ) {
    super(ref, adminService, notificationsService);
  }

  ngAfterViewInit() {
    this.fileInputHTML = this.fileInput.nativeElement;
  }

}
