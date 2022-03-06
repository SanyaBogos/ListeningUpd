// tslint:disable:curly
import { Injectable } from '@angular/core';
import * as _ from 'underscore';
import { ActionStrEventCombinationDictionary } from './models/keyStrCombinations';

@Injectable()
export class HotkeysService {

  private keysPressed: string[] = [];
  private actionsEvents: ActionStrEventCombinationDictionary[] = [];

  constructor() { }

  press(key: string, name: string) {
    if (this.keysPressed.indexOf(key) === -1)
      this.keysPressed.push(key);
    const keysPressed = this.keysPressed;

    const specialActionsEvents = _.findWhere(this.actionsEvents, { name: name });

    if (!specialActionsEvents)
      return;

    _.each(specialActionsEvents.actionEventCombination, (e) => {
      if (_.isEqual(keysPressed, e.combination))
        e.action();
    });
  }

  release(key: string) {
    let indexToRemove = this.keysPressed.indexOf(key);
    while (indexToRemove !== -1) {
      this.keysPressed.splice(indexToRemove, 1);
      indexToRemove = this.keysPressed.indexOf(key);
    }
  }

  releaseAll() {
    this.keysPressed.length = 0;
  }

  addEvents(actionsEvents: ActionStrEventCombinationDictionary): void {
    this.actionsEvents.push(actionsEvents);
  }

  removeEvents(name: string) {
    const index = _.findIndex(this.actionsEvents, { name: name });
    this.actionsEvents.splice(index, 1);
  }

}
