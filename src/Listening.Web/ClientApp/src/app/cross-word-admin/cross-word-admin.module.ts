import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListCrossWordComponent } from './list-cross-word/list-cross-word.component';
import { routing } from './cross-word-admin.routes';
import { SharedModule } from '@app/shared/shared.module';
import { EditCrosswordComponent } from './edit-crossword/edit-crossword.component';
import { GridCrosswordComponent } from './grid-crossword/grid-crossword.component';
import { EditQuestionComponent } from './edit-question/edit-question.component';
import { ChangeGridService } from './change-grid.service';
import { ListQuestionsComponent } from './list-questions/list-questions.component';
import { PaginationModule } from 'ngx-bootstrap';
import { Ng2TableModule } from 'ng2-table/ng2-table';

@NgModule({
  declarations: [ListCrossWordComponent, EditCrosswordComponent, GridCrosswordComponent, EditQuestionComponent, ListQuestionsComponent],
  imports: [
    routing,
    CommonModule,
    SharedModule,
    PaginationModule.forRoot(),
    Ng2TableModule
  ],
  providers: [ChangeGridService]
})
export class CrossWordAdminModule { }
