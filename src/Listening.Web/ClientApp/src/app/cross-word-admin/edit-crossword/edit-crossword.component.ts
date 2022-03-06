import { ChangeDetectionStrategy, Component, OnInit, OnDestroy, ChangeDetectorRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Status } from '@app/core/models/status';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { BaseSubscriptionsComponent } from '@app/shared/components/base-subscriptions/base-subscriptions.component';
import { ModalComponent } from '@app/shared/directives/modal/modal.component';
import { MySubscriptions } from '@app/shared/models/mySubscription';
import { CrosswordClient, CrosswordDto, CrosswordWithAdminsListDto, QuestionAndWordDescriptionDto, UserClient, UserViewModel } from 'apiDefinitions';
// import { Settings } from 'http2';
import { of } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { ChangeGridService } from '../change-grid.service';
import { Settings } from '../models/settings';

@Component({
  selector: 'appc-edit-crossword',
  templateUrl: './edit-crossword.component.html',
  styleUrls: ['./edit-crossword.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EditCrosswordComponent extends BaseSubscriptionsComponent implements OnInit {

  @ViewChild(ModalComponent, { static: false })
  private readonly _confirmDeletingModal: ModalComponent;

  private _isNewElement = true;
  private _errorTitle: string;

  public crosswordDto: CrosswordDto;
  // public questionAndWordDescriptionDto: QuestionAndWordDescriptionDto;
  public admins: UserViewModel[];
  public selectedAdmin: number;
  public gridSettings: Settings;

  constructor(
    private _ref: ChangeDetectorRef,
    private _route: ActivatedRoute,
    private _router: Router,
    private _userClient: UserClient,
    private _notificationsService: MyNotificationsService,
    private _changeGridService: ChangeGridService,
    public _crosswordClient: CrosswordClient,
  ) {
    super();

    this._subscriptions.add(
    );
  }

  ngOnInit() {
    this.gridSettings = <Settings>{ size: { height: 20, width: 25 } };
    // this.questionAndWordDescriptionDto = new QuestionAndWordDescriptionDto();

    this._route.params.pipe(
      switchMap((params: any) => {
        if (params && params.id) {
          this._isNewElement = false;
          return this._crosswordClient.getCrossword(params.id);
        }

        this._isNewElement = true;
        const crosswordWithAdmins = new CrosswordWithAdminsListDto();
        crosswordWithAdmins.crossword = new CrosswordDto();
        crosswordWithAdmins.crossword.assigneeId = 0;
        // crosswordWithAdmins.crossword.name
        return of<CrosswordWithAdminsListDto>(crosswordWithAdmins);
      })).subscribe(data => {
        this.crosswordDto = data.crossword;

        this.admins = data.admins;
        // this.useAudio = this._isNewElement ? true : !!data.textDto.audioName;

        if (data.admins == null || data.admins.length === 0)
          this._userClient.getAdmins().subscribe(admins => {
            this.admins = admins;
            this.admins[0].email = `${this.admins[0].email} *`;
            this.selectedAdmin = this.admins[0].id;
            this._ref.markForCheck();
          });

        if (this.crosswordDto.assigneeId !== 0)
          this.selectedAdmin = this.admins.find(x => x.id === this.crosswordDto.assigneeId).id;

        this._ref.markForCheck();
      });
  }

  checkTitle(): void {
    this._errorTitle = !this.crosswordDto.title
      ? 'title_not_empty' : '';
  }

  checkQuestion(): void {
    // if (!this.textDto.text)
    //   this.errorText = 'text_name_not_empty';
    // else if (this.textDto.text.length > 4000)
    //   this.errorText = 'unsupportable_text_length_4000';
    // else
    //   this.errorText = '';
  }

  countrySelected(country: string) {
    this.crosswordDto.country = country;
  }

  complexitySelected(complexityValue: number) {
    this.crosswordDto.complexity = complexityValue;
  }

  save() {
    // TODO: implement
  }

  saveCrossword() {
    // TODO: implement
  }

  applyNewSize() {
    this._changeGridService.announceChangeGridSize({ width: this.gridSettings.size.width, height: this.gridSettings.size.height });
  }

  remove() {
    this._confirmDeletingModal.hide();
    this._crosswordClient.delete(this.crosswordDto.id)
      .subscribe(() => {
        this._notificationsService.notify('scs_del', Status.Success);
        this._router.navigate(['crosswd-adm/list']);
      }, () => {
        this._router.navigate(['crosswd-adm/list']);
      });
  }



}
