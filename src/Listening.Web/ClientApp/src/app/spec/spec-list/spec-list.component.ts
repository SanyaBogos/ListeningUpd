import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { SpecClient, TypeHeaderDto } from 'apiDefinitions';
import { SpecDataService } from '../spec-data.service';

@Component({
  selector: 'appc-spec-list',
  templateUrl: './spec-list.component.html',
  styleUrls: ['./spec-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SpecListComponent implements OnInit {

  public items: TypeHeaderDto[];

  constructor(
    private _ref: ChangeDetectorRef,
    private _specClient: SpecClient,

  ) { }

  ngOnInit() {
    this._specClient.getHeaderDescription()
      .subscribe(data => {
        this.items = data;

        this._ref.markForCheck();
      });
  }

}
