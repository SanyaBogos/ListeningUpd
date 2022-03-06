// tslint:disable:curly
import {
  Component, OnInit, ElementRef, ViewChild, AfterViewInit,
  ChangeDetectionStrategy, ChangeDetectorRef, Inject
} from '@angular/core';
import { VideoDescription } from '@app/appshared/models/videoDescrption';
import { HubConnection, HubConnectionBuilder, IHttpConnectionOptions } from '@aspnet/signalr';
import { AccountService } from '../../core';
// import { Peer } from 'peerjs';
// import { } from 'peerjs';
// import * as easyrtc from 'easyrtc';
// import { easyrtc } from 'easyrtc';
// import 'easyrtc';


@Component({
  selector: 'appc-media-processing',
  templateUrl: './media-processing.component.html',
  styleUrls: ['./media-processing.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MediaProcessingComponent implements OnInit, AfterViewInit {

  @ViewChild('videoMyselfElement', { static: false }) videoMyselfElement: ElementRef<HTMLVideoElement>;
  @ViewChild('audioMyselfElement', { static: false }) audioMyselfElement: ElementRef<HTMLAudioElement>;
  @ViewChild('videoNeighborElement', { static: false }) videoNeighborElement: ElementRef<HTMLVideoElement>;

  private _hubConnection: HubConnection;

  private myVideo: HTMLVideoElement;
  private neigborVideo: HTMLVideoElement;
  private myAudio: HTMLAudioElement;

  public isVideo = true;
  public isAudio = true;
  public isPaused = false;
  public showingMyselfResult = false;
  public startedAsVideo = false;
  public videoDescriptions: VideoDescription[];

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private accountService: AccountService,
    private ref: ChangeDetectorRef
  ) { }

  ngOnInit() {
    const basePath = 'intro-video/online-cam/';

    this.videoDescriptions = [
      { name: 'off_cam', src: `${basePath}0-tutor`, isAllowed: true, type: 'webm' },
    ];

    this.hubInit();
    // perhaps shoud be useful https://github.com/webrtc/samples
    // https://www.npmjs.com/package/webrtc-adapter
    // let peer = new Peer(null, {
    //   debug: 2,
    //   host: 'localhost', port: 9000, path: '/myapp'
    // });

    // console.log(peer);
    // console.log(easyrtc);
  }

  ngAfterViewInit() {
    this.myVideo = this.videoMyselfElement.nativeElement;
    this.myAudio = this.audioMyselfElement.nativeElement;
    this.neigborVideo = this.videoNeighborElement.nativeElement;
  }

  hubInit() {
    const accountToken = this.accountService.accessToken;
    const connectionOptions = { accessTokenFactory: () => accountToken } as IHttpConnectionOptions;

    this._hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.baseUrl}streamHub`, connectionOptions)
      .build();

    const self = this;
    this._hubConnection.on('send', (stream: MediaStream) => {
      this.neigborVideo.srcObject = stream;
      this.neigborVideo.play();
      // self.messages.push(received);
      self.ref.markForCheck();
    });

    this._hubConnection.start()
      .then(() => {
        console.log('Hub connection started');
      })
      .catch(err => {
        console.log('Error while establishing connection: ' + err);
      });
  }

  recordMedia() {
    // const recorder = new RecordRTCPromisesHandler(this.myVideo.srcObject, {
    //   mimeType: 'video/webm',
    //   bitsPerSecond: 128000
    // });

  }

  sendMedia() {
    // this.myVideo.captu
    // const videoMediaStream = this.myVideo.srcObject as MediaStream;
    // const videoTrack = videoMediaStream.getVideoTracks()[0];
    // videoTrack.
    // zz.slice()

    // this._hubConnection.invoke('send', videoMediaStream);
    // // videoMediaStream.
    // // videoMediaStream.slice(this.sendStartIndex, this.defaultSendCount, '');
    // console.log(videoMediaStream);
  }

  startMedia() {
    this.showingMyselfResult = true;
    this.ref.markForCheck();

    const params = { audio: this.isAudio, video: this.isVideo };
    const self = this;

    navigator.mediaDevices.getUserMedia(params)
      .then(function (stream) {
        if (self.isVideo) {
          self.myVideo.srcObject = stream;
          self.myVideo.play();
          self.startedAsVideo = true;

          if (!self.isAudio)
            self.myVideo.muted = true;

        } else if (self.isAudio) {
          self.myAudio.srcObject = stream;
          self.myAudio.play();
          self.startedAsVideo = false;
        }

        self.ref.markForCheck();
      })
      .catch(function (err) {
        console.log(err);
        self.showingMyselfResult = false;
        self.ref.markForCheck();
      });
  }

  pauseMedia() {
    this.startedAsVideo ? this.myVideo.pause() : this.myAudio.pause();
    this.isPaused = true;
    this.ref.markForCheck();
  }

  playMedia() {
    this.startedAsVideo ? this.myVideo.play() : this.myAudio.play();
    this.isPaused = false;
    this.ref.markForCheck();
  }

  stopMedia() {
    const videoSrcObj = this.myVideo.srcObject as MediaStream;
    const audioSrcObj = this.myAudio.srcObject as MediaStream;

    if (videoSrcObj != null)
      videoSrcObj.getVideoTracks()[0].stop();

    if (audioSrcObj != null)
      audioSrcObj.getAudioTracks()[0].stop();

    this.myVideo.srcObject = null;
    this.myAudio.srcObject = null;

    this.showingMyselfResult = false;
    this.isPaused = false;
    this.ref.markForCheck();
  }

  // private changeShowingState(isVideo: boolean, isAudio: boolean) {
  //   if (this.startedAsVideo) {
  //     isVideo ? this.myVideo.play() : this.myVideo.pause();
  //     isAudio ? this.myVideo.muted = false : this.myVideo.muted = true;
  //   } else {
  //     isAudio ? this.myAudio.muted = false : this.myAudio.muted = true;
  //   }
  // }
}
