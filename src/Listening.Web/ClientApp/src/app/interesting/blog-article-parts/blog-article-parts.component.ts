import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Input } from '@angular/core';
import { ArticlePart } from '../models/article-part';

@Component({
  selector: 'appc-blog-article-parts',
  templateUrl: './blog-article-parts.component.html',
  styleUrls: ['./blog-article-parts.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BlogArticlePartsComponent implements OnInit {

  @Input() articleParts: ArticlePart[];

  constructor(
    private _ref: ChangeDetectorRef
  ) { }

  ngOnInit() {
  }

}
