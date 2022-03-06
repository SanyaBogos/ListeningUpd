import { Routes, RouterModule } from '@angular/router';
import { DebianFaiComponent } from './components/debian-fai.component';
// import { ImgRecognitionComponent } from './optical-recognition/img-recognition/img-recognition.component';
// import { YoutubeDownloadComponent } from './youtube-download/youtube-download.component';
// import { DebianFaiComponent } from './debian-fai/debian-fai.component';

const routes: Routes = [
    // { path: 'recognition', component: ImgRecognitionComponent },
    // { path: 'ytbdwnld', component: YoutubeDownloadComponent },
    { path: '', component: DebianFaiComponent },
];

export const routing = RouterModule.forChild(routes);
