// tslint:disable:curly
import { Injectable } from '@angular/core';
import { Dictionary } from '../models/dictionary.model';

@Injectable()
export class ObjectProcessService {

  getFilteringPropsFromObject(data: object): Dictionary {
    const props = Object.keys(data);
    const filteringProperties = new Dictionary();

    for (const key of props) {
      const val = Object.getOwnPropertyDescriptor(data, key).value;
      if (val)
        filteringProperties[this.capitalizeFirstLetter(key)] = val;
    }

    return filteringProperties;
  }

  buildCleanedObject(data: object): void {
    const props = Object.keys(data);

    for (const key of props) {
      let description = Object.getOwnPropertyDescriptor(data, key).value;
      if (description)
        description = '';
    }
  }

  private capitalizeFirstLetter(str: string) {
    return str.charAt(0).toUpperCase() + str.slice(1);
  }

}
