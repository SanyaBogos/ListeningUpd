import { TestBed, inject } from '@angular/core/testing';

import { TranslateTextService } from './translate-text.service';

describe('TranslateTextService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TranslateTextService]
    });
  });

  it('should be created', inject([TranslateTextService], (service: TranslateTextService) => {
    expect(service).toBeTruthy();
  }));
});
