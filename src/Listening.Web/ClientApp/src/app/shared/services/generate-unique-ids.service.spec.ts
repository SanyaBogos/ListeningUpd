import { TestBed } from '@angular/core/testing';

import { GenerateUniqueIdsService } from './generate-unique-ids.service';

describe('GenerateUniqueIdsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GenerateUniqueIdsService = TestBed.get(GenerateUniqueIdsService);
    expect(service).toBeTruthy();
  });
});
