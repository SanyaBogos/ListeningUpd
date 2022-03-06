import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StegImageBaseComponent } from './steg-image-base.component';

describe('StegImageBaseComponent', () => {
  let component: StegImageBaseComponent;
  let fixture: ComponentFixture<StegImageBaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StegImageBaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StegImageBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
