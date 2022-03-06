import { Routes, RouterModule } from '@angular/router';
import { LogComponent } from './log/log.component';

const routes: Routes = [
    { path: '', component: LogComponent },
];

export const routing = RouterModule.forChild(routes);
