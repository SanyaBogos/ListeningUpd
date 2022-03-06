import { ModalComponent } from '../../shared/directives/modal/modal.component';
import { Component, OnInit, Input, ViewChild, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { TextDescription } from '../models/textDescription';
import { WordClient } from '../../../apiDefinitions';
import { BuildWordService } from '../../shared/services/build-word.service';
import { MyNotificationsService } from '../../core/services/my-notifications.service';
import { Status } from '../../core/models/status';

@Component({
  selector: 'appc-text-description',
  templateUrl: './text-description.component.html',
  styleUrls: ['./text-description.component.scss'],
  providers: [WordClient],
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class TextDescriptionComponent implements OnInit {

  @ViewChild(ModalComponent, { static: false })
  private readonly confirmStartingOver: ModalComponent;

  @Input() description: TextDescription;
  @Input() guessingType: string;

  constructor(
    private cdRef: ChangeDetectorRef,
    private wordClient: WordClient,
    private notificationsService: MyNotificationsService,
    private buildWordService: BuildWordService
  ) { }

  ngOnInit() {
    this.cdRef.markForCheck();
  }

  public showStartOver() {
    this.confirmStartingOver.show();
    this.cdRef.markForCheck();
  }

  public hideStartOver() {
    this.confirmStartingOver.hide();
    this.cdRef.markForCheck();
  }

  public startOver() {
    this.confirmStartingOver.hide();
    const self = this;
    this.wordClient.finishGuessing(this.description.textId, this.guessingType)
      .subscribe(success => {
        self.buildWordService.announceStartOver();
        self.notificationsService.notify('started_from_scretch', Status.Info);
      });
  }

}
