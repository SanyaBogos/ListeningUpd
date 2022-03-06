import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NeedHelpComponent } from './need-help/need-help.component';
import { SharedModule } from '@app/shared/shared.module';
import { AppSharedModule } from '@app/appshared';
import { routing } from './need-help.routes';

@NgModule({
  declarations: [NeedHelpComponent],
  imports: [
    routing,
    CommonModule,
    SharedModule,
    AppSharedModule,
  ]
})
export class NeedHelpModule { }
