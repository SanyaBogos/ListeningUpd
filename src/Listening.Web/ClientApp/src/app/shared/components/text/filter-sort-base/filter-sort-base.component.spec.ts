import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterSortBaseComponent } from './filter-sort-base.component';

describe('FilterSortBaseComponent', () => {
  let component: FilterSortBaseComponent;
  let fixture: ComponentFixture<FilterSortBaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FilterSortBaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FilterSortBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
