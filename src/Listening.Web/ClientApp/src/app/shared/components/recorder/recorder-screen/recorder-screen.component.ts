import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { RecorderBaseComponent } from '../recorder-base/recorder-base.component';
import { GenerateUniqueIdsService } from '@app/shared/services/generate-unique-ids.service';

declare const videojs: any;
declare const RecordRTC: any;
// declare const applyVideoWorkaround: Function;
declare const applyScreenWorkaround: Function;

@Component({
  selector: 'appc-recorder-screen',
  templateUrl: './recorder-screen.component.html',
  // templateUrl: '../recorder-base/recorder-base.component.html',
  styleUrls: ['./recorder-screen.component.scss', '../recorder-base/recorder-base.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RecorderScreenComponent extends RecorderBaseComponent implements OnInit {

  constructor(
    public ref: ChangeDetectorRef,
    public generateUniqueIdsService: GenerateUniqueIdsService
  ) {
    super(ref);
    this.videoIdBase = 'scrVid-'
    this.videoId = this.videoIdBase + generateUniqueIdsService.getCaptureScreenId();
  }

  ngOnInit() { }

  ngAfterViewInit() {
    this.update();
  }

  update() {
    this.setSettings();

    this.baseVideoJsOptions.controlBar = {
      volumePanel: false,
      fullscreenToggle: false
    };
    this.baseVideoJsOptions.plugins.record.screen = true;
    this.baseVideoJsOptions.plugins.record.debug = true;

    // apply some workarounds for certain browsers
    // applyVideoWorkaround();
    applyScreenWorkaround();

    this.player = videojs(this.videoId, this.baseVideoJsOptions, function () {
      // print version information at startup
      var msg = 'Using video.js ' + videojs.VERSION +
        ' with videojs-record ' + videojs.getPluginVersion('record') +
        ' and recordrtc ' + RecordRTC.version;
      videojs.log(msg);
    });
    // error handling
    this.player.on('deviceError', function () {
      console.warn('device error:', this.player.deviceErrorCode);
    });
    this.player.on('error', function (element, error) {
      console.error(error);
    });
    // snapshot is available
    this.player.on('finishRecord', function () {
      // the blob object contains the image data that
      // can be downloaded by the user, stored on server etc.
      console.log('screen capture ready: ', this.player.recordedData);
    });

    this.ref.markForCheck();
  }

}
