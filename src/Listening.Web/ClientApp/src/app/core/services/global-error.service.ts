// tslint:disable:curly
import { ErrorHandler, Injectable, ApplicationRef, Injector /*, ChangeDetectorRef*/ } from '@angular/core';
import { MyNotificationsService } from './my-notifications.service';
import { Status } from '../models/status';
import { UtilityService } from './utility.service';
import { HttpErrorResponse } from '@angular/common/http';


@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

  private loginMessage = 'please_log_in';
  private unauthorizedMessage = 'unauthorized';
  private contactAdminMessage = 'contact_admin';
  private internalErrorMessage = 'internal_error';

  constructor(
    private notificationsService: MyNotificationsService,
    private inj: Injector
  ) { }

  handleError(errorResponse: any): void {
    const utilityService = this.inj.get(UtilityService);
    // const cdRef = this.inj.get(ChangeDetectorRef);

    if (errorResponse.rejection instanceof HttpErrorResponse) {
      const error = <HttpErrorResponse>errorResponse.rejection;
      this.notificationsService.notify(
        error.error.message, Status.Error);

      // cdRef.markForCheck();
      return;
    }

    if (errorResponse.status === 401) {
      utilityService.saveLastUrl();
      sessionStorage.clear();
      utilityService.navigateToSignIn();
      this.notificationsService.notify(
        this.loginMessage, Status.Error, this.unauthorizedMessage);
    } else if (errorResponse.status === 403) {
      // Forbidden
      utilityService.saveLastUrl();
      utilityService.navigateToSignIn();
      this.notificationsService.notify(
        this.loginMessage, Status.Error, this.unauthorizedMessage);
    } else if (errorResponse.status === 500) {
      this.notificationsService.notify(
        this.contactAdminMessage, Status.Error, this.internalErrorMessage);
    } else if (errorResponse.status === 400) {
      const self = this;
      if (errorResponse.response)
        JSON.parse(errorResponse.response).errors.forEach(err => {
          self.notificationsService.notify(
            err.message, Status.Error);
        });
    } else {
      console.log(errorResponse.response);
    }

    console.error(errorResponse);
    // cdRef.markForCheck();
    this.inj.get(ApplicationRef).tick();
  }

}
