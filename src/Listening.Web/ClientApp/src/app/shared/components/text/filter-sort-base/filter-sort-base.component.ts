// tslint:disable:curly
import { Component, OnInit, Input, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { ObjectProcessService } from '../../../services/object-process.service';
import { FilterSortService } from '../../../services/filter-sort.service';
import { TextDto, UserClient, UserViewModel, AdminTextQueryViewModel } from '../../../../../apiDefinitions';
import { DateFilter } from '@app/shared/models/date-filter';
import { AccountService } from '@app/core/services';

@Component({
  selector: 'appc-filter-sort-base',
  templateUrl: './filter-sort-base.component.html',
  styleUrls: ['./filter-sort-base.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FilterSortBaseComponent implements OnInit {

  private prevQueryViewModel: AdminTextQueryViewModel = new AdminTextQueryViewModel();

  public textDescription: TextDto = new TextDto();
  public queryViewModel: AdminTextQueryViewModel = new AdminTextQueryViewModel();
  public isVisible = false;
  public isFilteredOrSorted = false;
  public isEqualToPrev = false;

  public assignee: number;
  public admins: UserViewModel[];

  @Input() isAdmin = false;

  constructor(
    private cdRef: ChangeDetectorRef,
    private objectProcessService: ObjectProcessService,
    private filterSortService: FilterSortService,
    private accountService: AccountService,
    private userClient: UserClient
  ) {
    this.textDescription.country = '';
    this.queryViewModel.sortingName = '';
    this.queryViewModel.isAscending = false;
    this.queryViewModel.filteringProperties = {};

    if (this.accountService.isAdmin())
      this.userClient.getAdmins().subscribe(admins => {
        this.admins = admins;
        this.admins[0].email = `${this.admins[0].email} *`;
        this.cdRef.markForCheck();
      });
  }

  ngOnInit() {
    this.cdRef.markForCheck();
  }

  search() {
     this.prevQueryViewModel = JSON.parse(JSON.stringify(this.queryViewModel));

    this.checkEqualityToPrev();
    this.isFilteredOrSorted = true;
    this.filterSortService.announceQueryChange(this.queryViewModel);
    this.cdRef.markForCheck();
  }

  private _clearDateProps() {
    // clear other date properties
    const keys = Object.keys(this.queryViewModel.filteringProperties);
    const filteredKeys = keys.filter(x => x.includes(DateFilter.createdName) || x.includes(DateFilter.updatedName));

    for (const key of filteredKeys)
      this.queryViewModel.filteringProperties[key] = undefined;
  }

  clear() {
    this.queryViewModel.sortingName = '';
    this.textDescription.title = '';
    this.textDescription.subTitle = '';
    this.textDescription.country = '';
    this.textDescription.complexity = 0;

    this.queryViewModel.filteringProperties =
      this.objectProcessService.getFilteringPropsFromObject(new TextDto());
    this.isFilteredOrSorted = false;
    this.queryViewModel.includeAssignee = false;
    this.queryViewModel.includeCreateDate = false;
    this.queryViewModel.includeUpdateDate = false;
    this.prevQueryViewModel = JSON.parse(JSON.stringify(this.queryViewModel));
    this.filterSortService.announceQueryChange(this.queryViewModel);
    this.cdRef.markForCheck();
  }

  changeSorting(fieldName: string) {
    const isFieldsEqual = this.queryViewModel.sortingName === fieldName;
    if (!this.queryViewModel.sortingName || !isFieldsEqual) {
      this.queryViewModel.sortingName = fieldName;
      this.queryViewModel.isAscending = true;
    } else if (isFieldsEqual) {
      if (!this.queryViewModel.isAscending)
        this.queryViewModel.sortingName = '';
      this.queryViewModel.isAscending = false;
    }
    this.checkEqualityToPrev();
    this.cdRef.markForCheck();
  }

  checkEqualityToPrev(newValue?: { [key: string]: string; }) {
    if (newValue) {
      const keys = Object.keys(newValue);

      for (const key of keys)
        this.queryViewModel.filteringProperties[key] = newValue[key] ? newValue[key] : undefined;
    }

    this.isEqualToPrev = JSON.stringify(this.prevQueryViewModel) === JSON.stringify(this.queryViewModel);
  }

  checkCountry(country: string) {
    this.textDescription.country = country;
    this.checkEqualityToPrev({ Country: country });
  }

  complexitySelected(complexity: number) {
    this.textDescription.complexity = complexity;
    this.checkEqualityToPrev({ Complexity: `${complexity}` });
  }

  assigneeSelected(assigneeId: number) {
    this.textDescription.assignee = assigneeId;
    this.checkEqualityToPrev({ Assignee: `${assigneeId}` });
  }

  dateSelected(dateFilter: DateFilter) {
    this._clearDateProps();

    if (dateFilter) {
      const keyName = `${dateFilter.name} ${dateFilter.type}`;
      this.queryViewModel.filteringProperties[keyName] = dateFilter.date.singleDate.formatted;
    }
  }

  isSearchDisabled() {
    const result = !this.textDescription.title && !this.textDescription.subTitle && !this.textDescription.country
      && !this.textDescription.complexity
      && !(this.queryViewModel.includeAssignee || this.queryViewModel.includeCreateDate || this.queryViewModel.includeUpdateDate);

    return result;
  }

  isClearDisabled() {
    const isDirty = this.textDescription.title || this.textDescription.subTitle || this.textDescription.complexity
      || this.textDescription.country || this.queryViewModel.includeAssignee || this.queryViewModel.includeCreateDate || this.queryViewModel.includeUpdateDate
      || this.queryViewModel.sortingName;

    if (this.isFilteredOrSorted || isDirty)
      return false;

    return true;
  }

}
