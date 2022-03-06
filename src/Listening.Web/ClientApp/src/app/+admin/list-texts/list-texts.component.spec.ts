import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListTextsComponent } from './list-texts.component';

describe('ListTextsComponent', () => {
  let component: ListTextsComponent;
  let fixture: ComponentFixture<ListTextsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListTextsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListTextsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
