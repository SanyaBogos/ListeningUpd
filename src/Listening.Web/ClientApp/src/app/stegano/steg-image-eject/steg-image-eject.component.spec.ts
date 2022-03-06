import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StegImageEjectComponent } from './steg-image-eject.component';

describe('StegImageEjectComponent', () => {
  let component: StegImageEjectComponent;
  let fixture: ComponentFixture<StegImageEjectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StegImageEjectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StegImageEjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
