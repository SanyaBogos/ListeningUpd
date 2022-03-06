import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { QueryViewModel } from '../../../apiDefinitions';

@Injectable()
export class FilterSortService {

  private querySource = new Subject<QueryViewModel>();

  public query$ = this.querySource.asObservable();

  constructor() { }

  announceQueryChange(query: QueryViewModel) {
    this.querySource.next(query);
  }

}
