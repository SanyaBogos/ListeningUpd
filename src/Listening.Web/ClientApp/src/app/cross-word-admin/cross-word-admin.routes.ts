import { Routes, RouterModule } from '@angular/router';
import { EditCrosswordComponent } from './edit-crossword/edit-crossword.component';
import { ListCrossWordComponent } from './list-cross-word/list-cross-word.component';

const routes: Routes = [
    { path: 'list', component: ListCrossWordComponent },
    { path: 'edit', component: EditCrosswordComponent },
    { path: 'edit/:id', component: EditCrosswordComponent },
];

export const routing = RouterModule.forChild(routes);
