import { TestBed, inject } from '@angular/core/testing';

import { MyNotificationsService } from './my-notifications.service';

describe('NotificationsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MyNotificationsService]
    });
  });

  it('should ...', inject([MyNotificationsService], (service: MyNotificationsService) => {
    expect(service).toBeTruthy();
  }));
});
