import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { SiteVisibility } from '../models/siteVisibility';

@Component({
  selector: 'appc-optical-recognition',
  templateUrl: './optical-recognition.component.html',
  styleUrls: ['./optical-recognition.component.scss', '../../shared/styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class OpticalRecognitionComponent implements OnInit {

  public isInfoOCRVisible = false;
  public isOwnRecognitionVisible = true;
  public siteVisibilities: SiteVisibility[];

  constructor(private ref: ChangeDetectorRef) {
    this.siteVisibilities = [
      new SiteVisibility('https://www.newocr.com'),
      new SiteVisibility('https://www.onlineocr.net'),
      new SiteVisibility('https://finereaderonline.com'),
      new SiteVisibility('https://ocr.space'),
      new SiteVisibility('https://www.sodapdf.com'),
      new SiteVisibility('http://www.i2ocr.com'),
      new SiteVisibility('https://convertio.co'),
    ];
  }

  ngOnInit() { }

  changeOCRVisibility() {
    this.isInfoOCRVisible = !this.isInfoOCRVisible;
    this.ref.markForCheck();
  }

  changeOCRSiteVisibility(site: SiteVisibility) {
    site.isVisible = !site.isVisible;
    this.ref.markForCheck();
  }

  changeOwnOCRVisibility() {
    this.isOwnRecognitionVisible = !this.isOwnRecognitionVisible;
    this.ref.markForCheck();
  }

  goToLink(url: string) {
    window.open(url, "_blank");
  }
}
