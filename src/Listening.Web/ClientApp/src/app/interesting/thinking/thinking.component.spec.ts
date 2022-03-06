import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ThinkingComponent } from './thinking.component';

describe('ThinkingComponent', () => {
  let component: ThinkingComponent;
  let fixture: ComponentFixture<ThinkingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ThinkingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ThinkingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
