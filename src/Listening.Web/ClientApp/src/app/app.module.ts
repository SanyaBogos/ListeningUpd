import { BrowserModule, BrowserTransferStateModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { ServiceWorkerModule } from '@angular/service-worker';
import { OAuthModule } from 'angular-oauth2-oidc';
import { PrebootModule } from 'preboot';

import { environment } from '../environments/environment';

import { CoreModule } from './core';
import { AppSharedModule } from './appshared';
import { SimpleNotificationsModule } from './simple-notifications';

// Components
import { AppComponent } from './app.component';
import { FooterComponent, HeaderComponent, PrivacyComponent } from './components';
import { HomeComponent } from './home/home.component';
import { AppService } from './app.service';
import { TranslateModule } from '@ngx-translate/core';
import { NgxUiLoaderModule, NgxUiLoaderHttpModule, SPINNER, POSITION, PB_DIRECTION } from 'ngx-ui-loader';

import { MenubarModule } from 'primeng/menubar';
// import { VideoSharedModule } from './video-shared/video-shared.module';
import { QuillModule } from 'ngx-quill'

const randomValue = Math.floor(Math.random() * Object.keys(SPINNER).length);

export function appServiceFactory(appService: AppService): Function {
  return () => appService.getAppData();
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    FooterComponent,
    HeaderComponent,
    // ModalComponent,
    PrivacyComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    PrebootModule.withConfig({ appRoot: 'appc-root' }),
    BrowserAnimationsModule,
    BrowserTransferStateModule,
    CoreModule.forRoot(),
    TranslateModule.forRoot(),
    AppSharedModule,
    // VideoSharedModule,
    SimpleNotificationsModule.forRoot(),
    OAuthModule.forRoot(),
    QuillModule.forRoot(),
    MenubarModule,
    NgxUiLoaderModule.forRoot({
      bgsColor: 'red',
      bgsPosition: POSITION.bottomCenter,
      bgsSize: 40,
      // this construction is necessary to avoid compilation error (string couldn't be cast to SpinnerType `cause it's unexportable)
      fgsType: randomValue === 0 ? SPINNER.ballScaleMultiple
        : randomValue === 1 ? SPINNER.ballSpinClockwise
          : randomValue === 2 ? SPINNER.ballSpinClockwiseFadeRotating
            : randomValue === 3 ? SPINNER.ballSpinFadeRotating
              : randomValue === 4 ? SPINNER.chasingDots
                : randomValue === 5 ? SPINNER.circle
                  : randomValue === 6 ? SPINNER.cubeGrid
                    : randomValue === 7 ? SPINNER.doubleBounce
                      : randomValue === 8 ? SPINNER.fadingCircle
                        : randomValue === 9 ? SPINNER.foldingCube
                          : randomValue === 10 ? SPINNER.pulse
                            : randomValue === 11 ? SPINNER.rectangleBounce
                              : randomValue === 12 ? SPINNER.rectangleBounceParty
                                : randomValue === 13 ? SPINNER.rectangleBouncePulseOut
                                  : randomValue === 14 ? SPINNER.rectangleBouncePulseOutRapid
                                    : randomValue === 15 ? SPINNER.rotatingPlane
                                      : randomValue === 16 ? SPINNER.squareJellyBox
                                        : randomValue === 17 ? SPINNER.squareLoader
                                          : randomValue === 18 ? SPINNER.threeBounce
                                            : randomValue === 19 ? SPINNER.threeStrings
                                              : randomValue === 20 ? SPINNER.wanderingCubes
                                                : SPINNER.ballScaleMultiple,
      pbDirection: PB_DIRECTION.leftToRight, // progress bar direction
      pbThickness: 5, // progress bar thickness
    }),
    NgxUiLoaderHttpModule.forRoot({ showForeground: true, exclude: ['/api/Word', '/api/Chat'] }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', data: { state: 'home' } },
      { path: 'login', loadChildren: './account/+login/login.module#LoginModule' },
      { path: 'register', loadChildren: './account/+register/register.module#RegisterModule' },
      { path: 'createaccount', loadChildren: './account/+create/create.module#CreateAccountModule' },
      { path: 'profile', loadChildren: './account/+profile/profile.module#ProfileModule' },
      { path: 'real-time', loadChildren: './+real-time/real-time.module#RealTimeModule' },
      { path: 'admin', loadChildren: './+admin/admin.module#AdminModule' },
      { path: 'perfTexts', loadChildren: './performance-text/performance-text.module#PerformanceTextModule' },
      { path: 'results', loadChildren: './results/results.module#ResultsModule' },
      { path: 'crosswd', loadChildren: './cross-word/cross-word.module#CrossWordModule' },
      { path: 'crosswd-adm', loadChildren: './cross-word-admin/cross-word-admin.module#CrossWordAdminModule' },
      { path: 'crosswd-res', loadChildren: './cross-word-results/cross-word-results.module#CrossWordResultsModule' },
      { path: 'feedbacks', loadChildren: './feedback/feedback.module#FeedbackModule' },
      { path: 'interesting', loadChildren: './interesting/interesting.module#InterestingModule' },
      { path: 'spec', loadChildren: './spec/spec.module#SpecModule' },
      { path: 'additional', loadChildren: './additional/additional.module#AdditionalModule' },
      { path: 'debfai', loadChildren: './debian-fai/debian-fai.module#DebianFaiModule' },
      { path: 'math', loadChildren: './math/math.module#MathModule' },
      { path: 'need-help', loadChildren: './need-help/need-help.module#NeedHelpModule' },      
      { path: 'log', loadChildren: './log/log.module#LogModule' },
      { path: 'steg', loadChildren: './stegano/stegano.module#SteganoModule' },      
      { path: 'bkvts', loadChildren: './bukvitsa/bukvitsa.module#BukvitsaModule' },
      { path: 'privacy', component: PrivacyComponent },
    ], { initialNavigation: 'enabled', onSameUrlNavigation: 'reload' }),
    ServiceWorkerModule.register('/ngsw-worker.js', { enabled: environment.production }),
  ],
  providers: [
    AppService,
    { provide: APP_INITIALIZER, useFactory: appServiceFactory, deps: [AppService], multi: true }
  ],
  exports: [QuillModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
