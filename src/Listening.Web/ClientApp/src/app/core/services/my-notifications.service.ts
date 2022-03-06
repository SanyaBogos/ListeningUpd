import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Status } from '../models/status';
import { MessageDescription } from '../models/messageDescription';
import { WaiterDictionary } from '../models/waiterDictionary';

@Injectable()
export class MyNotificationsService {

  private notificationSource = new Subject<MessageDescription>();

  notification$ = this.notificationSource.asObservable();

  waiters: WaiterDictionary;

  constructor() {
    this.waiters = {};
    this.waiters[Status.Success] = 4000;
    this.waiters[Status.Warn] = 6000;
    this.waiters[Status.Info] = 6000;
    this.waiters[Status.Error] = 10000;
  }

  notify(message: string, status: Status, title?: string) {
    const messageDescription = new MessageDescription([message], status, title);
    this.notificationSource.next(messageDescription);
  }

  notifyWithParts(messageParts: string[], status: Status, title?: string) {
    const messageDescription = new MessageDescription(messageParts, status, title);
    this.notificationSource.next(messageDescription);
  }
}
