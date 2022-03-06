import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { RealTimeComponent } from './real-time.component';
import { ChatComponent } from './chat/chat.component';
import { MoveShapeComponent } from './move-shape/move-shape.component';
import { DraggableDirective } from './move-shape/draggable.directive';
import { MediaProcessingComponent } from './media-processing/media-processing.component';
import { UsersListComponent } from './users-list/users-list.component';
import { VideoRtcConversationComponent } from './video-rtc-conversation/video-rtc-conversation.component';
import { RecordComponent } from './record/record.component';

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild([
            { path: '', component: RealTimeComponent, },
            { path: 'chat', component: ChatComponent },
            { path: 'media-processing', component: MediaProcessingComponent },
            { path: 'record', component: RecordComponent },
            { path: 'video-rtc-conversation', component: VideoRtcConversationComponent },
        ])
    ],
    declarations: [
        RealTimeComponent,
        ChatComponent,
        MoveShapeComponent,
        DraggableDirective,
        MediaProcessingComponent,
        UsersListComponent,
        VideoRtcConversationComponent,
        RecordComponent
    ],
    providers: []
})
export class RealTimeModule { }
