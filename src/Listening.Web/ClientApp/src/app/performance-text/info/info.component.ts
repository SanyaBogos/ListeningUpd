import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ModalComponent } from '../../shared/directives/modal/modal.component';
import { BrowserDetectService } from '@app/shared/services/browser-detect.service';

@Component({
  selector: 'appc-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.scss'],
  providers: [BrowserDetectService],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class InfoComponent implements OnInit {

  @ViewChild(ModalComponent, { static: false })
  private readonly modalInfo: ModalComponent;

  public hideExceedFields = false;
  public includeShift = false;
  public includeAlt = false;

  constructor(
    private browserDetectService: BrowserDetectService,
    private cdRef: ChangeDetectorRef,
    private router: Router
  ) {
    const currentUrl = this.router.url;
    this.hideExceedFields = currentUrl.indexOf('/j/') > -1;
    this.includeShift = this.browserDetectService.includeShift();
    this.includeAlt = this.browserDetectService.includeAlt();
  }

  ngOnInit() {
  }

  public showInfoModal() {
    this.modalInfo.show();
    this.cdRef.markForCheck();
  }

  public hideInfoModal() {
    this.modalInfo.hide();
    this.cdRef.markForCheck();
  }

}
