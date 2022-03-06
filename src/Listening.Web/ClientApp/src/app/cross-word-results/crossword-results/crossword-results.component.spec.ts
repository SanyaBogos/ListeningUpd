import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CrosswordResultsComponent } from './crossword-results.component';

describe('CrosswordResultsComponent', () => {
  let component: CrosswordResultsComponent;
  let fixture: ComponentFixture<CrosswordResultsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CrosswordResultsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CrosswordResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
