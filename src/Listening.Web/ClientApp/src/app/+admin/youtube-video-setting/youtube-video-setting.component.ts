import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TimeStamp } from '@app/appshared/models/timeStamp';
import { Status } from '../../core/models/status';
// import { Status } from '@app/core/models/status';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { CuttingOptionsViewModel, FileClient, FileNameViewModel } from 'apiDefinitions';
import { VgAPI } from 'videogular2/compiled/core';
import { AbstractChangeSettingsComponent } from '../abstract-change-settings/abstract-change-settings.component';
import { AdminService } from '../admin.service';

@Component({
  selector: 'appc-youtube-video-setting',
  templateUrl: './youtube-video-setting.component.html',
  styleUrls: ['./youtube-video-setting.component.scss', '../../shared/styles/common-iniline.scss']
})
export class YoutubeVideoSettingComponent extends AbstractChangeSettingsComponent implements OnInit {

  @Input() videoName: string;
  @Output() videoHasCut = new EventEmitter<string | undefined>();

  private vgAPI: VgAPI;
  private cutFromValue: number;
  private cutToValue: number;

  public videoSrc: string;
  public videoType: string;
  public videoURL: string;
  public duration: number;
  public videoTTL: number;
  public error: string;
  public isGetVideoEnabled = true;

  private subscription: any;

  constructor(
    public cdRef: ChangeDetectorRef,
    public fileClient: FileClient,
    public adminService: AdminService,
    public notificationsService: MyNotificationsService
  ) {
    super(cdRef, adminService, notificationsService);
  }

  ngOnInit() {
    if (this.videoName) {
      this.videoSrc = `video/${this.videoName}`;
      this.videoType = `video/${this.getType(this.videoName)}`;
      this.cdRef.markForCheck();
    }
  }

  onPlayerReady(api: VgAPI) {
    this.vgAPI = api;
    const defaultMedia = this.vgAPI.getDefaultMedia();
    this.subscription = defaultMedia.subscriptions.durationChange
      .subscribe(() => {
        this.duration = defaultMedia.duration;
        this.cdRef.markForCheck();
      });
  }

  getVideo(): void {
    if (!this.videoURL)
      return;

    const urlVM = new FileNameViewModel();
    urlVM.name = this.videoURL;

    this.videoSrc = '';
    this.isGetVideoEnabled = false;
    this.cdRef.markForCheck();

    const self = this;

    this.fileClient.getVideo(urlVM)
      .subscribe(data => {
        self.videoSrc = 'video/' + data.fileName;
        this.videoType = `video/${this.getType(data.fileName)}`;
        self.videoTTL = data.ttl;
        self.isGetVideoEnabled = true;
        self.cdRef.markForCheck();
      });
  }

  dropVideo() {
    this.subscription.unsubscribe();
    this.videoSrc = '';
    this.vgAPI = null;
    this.cdRef.markForCheck();
  }

  getTimeStamp(event: TimeStamp) {
    if (event.type === 'f')
      this.cutFromValue = event.time;
    else
      this.cutToValue = event.time;

    const areValuesExist = this.cutFromValue && this.cutToValue;

    if (areValuesExist && this.cutFromValue > this.cutToValue)
      this.error = 'video_error_from_to_diff';
    else if (areValuesExist && this.cutFromValue === this.cutToValue)
      this.error = 'video_error_same_value';
    else
      this.error = '';

    this.cdRef.markForCheck();
  }

  setTimeToPlayer(seconds: number) {
    if (!this.vgAPI)
      return;
    this.vgAPI.getDefaultMedia().currentTime = seconds;
    this.cdRef.markForCheck();
  }

  get getTimeFromPlayer(): number {
    if (!this.vgAPI || !this.vgAPI.getDefaultMedia())
      return 0;
    return this.vgAPI.getDefaultMedia().currentTime;
  }

  cutVideo() {
    const options = new CuttingOptionsViewModel();
    options.fileName = this.videoSrc.split('/')[1];
    options.from = this.cutFromValue;
    options.to = this.cutToValue;
    this.cdRef.markForCheck();
    const self = this;

    this.fileClient.cutVideo(options)
      .subscribe((data) => {
        self.videoHasCut.emit(data.fileName);
        self.notificationsService.notify('video_success_cut', Status.Success);
        self.cdRef.markForCheck();
      });
  }

  private getType(videoName: string) {
    const type = videoName.substring(videoName.lastIndexOf('.') + 1);
    return type;
  }

}
