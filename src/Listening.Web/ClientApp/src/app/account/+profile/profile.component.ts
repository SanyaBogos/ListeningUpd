import { Component } from '@angular/core';

import { ProfileService } from './profile.service';

@Component({
    selector: 'appc-profile',
    templateUrl: './profile.component.html'
})
export class ProfileComponent {
    menus = [
        { route: 'userinfo', text: 'usr_info' },
        { route: 'updatepassword', text: 'upd_passwd' },
        { route: 'userphoto', text: 'usr_photo' },
        { route: 'otheraccounts', text: 'othr_accs' }
    ];

    constructor(public profileService: ProfileService) { }
}
