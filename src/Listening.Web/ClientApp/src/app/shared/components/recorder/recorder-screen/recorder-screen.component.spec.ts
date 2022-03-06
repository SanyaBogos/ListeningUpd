import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecorderScreenComponent } from './recorder-screen.component';

describe('RecorderScreenComponent', () => {
  let component: RecorderScreenComponent;
  let fixture: ComponentFixture<RecorderScreenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecorderScreenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecorderScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
