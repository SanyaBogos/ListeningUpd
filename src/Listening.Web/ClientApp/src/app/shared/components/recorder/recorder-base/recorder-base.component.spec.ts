import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecorderBaseComponent } from './recorder-base.component';

describe('RecorderBaseComponent', () => {
  let component: RecorderBaseComponent;
  let fixture: ComponentFixture<RecorderBaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecorderBaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecorderBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
