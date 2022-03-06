import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Status } from '@app/core/models/status';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { BackupClient, FileParameter, FileResponse, TextClient } from 'apiDefinitions';
import { BackupType } from '../models/backupType';

@Component({
  selector: 'appc-backup',
  templateUrl: './backup.component.html',
  styleUrls: ['./backup.component.scss', '../../shared/styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BackupComponent implements OnInit, AfterViewInit {

  @ViewChild('rstrBlgInpt', { static: false }) rstrBlgInpt: ElementRef<HTMLInputElement>;
  private rstrBlgInptHTML: HTMLInputElement;

  @ViewChild('rstrSpcInpt', { static: false }) rstrSpcInpt: ElementRef<HTMLInputElement>;
  private rstrSpcInptHTML: HTMLInputElement;

  @ViewChild('rstrSQLInpt', { static: false }) rstrSQLInpt: ElementRef<HTMLInputElement>;
  private rstrSQLInptHTML: HTMLInputElement;

  private _dictionaryActions: Map<BackupType, Function>;
  public keys: BackupType[];

  constructor(
    private _backupClient: BackupClient,
    private _notificationsService: MyNotificationsService
    // private _textClient: TextClient
  ) {
    // for now this code is too complex, however, let's store it in git history
    const { getSpecBackup, getBlogBackup, getSQLBackup, getFullBackup } = this._backupClient;

    this._dictionaryActions = new Map<BackupType, Function>();

    this._dictionaryActions.set(BackupType.Spec, getSpecBackup);
    this._dictionaryActions.set(BackupType.Blog, getBlogBackup);
    this._dictionaryActions.set(BackupType.SQL, getSQLBackup);
    this._dictionaryActions.set(BackupType.Full, getFullBackup);

    // this.keys = this._dictionaryActions.keys();
    // console.log(this._dictionaryActions.keys());
    // console.log(this._dictionaryActions.values());

  }

  ngOnInit(): void { }

  ngAfterViewInit() {
    this.rstrBlgInptHTML = this.rstrBlgInpt.nativeElement;
    this.rstrSpcInptHTML = this.rstrSpcInpt.nativeElement;
    this.rstrSQLInptHTML = this.rstrSQLInpt.nativeElement;
  }

  // for now this code is too complex, however, let's store it in git history
  getBackup(backupType: BackupType) {
    this._dictionaryActions[backupType]()
      .subscribe((fileDescription) => {
        saveAs(fileDescription.data, fileDescription.fileName);
      });
  }

  getBackupSpecFiles() {
    this._backupClient.getSpecBackup()
      .subscribe(this._saveAs);
  }

  getBackupBlogFiles() {
    this._backupClient.getBlogBackup()
      .subscribe(this._saveAs);
  }

  getBackupSQLFiles() {
    this._backupClient.getSQLBackup()
      .subscribe(this._saveAs);
  }

  getBackupFullFiles() {
    this._backupClient.getFullBackup()
      .subscribe(this._saveAs);
  }

  restoreBlog() {
    this.rstrBlgInptHTML.click();
  }

  restoreSpec() {
    this.rstrSpcInptHTML.click();
  }

  restoreSQL() {
    this.rstrSQLInptHTML.click();
  }

  restoreFileSelected(event: any, option: number) {
    const fileList: FileList = event.target.files;
    const file: File = fileList[0];
    const fileParam = { data: file, fileName: file.name } as FileParameter;

    const handler = this._successHandler.bind(this);

    switch (option) {
      case 1:
        this._backupClient.restoreBlogData(fileParam)
          .subscribe(handler);
        break;

      case 2:
        this._backupClient.restoreBlogData(fileParam)
          .subscribe(handler);
        break;

      case 3:
        this._backupClient.restoreBlogData(fileParam)
          .subscribe(handler);
        break;

      default:
        break;
    }
  }

  private _successHandler() {
    this._notificationsService.notify('sccs_rstr', Status.Success);
  }

  private _saveAs(fileDescription: FileResponse) {
    saveAs(fileDescription.data, fileDescription.fileName);
  }

}
