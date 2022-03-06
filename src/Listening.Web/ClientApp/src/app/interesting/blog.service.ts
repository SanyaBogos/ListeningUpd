import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { PostDto, PostWriteDto, PostDescriptionDto, TopicDto } from 'apiDefinitions';
import { ArticlePart } from './models/article-part';

@Injectable()
export class BlogService {

  private _regexp = /vid-=-([a-z0-9_-]+.[a-z0-9]{2,8})</ig;
  private _postsSource = new Subject<PostDto[]>();
  private _postDescriptionsSource = new Subject<PostDescriptionDto[]>();
  private _currentPostSource = new Subject<PostWriteDto>();
  private _postInsetedSource = new Subject<PostWriteDto>();
  private _postUpdatedSource = new Subject<PostWriteDto>();

  posts$ = this._postsSource.asObservable();
  postDescriptions$ = this._postDescriptionsSource.asObservable();
  currentPost$ = this._currentPostSource.asObservable();
  postInseted$ = this._postInsetedSource.asObservable();
  postUpdated$ = this._postUpdatedSource.asObservable();

  constructor() {
    this._regexp
  }

  announcePostsChange(posts: PostDto[]) {
    this._postsSource.next(posts);
  }

  announcePostDescriptionsChange(postDescriptions: PostDescriptionDto[]) {
    this._postDescriptionsSource.next(postDescriptions);
  }

  announceCurrentPostsChange(post: PostWriteDto) {
    this._currentPostSource.next(post);
  }

  announcePostInsertedChange(post: PostWriteDto) {
    this._postInsetedSource.next(post);
  }

  announcePostUpdatedChange(post: PostWriteDto) {
    this._postUpdatedSource.next(post);
  }

  getTopicsDictionary(topics: TopicDto[]): { [id: number]: string; } {
    const topicsDictionary: { [id: number]: string; } = {};

    topics.forEach(x => {
      topicsDictionary[x.id] = x.name;
    });

    return topicsDictionary;
  }

  getNameAndTypeParts(fullName: string) {
    const index = fullName.lastIndexOf('.');
    const result = [fullName.substring(0, index), fullName.substring(index + 1)];
    return result;
  }

  getArticleParts(text: string): ArticlePart[] {
    let regexpResult: RegExpExecArray;
    let prevIndex: number = 0;
    const articleParts = [];

    // console.log(text);

    do {
      regexpResult = this._regexp.exec(text);

      if (!regexpResult || regexpResult.length === 0){
        articleParts.push(new ArticlePart(text.substring(prevIndex)));
        break;
      }

      const index = regexpResult.index + regexpResult[0].length + 15;

      const nameAndType = this.getNameAndTypeParts(regexpResult[1]);
      const articlePart = new ArticlePart(text.substring(prevIndex, index), nameAndType[0], nameAndType[1]);
      articleParts.push(articlePart);

      prevIndex = index;
    } while (regexpResult);

    return articleParts;
  }

  getVideoIndex(text: string) {
    const regexpResult = this._regexp.exec(text);
    const result = regexpResult.index - 44;
    return result;
  }

}
