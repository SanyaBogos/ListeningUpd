import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CrosswordResultsComponent } from './crossword-results/crossword-results.component';
import { PaginationModule } from 'ngx-bootstrap';
import { Ng2TableModule } from 'ng2-table';
import { routing } from './cross-word-results.routes';

@NgModule({
  declarations: [CrosswordResultsComponent],
  imports: [
    routing,
    CommonModule,
    PaginationModule.forRoot(),
    Ng2TableModule
  ]
})
export class CrossWordResultsModule { }
