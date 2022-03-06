import { TestBed, inject } from '@angular/core/testing';

import { CheckInputService } from './check-input.service';

describe('CheckInputService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CheckInputService]
    });
  });

  it('should be created', inject([CheckInputService], (service: CheckInputService) => {
    expect(service).toBeTruthy();
  }));
});
