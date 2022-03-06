import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { Title, Meta } from '@angular/platform-browser';

import { Params, ActivatedRoute, Router } from '@angular/router';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { authConfig } from './auth.config';

import { routerTransition } from './router.animations';
import { ExternalLoginStatus } from './app.models';
import { AppService } from './app.service';

@Component({
  selector: 'appc-root',
  animations: [routerTransition],
  styleUrls: ['./app.component.scss'],
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  public notificationOptions = {
    position: ['top', 'right'],
    timeOut: 5000,
    lastOnBottom: true
  };

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    @Inject(PLATFORM_ID) private platformId: string,
    private _router: Router,
    private _title: Title,
    private _meta: Meta,
    private _appService: AppService,
    private _activatedRoute: ActivatedRoute,
    private _oauthService: OAuthService,
  ) {
    if (isPlatformBrowser(this.platformId)) {
      this.configureOidc();
    }
  }

  public ngOnInit() {
    this.updateTitleAndMeta();

    this._activatedRoute.queryParams.subscribe((params: Params) => {
      const param = params['externalLoginStatus'];
      if (param) {
        const status = <ExternalLoginStatus>+param;
        switch (status) {
          case ExternalLoginStatus.CreateAccount:
            this._router.navigate(['createaccount']);
            break;
          default:
            break;
        }
      }
    });
  }

  public getState(outlet: any) {
    return outlet.activatedRouteData.state;
  }

  private configureOidc() {
    this._oauthService.configure(authConfig(this.baseUrl));
    this._oauthService.setStorage(localStorage);
    this._oauthService.tokenValidationHandler = new JwksValidationHandler();
    this._oauthService.loadDiscoveryDocumentAndTryLogin();
  }

  private updateTitleAndMeta() {
    this._title.setTitle(this._appService.appData.content['app_title']);
    this._meta.addTags([
      { name: 'description', content: this._appService.appData.content['app_description'] },
      { property: 'og:title', content: this._appService.appData.content['app_title'] },
      { property: 'og:description', content: this._appService.appData.content['app_description'] }
    ]);
  }
}
