import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VideoRtcConversationComponent } from './video-rtc-conversation.component';

describe('VideoRtcConversationComponent', () => {
  let component: VideoRtcConversationComponent;
  let fixture: ComponentFixture<VideoRtcConversationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VideoRtcConversationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VideoRtcConversationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
