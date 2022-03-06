import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogArticlePartsComponent } from './blog-article-parts.component';

describe('BlogArticlePartsComponent', () => {
  let component: BlogArticlePartsComponent;
  let fixture: ComponentFixture<BlogArticlePartsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlogArticlePartsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlogArticlePartsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
