import { Routes, RouterModule } from '@angular/router';

import { AdminComponent } from './admin.component';
import { ListTextsComponent } from './list-texts/list-texts.component';
import { EditTextComponent } from './edit-text/edit-text.component';
import { RolesComponent } from './roles/roles.component';
import { BackupComponent } from './backup/backup.component';

const routes: Routes = [
  { path: '', component: AdminComponent },
  { path: 'texts', component: ListTextsComponent },
  { path: 'roles', component: RolesComponent },
  { path: 'bckp', component: BackupComponent },
  { path: 'edit', component: EditTextComponent },
  { path: 'edit/:textId', component: EditTextComponent },
];

export const routing = RouterModule.forChild(routes);
