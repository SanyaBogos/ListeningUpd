import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppSharedModule } from '../appshared';
// Components
import { SocialLoginComponent } from './components/social-login/social-login.component';
import { DynamicFormComponent } from './forms/dynamic-form/dynamic-form.component';
import { DynamicFormControlComponent } from './forms/dynamic-form-control/dynamic-form-control.component';
import { ErrorSummaryComponent } from './forms/error-summary/error-summary.component';
// Pipes
import { UppercasePipe } from './pipes/uppercase.pipe';
// Services
import { FormControlService } from './forms/form-control.service';
import { SubMenuComponent } from './components/sub-menu/sub-menu.component';

import { ModalComponent } from './directives/modal/modal.component';
import { CountrySelectorComponent } from './components/country-selector/country-selector.component';
import { CountriesListService } from './services/countries-list.service';
import { SortingIconComponent } from './components/sorting-icon/sorting-icon.component';
import { ObjectProcessService } from './services/object-process.service';
import { FilterSortBaseComponent } from './components/text/filter-sort-base/filter-sort-base.component';
import { FilterSortService } from './services/filter-sort.service';
import { TooltipModule } from 'ngx-bootstrap';
import { PasswordComponent } from './components/password/password.component';
import { BuildWordService } from './services/build-word.service';
import { ChartEventsService } from './services/chart-events.service';
import { SafePipe } from './pipes/safe.pipe';
import { BrowserDetectService } from './services/browser-detect.service';
// import { CountdownComponent } from '../appshared/components/countdown/countdown.component';
import { RecorderScreenComponent } from './components/recorder/recorder-screen/recorder-screen.component';
import { RecorderMediaComponent } from './components/recorder/recorder-media/recorder-media.component';
import { RecorderBaseComponent } from './components/recorder/recorder-base/recorder-base.component';
import { GenerateUniqueIdsService } from './services/generate-unique-ids.service';
import { ComplexityComponent } from './components/complexity/complexity.component';
import { TopicComponent } from './components/topic/topic.component';
import { AngularMyDatePickerModule } from 'angular-mydatepicker';
import { DateFilterComponent } from './components/date-filter/date-filter.component';
import { GenerateObjectByEnumService } from './services/generate-object-by-enum.service';
import { BaseSubscriptionsComponent } from './components/base-subscriptions/base-subscriptions.component';
import { EmailComponent } from './components/email/email.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    AppSharedModule,
    // No need to export as these modules don't expose any components/directive etc'
    NgbModule.forRoot(),
    TooltipModule.forRoot(),
    AngularMyDatePickerModule
  ],
  declarations: [
    SocialLoginComponent,
    DynamicFormComponent,
    DynamicFormControlComponent,
    ErrorSummaryComponent,
    UppercasePipe,
    SafePipe,
    SubMenuComponent,
    ModalComponent,
    CountrySelectorComponent,
    SortingIconComponent,
    FilterSortBaseComponent,
    PasswordComponent,
    EmailComponent,
    RecorderScreenComponent,
    RecorderMediaComponent,
    RecorderBaseComponent,
    ComplexityComponent,
    TopicComponent,
    DateFilterComponent,
    BaseSubscriptionsComponent
  ],
  exports: [
    // Modules
    RouterModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AppSharedModule,
    NgbModule,
    TooltipModule,
    // Providers, Components, directive, pipes
    SocialLoginComponent,
    DynamicFormComponent,
    DynamicFormControlComponent,
    ErrorSummaryComponent,
    SubMenuComponent,
    UppercasePipe,
    SafePipe,

    ModalComponent,
    CountrySelectorComponent,
    SortingIconComponent,
    FilterSortBaseComponent,
    PasswordComponent,
    EmailComponent,
    RecorderScreenComponent,
    RecorderMediaComponent,
    ComplexityComponent,
    TopicComponent,
    DateFilterComponent,
    BaseSubscriptionsComponent
  ],
  providers: [
    FormControlService, CountriesListService, FilterSortService,
    ObjectProcessService, BuildWordService, ChartEventsService,
    BrowserDetectService, GenerateUniqueIdsService, GenerateObjectByEnumService
  ]

})
export class SharedModule { }
