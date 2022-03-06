import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { FeedbackClient, FeedbackQueryViewModel, PagedDataViewModelOfFeedbackDto, FeedbackDto, FeedbackInsertDto } from 'apiDefinitions';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { Status } from '@app/core/models/status';
import { AccountService } from '@app/core/services';

@Component({
  selector: 'appc-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FeedbackComponent implements OnInit {

  private p = 1;
  private itemPerPage = 10;
  private query: FeedbackQueryViewModel;
  public feedbacks: PagedDataViewModelOfFeedbackDto;
  public currentFeedback: FeedbackInsertDto;

  constructor(
    private cdRef: ChangeDetectorRef,
    private feedbackClient: FeedbackClient,
    private notificationsService: MyNotificationsService,
    private accountService: AccountService
  ) {
    this.feedbacks = <PagedDataViewModelOfFeedbackDto>{ count: 0, data: [] };
    this.currentFeedback = <FeedbackInsertDto>{ topic: '', details: '', isVisible: true };
  }

  ngOnInit() {
    this.query = new FeedbackQueryViewModel();
    this.query.page = this.p;
    this.query.isAscending = true;
    this.query.elementsPerPage = this.itemPerPage;

    this.getFeedbacks();
  }

  saveFeedback() {
    const feedbackToAdd = new FeedbackDto();
    Object.assign(feedbackToAdd, this.currentFeedback);

    this.feedbackClient.postFeedbacks(this.currentFeedback)
      .subscribe(id => {
        feedbackToAdd.id = id;
        feedbackToAdd.email = this.accountService.user.name;
        feedbackToAdd.createdTime = new Date();
        this.feedbacks.data.unshift(feedbackToAdd);
        this.currentFeedback = <FeedbackInsertDto>{ isVisible: true };
        this.cdRef.markForCheck();
        this.notificationsService.notify('success_feedback_add', Status.Success);
      });
  }

  pageChanged(page: number) {
    this.p = page;
    this.query.page = page;
    this.getFeedbacks();
  }

  private getFeedbacks() {
    this.feedbackClient.getFeedbacks(this.query)
      .subscribe(data => {
        this.feedbacks = data;
        this.cdRef.markForCheck();
      });
  }

}
