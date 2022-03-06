import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { YouTubeDownloaderClient, VideoStreamInfoViewModel } from 'apiDefinitions';
import { DownloadService } from '@app/appshared/download/download.service';

@Component({
  selector: 'appc-youtube-download',
  templateUrl: './youtube-download.component.html',
  styleUrls: ['./youtube-download.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class YoutubeDownloadComponent implements OnInit {

  url: string;
  info: VideoStreamInfoViewModel;

  constructor(
    private ref: ChangeDetectorRef,
    private downloadService: DownloadService,
    private youtubeClient: YouTubeDownloaderClient
  ) { }

  ngOnInit() {
    // this.ref.markForCheck();
  }

  getInfo() {
    // var result = await this.youtubeClient.getVideoInfo(this.url);
    // result
    this.youtubeClient.getVideoInfo(this.url)
      .subscribe(info => {
        console.log(info);
        this.info = info;
        this.ref.markForCheck();
      });
  }

  download() {
    this.youtubeClient.download(this.url).subscribe(file => {
      // file.
      const blob = new Blob([file.data], { type: 'video/mp4' });
      // window.URL.createObjectURL(blob);
      let link = URL.createObjectURL(blob);
      this.downloadService.run(link, file.fileName ? file.fileName : 'newVideo.mp4');
    });
  }

}
