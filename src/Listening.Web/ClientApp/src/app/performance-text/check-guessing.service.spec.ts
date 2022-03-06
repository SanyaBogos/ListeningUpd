import { TestBed, inject } from '@angular/core/testing';

import { CheckGuessingService } from './check-guessing.service';

describe('CheckGuessingService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CheckGuessingService]
    });
  });

  it('should be created', inject([CheckGuessingService], (service: CheckGuessingService) => {
    expect(service).toBeTruthy();
  }));
});
