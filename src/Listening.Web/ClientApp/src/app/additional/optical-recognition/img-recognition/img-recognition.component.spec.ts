import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ImgRecognitionComponent } from './img-recognition.component';

describe('ImgRecognitionComponent', () => {
  let component: ImgRecognitionComponent;
  let fixture: ComponentFixture<ImgRecognitionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ImgRecognitionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ImgRecognitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
