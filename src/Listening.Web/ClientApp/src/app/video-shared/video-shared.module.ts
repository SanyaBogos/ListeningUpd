import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VgCoreModule } from 'videogular2/compiled/core';
import { VgControlsModule } from 'videogular2/compiled/controls';
import { VgOverlayPlayModule } from 'videogular2/compiled/overlay-play';
import { VgBufferingModule } from 'videogular2/compiled/buffering';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    VgCoreModule, VgControlsModule, VgOverlayPlayModule, VgBufferingModule
  ],
  exports: [
    VgCoreModule, VgControlsModule, VgOverlayPlayModule, VgBufferingModule
  ]
})
export class VideoSharedModule { }
