import { TestBed } from '@angular/core/testing';

import { BrowserDetectService } from './browser-detect.service';

describe('BrowserDetectService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BrowserDetectService = TestBed.get(BrowserDetectService);
    expect(service).toBeTruthy();
  });
});
