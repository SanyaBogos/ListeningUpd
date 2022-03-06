import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GuessingDiagramComponent } from './guessing-diagram.component';

describe('GuessingDiagramComponent', () => {
  let component: GuessingDiagramComponent;
  let fixture: ComponentFixture<GuessingDiagramComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GuessingDiagramComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GuessingDiagramComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
