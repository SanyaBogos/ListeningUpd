import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VideoSettingComponent } from './video-setting.component';

describe('VideoSettingComponent', () => {
  let component: VideoSettingComponent;
  let fixture: ComponentFixture<VideoSettingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VideoSettingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VideoSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
