import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StegImageInjectComponent } from './steg-image-inject/steg-image-inject.component';
import { routing } from './stegano.routes';
import { AppSharedModule } from '@app/appshared';
import { SliderModule } from 'primeng/slider';
import { DataOperationsService } from './data-operations.service';
import { ImgTransformService } from './img-transform.service';
import { StegImageEjectComponent } from './steg-image-eject/steg-image-eject.component';
import { StegImageBaseComponent } from './steg-image-base/steg-image-base.component';
import { StegSettingsComponent } from './steg-settings/steg-settings.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { DownloadService } from '@app/appshared/download/download.service';

@NgModule({
  declarations: [StegImageInjectComponent, StegImageEjectComponent, StegImageBaseComponent, StegSettingsComponent],
  imports: [
    routing,
    AppSharedModule,
    SliderModule,
    DragDropModule,
    CommonModule
  ],
  providers: [DataOperationsService, ImgTransformService, DownloadService]
})
export class SteganoModule { }
