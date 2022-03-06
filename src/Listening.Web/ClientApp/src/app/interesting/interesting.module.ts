import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlogComponent } from './blog/blog.component';
import { BlogArticlePartsComponent } from './blog-article-parts/blog-article-parts.component';
import { routing } from './interesting.routes';
import { AppSharedModule } from '@app/appshared';
import { KnowledgeComponent } from './knowledge/knowledge.component';
import { InterestComponent } from './interest/interest.component';
import { ThinkingComponent } from './thinking/thinking.component';
import { VideoSharedModule } from '@app/video-shared/video-shared.module';
import { QuillModule } from 'ngx-quill';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { SharedModule } from '@app/shared/shared.module';
import { EditBlogComponent } from './edit-blog/edit-blog.component';
import { FullBlogComponent } from './full-blog/full-blog.component';
import { BlogService } from './blog.service';
import { ListBlogDescriptionComponent } from './list-blog-description/list-blog-description.component';
import { BlogArticleComponent } from './blog-article/blog-article.component';

@NgModule({
  declarations: [BlogComponent, BlogArticlePartsComponent, KnowledgeComponent, InterestComponent, ThinkingComponent,
    EditBlogComponent, FullBlogComponent, ListBlogDescriptionComponent, BlogArticleComponent, 
    ],
  imports: [
    routing,
    CommonModule,
    AppSharedModule,
    SharedModule,
    VideoSharedModule,
    QuillModule,
    AutoCompleteModule,
  ],
  providers: [BlogService]
})
export class InterestingModule { }
