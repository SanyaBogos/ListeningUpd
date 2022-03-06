import { ChangeDetectorRef, Component, ElementRef, EventEmitter, Input, OnDestroy, Output, ViewChild } from '@angular/core';
import { AdminService } from '../admin.service';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { Status } from '@app/core/models/status';
import { MySubscriptions } from '@app/shared/models/mySubscription';

@Component({
  selector: 'appc-abstract-change-settings',
  templateUrl: './abstract-change-settings.component.html',
  styleUrls: ['./abstract-change-settings.component.scss']
})
export class AbstractChangeSettingsComponent implements OnDestroy {

  @ViewChild('fileInput', { static: false }) fileInput: ElementRef<HTMLInputElement>;
  protected fileInputHTML: HTMLInputElement;

  @Input() name: string;
  @Input() useAudio: boolean;

  @Output() fileUpdated = new EventEmitter();

  public isFileChanged = false;

  private _subscriptions = new MySubscriptions();

  constructor(
    public ref: ChangeDetectorRef,
    public adminService: AdminService,
    public notificationsService: MyNotificationsService
  ) {
    this._subscriptions.add(
      adminService.useAudio$.subscribe(data => { this.useAudio = data; })
    );
  }

  ngOnDestroy(): void {
    this._subscriptions.remove();
  }

  changeType(value: boolean) {
    this.useAudio = value;
    this.adminService.announceChangeType(value);
  }

  fileChanged(event: any) {
    const fileList: FileList = event.target.files;
    if (fileList.length === 1) {
      const file: File = fileList[0];
      this.isFileChanged = true;
      this.name = file.name;

      /// TODO: probably I'll do that or maybe not... 
      /// currently it's denied by browsers or angular to play audio of file, which is going to be upload...

      // this.type = `audio/${file.name.substring(file.name.indexOf('.') + 1)}`;
      // this.audioType = `audio/mp3`;
      // this.audioSrc = '';

      /*const reader = new FileReader();

      reader.onload = (e: any) => {
        this.audioSrc = e.target.result;
        this.safeAudioSrc = this._sanitizer.sanitize(SecurityContext.URL, e.target.result);
        this._ref.markForCheck();
      };
      reader.readAsDataURL(file);*/

      this.fileUpdated.emit(file);
      this.ref.markForCheck();
    } else if (fileList.length > 1) {
      this.notificationsService.notify(
        'only_one_file', Status.Error, 'to_many_files');
    }
  }

  isFileExist(): boolean {
    if (!this.fileInputHTML)
      return false;

    const fi = this.fileInputHTML;
    return fi.files !== null && fi.files.length > 0 && fi.files[0] !== null;
  }

}
