import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { ErrorLogDto, LogClient, LogDto } from 'apiDefinitions';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { Status } from '@app/core/models/status';
import { AccountService } from '@app/core/services';

@Component({
  selector: 'appc-log',
  templateUrl: './log.component.html',
  styleUrls: ['./log.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LogComponent implements OnInit {

  public files: string[];
  // public selectedFile: string;
  public isError: boolean;
  public logs: LogDto[];
  public errors: ErrorLogDto[];


  constructor(
    private _cdRef: ChangeDetectorRef,
    private _logClient: LogClient,
    private _notificationsService: MyNotificationsService,
    private _accountService: AccountService
  ) {
  }

  ngOnInit() {
  }

  public getLogFiles(isError: boolean) {
    this.isError = isError;

    this._logClient.getFilesArray(isError)
      .subscribe(files => {
        this.files = files;
        this._cdRef.markForCheck();
      });

  }

  fileSelected(fileName: string) {
    if (this.isError)
      this._getErrors(fileName);
    else
      this._getLogs(fileName);
  }

  clear() {
    this.logs.length = 0;
    this.errors.length = 0;
    this.files.length = 0;    
  }

  private _getLogs(fileName: string) {
    if (!fileName)
      return;

    this._logClient.getLogs(fileName)
      .subscribe(logs => {
        this.logs = logs;
        this._cdRef.markForCheck();
      });
  }

  private _getErrors(fileName: string) {
    if (!fileName)
      return;

    this._logClient.getErrors(fileName)
      .subscribe(errors => {
        this.errors = errors;
        this._cdRef.markForCheck();
      });
  }

}
