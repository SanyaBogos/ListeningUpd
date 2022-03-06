import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnOpticalRecognitionComponent } from './own-optical-recognition.component';

describe('OwnOpticalRecognitionComponent', () => {
  let component: OwnOpticalRecognitionComponent;
  let fixture: ComponentFixture<OwnOpticalRecognitionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OwnOpticalRecognitionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OwnOpticalRecognitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
