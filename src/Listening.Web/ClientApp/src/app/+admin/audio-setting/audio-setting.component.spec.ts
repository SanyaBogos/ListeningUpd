import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AudioSettingComponent } from './audio-setting.component';

describe('AudioSettingComponent', () => {
  let component: AudioSettingComponent;
  let fixture: ComponentFixture<AudioSettingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AudioSettingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AudioSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
