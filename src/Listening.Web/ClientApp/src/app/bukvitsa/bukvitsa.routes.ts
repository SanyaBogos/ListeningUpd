import { Routes, RouterModule } from '@angular/router';
import { BukvitsaImageComponent } from './bukvitsa-image/bukvitsa-image.component';
import { BukvitsaPageXComponent } from './bukvitsa-page-x/bukvitsa-page-x.component';
import { BukvitsaPageComponent } from './bukvitsa-page/bukvitsa-page.component';

const routes: Routes = [
    { path: 'img', component: BukvitsaImageComponent },
    { path: 'act', component: BukvitsaPageComponent },
    { path: 'actx', component: BukvitsaPageXComponent },
];

export const routing = RouterModule.forChild(routes);
