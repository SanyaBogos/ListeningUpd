import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Status } from '@app/core/models/status';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { AdminClient, ApplicationUserDto, UserWithRolesViewModel, UserWithRoleViewModel } from 'apiDefinitions';

@Component({
  selector: 'appc-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.scss', '../../shared/styles/common.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RolesComponent implements OnInit {

  public userWithRoles: UserWithRolesViewModel;
  // public roles: string[];
  public roles: string[];
  public isNewUser: boolean = true;
  public newUser: ApplicationUserDto = new ApplicationUserDto();
  public isAddUserVisible: boolean;
  public isUpdate: boolean;
  public selectedRole: number;

  public rows: Array<any> = [];
  public columns: Array<any> = [
    { title: 'Email', name: 'email', filtering: { filterString: '', placeholder: 'Filter by name' } },
    { title: 'Role', name: 'roleName', filtering: { filterString: '', placeholder: 'Filter by role' } },
  ];
  public page: number = 1;
  public itemsPerPage: number = 10;
  public maxSize: number = 5;
  public numPages: number = 1;
  public length: number = 0;

  public config: any = {
    paging: true,
    sorting: { columns: this.columns },
    filtering: { filterString: '' },
    className: ['table-striped', 'table-bordered']
  };

  private data: UserWithRoleViewModel[];

  constructor(
    private _ref: ChangeDetectorRef,
    private _notificationsService: MyNotificationsService,
    private _adminClient: AdminClient
  ) {
    this.data = [];

    this._adminClient.getUsersAndRolesMO()
      .subscribe(res => {
        this.userWithRoles = res;
        this.roles = res.roles.map(x => x.name);
        this.data = res.users.map(x => <UserWithRoleViewModel>{ id: x.id, email: x.email, roleName: res.roles.find(y => y.id == x.roleId).name });
        this.newUser.role = this.roles[0];
        this.onChangeTable(this.config);

        this._ref.markForCheck();
      });

    // this._adminClient.getUsersAndRoles()
    //   .subscribe(result => {
    //     this.userWithRoles = result;
    //     this.data = result.users;
    //     this.roles = result.roles.map(x => x.name);
    //     // console.log(result);
    //     this.onChangeTable(this.config);

    //     this._ref.markForCheck();
    //   });
  }

  ngOnInit(): void { }

  changePage(page: any, data: Array<any> = this.data): Array<any> {
    let start = (page.page - 1) * page.itemsPerPage;
    let end = page.itemsPerPage > -1 ? (start + page.itemsPerPage) : data.length;
    return data.slice(start, end);
  }

  changeSort(data: any, config: any): any {
    if (!config.sorting) {
      return data;
    }

    let columns = this.config.sorting.columns || [];
    let columnName: string = void 0;
    let sort: string = void 0;

    for (let i = 0; i < columns.length; i++) {
      if (columns[i].sort !== '' && columns[i].sort !== false) {
        columnName = columns[i].name;
        sort = columns[i].sort;
      }
    }

    if (!columnName) {
      return data;
    }

    // simple sorting
    return data.sort((previous: any, current: any) => {
      if (previous[columnName] > current[columnName]) {
        return sort === 'desc' ? -1 : 1;
      } else if (previous[columnName] < current[columnName]) {
        return sort === 'asc' ? -1 : 1;
      }
      return 0;
    });
  }

  changeFilter(data: any, config: any): any {
    let filteredData: Array<any> = data;
    this.columns.forEach((column: any) => {
      if (column.filtering) {
        filteredData = filteredData.filter((item: any) => {
          return item[column.name].match(column.filtering.filterString);
        });
      }
    });

    if (!config.filtering) {
      return filteredData;
    }

    if (config.filtering.columnName) {
      return filteredData.filter((item: any) =>
        item[config.filtering.columnName].match(this.config.filtering.filterString));
    }

    let tempArray: Array<any> = [];
    filteredData.forEach((item: any) => {
      let flag = false;
      this.columns.forEach((column: any) => {
        if (item[column.name].toString().match(this.config.filtering.filterString)) {
          flag = true;
        }
      });
      if (flag) {
        tempArray.push(item);
      }
    });
    filteredData = tempArray;

    return filteredData;
  }

  onChangeTable(config: any, page: any = { page: this.page, itemsPerPage: this.itemsPerPage }): void {
    if (config.filtering) {
      Object.assign(this.config.filtering, config.filtering);
    }

    if (config.sorting) {
      Object.assign(this.config.sorting, config.sorting);
    }

    let filteredData = this.changeFilter(this.data, this.config);
    let sortedData = this.changeSort(filteredData, this.config);
    this.rows = page && config.paging ? this.changePage(page, sortedData) : sortedData;
    this.length = sortedData.length;
  }

  onCellClick(data: any): void {
    const userWithRole = data.row as UserWithRoleViewModel;

    this._adminClient.getUser(userWithRole.id)
      .subscribe(user => {
        this.newUser = user;
        this.isAddUserVisible = true;
        this.isNewUser = false;

        this._ref.markForCheck();
      });
  }

  changeAddUserVisibility(): void {
    this.isAddUserVisible = !this.isAddUserVisible;
    this._ref.markForCheck();
  }

  create(): void {
    this._adminClient.addUser(this.newUser)
      .subscribe((id: number) => {
        this.data.push(<UserWithRoleViewModel>{ id: id, email: this.newUser.email, roleName: this.newUser.role });
        this._notificationsService.notify('saved_succ', Status.Success);
      });
  }

  update(): void {
    this._adminClient.updateUser(this.newUser)
      .subscribe(() => {
        let user = this.data.find(x => x.id == this.newUser.id);
        user.email = this.newUser.email;
        user.roleName = this.newUser.role;

        this._notificationsService.notify('updated_succ', Status.Success);
      });
  }

  clear(): void {
    this.newUser = new ApplicationUserDto();
    this.isNewUser = true;
    this._ref.markForCheck();
  }
}
