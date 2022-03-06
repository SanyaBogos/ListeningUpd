import { Injectable } from '@angular/core';

@Injectable()
export class DownloadService {

  constructor() { }

  run(url: string, name: string) {
    // just for download process therefore I didn't create it on html template
    var link = document.createElement("a");
    link.download = name;
    link.href = url;
    // link.
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    link = null;
  }
}
