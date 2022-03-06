import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ResultsComponent } from './results/results.component';
import { routing } from './results.routes';
import { SharedModule } from '../shared/shared.module';
import { PaginationModule } from 'ngx-bootstrap';
import { Ng2TableModule } from 'ng2-table/ng2-table';

@NgModule({
  imports: [routing, SharedModule, CommonModule, PaginationModule.forRoot(), Ng2TableModule],
  declarations: [ResultsComponent]
})
export class ResultsModule { }
