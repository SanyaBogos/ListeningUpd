import { TestBed, inject } from '@angular/core/testing';

import { SeparatedTextService } from './separated-text.service';

describe('SeparatedTextService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SeparatedTextService]
    });
  });

  it('should be created', inject([SeparatedTextService], (service: SeparatedTextService) => {
    expect(service).toBeTruthy();
  }));
});
