import { Routes, RouterModule } from '@angular/router';
import { BlogComponent } from './blog/blog.component';
import { InterestComponent } from './interest/interest.component';
import { KnowledgeComponent } from './knowledge/knowledge.component';
import { BlogArticleComponent } from './blog-article/blog-article.component';

const routes: Routes = [
    { path: 'blog', component: BlogComponent },
    { path: 'blog/:id', component: BlogArticleComponent },
    { path: 'interest', component: InterestComponent },
    { path: 'knowledge', component: KnowledgeComponent },
];

export const routing = RouterModule.forChild(routes);
