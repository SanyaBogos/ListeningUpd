import { TestBed } from '@angular/core/testing';

import { BukvitsaService } from './bukvitsa.service';

describe('BukvitsaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BukvitsaService = TestBed.get(BukvitsaService);
    expect(service).toBeTruthy();
  });
});
