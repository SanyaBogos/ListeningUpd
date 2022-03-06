import { Routes, RouterModule } from '@angular/router';
import { TextsListAggregatedComponent } from './texts-list-aggregated/texts-list-aggregated.component';
import { MainTextComponent } from './main-text/main-text.component';

const routes: Routes = [
    { path: '', component: TextsListAggregatedComponent },
    { path: ':textFormType/:listeningType/:textId/:title/:fileName', component: MainTextComponent },
    { path: ':textFormType/:listeningType/:textId/:title/:fileName/:subTitle', component: MainTextComponent },
];

export const routing = RouterModule.forChild(routes);
