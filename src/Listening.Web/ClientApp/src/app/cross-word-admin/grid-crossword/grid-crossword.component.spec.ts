import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GridCrosswordComponent } from './grid-crossword.component';

describe('GridCrosswordComponent', () => {
  let component: GridCrosswordComponent;
  let fixture: ComponentFixture<GridCrosswordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GridCrosswordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GridCrosswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
