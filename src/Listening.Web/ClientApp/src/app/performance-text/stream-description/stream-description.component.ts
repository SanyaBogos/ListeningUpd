import { Component, OnInit, AfterViewInit, ViewChild, ElementRef, Input, OnDestroy, HostListener, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { TextDescription } from '../models/textDescription';
import { HotkeysService } from '../hotkeys.service';
import { VgAPI } from 'videogular2/compiled/core';
import { BrowserDetectService } from '@app/shared/services/browser-detect.service';
import { ActionStrEventCombination, ActionStrEventCombinationDictionary, KeyStrCombinations } from '../models/keyStrCombinations';

@Component({
  selector: 'appc-stream-description',
  templateUrl: './stream-description.component.html',
  styleUrls: ['./stream-description.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [BrowserDetectService]
})
export class StreamDescriptionComponent implements OnInit, OnDestroy, AfterViewInit {

  @ViewChild('audioplayer', { static: false }) audioPlayerRef: ElementRef<HTMLAudioElement>;
  @Input() description: TextDescription;
  private componentName = 'description';
  private vgAPI: VgAPI;

  public audio: string;
  public player: HTMLAudioElement;
  public video: string;

  constructor(
    public browserDetectService: BrowserDetectService,
    public hotkeysService: HotkeysService,
    private cdRef: ChangeDetectorRef
  ) { }

  ngOnInit() { }

  ngAfterViewInit() {
    if (this.description.listeningType === 'a') {
      this.audio = `audio/${this.description.fileName}`;
      this.player = this.audioPlayerRef.nativeElement;
      this.prepareEvents();
    } else {
      this.video = `video/${this.description.fileName}`;
      // console.log(this.video);
    }

    this.cdRef.markForCheck();
  }

  ngOnDestroy(): void {
    this.hotkeysService.releaseAll();
    this.hotkeysService.removeEvents(this.componentName);
    this.vgAPI = null;
  }

  onPlayerReady(api: VgAPI) {
    if (this.description.listeningType === 'v') {
      this.vgAPI = api;
      this.prepareEvents();
    }
  }

  @HostListener('window:keydown', ['$event'])
  press(event: KeyboardEvent) {
    this.hotkeysService.press(event.key, this.componentName);
  }

  @HostListener('window:keyup', ['$event'])
  release(event: KeyboardEvent) {
    this.hotkeysService.release(event.key);
  }

  @HostListener('window:focus')
  releaseAll() {
    this.hotkeysService.releaseAll();
  }

  private playStopAudio() {
    this.player.paused
      ? this.player.play()
      : this.player.pause();
  }

  private videoPlayStop() {
    this.vgAPI.getDefaultMedia().state === 'paused'
      ? this.vgAPI.getDefaultMedia().play()
      : this.vgAPI.getDefaultMedia().pause();
  }

  private volumeUp() {
    const value = this.player.volume + 0.2;
    this.player.volume = value >= 1 ? 1 : value;
  }

  private videoVolumeUp() {
    const value = this.vgAPI.volume + 0.2;
    this.vgAPI.volume = value >= 1 ? 1 : value;
  }

  private volumeDown() {
    const value = this.player.volume - 0.2;
    this.player.volume = value <= 0 ? 0 : value;
  }

  private videoVolumeDown() {
    const value = this.vgAPI.volume - 0.2;
    this.vgAPI.volume = value <= 0 ? 0 : value;
  }

  private prepareEvents() {
    const actionsEvents = this.getActionEvents(!!this.audio);
    const actionsEventsDictionary = new ActionStrEventCombinationDictionary(
      this.componentName, actionsEvents);
    this.hotkeysService.addEvents(actionsEventsDictionary);
  }

  private getActionEvents(isAudio: boolean): ActionStrEventCombination[] {
    let actionsEvents: ActionStrEventCombination[];
    const keyAlternativeCombination = this.browserDetectService.includeShift() ? KeyStrCombinations.shiftSpace
      : this.browserDetectService.includeAlt() ? KeyStrCombinations.ctrlAltSpace : KeyStrCombinations.ctrlSpace;

    if (isAudio) {
      actionsEvents = [
        new ActionStrEventCombination(KeyStrCombinations.ctrlLeft, () => --this.player.currentTime),
        new ActionStrEventCombination(KeyStrCombinations.ctrlRight, () => ++this.player.currentTime),
        new ActionStrEventCombination(keyAlternativeCombination, () => this.playStopAudio()),
        new ActionStrEventCombination(KeyStrCombinations.altDown, () => this.volumeDown()),
        new ActionStrEventCombination(KeyStrCombinations.altUp, () => this.volumeUp()),
      ];
    } else {
      actionsEvents = [
        new ActionStrEventCombination(KeyStrCombinations.ctrlLeft, () => this.vgAPI.getDefaultMedia().currentTime -= 1),
        new ActionStrEventCombination(KeyStrCombinations.ctrlRight, () => this.vgAPI.getDefaultMedia().currentTime += 1),
        new ActionStrEventCombination(keyAlternativeCombination, () => this.videoPlayStop()),
        new ActionStrEventCombination(KeyStrCombinations.altDown, () => this.videoVolumeDown()),
        new ActionStrEventCombination(KeyStrCombinations.altUp, () => this.videoVolumeUp()),
      ];
    }

    return actionsEvents;
  }
}
