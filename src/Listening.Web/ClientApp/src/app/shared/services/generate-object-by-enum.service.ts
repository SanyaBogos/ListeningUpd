import { Injectable } from '@angular/core';
import { KeyValuePair } from '../models/keyValue';

@Injectable({
  providedIn: 'root'
})
export class GenerateObjectByEnumService {

  constructor() { }

  getItems<T extends KeyValuePair>(keysAndValues: string[], isLower: boolean = false) {
    // const keysAndValues = Object.keys(DeviceType); // result of this: [ "1", "2", "USB", "CD" ]
    const items: T[] = [];
    const halfCount = keysAndValues.length / 2;

    for (let i = 0; i < halfCount; i++) {
      const val = keysAndValues[i];
      const key = keysAndValues[i + halfCount];
      // items.push(new T(Number.parseInt(val, 10), key));
      items.push({
        id: Number.parseInt(val, 10),
        name: isLower ? key.toLowerCase(): key
      } as T);
    }

    return items;
  }
}
