import { Injectable } from '@angular/core';
import { detect } from 'detect-browser';

@Injectable()
export class BrowserDetectService {

  constructor() { }

  isFirefox() {
    return detect().name === 'firefox';
  }

  includeShift() {
    const browserName = detect().name;
    return browserName === 'ie';
  }

  includeAlt() {
    const browserName = detect().name;
    return browserName === 'firefox' || browserName === 'opera' || browserName === 'edge';
  }
}
