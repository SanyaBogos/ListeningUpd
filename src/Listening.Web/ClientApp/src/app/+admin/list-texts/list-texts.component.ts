import { Router } from '@angular/router';
import { Component, OnInit, ChangeDetectionStrategy, /*ChangeDetectorRef,*/ ViewChild, ElementRef, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { TextClient, TextDescriptionDto, FileParameter, PagedDataViewModelOfTextDescriptionEnhancedDto, AdminTextQueryViewModel, SqlBackupClient } from '../../../apiDefinitions';
import { saveAs } from 'file-saver';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { Status } from '@app/core/models/status';
import { Observable } from 'rxjs';
import { FilterSortService } from '@app/shared/services/filter-sort.service';

@Component({
  selector: 'appc-list-texts',
  templateUrl: './list-texts.component.html',
  styleUrls: ['./list-texts.component.scss', '../../shared/styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ListTextsComponent implements OnInit, AfterViewInit {

  @ViewChild('restoreInput', { static: false }) restoreInput: ElementRef<HTMLInputElement>;
  private restoreInputHTML: HTMLInputElement;

  @ViewChild('restoreSqlInput', { static: false }) restoreSqlInput: ElementRef<HTMLInputElement>;
  private restoreSqlInputHTML: HTMLInputElement;

  private p = 1;
  private itemPerPage = 10;
  private query: AdminTextQueryViewModel;

  public textDescriptions$: Observable<PagedDataViewModelOfTextDescriptionEnhancedDto>;

  constructor(
    private _cdRef: ChangeDetectorRef,
    private _router: Router,
    private _notificationsService: MyNotificationsService,
    public filterSortService: FilterSortService,
    public textClient: TextClient,
    public sqlBackupClient: SqlBackupClient
  ) {
    filterSortService.query$
      .subscribe(data => {
        Object.assign(this.query, data);

        this.textDescriptions$ = this.textClient.getTextsEnhanced(this.query);
        this._cdRef.markForCheck();
      });

  }

  ngOnInit() {
    this.query = new AdminTextQueryViewModel();
    this.query.page = this.p;
    this.query.isAscending = true;
    this.query.elementsPerPage = this.itemPerPage;

    this.refreshData();
  }

  ngAfterViewInit() {
    this.restoreInputHTML = this.restoreInput.nativeElement;
    this.restoreSqlInputHTML = this.restoreSqlInput.nativeElement;
  }

  editText(textDescription: TextDescriptionDto) {
    this._router.navigate(['admin/edit', textDescription.textId]);
  }

  createNewText() {
    this._router.navigate(['admin/edit']);
  }

  createBackup() {
    this.textClient.getBackup()
      .subscribe((fileDescription) => {
        saveAs(fileDescription.data, fileDescription.fileName);
      });
  }

  createSqlBackup() {
    this.sqlBackupClient.getBackup()
      .subscribe((fileDescription) => {
        saveAs(fileDescription.data, fileDescription.fileName);
      });
  }

  restoreBackup() {
    this.restoreInputHTML.click();
  }

  restoreSqlBackup() {
    this.restoreSqlInputHTML.click();
  }

  restoreFileSelected(event: any, isSql: boolean = false) {
    const fileList: FileList = event.target.files;
    const file: File = fileList[0];
    const fileParam = { data: file, fileName: file.name } as FileParameter;
    const self = this;

    const successHandler = () => {
      self._notificationsService.notify('success_restore', Status.Success);
    };

    if (!isSql)
      this.textClient.restoreFromBackup(fileParam)
        .subscribe(successHandler);
    else
      this.sqlBackupClient.restoreFromBackup(fileParam)
        .subscribe(successHandler);
  }


  pageChanged(page: number) {
    this.p = page;
    this.query.page = page;
    this.textDescriptions$ = this.textClient.getTextsEnhanced(this.query);
    this._cdRef.markForCheck();
  }

  refreshData() {
    this.textDescriptions$ = this.textClient.getTextsEnhanced(this.query);
    this._cdRef.markForCheck();
  }
}
