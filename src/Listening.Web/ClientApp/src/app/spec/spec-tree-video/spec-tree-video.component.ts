import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Input, HostListener, AfterViewInit } from '@angular/core';
import { TreeNode } from 'primeng/api';
import { AccountService } from '@app/core';
import { VgAPI } from 'videogular2/compiled/core';
import { TimeStamp } from '@app/appshared/models/timeStamp';
import { SpecDataService } from '../spec-data.service';
import { Video } from '../models/video';
import { AppService } from '@app/app.service';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { Status } from '@app/core/models/status';
import { ClipboardService } from 'ngx-clipboard';
import { ActivatedRoute, Router } from '@angular/router';
import { VideoIdentifier } from '../models/videoIdentifier';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { PathOut } from '../models/pathOut';

@Component({
  selector: 'appc-spec-tree-video',
  templateUrl: './spec-tree-video.component.html',
  styleUrls: ['./spec-tree-video.component.scss', '../../shared/styles/common-iniline.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SpecTreeVideoComponent implements OnInit, AfterViewInit {

  private play: string = 'fa fa-play-circle text-success';
  private vgAPI: VgAPI;

  @Input() files: TreeNode[];

  public prevSelectedFile: TreeNode;
  public videoSrc: string;
  public description: string;
  public name: string;
  public videoId: number;
  public timeShiftInSeconds: number;

  public speeds: number[] = [];
  public selectedSpeed: number = 1;
  public duration: number;
  public timeShift: number = 5;
  public menuTooltip: string;

  public showCommentArea: boolean;
  public showFiles: boolean = true;

  public isAdm: boolean = false;
  public isEdit: boolean = false;

  constructor(
    private _ref: ChangeDetectorRef,
    private _accountService: AccountService,
    private _notificationsService: MyNotificationsService,
    private _clipboardService: ClipboardService,
    private _route: ActivatedRoute,
    private _appService: AppService,
    private _specDataService: SpecDataService
  ) {
    for (let i = 1; i <= 20; i++)
      this.speeds.push(i / 4);

    this.isAdm = this._accountService.isSpecAdm();
  }

  ngOnInit() {
    if (!this._accountService.isSpec() && !this._accountService.isSpecAdm())
      return;

    this._setParents(this.files);

    this._route.params
      .subscribe((params: VideoIdentifier) => {
        this.videoId = params.videoId;
        this.timeShiftInSeconds = params.timeShiftInSeconds;
        this._ref.markForCheck();
      });
  }

  ngAfterViewInit() {
    if (!this.videoId)
      return;

    const pathOut = new PathOut();
    this._getPath(this.videoId, this.files, pathOut);

    this._expandParents(pathOut.node);
    pathOut.node.icon = this.play;
    this.prevSelectedFile = pathOut.node;
    this.videoSrc = pathOut.path;

    this._ref.detectChanges();
  }


  nodeSelect(event: any) {
    const node = event.node as TreeNode;
    const icon = node.icon as string;

    if (icon && !(icon.indexOf('folder') > -1)) {
      if (this.prevSelectedFile)
        this.prevSelectedFile.icon = this._specDataService.getVideoClass();

      node.icon = this.play;

      this.prevSelectedFile = node;
      this.videoSrc = null;

      setTimeout(() => {
        const data = JSON.parse(node.data) as Video;

        this.videoSrc = data.path;
        this.description = data.description;
        this.videoId = data.id;
        this.name = node.label;
        this.selectedSpeed = 1;
        this.timeShiftInSeconds = 0;
        this._ref.markForCheck();
      }, 100);
    }

    this._ref.markForCheck();
  }

  speedSelected(speed: number) {
    this.vgAPI.playbackRate = speed;
    this._ref.markForCheck();
  }

  onPlayerReady(api: VgAPI) {
    this.vgAPI = api;
    const defaultMedia = this.vgAPI.getDefaultMedia();

    if (this.timeShiftInSeconds)
      defaultMedia.currentTime = this.timeShiftInSeconds;

    defaultMedia.subscriptions.durationChange
      .subscribe(() => {
        this.duration = defaultMedia.duration;
        this._ref.markForCheck();
      });
  }

  getTimeStamp(timeStamp: TimeStamp) {
    // if (event.type === 'f')
    //   this.cutFromValue = event.time;
    // else
    //   this.cutToValue = event.time;

    // const areValuesExist = this.cutFromValue && this.cutToValue;

    // if (areValuesExist && this.cutFromValue > this.cutToValue)
    //   this.error = 'video_error_from_to_diff';
    // else if (areValuesExist && this.cutFromValue === this.cutToValue)
    //   this.error = 'video_error_same_value';
    // else
    //   this.error = '';

    // timeStamp.time

    this._ref.markForCheck();
  }

  setTimeToPlayer(seconds: number) {
    if (!this.vgAPI)
      return;

    this.vgAPI.getDefaultMedia().currentTime = seconds;
    this._ref.markForCheck();
  }

  shiftTime(isBack: boolean) {
    if (!this.vgAPI)
      return;

    let current = this.vgAPI.getDefaultMedia().currentTime;

    if (isBack)
      current -= this.timeShift;
    else
      current += this.timeShift;

    this.vgAPI.getDefaultMedia().currentTime = current;

    this._ref.markForCheck();
  }

  menuNodeTooltipOver(param: any) {
    if (param && param.target && param.target.textContent) {
      this.menuTooltip = param.target.textContent;
    }
  }

  menuNodeTooltipOut(param: any) {
    this.menuTooltip = '';
  }

  getVideoUrl() {
    const fullHref = window.location.href;
    let slashCount = 0;
    let finalIndex = 0;

    for (let i = 0; i < fullHref.length; i++) {
      if (fullHref[i] == '/' && fullHref[i - 1] != '/' && fullHref[i + 1] != '/') {
        if (++slashCount == 4) {
          finalIndex = i;
          break;
        }
      }
    }

    const baseUrl = finalIndex == 0 ? fullHref : fullHref.substring(0, finalIndex);
    const time = this.vgAPI.getDefaultMedia().currentTime;
    this._clipboardService.copyFromContent(`${baseUrl}/${this.videoId}/${time}`);

    const copyMessage = this._appService.appData.content['copied_to_buffer'];
    this._notificationsService.notify(copyMessage, Status.Info);
  }

  @HostListener('window:keydown', ['$event'])
  press(event: KeyboardEvent) {
    if ((<any>(event.target)).id === 'myVid')
      event.preventDefault();

    if (event.key === 'ArrowLeft')
      this.shiftTime(true);
    else if (event.key === 'ArrowRight')
      this.shiftTime(false);
  }

  get getTimeFromPlayer(): number {
    if (!this.vgAPI || !this.vgAPI.getDefaultMedia())
      return 0;

    return this.vgAPI.getDefaultMedia().currentTime;
  }

  private _getPath(id: number, files: TreeNode[], pathOut: PathOut) {
    for (const file of files) {
      if (file.data) {
        const data = JSON.parse(file.data) as Video;

        if (data.id == id) {
          pathOut.path = data.path;
          pathOut.node = file;
        }
      } else if (file.children && file.children.length > 0) {
        this._getPath(id, file.children, pathOut);
      }
    }

    return;
  }

  private _expandParents(file: TreeNode) {
    let currentFile = file;

    do {
      currentFile = currentFile.parent;
      currentFile.expanded = true;
    } while (currentFile.parent);
  }

  private _setParents(files: TreeNode[]) {
    for (const file of files) {
      if (file.children && file.children.length > 0) {
        for (const child of file.children)
          child.parent = file;

        this._setParents(file.children);
      }
    }
  }

}
