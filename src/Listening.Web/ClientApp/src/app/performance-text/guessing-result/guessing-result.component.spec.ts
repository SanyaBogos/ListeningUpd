import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GuessingResultComponent } from './guessing-result.component';

describe('GuessingResultComponent', () => {
  let component: GuessingResultComponent;
  let fixture: ComponentFixture<GuessingResultComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GuessingResultComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GuessingResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
