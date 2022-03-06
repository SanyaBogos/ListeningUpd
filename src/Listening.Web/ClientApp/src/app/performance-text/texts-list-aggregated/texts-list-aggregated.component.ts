import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
// import { routerTransition } from '../../router.animations';

@Component({
  selector: 'appc-texts-list-aggregated',
  templateUrl: './texts-list-aggregated.component.html',
  styleUrls: ['./texts-list-aggregated.component.scss'],
  // animations: [routerTransition()],
  // host: hostStyle()
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TextsListAggregatedComponent implements OnInit {

  constructor(
    private cdRef: ChangeDetectorRef,
  ) { }

  ngOnInit() {
    this.cdRef.markForCheck();
  }

}
