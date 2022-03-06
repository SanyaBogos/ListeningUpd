import { Component, ChangeDetectionStrategy, ChangeDetectorRef, OnDestroy, Input } from '@angular/core';
import { RecordSettings } from '@app/app.models';
import { VideoJsOptions } from '@app/shared/models/videojs/video-js-options';

@Component({
  selector: 'appc-recorder-base',
  templateUrl: './recorder-base.component.html',
  styleUrls: ['./recorder-base.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RecorderBaseComponent implements OnDestroy {

  @Input() settings: RecordSettings;

  player: any;
  videoIdBase: string;
  baseVideoJsOptions: VideoJsOptions;
  videoId: string;

  constructor(
    public ref: ChangeDetectorRef
  ) {
    // TODO: get video identifier from server (videoId)
    // this.videoId = this.videoIdBase + generateUniqueIdsService.getCaptureVideoCameraId();
    // this.ref.markForCheck();
    // this.baseVideoJsOptions = {
    //   controls: true,
    //   width: undefined,
    //   height: undefined,
    //   fluid: false,
    //   controlBar: undefined,
    //   plugins: {
    //     record: {
    //       audio: false,
    //       video: false,
    //       screen: false,
    //       maxLength: undefined,
    //       debug: false
    //     },
    //     wavesurfer: undefined
    //   }
    // };

    this.baseVideoJsOptions = new VideoJsOptions();
  }

  ngOnDestroy(): void {
    if (this.player) {
      // this.player.record().destroy();
      this.player.dispose();
      this.player = null;
    }
  }

  setSettings() {
    this.baseVideoJsOptions.width = this.settings.resolution.width;
    this.baseVideoJsOptions.height = this.settings.resolution.height;
    this.baseVideoJsOptions.plugins.record.maxLength = this.settings.maxLength;
    // this.additionalSettings();

    // if (this.settings.isAudioOnly != null && this.settings.isAudioOnly) {
    //   this.baseVideoJsOptions.plugins.record.audio = true;
    //   this.baseVideoJsOptions.plugins.record.video = false;
    //   this.baseVideoJsOptions.plugins.wavesurfer = {
    //     src: 'live',
    //     waveColor: '#36393b',
    //     progressColor: 'black',
    //     debug: true,
    //     cursorWidth: 1,
    //     msDisplayMax: 20,
    //     hideScrollbar: true
    //   };
    // }
    // else {
    //   this.baseVideoJsOptions.plugins.record.audio = true;
    //   this.baseVideoJsOptions.plugins.record.video = true;
    // }

  }

  saveVideo() {
    this.player.record().saveAs({ 'video': 'my-video-file-name' });
  }

  // additionalSettings() { }

}
