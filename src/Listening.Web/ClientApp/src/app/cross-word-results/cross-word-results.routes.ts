import { Routes, RouterModule } from '@angular/router';
import { CrosswordResultsComponent } from './crossword-results/crossword-results.component';

const routes: Routes = [
    { path: '', component: CrosswordResultsComponent },
];

export const routing = RouterModule.forChild(routes);
