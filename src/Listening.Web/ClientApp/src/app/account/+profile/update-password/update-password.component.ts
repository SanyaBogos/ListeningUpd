import { Component, OnInit, EventEmitter } from '@angular/core';

import { NotificationsService } from '@app/simple-notifications';
import { ProfileService } from '../profile.service';
import { UpdatePasswordModel } from '../profile.models';
import { ControlBase } from '../../../shared/forms/controls/control-base';
import { ControlTextbox } from '../../../shared/forms/controls/control-textbox';

@Component({
  selector: 'appc-update-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.scss'],
})
export class UpdatePasswordComponent implements OnInit {
  public controls: Array<ControlBase<string>> = [
    new ControlTextbox({
      key: 'oldPassword',
      label: 'current_passwd',
      placeholder: 'current_passwd',
      value: '',
      type: 'password',
      order: 1
    }),
    new ControlTextbox({
      key: 'newPassword',
      label: 'new_passwd',
      placeholder: 'new_passwd',
      value: '',
      type: 'password',
      required: true,
      order: 2
    }),
    new ControlTextbox({
      key: 'confirmPassword',
      label: 'verify_passwd',
      placeholder: 'verify_passwd',
      value: '',
      type: 'password',
      required: true,
      order: 3
    })
  ];

  public reset = new EventEmitter<boolean>();
  constructor(
    public profileService: ProfileService,
    private ns: NotificationsService
  ) { }

  public ngOnInit() { }

  public save(model: UpdatePasswordModel): void {
    this.profileService.changePassword(model)
      .subscribe(() => {
        this.reset.emit(true);
        this.ns.success('Password changed successfully');
      });
  }
}
