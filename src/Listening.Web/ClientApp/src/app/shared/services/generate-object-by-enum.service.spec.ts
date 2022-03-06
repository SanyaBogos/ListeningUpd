import { TestBed } from '@angular/core/testing';

import { GenerateObjectByEnumService } from './generate-object-by-enum.service';

describe('GenerateObjectByEnumService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GenerateObjectByEnumService = TestBed.get(GenerateObjectByEnumService);
    expect(service).toBeTruthy();
  });
});
