import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeVideoSettingComponent } from './youtube-video-setting.component';

describe('YoutubeVideoSettingComponent', () => {
  let component: YoutubeVideoSettingComponent;
  let fixture: ComponentFixture<YoutubeVideoSettingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YoutubeVideoSettingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YoutubeVideoSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
