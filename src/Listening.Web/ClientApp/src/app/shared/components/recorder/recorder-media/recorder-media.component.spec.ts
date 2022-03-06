import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecorderMediaComponent } from './recorder-media.component';

describe('RecorderMediaComponent', () => {
  let component: RecorderMediaComponent;
  let fixture: ComponentFixture<RecorderMediaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecorderMediaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecorderMediaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
