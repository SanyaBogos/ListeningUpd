import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OpticalRecognitionComponent } from './optical-recognition.component';

describe('OpticalRecognitionComponent', () => {
  let component: OpticalRecognitionComponent;
  let fixture: ComponentFixture<OpticalRecognitionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpticalRecognitionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpticalRecognitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
