import { Component, OnInit, AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { GenerateUniqueIdsService } from '@app/shared/services/generate-unique-ids.service';
import { RecorderBaseComponent } from '../recorder-base/recorder-base.component';

declare const videojs: any;
declare const RecordRTC: any;
declare const WaveSurfer: any;
// declare const applyVideoWorkaround: Function;
declare const applyAudioWorkaround: Function;

@Component({
  selector: 'appc-recorder-media',
  templateUrl: './recorder-media.component.html',
  // templateUrl: '../recorder-base/recorder-base.component.html',
  styleUrls: ['./recorder-media.component.scss', '../recorder-base/recorder-base.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RecorderMediaComponent extends RecorderBaseComponent implements OnInit, AfterViewInit {

  constructor(
    public ref: ChangeDetectorRef,
    public generateUniqueIdsService: GenerateUniqueIdsService) {
    super(ref);

    this.videoIdBase = 'cptVid-';
    this.videoId = this.videoIdBase + generateUniqueIdsService.getCaptureVideoCameraId();
  }

  ngOnInit() { }

  ngAfterViewInit() {
    this.update();
  }

  // additionalSettings() {
  //   // if (this.settings.isAudioOnly != null && this.settings.isAudioOnly) {
  //   //   this.baseVideoJsOptions.plugins.record.audio = true;
  //   //   this.baseVideoJsOptions.plugins.record.video = false;
  //   // }
  //   // else {
  //   //   this.baseVideoJsOptions.plugins.record.audio = true;
  //   //   this.baseVideoJsOptions.plugins.record.video = true;
  //   // }

  // }

  update() {
    this.setSettings();

    this.baseVideoJsOptions.plugins.record.debug = true;

    if (this.settings.isAudioOnly != null && this.settings.isAudioOnly) {
      this.baseVideoJsOptions.plugins.wavesurfer = {
        src: 'live',
        waveColor: '#36393b',
        progressColor: 'black',
        debug: true,
        cursorWidth: 1,
        msDisplayMax: 20,
        hideScrollbar: true
      };

      this.baseVideoJsOptions.plugins.record.audio = true;
      this.baseVideoJsOptions.plugins.record.video = false;
      applyAudioWorkaround();

      this.player = videojs(this.videoId, this.baseVideoJsOptions, function () {
        // print version information at startup
        var msg = 'Using video.js ' + videojs.VERSION +
          ' with videojs-record ' + videojs.getPluginVersion('record') +
          ', videojs-wavesurfer ' + videojs.getPluginVersion('wavesurfer') +
          ', wavesurfer.js ' + WaveSurfer.VERSION + ' and recordrtc ' +
          RecordRTC.version;
        videojs.log(msg);
      });
    }
    else {
      this.baseVideoJsOptions.plugins.record.audio = true;
      this.baseVideoJsOptions.plugins.record.video = true;
      // applyVideoWorkaround();

      this.player = videojs(this.videoId, this.baseVideoJsOptions, () => {
        // print version information at startup
        var msg = 'Using video.js ' + videojs.VERSION +
          ' with videojs-record ' + videojs.getPluginVersion('record') +
          ' and recordrtc ' + RecordRTC.version;
        videojs.log(msg);
        this.ref.markForCheck();
      });
    }

    // apply some workarounds for opera browser



    // error handling
    this.player.on('deviceError', () => {
      console.log('device error:', this.player.deviceErrorCode);
    });

    this.player.on('error', (element, error) => {
      console.error(error);
    });

    // user clicked the record button and started recording
    this.player.on('startRecord', () => {
      console.log('started recording!');
    });

    // user completed recording and stream is available
    this.player.on('finishRecord', () => {
      // the blob object contains the recorded data that
      // can be downloaded by the user, stored on server etc.
      console.log('finished recording: ', this.player.recordedData);
      // player.record().saveAs({'video': 'my-video-file-name.webm'});
    });

    this.ref.markForCheck();
  }

}
