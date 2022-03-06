import { TestBed } from '@angular/core/testing';

import { ArchitectureWithVersionsService } from './architecture-with-versions.service';

describe('ArchitectureWithVersionsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ArchitectureWithVersionsService = TestBed.get(ArchitectureWithVersionsService);
    expect(service).toBeTruthy();
  });
});
