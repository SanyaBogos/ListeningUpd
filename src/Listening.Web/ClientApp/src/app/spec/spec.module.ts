import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SpecDataService } from './spec-data.service';
import { SpecGeneralComponent } from './spec-general/spec-general.component';
import { SpecTreeVideoComponent } from './spec-tree-video/spec-tree-video.component';
import { SpecListComponent } from './spec-list/spec-list.component';
import { routing } from './spec.routes';
import { AppSharedModule } from '@app/appshared';
import { SharedModule } from '@app/shared/shared.module';
import { VideoSharedModule } from '@app/video-shared/video-shared.module';
import { QuillModule } from 'ngx-quill';
import { TreeModule } from 'primeng/tree';
import { TooltipModule } from 'primeng/tooltip';
import { BooksComponent } from './books/books.component';


@NgModule({
  declarations: [BooksComponent, SpecTreeVideoComponent, SpecGeneralComponent, SpecListComponent],
  imports: [
    routing,
    CommonModule,
    AppSharedModule,
    SharedModule,
    VideoSharedModule,
    QuillModule,
    TreeModule,
    TooltipModule
  ],
  providers: [SpecDataService]
})
export class SpecModule { }
