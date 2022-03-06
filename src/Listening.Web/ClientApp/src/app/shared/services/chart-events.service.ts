import { Injectable } from '@angular/core';
import { Counts } from '../../performance-text/models/counts';
import { Subject } from 'rxjs';
import { IncrementCount } from '../../performance-text/models/incrementCount';
import { DiagramElementType } from '../../performance-text/models/diagram';

@Injectable()
export class ChartEventsService {

  private countsCalculatedSource = new Subject<Counts>();
  private countsIncrementSource = new Subject<IncrementCount>();
  private errorCountIncrementSource = new Subject<number>();

  countsCalculated$ = this.countsCalculatedSource.asObservable();
  countsIncrement$ = this.countsIncrementSource.asObservable();
  errorCountIncrement$ = this.errorCountIncrementSource.asObservable();

  constructor() { }

  announceCountsCalculated(counts: Counts) {
    this.countsCalculatedSource.next(counts);
  }

  announceCountsIncrement(fieldForIncrement: DiagramElementType, count: number) {
    this.countsIncrementSource.next(new IncrementCount(fieldForIncrement, count));
  }

  announceErrorCountIncrement(errorCount: number) {
    this.errorCountIncrementSource.next(errorCount);
  }
}
