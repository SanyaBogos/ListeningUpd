import { Component, OnInit, OnDestroy, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { MessageDescription } from '../../models/messageDescription';
import { Status } from '../../models/status';
import { MessageDescriptionEnhanced } from '../../models/messageDescriptionEnhanced';
import { MyNotificationsService } from '../../services/my-notifications.service';
import { BaseSubscriptionsComponent } from '@app/shared/components/base-subscriptions/base-subscriptions.component';

@Component({
  selector: 'appc-my-notifications',
  templateUrl: './my-notifications.component.html',
  styleUrls: ['./my-notifications.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MyNotificationsComponent extends BaseSubscriptionsComponent implements OnInit {

  private maxId = 0;
  private baseClass = 'growl-item alert icon alert-dismissable';

  public messages: MessageDescriptionEnhanced[] = [];

  constructor(
    private cdRef: ChangeDetectorRef,
    public notificationsService: MyNotificationsService,
  ) {
    super();

    this._subscriptions.add(
      notificationsService.notification$.subscribe(this.notify.bind(this))
    );
  }

  ngOnInit() { }

  notify(messageDescription: MessageDescription) {
    const maxId = this.maxId;
    const timeoutCallbackNumber = window.setTimeout(() => this.closeNotification(maxId),
      this.notificationsService.waiters[messageDescription.status]);
    this.messages.push(
      new MessageDescriptionEnhanced(this.maxId++, messageDescription, timeoutCallbackNumber));
    this.cdRef.detectChanges();
  }

  closeNotification(id: number) {
    const index = this.messages.findIndex(e => e.id === id);
    window.clearTimeout(this.messages[index].timeoutCallbackNumber);
    this.messages.splice(index, 1);
    this.cdRef.detectChanges();
  }

  getClass(status: Status) {
    const base = `${this.baseClass} alert-`;
    switch (status) {
      case Status.Success: return `${base}success`;
      case Status.Warn: return `${base}warning`;
      case Status.Info: return `${base}info`;
      case Status.Error: return `${base}error alert-danger`;
    }
  }

}
