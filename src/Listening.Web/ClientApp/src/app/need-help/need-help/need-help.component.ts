import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'appc-need-help',
  templateUrl: './need-help.component.html',
  styleUrls: ['./need-help.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NeedHelpComponent implements OnInit {

  constructor(
    public cdRef: ChangeDetectorRef
  ) { }

  ngOnInit() {
  }

}
