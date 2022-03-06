import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'appc-list-cross-word',
  templateUrl: './list-cross-word.component.html',
  styleUrls: ['./list-cross-word.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ListCrossWordComponent implements OnInit {

  constructor(
    private _router: Router,

  ) { }

  ngOnInit() {
  }

  createNewCrossword() {
    this._router.navigate(['crosswd-adm/edit']);
  }

}
