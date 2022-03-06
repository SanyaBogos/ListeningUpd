import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { BookDto } from 'apiDefinitions';

@Component({
  selector: 'appc-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BooksComponent implements OnInit {

  @Input() items: BookDto[];

  constructor() { }

  ngOnInit() {
  }

}
