import { TestBed, inject } from '@angular/core/testing';

import { BuildResultService } from './build-result.service';

describe('BuildResultService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BuildResultService]
    });
  });

  it('should be created', inject([BuildResultService], (service: BuildResultService) => {
    expect(service).toBeTruthy();
  }));
});
