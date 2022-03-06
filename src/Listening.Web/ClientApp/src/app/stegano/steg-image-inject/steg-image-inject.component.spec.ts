import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StegImageInjectComponent } from './steg-image-inject.component';

describe('StegImageComponent', () => {
  let component: StegImageInjectComponent;
  let fixture: ComponentFixture<StegImageInjectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StegImageInjectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StegImageInjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
