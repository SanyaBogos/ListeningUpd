import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Input } from '@angular/core';
import { BlogService } from '../blog.service';
import { PostDto, BlogClient, TopicDto, PostWriteDto } from 'apiDefinitions';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { Status } from '@app/core/models/status';
import { AccountService } from '@app/core';
import { ArticlePart } from '../models/article-part';

@Component({
  selector: 'appc-full-blog',
  templateUrl: './full-blog.component.html',
  styleUrls: ['./full-blog.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FullBlogComponent implements OnInit {

  @Input() topics: TopicDto[];

  public posts: PostDto[];
  public isSuper: boolean;
  public topicsDictionary: { [id: number]: string; };

  constructor(
    private _ref: ChangeDetectorRef,
    private _accountService: AccountService,
    private _blogService: BlogService,
    private _blogClient: BlogClient,
    private _notificationsService: MyNotificationsService
  ) { }

  ngOnInit() {
    this.isSuper = this._accountService.isSuper();
    this.topicsDictionary = this._blogService.getTopicsDictionary(this.topics);

    this._blogService.posts$
      .subscribe(posts => {
        this.posts = posts;
        this._ref.markForCheck();
      });

    this._blogService.postInseted$
      .subscribe(post => {
        this.posts.push(post);
      });

    this._blogService.postUpdated$
      .subscribe(post => {
        const index = this.posts.findIndex(x => x.id == post.id);
        let postToUpdate = this.posts[index];

        if (!postToUpdate)
          return;

        postToUpdate = new PostDto(post.toJSON());
        postToUpdate.lastModifiedDate = new Date();

        this.posts.splice(index, 1, postToUpdate);
        this._ref.markForCheck();
      });
  }

  getArticleParts(text: string): ArticlePart[] {
    const result = this._blogService.getArticleParts(text);
    return result;
  }

  edit(id: number) {
    const post = this.posts.find(x => x.id == id);
    const postNew = new PostWriteDto(post.toJSON());
    postNew.createdDate = post.createdDate;
    postNew.lastModifiedDate = post.lastModifiedDate;
    this._blogService.announceCurrentPostsChange(postNew);
  }

  delete(id: number) {
    this._blogClient.delete(id)
      .subscribe(() => {
        this._notificationsService.notify('success_article_remove', Status.Success);
        this.posts = this.posts.filter(x => x.id != id);
        this._ref.markForCheck();
      });
  }

}
