import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FeedbackComponent } from './feedback/feedback.component';
import { routing } from './feedback.routes';
import { AppSharedModule } from '@app/appshared';

@NgModule({
  imports: [
    routing,
    CommonModule,
    AppSharedModule
  ],
  declarations: [FeedbackComponent]
})
export class FeedbackModule { }
