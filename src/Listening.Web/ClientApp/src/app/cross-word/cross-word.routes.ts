import { Routes, RouterModule } from '@angular/router';
import { CrosswordListComponent } from './crossword-list/crossword-list.component';
import { CrosswordComponent } from './crossword/crossword.component';

const routes: Routes = [
    { path: '', component: CrosswordListComponent },
    { path: 'cwd', component: CrosswordComponent },
    { path: 'cwd/:id', component: CrosswordComponent },
];

export const routing = RouterModule.forChild(routes);
