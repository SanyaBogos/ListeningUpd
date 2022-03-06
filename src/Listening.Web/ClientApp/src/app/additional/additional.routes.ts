import { Routes, RouterModule } from '@angular/router';
import { ImgRecognitionComponent } from './optical-recognition/img-recognition/img-recognition.component';
import { YoutubeDownloadComponent } from './youtube-download/youtube-download.component';

const routes: Routes = [
    { path: 'recognition', component: ImgRecognitionComponent },
    { path: 'ytbdwnld', component: YoutubeDownloadComponent },
];

export const routing = RouterModule.forChild(routes);
