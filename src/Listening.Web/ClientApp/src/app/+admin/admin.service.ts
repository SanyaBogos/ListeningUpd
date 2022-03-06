import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class AdminService {

  private useAudioSource = new Subject<boolean>();
  private ocrTextSource = new Subject<string>();

  useAudio$ = this.useAudioSource.asObservable();
  ocrText$ = this.ocrTextSource.asObservable();

  constructor() { }

  announceChangeType(useAudio: boolean) {
    this.useAudioSource.next(useAudio);
  }

  announcePushOcrText(ocrText: string) {
    this.ocrTextSource.next(ocrText);
  }

}
