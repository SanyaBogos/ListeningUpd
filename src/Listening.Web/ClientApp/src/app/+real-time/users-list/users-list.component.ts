import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ChatAvailableUserDto } from '../../../apiDefinitions';

@Component({
  selector: 'appc-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss', '../../shared/styles/common.scss']
})
export class UsersListComponent implements OnInit {

  @Input() users: ChatAvailableUserDto[];
  @Input() isActive: boolean;

  @Output() selectCurrentUser = new EventEmitter<ChatAvailableUserDto>();

  constructor() { }

  ngOnInit() { }

  userSelected(user: ChatAvailableUserDto) {
    this.selectCurrentUser.emit(user);
  }

  getFullName(user: ChatAvailableUserDto) {
    return (user.firstName && user.lastName) ? `${user.firstName} - ${user.lastName} - (${user.email})`
      : user.firstName ? `${user.firstName} - (${user.email})`
        : user.lastName ? `${user.lastName} - (${user.email})`
          : `${user.email}`;
  }
}
