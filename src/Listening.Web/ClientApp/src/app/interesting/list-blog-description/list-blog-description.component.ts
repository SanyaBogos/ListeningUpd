import { Component, OnInit, Input, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { TopicDto, PostDescriptionDto } from 'apiDefinitions';
import { BlogService } from '../blog.service';
import { Router } from '@angular/router';

@Component({
  selector: 'appc-list-blog-description',
  templateUrl: './list-blog-description.component.html',
  styleUrls: ['./list-blog-description.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ListBlogDescriptionComponent implements OnInit {

  @Input() topics: TopicDto[];

  public postDescriptions: PostDescriptionDto[];
  public topicsDictionary: { [id: number]: string; };

  constructor(
    private cdRef: ChangeDetectorRef,
    private blogService: BlogService,
    private router: Router
  ) {
    this.blogService.postDescriptions$
      .subscribe(postDescriptions => {
        this.postDescriptions = postDescriptions;
        this.cdRef.markForCheck();
      });
  }

  ngOnInit() {
    this.topicsDictionary = this.blogService.getTopicsDictionary(this.topics);
  }

  choseArticle(id: number) {
    this.router.navigate([`interesting/blog`,id]);
  }

}
