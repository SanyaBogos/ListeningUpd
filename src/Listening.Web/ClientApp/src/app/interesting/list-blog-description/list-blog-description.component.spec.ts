import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListBlogDescriptionComponent } from './list-blog-description.component';

describe('ListBlogDescriptionComponent', () => {
  let component: ListBlogDescriptionComponent;
  let fixture: ComponentFixture<ListBlogDescriptionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListBlogDescriptionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListBlogDescriptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
