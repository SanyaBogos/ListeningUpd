import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCrosswordComponent } from './edit-crossword.component';

describe('EditCrosswordComponent', () => {
  let component: EditCrosswordComponent;
  let fixture: ComponentFixture<EditCrosswordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditCrosswordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditCrosswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
