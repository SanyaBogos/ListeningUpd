import { TestBed, inject } from '@angular/core/testing';

import { ObjectProcessService } from './object-process.service';

describe('ObjectProcessService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ObjectProcessService]
    });
  });

  it('should be created', inject([ObjectProcessService], (service: ObjectProcessService) => {
    expect(service).toBeTruthy();
  }));
});
