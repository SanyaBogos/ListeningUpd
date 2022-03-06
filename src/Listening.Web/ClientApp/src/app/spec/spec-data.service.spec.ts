import { TestBed } from '@angular/core/testing';

import { SpecDataService } from './spec-data.service';

describe('SpecDataService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SpecDataService = TestBed.get(SpecDataService);
    expect(service).toBeTruthy();
  });
});
