import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DebianFaiComponent } from './components/debian-fai.component';
import { routing } from './debian-fai.routes';
import { AppSharedModule } from '@app/appshared';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { TutorialComponent } from './components/tutorial/tutorial.component';
import { ClipboardModule } from 'ngx-clipboard';
import { TooltipModule } from 'ngx-bootstrap';
// import { TreeSelectModule } from 'primeng/';

@NgModule({
  declarations: [DebianFaiComponent, TutorialComponent],
  imports: [routing, CommonModule, AppSharedModule, NgxChartsModule, DragDropModule,
    ClipboardModule, TooltipModule.forRoot(), /*TreeSelectModule*/]
})
export class DebianFaiModule { }
