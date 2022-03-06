import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { BlogClient, SinglePostDto } from 'apiDefinitions';
import { BlogIdentifier } from '../models/blogIdentifier';
import { ArticlePart } from '../models/article-part';
import { BlogService } from '../blog.service';

@Component({
  selector: 'appc-blog-article',
  templateUrl: './blog-article.component.html',
  styleUrls: ['./blog-article.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BlogArticleComponent implements OnInit {

  public post: SinglePostDto;
  public articleParts: ArticlePart[];

  constructor(
    private _ref: ChangeDetectorRef,
    private _route: ActivatedRoute,
    private _router: Router,
    private _blogService: BlogService,
    private _blogClient: BlogClient
  ) { }

  ngOnInit() {
    const self = this;

    this._route.params.pipe(
      switchMap((params: BlogIdentifier) => {
        return self._blogClient.getPost(params.id);
      }))
      .subscribe(post => {
        self.post = post;
        self.articleParts = self._blogService.getArticleParts(post.message);

        self._ref.markForCheck();
      });
  }

  goToBlog() {
    this._router.navigate(['interesting/blog']);
  }

}
