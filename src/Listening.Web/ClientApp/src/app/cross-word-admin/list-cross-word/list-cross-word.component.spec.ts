import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListCrossWordComponent } from './list-cross-word.component';

describe('ListCrossWordComponent', () => {
  let component: ListCrossWordComponent;
  let fixture: ComponentFixture<ListCrossWordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListCrossWordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListCrossWordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
