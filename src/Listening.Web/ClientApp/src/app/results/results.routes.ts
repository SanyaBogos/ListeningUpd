import { Routes, RouterModule } from '@angular/router';
import { ResultsComponent } from './results/results.component';

const routes: Routes = [
    { path: '', component: ResultsComponent },
];

export const routing = RouterModule.forChild(routes);
