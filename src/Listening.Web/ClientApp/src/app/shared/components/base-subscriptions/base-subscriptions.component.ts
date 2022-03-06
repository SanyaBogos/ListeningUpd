import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Output, EventEmitter, Input, OnDestroy } from '@angular/core';
import { MySubscriptions } from '@app/shared/models/mySubscription';

@Component({
  selector: 'appc-base-subscriptions',
  templateUrl: './base-subscriptions.component.html',
  styleUrls: ['./base-subscriptions.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BaseSubscriptionsComponent implements OnDestroy {

  protected _subscriptions = new MySubscriptions();

  ngOnDestroy(): void {
    this._subscriptions.remove();
  }
}
