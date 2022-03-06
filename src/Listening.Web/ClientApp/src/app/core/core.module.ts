import { NgModule, Optional, SkipSelf, ModuleWithProviders, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

// App level services
import { AccountService } from './services/account.service';
import { DataService } from './services/data.service';
import { GlobalErrorHandler } from './services/global-error.service';
import { AuthInterceptor } from './services/interceptors/auth-interceptor';
import { MyNotificationsComponent } from './components/my-notifications/my-notifications.component';
import { MyNotificationsService } from './services/my-notifications.service';
import { UtilityService } from './services/utility.service';
import { AppSharedModule } from '../appshared';
import { AccountNotificationService } from './services/account-notification.service';

@NgModule({
    declarations: [MyNotificationsComponent],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        RouterModule,
        AppSharedModule
    ],
    exports: [
        FormsModule,
        HttpClientModule,
        ReactiveFormsModule,
        RouterModule,
        // AppSharedModule,
        MyNotificationsComponent,
    ],
    providers: [MyNotificationsService, UtilityService, AccountNotificationService]
})
export class CoreModule {
    // forRoot allows to override providers
    // https://angular.io/docs/ts/latest/guide/ngmodule.html#!#core-for-root
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: CoreModule,
            providers: [
                AccountService,
                DataService,
                // UtilityService,
                // Location,
                { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
                { provide: ErrorHandler, useClass: GlobalErrorHandler }
            ]
        };
    }
    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        if (parentModule) {
            throw new Error('CoreModule is already loaded. Import it in the AppModule only');
        }
    }
}

export * from './services';
