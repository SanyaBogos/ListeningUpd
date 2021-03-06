import { Component, OnInit } from '@angular/core';

import { NotificationsService } from '@app/simple-notifications';
import { UserInfoModel } from '../profile.models';
import { ProfileService } from '../profile.service';
import { ControlBase } from '../../../shared/forms/controls/control-base';
import { ControlTextbox } from '../../../shared/forms/controls/control-textbox';

@Component({
  selector: 'appc-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {
  public controls: Array<ControlBase<string>> = [
    new ControlTextbox({
      key: 'firstName',
      label: 'app_firstname',
      placeholder: 'app_firstname',
      value: '',
      type: 'text',
      required: true,
      order: 1
    }),
    new ControlTextbox({
      key: 'lastName',
      label: 'app_lastname',
      placeholder: 'app_lastname',
      value: '',
      type: 'text',
      required: true,
      order: 2
    }),
    new ControlTextbox({
      key: 'phoneNumber',
      label: 'app_mobile',
      placeholder: 'app_mobile',
      value: '',
      type: 'text',
      required: false,
      order: 3
    })
  ];

  constructor(
    public profileService: ProfileService,
    private ns: NotificationsService
  ) { }

  public ngOnInit() { }

  public save(model: UserInfoModel): void {
    this.profileService.userInfo(model)
      .subscribe((res: UserInfoModel) => {
        this.ns.success(`Name changed to ${res.firstName} ${res.lastName}`);
      });

  }

}
