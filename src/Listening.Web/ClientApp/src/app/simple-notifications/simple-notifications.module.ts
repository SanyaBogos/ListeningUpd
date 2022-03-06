import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimpleNotificationsComponent } from './simple-notifications/simple-notifications.component';
import { NotificationComponent } from './simple-notifications/notification.component';
import { MaxPipe } from './simple-notifications/max.pipe';
import { NotificationsService } from './simple-notifications/notifications.service';
// import { MyNotificationsService } from './simple-notifications/my-notifications.service';
// import { MyNotificationsComponent } from './components/my-notifications/my-notifications.component';

// Type
export * from './simple-notifications/interfaces/notification.type';
export * from './simple-notifications/interfaces/notification-event.type';
export * from './simple-notifications/interfaces/options.type';
export * from './simple-notifications/interfaces/icons';

export * from './simple-notifications/simple-notifications.component';
export * from './simple-notifications/notification.component';
export * from './simple-notifications/max.pipe';
export * from './simple-notifications/notifications.service';

@NgModule({
  providers: [/*MyNotificationsService,*/ NotificationsService],
  imports: [
    CommonModule
  ],
  declarations: [
    SimpleNotificationsComponent,
    // MyNotificationsComponent,
    NotificationComponent,
    MaxPipe
  ],
  exports: [SimpleNotificationsComponent /*, MyNotificationsComponent*/]
})
export class SimpleNotificationsModule {
  public static forRoot(): ModuleWithProviders {
    return {
      ngModule: SimpleNotificationsModule,
    };
  }
}
