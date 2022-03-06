import { TestBed } from '@angular/core/testing';

import { GeneratePreseedService } from './generate-preseed.service';

describe('GeneratePreseedService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GeneratePreseedService = TestBed.get(GeneratePreseedService);
    expect(service).toBeTruthy();
  });
});
