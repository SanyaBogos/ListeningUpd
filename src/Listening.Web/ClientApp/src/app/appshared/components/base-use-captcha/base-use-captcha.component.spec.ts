import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseUseCaptchaComponent } from './base-use-captcha.component';

describe('BaseUseCaptchaComponent', () => {
  let component: BaseUseCaptchaComponent;
  let fixture: ComponentFixture<BaseUseCaptchaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BaseUseCaptchaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseUseCaptchaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
