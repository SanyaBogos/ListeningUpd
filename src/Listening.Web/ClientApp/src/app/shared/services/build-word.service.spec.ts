import { TestBed, inject } from '@angular/core/testing';

import { BuildWordService } from './build-word.service';

describe('BuildWordService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BuildWordService]
    });
  });

  it('should ...', inject([BuildWordService], (service: BuildWordService) => {
    expect(service).toBeTruthy();
  }));

});
