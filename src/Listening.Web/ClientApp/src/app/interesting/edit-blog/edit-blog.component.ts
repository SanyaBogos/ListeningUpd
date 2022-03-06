import { Component, OnInit, ChangeDetectionStrategy, ViewChild, ChangeDetectorRef, Input } from '@angular/core';
import { AccountService } from '@app/core';
import { ModalComponent } from '@app/shared/directives/modal/modal.component';
import { BlogClient, PostWriteDto, TopicDto, PriorityDto, IPostWriteDto, FileClient, FileParameter, AdditionalDto } from 'apiDefinitions';
import { MyNotificationsService } from '@app/core/services/my-notifications.service';
import { Status } from '@app/core/models/status';
import { BlogService } from '../blog.service';
import { ArticlePart } from '../models/article-part';
import { AppService } from '@app/app.service';
import { Actions } from '../models/actions';

@Component({
  selector: 'appc-edit-blog',
  templateUrl: './edit-blog.component.html',
  styleUrls: ['./edit-blog.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EditBlogComponent implements OnInit {

  @Input() additional: AdditionalDto;

  @ViewChild('blogPreviewInfo', { static: false })
  private readonly blogPreviewInfo: ModalComponent;

  @ViewChild('confirmBlg', { static: false })
  private readonly confirmBlg: ModalComponent;

  public isSuper: boolean;
  public currentPost: PostWriteDto;
  public filteredTopicsDto: TopicDto[] = [];
  public selectedTopics: TopicDto[] = [];

  public videoFile: File;
  public videoFileName: string;
  public articleParts: ArticlePart[] = [];
  public isPreviewVisible: boolean;
  public modalMessages: string[];
  public selectedVideoForDelete: string;
  public selectedVideo: string;


  private currentTopic: string;
  private _currentConfirmAction: Actions;
  private _currentArticleToMerge: ArticlePart;
  private _dictionaryActions: Map<Actions, Function>;

  constructor(
    private _cdRef: ChangeDetectorRef,
    private _accountService: AccountService,
    private _fileClient: FileClient,
    private _blogService: BlogService,
    private _blogClient: BlogClient,
    private _appService: AppService,
    private _notificationsService: MyNotificationsService
  ) {
    this.articleParts = [new ArticlePart()];
    this._dictionaryActions = new Map<Actions, Function>();
    this._dictionaryActions.set(Actions.SaveArticle, this.save);
    this._dictionaryActions.set(Actions.DeleteVideo, this.deleteVideo);
    this._dictionaryActions.set(Actions.DeleteVideo, this.deleteVideo);
  }

  ngOnInit() {
    const { videos } = this.additional;

    if (videos && videos.length > 0)
      this.selectedVideo = videos[0];

    this.isSuper = this._accountService.isSuper();

    this.currentPost = new PostWriteDto(<IPostWriteDto>{ id: 0 });
    const { topics, priorities } = this.additional;

    this._blogService.currentPost$
      .subscribe((post) => {
        this.currentPost = post;
        this.articleParts = this._blogService.getArticleParts(post.message);
        this.selectedTopics = topics.filter(x => post.topicIds.indexOf(x.id) > -1);
        this._cdRef.markForCheck();
      });

    priorities.unshift(...priorities.splice(priorities.length - 1));
    this.currentPost.priorityId = priorities[0].id;
  }

  preview() {
    this.isPreviewVisible = true;

    this.blogPreviewInfo.show();
    this._cdRef.markForCheck();
  }

  hideModal() {
    this.isPreviewVisible = false;
    this.blogPreviewInfo.hide();
    this._cdRef.markForCheck();
  }

  presave() {
    const content = this._appService.appData.content;
    this.modalMessages = [content['inst_blg_art_cnfrm'], content['cnfrm_insrt_blg_art_qst']];
    this._currentConfirmAction = Actions.SaveArticle;

    if (this.selectedTopics.length === 0)
      this.confirmBlg.show();
    else
      this.save(this);
  }

  runAction() {
    this._dictionaryActions.get(this._currentConfirmAction)(this);
  }

  save(context: EditBlogComponent) {
    const isNew = context.currentPost.id == 0;
    context.currentPost.topicIds = context.selectedTopics.map(x => x.id);
    context.currentPost.message = context.articleParts.map(x => x.text).join('');

    if (isNew)
      context._blogClient.add(context.currentPost)
        .subscribe((id: number) => {
          context.currentPost.id = id;
          context._blogService.announcePostInsertedChange(context.currentPost);
          context.clean();

          context.confirmBlg.hide();
          context._notificationsService.notify('scs_artcl_sav', Status.Success);
        });
    else {
      const post = context.currentPost;
      context._blogClient.update(post)
        .subscribe(() => {
          context._blogService.announcePostUpdatedChange(post);
          context.clean();

          context.confirmBlg.hide();
          context._notificationsService.notify('scs_artcl_upd', Status.Success);
        });
    }
  }

  search(event: any) {
    this.currentTopic = event.query;
    this.filteredTopicsDto = [];
    const selectedTopicIds = this.selectedTopics.map(x => x.id);
    const filtered = this.additional.topics.filter(x => x.name.startsWith(event.query) && !(selectedTopicIds.indexOf(x.id) > -1));
    this.filteredTopicsDto = this.filteredTopicsDto.concat(filtered);
  }

  addValue(event: any) {
    if (event.key === 'Enter' && this.currentTopic && (!this.filteredTopicsDto || this.filteredTopicsDto.length === 0)) {
      this._blogClient.addTopic(this.currentTopic)
        .subscribe((id: number) => {
          var newTopicDto = new TopicDto();
          newTopicDto.id = id;
          newTopicDto.name = this.currentTopic;
          this.additional.topics.push(newTopicDto);
          this.selectedTopics.push(newTopicDto);
          this.filteredTopicsDto = [];
          this.currentTopic = '';
          this._cdRef.markForCheck();
        });
    }
  }

  prioritySelected(priorityId: number) {
    this.currentPost.priorityId = priorityId;
  }

  videoSelected(videoName: string) {
    this.selectedVideo = videoName;
  }

  clean() {
    this.currentPost = new PostWriteDto(<IPostWriteDto>{ id: 0 });
    this.selectedTopics = [];
    this.filteredTopicsDto = [];
    this.articleParts = [new ArticlePart()];
  }

  fileChangeEvent(event: any): void {
    var file = event.target.files[0] as File;
    this.videoFile = file;
    const { name } = file;
    this.videoFileName = name;
    this._cdRef.markForCheck();
  }

  saveVideo() {
    this._fileClient.saveBlogVideo(this.videoFileName, { data: this.videoFile } as FileParameter)
      .subscribe((data) => {
        this.videoFileName = data.name;
        this.additional.videos.push(data.name);
        this._injectVideo(data.name);

        this._notificationsService.notify(
          'scs_vid_sav', Status.Success);

        this._cdRef.markForCheck();
      });
  }

  private _injectVideo(videoFileName: string) {
    const lastArticle = this.articleParts[this.articleParts.length - 1];
    const nameAndType = this._blogService.getNameAndTypeParts(videoFileName);
    lastArticle.videoName = nameAndType[0];
    lastArticle.videoType = nameAndType[1];

    const videoHtml = `<br\/><p><span style="color: rgb(255, 255, 255);">vid-=-${videoFileName}<\/span><\/p><br\/>`;
    lastArticle.text = lastArticle.text ? lastArticle.text.concat(videoHtml) : videoHtml;

    this.articleParts.push(new ArticlePart());
  }

  isAvailableClick() {
    const result = this.articleParts && (this.articleParts.length > 1 || (this.articleParts[0] && this.articleParts[0].text && this.articleParts[0].text.trim()));
    return result;
  }

  addVideo() {
    this._injectVideo(this.selectedVideo);
    this._cdRef.markForCheck();
  }

  preFinalVideoRemove() {
    const content = this._appService.appData.content;
    this.modalMessages = [content['fin_rmv_vid_cnfrm'], content['fin_rmv_vid_qst']];
    this._currentConfirmAction = Actions.FinalDeleteVideo;

    this._cdRef.markForCheck();
  }

  preDeleteVideo(article: ArticlePart) {
    this.selectedVideoForDelete = `${article.videoName}.${article.videoType}`;
    const content = this._appService.appData.content;
    this.modalMessages = [content['rmv_vid_cnfrm'], `${content['rmv_vid_qst']} - ${this.selectedVideoForDelete}`];
    this._currentConfirmAction = Actions.DeleteVideo;
    this._currentArticleToMerge = article;
    this._cdRef.markForCheck();

    this.confirmBlg.show();
  }

  deleteVideo(context: EditBlogComponent) {
    const currentArticle = context._currentArticleToMerge;
    const { text } = currentArticle;
    const indexOfTextEnd = context._blogService.getVideoIndex(text);
    let result = text.substring(0, indexOfTextEnd);
    const indexOfCurrentArticle = context.articleParts.indexOf(currentArticle);

    if (context.articleParts.length === indexOfCurrentArticle + 1) {
      context.confirmBlg.hide();
      return;
    }

    const nextArticle = context.articleParts[indexOfCurrentArticle + 1];
    result += nextArticle.text;
    currentArticle.text = result;
    currentArticle.videoName = nextArticle.videoName;
    currentArticle.videoType = nextArticle.videoType;

    context.articleParts.splice(indexOfCurrentArticle + 1, 1);

    context._cdRef.markForCheck();
    context.confirmBlg.hide();
  }

  finalDeleteVideo(context: EditBlogComponent) {
    if (this.selectedVideo)
      this._fileClient.deleteBlogVideo(this.selectedVideo);

    context._cdRef.markForCheck();
    context.confirmBlg.hide();
  }

}
