import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { AccountService } from '@app/core/services';
import { BlogClient, PostDto, AdditionalDto } from 'apiDefinitions';
import { BlogService } from '../blog.service';

@Component({
  selector: 'appc-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BlogComponent implements OnInit {

  private _isSuper: boolean;

  public additional: AdditionalDto;
  public isAllSelected?: boolean = null;

  constructor(
    private cdRef: ChangeDetectorRef,
    public blogClient: BlogClient,
    public blogService: BlogService,
    public accountService: AccountService
  ) { }

  ngOnInit() {
    this._isSuper = this.accountService.isSuper();

    this.blogClient.getAdditionalInfo()
      .subscribe(x => {
        this.additional = x;
        this.cdRef.markForCheck();
      });
  }

  getAllArticles() {
    this.isAllSelected = true;
    
    this.blogClient.get(this._isSuper)
      .subscribe((result: PostDto[]) => {
        this.blogService.announcePostsChange(result);
        this.cdRef.markForCheck();
      });
  }

  getDescriptions() {
    this.isAllSelected = false;

    this.blogClient.getDescriptions(true)
      .subscribe((postDescriptions) => {
        this.blogService.announcePostDescriptionsChange(postDescriptions);
        this.cdRef.markForCheck();
      });
  }

}
