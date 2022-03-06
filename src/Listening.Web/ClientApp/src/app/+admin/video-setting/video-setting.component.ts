// tslint:disable:curly
import {
  Component, 
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  AfterViewInit
} from '@angular/core';
import { AdminService } from '../admin.service';
import { AbstractChangeSettingsComponent } from '../abstract-change-settings/abstract-change-settings.component';
import { MyNotificationsService } from '../../core/services/my-notifications.service';

@Component({
  selector: 'appc-video-setting',
  templateUrl: './video-setting.component.html',
  styleUrls: ['./video-setting.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class VideoSettingComponent extends AbstractChangeSettingsComponent implements AfterViewInit {

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
