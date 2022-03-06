import { TestBed, inject } from '@angular/core/testing';

import { ChartEventsService } from './chart-events.service';

describe('ChartEventsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ChartEventsService]
    });
  });

  it('should be created', inject([ChartEventsService], (service: ChartEventsService) => {
    expect(service).toBeTruthy();
  }));
});
