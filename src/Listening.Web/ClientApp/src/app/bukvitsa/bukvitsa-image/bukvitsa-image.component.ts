import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
  selector: 'appc-bukvitsa-image',
  templateUrl: './bukvitsa-image.component.html',
  styleUrls: ['./bukvitsa-image.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BukvitsaImageComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
