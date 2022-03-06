import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { Resolution, RecordSettings } from '@app/app.models';
import { VideoDescription } from '@app/appshared/models/videoDescrption';

@Component({
  selector: 'appc-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RecordComponent implements OnInit {

  public settings: RecordSettings;
  public defaultResolutions: Resolution[];
  public maxVideoLength: number = 120;
  public modes: string[] = ['screen', 'video', 'audio'];
  public currentMode: string;
  public currentModeApplied: string;
  public videoDescriptions: VideoDescription[];

  constructor(public ref: ChangeDetectorRef) {
    this.defaultResolutions = [
      new Resolution(320, 240),
      new Resolution(640, 480),
      new Resolution(1280, 720),
      new Resolution(1920, 540),
      new Resolution(1920, 1080),
    ];

    this.buildNewSettings(this.modes[0]);
  }

  ngOnInit() { 
    const basePath = 'intro-video/record/';

    this.videoDescriptions = [
      { name: 'rec_scr', src: `${basePath}0-scr`, isAllowed: true, type: 'webm' },
      { name: 'rec_vid', src: `${basePath}1-vid`, isAllowed: true, type: 'webm' },
      { name: 'rec_aud', src: `${basePath}2-aud`, isAllowed: true, type: 'webm' },
    ];
  }

  resolutionSelected(resolutionString: string) {
    const splittedValues = resolutionString.split('x');
    var resolution = new Resolution(Number(splittedValues[0]), Number(splittedValues[1]));
    this.settings.resolution = resolution;
  }

  modeSelected(mode: string) {
    this.currentMode = mode;

    if (mode === 'audio')
      this.settings.isAudioOnly = true;
    else if (mode === 'video')
      this.settings.isAudioOnly = false;
    else
      this.settings.isAudioOnly = undefined;
  }

  apply() {
    this.settings.maxLength = this.maxVideoLength;
    this.currentModeApplied = '';

    setTimeout(() => {
      this.currentModeApplied = this.currentMode;
      this.ref.markForCheck();
    }, 1);

  }

  private buildNewSettings(mode: string, resolution: Resolution = null) {
    this.settings = new RecordSettings();
    this.settings.resolution = resolution ? resolution : this.defaultResolutions[0];
    this.settings.maxLength = this.maxVideoLength;
    this.settings.isAudioOnly = true;
    this.currentMode = mode;
    this.currentModeApplied = mode;
  }

}
