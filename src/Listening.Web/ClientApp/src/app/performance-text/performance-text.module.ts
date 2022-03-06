import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routing } from './performance-text.routes';
import { SharedModule } from '../shared/shared.module';
import { VgCoreModule } from 'videogular2/compiled/core';
import { VgControlsModule } from 'videogular2/compiled/controls';
import { VgOverlayPlayModule } from 'videogular2/compiled/overlay-play';
import { VgBufferingModule } from 'videogular2/compiled/buffering';
import { ChartsModule } from 'ng2-charts';
import { TextsListComponent } from './texts-list/texts-list.component';
import { TextDescriptionComponent } from './text-description/text-description.component';
import { StreamDescriptionComponent } from './stream-description/stream-description.component';
import { TextsListAggregatedComponent } from './texts-list-aggregated/texts-list-aggregated.component';
import { ModalComponent } from '../shared/directives/modal/modal.component';
import { HotkeysService } from './hotkeys.service';
import { GeneralHeaderComponent } from './general-header/general-header.component';
import { MainTextComponent } from './main-text/main-text.component';
import { InfoComponent } from './info/info.component';
import { JoinedTextComponent } from './joined-text/joined-text.component';
import { SeparatedTextComponent } from './separated-text/separated-text.component';
import { CheckGuessingService } from './check-guessing.service';
import { AbstractTextComponent } from './abstract-text/abstract-text.component';
import { SeparatedTextService } from './separated-text.service';
import { GuessingDiagramComponent } from './guessing-diagram/guessing-diagram.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { GuessingResultComponent } from './guessing-result/guessing-result.component';
import { BuildResultService } from './build-result.service';
import { AppSharedModule } from '../appshared';
import { TranslateTextService } from './translate-text.service';

@NgModule({
  imports: [routing, CommonModule,
    SharedModule, AppSharedModule, ChartsModule, VgCoreModule,
    VgControlsModule, VgOverlayPlayModule, VgBufferingModule,
    NgxChartsModule
  ],
  declarations: [TextsListComponent, TextDescriptionComponent, InfoComponent, StreamDescriptionComponent,
    TextsListAggregatedComponent, GeneralHeaderComponent, MainTextComponent,
    JoinedTextComponent, SeparatedTextComponent, AbstractTextComponent,
    GuessingDiagramComponent,
    GuessingResultComponent],
  exports: [ModalComponent],
  providers: [CheckGuessingService, HotkeysService, SeparatedTextService,
    BuildResultService, TranslateTextService]
})
export class PerformanceTextModule { }
