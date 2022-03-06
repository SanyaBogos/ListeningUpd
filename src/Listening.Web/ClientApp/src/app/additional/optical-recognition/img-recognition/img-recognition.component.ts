import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { AccountService } from '@app/core/services';

@Component({
  selector: 'appc-img-recognition',
  templateUrl: './img-recognition.component.html',
  styleUrls: ['./img-recognition.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ImgRecognitionComponent implements OnInit {

  public isCaptured: boolean;

  constructor(
    private _accountService: AccountService
  ) { }

  ngOnInit() {
    this.isCaptured = !(this._accountService.isAdmin() || this._accountService.isSuper());
  }

}
