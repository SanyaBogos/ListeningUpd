import { Routes, RouterModule } from '@angular/router';
import { NeedHelpComponent } from './need-help/need-help.component';

const routes: Routes = [
    { path: '', component: NeedHelpComponent },
];

export const routing = RouterModule.forChild(routes);
