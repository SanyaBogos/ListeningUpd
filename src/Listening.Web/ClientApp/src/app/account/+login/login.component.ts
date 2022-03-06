// tslint:disable:curly
import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, HostListener } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { AccountNotificationService } from '../../core/services/account-notification.service';
import { UtilityService } from '@app/core/services/utility.service';

@Component({
    selector: 'appc-login',
    styleUrls: ['./login.component.scss'],
    templateUrl: './login.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnInit {
    public loginModel: ILoginModel = {} as ILoginModel;

    public controls: any;

    constructor(
        public _oAuthService: OAuthService,
        private _accountNotificationService: AccountNotificationService,
        private _cdRef: ChangeDetectorRef,
        private _utilityService: UtilityService
    ) {
    }

    login(): void {
        const { username, password } = this.loginModel;

        this._oAuthService.fetchTokenUsingPasswordFlow(username, password)
            .then((x: any) => {
                localStorage.setItem('id_token', x.id_token);
                this._oAuthService.setupAutomaticSilentRefresh();
                this._utilityService.navigateToLastUrl();
                this._accountNotificationService.loginNotify();
                this._cdRef.markForCheck();
            });
    }

    @HostListener('window:keydown', ['$event'])
    press(event: KeyboardEvent) {
        if (event.key === 'Enter')
            this.login();
    }

    ngOnInit() { }

}
