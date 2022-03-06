import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SpecTreeVideoComponent } from './spec-tree-video.component';

describe('SpecTreeVideoComponent', () => {
  let component: SpecTreeVideoComponent;
  let fixture: ComponentFixture<SpecTreeVideoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SpecTreeVideoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpecTreeVideoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
