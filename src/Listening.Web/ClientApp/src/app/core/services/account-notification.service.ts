import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountNotificationService {

  private loginSource = new Subject();
  // private logoutSource = new Subject();

  login$ = this.loginSource.asObservable();
  logout$ = this.loginSource.asObservable();

  constructor() { }

  loginNotify() {
    this.loginSource.next();
  }

  // logoutNotify() {
  //   this.logoutSource.next();
  // }
}
