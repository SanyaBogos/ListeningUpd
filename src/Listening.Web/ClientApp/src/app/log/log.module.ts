import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogComponent } from './log/log.component';
import { routing } from './log.routes';
import { AppSharedModule } from '@app/appshared';

@NgModule({
  imports: [
    routing,
    CommonModule,
    AppSharedModule
  ],
  declarations: [LogComponent]
})
export class LogModule { }
