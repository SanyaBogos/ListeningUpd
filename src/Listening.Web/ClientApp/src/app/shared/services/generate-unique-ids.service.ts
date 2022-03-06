import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GenerateUniqueIdsService {

  private captureVideoCameraId: number;
  private captureAudioFromCameraId: number;
  private captureScreenId: number;

  constructor() {
    this.captureVideoCameraId = 0;
    this.captureAudioFromCameraId = 0;
    this.captureScreenId = 0;
  }

  getCaptureVideoCameraId() {
    return this.captureVideoCameraId++;
  }

  getCaptureAudioFromCameraId() {
    return this.captureAudioFromCameraId++;
  }

  getCaptureScreenId() {
    return this.captureScreenId++;
  }

}
