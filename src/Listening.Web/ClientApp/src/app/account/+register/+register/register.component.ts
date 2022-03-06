// tslint:disable:curly
// tslint:disable:no-unused-expression
import { Component, OnInit, HostListener, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { DataService } from '../../../core/services/data.service';
import { AccountClient, CaptchaCheckDto, RegisterViewModel } from '../../../../apiDefinitions';
import { CheckInputService } from '../../check-input.service';
import { MyNotificationsService } from '../../../core/services/my-notifications.service';
import { Status } from '../../../core/models/status';
import { AppService } from '@app/app.service';


@Component({
    selector: 'appc-register',
    templateUrl: './register.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegisterComponent implements OnInit {
    public model: RegisterViewModel = {} as RegisterViewModel;
    public passwordConfirm: string;
    public captchaImage: string = null;
    public captchaTTL: number = 30;
    public acceptedTerms: false;
    public passwordError = '';

    private _passwordNotSameError: string = '';

    constructor(
        private _router: Router,
        private _route: ActivatedRoute,
        private _accountClient: AccountClient,
        private _appService: AppService,
        private _cdRef: ChangeDetectorRef,
        private _checkInputService: CheckInputService,
        private _notificationsService: MyNotificationsService
    ) {
        this._passwordNotSameError = this._appService.appData.content['passwords_not_the_same'];
    }

    register(): void {
        const emailCheckResult = this._checkInputService.emailCheck(this.model.email);
        const passwordCheckResult = this._checkInputService.passwordCheck(this.model.password);
        const confirmPasswordCheckResult = this._checkInputService.confirmPasswordCheck(this.model.password, this.passwordConfirm);
        const captchaCheckResult = this._checkInputService.captchaCheck(this.model.captcha.captcha);

        if (emailCheckResult || passwordCheckResult || confirmPasswordCheckResult || captchaCheckResult) {
            emailCheckResult && this._notificationsService.notify(emailCheckResult, Status.Error);
            passwordCheckResult && this._notificationsService.notifyWithParts(passwordCheckResult, Status.Error);
            confirmPasswordCheckResult && this._notificationsService.notify(confirmPasswordCheckResult, Status.Error);
            captchaCheckResult && this._notificationsService.notify(captchaCheckResult, Status.Error);
            return;
        }

        this._accountClient.register(this.model)
            .subscribe(() => {
                this._router.navigate(['../registerconfirmation'],
                    { relativeTo: this._route, queryParams: { emailConfirmed: true } });
            });
    }

    @HostListener('window:keydown', ['$event'])
    press(event: KeyboardEvent) {
        if (event.key === 'Enter')
            this.register();
    }

    ngOnInit() { }

    updateCaptchaObj(captcha: CaptchaCheckDto) {
        this.model.captcha = captcha;
    }

    passwordChanged1(newPassword: string) {
        this.model.password = newPassword;
    }

    passwordChanged2(newPassword: string) {
        this.passwordConfirm = newPassword;

        if (this.model.password !== this.passwordConfirm)
            this.passwordError = this._passwordNotSameError;
        else
            this.passwordError = '';

        this._cdRef.markForCheck();
    }

}
