import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlotComponent } from './plot/plot.component';
import { routing } from './math.routes';
import { AppSharedModule } from '@app/appshared';
import { ColorPickerModule } from 'ngx-color-picker';

@NgModule({
  declarations: [PlotComponent],
  imports: [routing, CommonModule, AppSharedModule, ColorPickerModule]
})
export class MathModule { }
