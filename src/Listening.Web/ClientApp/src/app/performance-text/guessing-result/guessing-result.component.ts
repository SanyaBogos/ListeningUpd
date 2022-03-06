import { Component, ViewChild, ChangeDetectionStrategy, ChangeDetectorRef, Input, AfterViewInit } from '@angular/core';
import { ModalComponent } from '../../shared/directives/modal/modal.component';
import { DiagramElement } from '../models/diagram';
import { Router } from '@angular/router';

@Component({
  selector: 'appc-guessing-result',
  templateUrl: './guessing-result.component.html',
  styleUrls: ['./guessing-result.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GuessingResultComponent implements AfterViewInit {

  @Input() resultsData: DiagramElement[];
  @Input() resultsWordsData: DiagramElement[];

  @ViewChild(ModalComponent, { static: false })
  private readonly _modalGuessingResults: ModalComponent;

  public view: number[] = [480, 200];
  public colorScheme = {
    domain: ['#e0e0e0', '#eb9316', '#419641', '#2d6ca2']
  };
  public colorSchemeWords = {
    domain: ['#419641', '#eb9316', 'gold']
  };

  constructor(
    private _cdRef: ChangeDetectorRef,
    private _router: Router
  ) { }

  ngAfterViewInit() {
    this._modalGuessingResults.show();
    this._cdRef.markForCheck();
  }

  hideModal() {
    this._modalGuessingResults.hide();
    this._cdRef.markForCheck();
  }

  goToTexts() {
    this._router.navigate(['perfTexts']);
    this._cdRef.markForCheck();
  }

  goToResults() {
    this._router.navigate(['results']);
    this._cdRef.markForCheck();
  }
}
