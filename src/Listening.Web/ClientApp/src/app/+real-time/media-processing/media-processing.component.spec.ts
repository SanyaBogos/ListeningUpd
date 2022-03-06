import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VideoConversationComponent } from './video-conversation.component';

describe('VideoConversationComponent', () => {
  let component: VideoConversationComponent;
  let fixture: ComponentFixture<VideoConversationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VideoConversationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VideoConversationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
