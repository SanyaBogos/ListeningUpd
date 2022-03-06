import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookDto, SpecClient } from 'apiDefinitions';
import { TreeNode } from 'primeng/api';
import { switchMap } from 'rxjs/operators';
import { VideoIdentifier } from '../models/videoIdentifier';
import { SpecDataService } from '../spec-data.service';

@Component({
  selector: 'appc-spec-general',
  templateUrl: './spec-general.component.html',
  styleUrls: ['./spec-general.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SpecGeneralComponent implements OnInit {

  public files: TreeNode[];
  public books: BookDto[];
  public name: string;
  public originalLink: string;
  public originalSite: string;
  public description: string;

  constructor(
    private _ref: ChangeDetectorRef,
    private _route: ActivatedRoute,
    private _specClient: SpecClient,
    private _specDataService: SpecDataService,
  ) { }

  ngOnInit() {
    const self = this;
    let videoIdentifier: VideoIdentifier;

    this._route.params.pipe(
      switchMap((params: VideoIdentifier) => {
        videoIdentifier = params;
        return self._specClient.getVideoDescriptionList(params.courseId);
      }))
      .subscribe(course => {
        this.originalSite = course.originalSite;
        this.originalLink = course.originalLink;
        this.description = course.description;
        this.name = course.name;
        this.books = course.books;

        const foldersWithFiles = this._specDataService.getFoldersAndFiles(course, videoIdentifier.videoId);

        this.files = foldersWithFiles;

        self._ref.markForCheck();
      });
  }

}
