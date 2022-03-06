import { Component, ChangeDetectionStrategy } from '@angular/core';
import { routerTransition } from '../router.animations';

@Component({
  selector: 'appc-real-time',
  animations: [routerTransition],
  templateUrl: './real-time.component.html',
  styleUrls: ['./real-time.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RealTimeComponent {
  menus = [
    { route: 'chat', text: 'app_nav_signalr' },
    { route: 'video-rtc-conversation', text: 'video_conversation' }
  ];

  public getState(outlet: any) {
    return outlet.activatedRouteData.state;
  }

}
