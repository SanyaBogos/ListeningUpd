import { Routes, RouterModule } from '@angular/router';
import { StegImageInjectComponent } from './steg-image-inject/steg-image-inject.component';
import { StegImageEjectComponent } from './steg-image-eject/steg-image-eject.component';

const routes: Routes = [
    { path: 'img-inj', component: StegImageInjectComponent },
    { path: 'img-ej', component: StegImageEjectComponent },
];

export const routing = RouterModule.forChild(routes);
