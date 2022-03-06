import { TestBed, inject } from '@angular/core/testing';

import { ChangeGridService } from './change-grid.service';

describe('ChecChangeGridServiceGuessingService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ChangeGridService]
    });
  });

  it('should be created', inject([ChangeGridService], (service: ChangeGridService) => {
    expect(service).toBeTruthy();
  }));
});
