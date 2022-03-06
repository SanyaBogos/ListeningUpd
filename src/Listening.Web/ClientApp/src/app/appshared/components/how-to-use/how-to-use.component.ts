import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { VideoDescription } from '@app/appshared/models/videoDescrption';
import { VgAPI } from 'videogular2/compiled/core';

@Component({
  selector: 'appc-how-to-use',
  templateUrl: './how-to-use.component.html',
  styleUrls: ['./how-to-use.component.scss', '../../../shared/styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HowToUseComponent implements OnInit {

  @Input() videoDescriptions: VideoDescription[];

  private vgAPI: VgAPI;
  
  public duration: number;

  constructor(private _ref: ChangeDetectorRef) { }

  ngOnInit() {
  }

  changeVideoVisibility(description: VideoDescription) {
    description.isVisible = !description.isVisible;
    this._ref.markForCheck();
  }

  onPlayerReady(api: VgAPI) {
    this.vgAPI = api;
    const defaultMedia = this.vgAPI.getDefaultMedia();
    defaultMedia.subscriptions.durationChange
      .subscribe(() => {
        this.duration = defaultMedia.duration;
        this._ref.markForCheck();
      });
  }
  
}
