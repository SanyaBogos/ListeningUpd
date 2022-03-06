import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BukvitsaComponent } from './bukvitsa/bukvitsa.component';
import { BukvitsaPageComponent } from './bukvitsa-page/bukvitsa-page.component';
import { routing } from './bukvitsa.routes';
import { BukvitsaImageComponent } from './bukvitsa-image/bukvitsa-image.component';
import { ColorPickerModule } from 'ngx-color-picker';
import { AppSharedModule } from '@app/appshared';
import { SliderModule } from 'primeng/slider';
import { BukvitsaBaseComponent } from './bukvitsa-base/bukvitsa-base.component';
import { BukvitsaService } from './bukvitsa.service';
import { BukvitsyTableComponent } from './bukvitsy-table/bukvitsy-table.component';
import { BukvitsyTableXComponent } from './bukvitsy-table-x/bukvitsy-table-x.component';
import { BukvitsaPageXComponent } from './bukvitsa-page-x/bukvitsa-page-x.component';

@NgModule({
  declarations: [BukvitsaBaseComponent, BukvitsaComponent, BukvitsaPageComponent, BukvitsaImageComponent, BukvitsyTableComponent, 
    BukvitsaPageXComponent, BukvitsyTableXComponent],
  imports: [
    routing,
    CommonModule,
    AppSharedModule,
    ColorPickerModule,
    SliderModule
  ],
  providers: [BukvitsaService]
})
export class BukvitsaModule { }
