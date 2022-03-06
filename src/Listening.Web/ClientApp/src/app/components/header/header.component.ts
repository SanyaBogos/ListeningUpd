import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

import { AccountService, DataService } from '@app/core';
import { AppService } from '../../app.service';
import { AccountNotificationService } from '../../core/services/account-notification.service';

import { MenuItem } from 'primeng/api';
import { CountriesListService } from '@app/shared/services/countries-list.service';
import { SpecClient } from 'apiDefinitions';
import { BaseSubscriptionsComponent } from '@app/shared/components/base-subscriptions/base-subscriptions.component';

@Component({
    selector: 'appc-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss'],
    providers: [CountriesListService],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeaderComponent extends BaseSubscriptionsComponent implements OnInit {

    public isCollapsed = true;
    public isSuper = false;
    public isAdmin = false;
    public isSpec = false;
    public languageItems: MenuItem[];
    public menuItems: MenuItem[];

    constructor(
        private _cdRef: ChangeDetectorRef,
        private _accountNotificationService: AccountNotificationService,
        private _accountService: AccountService,
        private _dataService: DataService,
        private _appService: AppService,
        private _oAuthService: OAuthService,
        private _countriesListService: CountriesListService,
        private _specClient: SpecClient,
        private _router: Router
    ) {
        super();
        const self = this;
        const currentCulture = this._appService.appData.cultures.find(x => x.current);
        const culturesToSelect = this._appService.appData.cultures.filter(x => !x.current);
        const itemsToSelect = culturesToSelect.map(x => <MenuItem>{
            label: self._countriesListService.getLanguageName(self._getShortCountryName(x.value)),
            icon: self._getCultureStyle(x),
            url: `/api/setlanguage/${x.value}`
        });

        this.languageItems = [{
            icon: this._getCultureStyle(currentCulture),
            items: itemsToSelect
        }];


        this._subscriptions.add(
            this._accountNotificationService.login$.subscribe(this._checkRole.bind(this))
        );

        this._refreshHeader();
    }


    ngOnInit(): void {
        const self = this;
        self._checkRole();

    }

    public get isLoggedIn(): boolean {
        return this._accountService.isLoggedIn;
    }

    public get user(): IProfileModel | undefined {
        return this._accountService.user;
    }

    public get cultures(): ICulture[] {
        return this._appService.appData.cultures;
    }

    public get currentCulture(): ICulture {
        return this.cultures.filter(x => x.current)[0];
    }

    public toggleNav() {
        this.isCollapsed = !this.isCollapsed;
        this._cdRef.markForCheck();
    }

    public logout() {
        if (this._router.url.includes('perfTexts')) {
            setTimeout(() => {
                this._logout();
            }, 1000);
        } else {
            this._logout();
        }
    }

    private _logout() {
        this._dataService.post('api/Account/logout')
            .subscribe(() => {
                this._oAuthService.logOut();
                this.isSuper = false;
                this.isAdmin = false;
                this.isSpec = false;
                this._refreshHeader();
                this._router.navigate(['/login']);
                // this.accountNotificationService.logoutNotify();
                this._cdRef.markForCheck();
            });
    }

    private _checkRole() {
        this.isSuper = this._accountService.isSuper();
        this.isAdmin = this._accountService.isAdmin() || this._accountService.isSuper();
        this.isSpec = this._accountService.isSpec() || this._accountService.isSpecAdm();
        this._refreshHeader();
        this._cdRef.markForCheck();
    }

    private _refreshHeader() {
        const main = this._appService.appData.content['app_nav_main'];
        const home = this._appService.appData.content['app_nav_home'];
        const additionalTools = this._appService.appData.content['app_nav_additional_tools'];
        const camera = this._appService.appData.content['app_nav_online_camera'];
        const record = this._appService.appData.content['record_stream'];
        const textRecognition = this._appService.appData.content['text_recognition'];
        const youtubeDownload = this._appService.appData.content['youtube_download'];
        const debianFAI = this._appService.appData.content['debian_fai'];
        const math = this._appService.appData.content['math'];
        const plot = this._appService.appData.content['plot'];

        const stegano = this._appService.appData.content['steg'];
        const stegImg = this._appService.appData.content['steg_img'];
        const stegVideo = this._appService.appData.content['steg_vid'];
        const stegAudio = this._appService.appData.content['steg_aud'];
        const inject = this._appService.appData.content['inj'];
        const eject = this._appService.appData.content['ej'];
        const bukvitsa = this._appService.appData.content['bkv'];
        const bukvitsaImage = this._appService.appData.content['bkv_img'];
        const bukvitsaInteraction = this._appService.appData.content['bkv_act'];
        const bukvitsaInteractionX = this._appService.appData.content['bkv_act_x'];
        // const interesting = this._appService.appData.content['interesting'];
        const blog = this._appService.appData.content['blog'];
        const knowledge = this._appService.appData.content['knowledge'];

        const separator = {
            separator: true
        };

        const mainItem = {
            label: main,
            icon: 'glyphicon glyphicon-headphones',
            items: [{
                label: home,
                icon: 'fa fa-home',
                routerLink: "/"
            }]
        } as MenuItem;

        const cameraItem = {
            label: camera,
            icon: 'fa fa-camera-retro',
            routerLink: 'real-time/media-processing'
        } as MenuItem;

        const recordItem = {
            label: record,
            icon: 'glyphicon glyphicon-record',
            routerLink: 'real-time/record'
        } as MenuItem;

        const textRecognitionItem = {
            label: textRecognition,
            icon: 'fa fa-eye',
            routerLink: 'additional/recognition'
        } as MenuItem;

        const youtubeDownloadItem = {
            label: youtubeDownload,
            icon: 'fa fa-youtube',
            routerLink: 'additional/ytbdwnld'
        } as MenuItem;

        const debianFaiItem = {
            label: debianFAI,
            icon: 'glyphicon glyphicon-cd',
            routerLink: 'debfai'
        } as MenuItem;

        const mathItem = {
            label: math,
            icon: 'glyphicon glyphicon-queen',
            items: [{
                label: plot,
                icon: 'glyphicon glyphicon-stats',
                routerLink: 'math',
            }]
        } as MenuItem;

        const bukvitsaItem = {
            label: bukvitsa,
            // icon: 'glyphicon glyphicon-queen',
            items: [
                {
                    label: bukvitsaImage,
                    icon: 'glyphicon glyphicon-picture',
                    routerLink: 'bkvts/img',
                },
                {
                    label: bukvitsaInteraction,
                    // icon: 'glyphicon glyphicon-stats',
                    routerLink: 'bkvts/act',
                },
                {
                    label: bukvitsaInteractionX,
                    // icon: 'glyphicon glyphicon-stats',
                    routerLink: 'bkvts/actx',
                },
            ]
        } as MenuItem;

        const steganoItem = {
            label: stegano,
            icon: 'fa fa-user-secret text-success',
            items: [
                {
                    label: stegImg,
                    icon: 'fa fa-image',
                    items: [
                        {
                            label: inject,
                            icon: 'fa fa-puzzle-piece',
                            routerLink: 'steg/img-inj'
                        } as MenuItem,
                        {
                            label: eject,
                            icon: 'glyphicon glyphicon-open-file',
                            routerLink: 'steg/img-ej'
                        } as MenuItem
                    ]
                } as MenuItem,
                {
                    label: stegAudio,
                    icon: 'fa fa-volume-up'

                } as MenuItem,
                {
                    label: stegVideo,
                    icon: 'fa fa-film'

                } as MenuItem,

            ]
        } as MenuItem;

        const additionItem = {
            label: additionalTools,
            icon: 'glyphicon glyphicon-cog',
            items: [cameraItem, recordItem, textRecognitionItem, youtubeDownloadItem, debianFaiItem, steganoItem,
                mathItem, bukvitsaItem]
        } as MenuItem;

        const blogItem = {
            label: blog,
            icon: 'fa fa-pencil text-success',
            routerLink: 'interesting/blog'
        } as MenuItem;

        const knowledgeItem = {
            label: knowledge,
            icon: 'glyphicon glyphicon-globe text-primary',
            items: [blogItem]
        } as MenuItem;

        if (!this.isLoggedIn) {

            this.menuItems = [mainItem, additionItem, /*interestingItem*/ knowledgeItem];

            return;
        }

        const texts = this._appService.appData.content['nav_txt_list'];
        const crosswords = this._appService.appData.content['nav_crsswd_list'];
        const results = this._appService.appData.content['nav_rslts'];
        const crosswordResults = this._appService.appData.content['nav_crsswd_res'];
        const feedbacks = this._appService.appData.content['nav_fdbcks'];
        const need = this._appService.appData.content['nav_need_help'];
        const adminTexts = this._appService.appData.content['nav_txts_adm'];
        const adminCrosswords = this._appService.appData.content['nav_crsswd_adm'];
        const adminRoles = this._appService.appData.content['nav_roles_adm'];
        const adminBackups = this._appService.appData.content['nav_bckp_adm'];
        const logs = this._appService.appData.content['log'];
        const chat = this._appService.appData.content['app_nav_signalr'];
        const messages = this._appService.appData.content['messages_table_header'];
        const videoConversation = this._appService.appData.content['video_conversation'];
        const specific = this._appService.appData.content['specific_info'];
      
        const homeItm = {
            label: home,
            icon: 'fa fa-home',
            routerLink: "/"
        };

        const textsItm = {
            label: texts,
            icon: 'fa fa-assistive-listening-systems',
            routerLink: "perfTexts"
        };

        const crosswordsItm = {
            label: crosswords,
            // icon: 'fa fa-assistive-listening-systems',
            routerLink: "crosswd"
        };

        const resultsItm = {
            label: results,
            icon: 'fa fa-list-alt',
            routerLink: "results"
        };

        const crosswordResultsItm = {
            label: crosswordResults,
            // icon: 'fa fa-list-alt',
            routerLink: "crosswd-res"
        };

        const adminTextsItm = {
            label: adminTexts,
            icon: 'fa fa-user-secret text-danger',
            routerLink: "admin/texts"
        };

        const adminCrosswordItem = {
            label: adminCrosswords,
            // icon: 'fa fa-user-secret text-danger',
            routerLink: "crosswd-adm/list"
        };

        const adminRolesItm = {
            label: adminRoles,
            icon: 'fa fa-address-book-o text-danger',
            routerLink: "admin/roles"
        };

        const adminBackupsItm = {
            label: adminBackups,
            icon: 'fa fa-server text-primary',
            routerLink: "admin/bckp"
        };

        const logsItm = {
            label: logs,
            icon: 'fa fa-book-open',
            routerLink: "log"
        };

        const feedbackItm = {
            label: feedbacks,
            icon: 'fa fa-book',
            routerLink: "feedbacks"
        };

        const helpItm = {
            label: need,
            icon: 'fa fa-hand-holding',
            routerLink: "need-help"
        };

        if (this.isSuper)
            mainItem.items = [homeItm, separator, textsItm, crosswordsItm, resultsItm, crosswordResultsItm, separator,
                adminTextsItm, adminCrosswordItem, adminRolesItm, adminBackupsItm, logsItm, separator, feedbackItm, helpItm];
        else if (this.isAdmin)
            mainItem.items = [homeItm, separator, textsItm, crosswordsItm, resultsItm, crosswordResultsItm, separator,
                adminTextsItm, adminCrosswordItem, separator, feedbackItm, helpItm];
        else
            mainItem.items = [homeItm, separator, textsItm, crosswordsItm, resultsItm, crosswordResultsItm,
                separator, feedbackItm, helpItm];

        const chatItem = {
            label: chat,
            icon: 'fa fa-comments-o',
            items: [{
                label: messages,
                icon: 'fa fa-comments',
                routerLink: 'real-time/chat'
            },
            {
                label: videoConversation,
                icon: 'glyphicon glyphicon-facetime-video',
                routerLink: 'real-time/video-rtc-conversation'
            }
            ]
        } as MenuItem;


        if (this.isSpec) {

            this._specClient.getHeaderDescription()
                .subscribe(res => {

                    const typeItems = res.map(x =>
                        <MenuItem>{
                            label: this._appService.appData.content[x.name],
                            items: x.courses.map(y => <MenuItem>{
                                label: `${y.name} - ${this._appService.appData.content[y.author]}`,
                                routerLink: `spec/gen/${y.id}`,
                            })
                        });

                    const specItem = {
                        label: specific,
                        icon: 'glyphicon glyphicon-tint text-info',
                        items: typeItems,
                        routerLink: 'spec/lst'
                    } as MenuItem;

                    knowledgeItem.items = [blogItem, specItem]
                });
        }

        additionItem.items = [chatItem, cameraItem, recordItem, textRecognitionItem, youtubeDownloadItem, debianFaiItem, steganoItem, mathItem, bukvitsaItem];
        this.menuItems = [mainItem, additionItem, knowledgeItem];
    }

    private _getShortCountryName(fullAlias: string) {
        return fullAlias.split('-')[1].toLowerCase();
    }

    private _getCultureStyle(culture: ICulture) {
        return `flag flag-${this._getShortCountryName(culture.value)} country-flag`;
    }
}
