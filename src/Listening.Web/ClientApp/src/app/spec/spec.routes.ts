import { Routes, RouterModule } from '@angular/router';
import { SpecGeneralComponent } from './spec-general/spec-general.component';
import { SpecListComponent } from './spec-list/spec-list.component';

const routes: Routes = [
    { path: 'lst', component: SpecListComponent },
    { path: 'gen/:courseId', component: SpecGeneralComponent },
    { path: 'gen/:courseId/:videoId', component: SpecGeneralComponent },
    { path: 'gen/:courseId/:videoId/:timeShiftInSeconds', component: SpecGeneralComponent },
];

export const routing = RouterModule.forChild(routes);
