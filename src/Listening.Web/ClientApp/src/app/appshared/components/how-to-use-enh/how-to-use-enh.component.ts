import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { VideoDescription } from '@app/appshared/models/videoDescrption';

@Component({
  selector: 'appc-how-to-use-enh',
  templateUrl: './how-to-use-enh.component.html',
  styleUrls: ['./how-to-use-enh.component.scss', '../../../shared/styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HowToUseEnhComponent implements OnInit {

  @Input() videoDescriptions: VideoDescription[];
  public isHowUseVisible: boolean = false;

  constructor(private _ref: ChangeDetectorRef) { }

  ngOnInit() {
  }

  changeHowUseVisibility() {
    this.isHowUseVisible = !this.isHowUseVisible;
    this._ref.markForCheck();
  }
  // changeVideoVisibility(description: VideoDescription) {
  //   description.isVisible = !description.isVisible;
  //   this._ref.markForCheck();
  // }
  
}
