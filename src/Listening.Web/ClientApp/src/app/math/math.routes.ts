import { Routes, RouterModule } from '@angular/router';
import { PlotComponent } from './plot/plot.component';

const routes: Routes = [
    { path: '', component: PlotComponent },
];

export const routing = RouterModule.forChild(routes);
