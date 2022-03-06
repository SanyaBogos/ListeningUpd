import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { VideoDescription } from '@app/appshared/models/videoDescrption';
import { AccountService } from '../core';
// import { VideoDescription } from '@app/app.models';

@Component({
  selector: 'appc-home-component',
  styleUrls: ['../shared/styles/common.scss'],
  templateUrl: './home.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent implements OnInit {

  public isLoggedIn = false;
  public videoDescriptions: VideoDescription[];

  constructor(
    private router: Router,
    private accountService: AccountService,
  ) {
    const isAdmin = accountService.isLoggedIn && accountService.isAdmin();
    const basePath = 'intro-video/lstng/';

    this.videoDescriptions = [
      { name: 'intro_lang_description', src: `${basePath}0-languages`, isAllowed: true, type: 'webm' },
      { name: 'intro_search_and_filter_description', src: `${basePath}0-search-and-filter`, isAllowed: true, type: 'webm' },
      { name: 'intro_joined_description', src: `${basePath}1-joined-text-transcribing`, isAllowed: true, type: 'webm' },
      { name: 'intro_joined_res', src: `${basePath}2-joined-continue`, isAllowed: true, type: 'webm' },
      { name: 'intro_separated_description', src: `${basePath}3-separated`, isAllowed: true, type: 'webm' },
      { name: 'intro_save_new_text_audio_description', src: `${basePath}4-save-new-text`, isAllowed: isAdmin, type: 'webm' },
    ];
  }

  ngOnInit(): void {
    this.isLoggedIn = this.accountService.isLoggedIn;
  }

  goToText() {
    this.router.navigate(['perfTexts']);
  }

  goToLogin() {
    this.router.navigate(['login']);
  }
  
}
