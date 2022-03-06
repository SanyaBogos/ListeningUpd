import { Component, OnInit, Input } from '@angular/core';
import { QueryViewModel } from '../../../../apiDefinitions';
// import { QueryViewModel } from '../../../apiDefinitions';

@Component({
  selector: 'appc-sorting-icon',
  templateUrl: './sorting-icon.component.html',
  styleUrls: ['./sorting-icon.component.scss']
})
export class SortingIconComponent implements OnInit {

  @Input() initStateName: string;
  @Input() isFilteredOrSorted: boolean;
  @Input() queryViewModel: QueryViewModel;

  constructor() { }

  ngOnInit() {
  }

  isInitEqualsCurrent() {
    return this.initStateName === this.queryViewModel.sortingName;
  }

}
