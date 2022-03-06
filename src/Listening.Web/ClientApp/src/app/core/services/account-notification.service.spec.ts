import { TestBed, inject } from '@angular/core/testing';

import { AccountNotificationService } from './account-notification.service';

describe('AccountNotificationService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AccountNotificationService]
    });
  });

  it('should be created', inject([AccountNotificationService], (service: AccountNotificationService) => {
    expect(service).toBeTruthy();
  }));
});
