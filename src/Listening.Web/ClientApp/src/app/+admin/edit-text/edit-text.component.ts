// tslint:disable:curly
import { of } from 'rxjs';
import {
  TextDto, TextClient, FileClient, FileParameter,
  UserViewModel,
  TextWithAdminsListDto,
  UserClient,
  StringIdsDto
} from '../../../apiDefinitions';
import { Router, ActivatedRoute } from '@angular/router';
import {
  Component, OnInit, ViewChild,
  ChangeDetectionStrategy, ChangeDetectorRef, OnDestroy
} from '@angular/core';
import { AdminService } from '../admin.service';
import { ModalComponent } from '../../shared/directives/modal/modal.component';
import { switchMap } from 'rxjs/operators';
import { MyNotificationsService } from '../../core/services/my-notifications.service';
import { Status } from '../../core/models/status';
import { BaseSubscriptionsComponent } from '@app/shared/components/base-subscriptions/base-subscriptions.component';

@Component({
  selector: 'appc-edit-text',
  templateUrl: './edit-text.component.html',
  styleUrls: ['./edit-text.component.scss', '../../shared/styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EditTextComponent extends BaseSubscriptionsComponent implements OnInit {

  @ViewChild(ModalComponent, { static: false })
  private readonly _confirmDeletingModal: ModalComponent;

  private _audioFile: File;
  private _videoFile: File;
  private _isFileChanged: boolean = false;

  public errorTitle: string;
  public errorText: string;
  public useAudio = true;
  public isNewElement = true;


  public textDto: TextDto;
  public admins: UserViewModel[];
  public selectedAdmin: number;

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _ref: ChangeDetectorRef,
    private _textClient: TextClient,
    private _userClient: UserClient,
    private _fileClient: FileClient,
    private _notificationsService: MyNotificationsService,
    public adminService: AdminService
  ) {
    super();

    this._subscriptions.add(
      adminService.useAudio$.subscribe(data => { this.useAudio = data; }),
      adminService.ocrText$.subscribe(data => { this.textDto.text = data; })
    );

  }

  ngOnInit() {
    this._route.params.pipe(
      switchMap((params: any) => {
        if (params && params.textId) {
          this.isNewElement = false;
          return this._textClient.getText(params.textId);
        }

        this.isNewElement = true;
        const result = new TextWithAdminsListDto();
        result.textDto = new TextDto();
        result.textDto.assignee = 0;
        return of<TextWithAdminsListDto>(result);
      })).subscribe(data => {
        this.textDto = data.textDto;
        this.admins = data.admins;
        this.useAudio = this.isNewElement ? true : !!data.textDto.audioName;

        if (data.admins == null || data.admins.length === 0)
          this._userClient.getAdmins().subscribe(admins => {
            this.admins = admins;
            this.admins[0].email = `${this.admins[0].email} *`;
            this.selectedAdmin = this.admins[0].id;
            this._ref.markForCheck();
          });

        if (this.textDto.assignee !== 0)
          this.selectedAdmin = this.admins.find(x => x.id === this.textDto.assignee).id;

        this._ref.markForCheck();
      });
  }

  audioFileUpdate(file: File) {
    this._audioFile = file;
    this.textDto.audioName = file.name;
    this._isFileChanged = true;
    this._ref.markForCheck();
  }

  videoFileUpdate(file: File) {
    this._videoFile = file;
    this.textDto.videoName = file.name;
    this._isFileChanged = true;
    this._ref.markForCheck();
  }

  videoFileYoutubeUpdate(videoName: string) {
    this.textDto.videoName = videoName;
    this._ref.markForCheck();
  }

  checkText(): void {
    if (!this.textDto.text)
      this.errorText = 'text_name_not_empty';
    else if (this.textDto.text.length > 4000)
      this.errorText = 'unsupportable_text_length_4000';
    else
      this.errorText = '';
  }

  checkTitle(): void {
    this.errorTitle = !this.textDto.title
      ? 'title_not_empty' : '';
  }

  isAudioAndVideoSettingsCorrect(): boolean {
    if (this.useAudio && !this.textDto.audioName) {
      this._notificationsService.notify('audio_should_fill', Status.Error);
      return false;
    }

    if (!this.useAudio && !this.textDto.videoName) {
      this._notificationsService.notify('video_should_fill', Status.Error);
      return false;
    }

    return true;
  }

  isErrorExist() {
    return this.errorTitle || this.errorText;
  }

  countrySelected(country: string) {
    this.textDto.country = country;
  }

  complexitySelected(complexityValue: number) {
    this.textDto.complexity = complexityValue;
  }

  save() {
    this.checkTitle();
    this.checkText();

    if (this.isErrorExist() && !this.isAudioAndVideoSettingsCorrect())
      return;

    if (this.useAudio && this._isFileChanged && this._audioFile)
      this._fileClient.saveListeningAudio(this.textDto.audioName, { data: this._audioFile } as FileParameter)
        .subscribe((data) => {
          this.textDto.audioName = data.name;
          this._notificationsService.notify(
            'scs_aud_sav', Status.Success);
          this._isFileChanged = false;
          this.saveText();
        });
    else if (!this.useAudio && this._isFileChanged && this._videoFile)
      this._fileClient.saveListeningVideo(this.textDto.videoName, { data: this._videoFile } as FileParameter)
        .subscribe((data) => {
          this.textDto.videoName = data.name;
          this._notificationsService.notify(
            'scs_vid_sav', Status.Success);
          this._isFileChanged = false;
          this.saveText();
        });
    else
      this.saveText();
  }

  remove() {
    this._confirmDeletingModal.hide();
    this._textClient.deleteText(this.textDto.id)
      .subscribe(() => {
        this._notificationsService.notify(
          'scs_del', Status.Success);
        this._router.navigate(['admin/texts']);
      }, () => {
        this._router.navigate(['admin/texts']);
      });
  }

  private saveText() {
    if (this.useAudio)
      this.textDto.videoName = '';
    else
      this.textDto.audioName = '';

    if (!this.textDto.id)
      this._textClient.postText(this.textDto)
        .subscribe((data: StringIdsDto) => {
          this._notificationsService.notify(
            'scs_add_txt', Status.Success);
          this.textDto.id = data.ids[0];
        });
    else
      this._textClient.putText(this.textDto)
        .subscribe((warningExist) => {
          if (warningExist)
            this._notificationsService.notify(
              'text_referenced', Status.Warn);
          else
            this._notificationsService.notify(
              'scs_upd_txt', Status.Success);
        });
  }

}
