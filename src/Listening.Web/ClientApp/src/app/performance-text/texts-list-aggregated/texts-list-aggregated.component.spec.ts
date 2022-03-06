import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TextsListAggregatedComponent } from './texts-list-aggregated.component';

describe('TextsListAggregatedComponent', () => {
  let component: TextsListAggregatedComponent;
  let fixture: ComponentFixture<TextsListAggregatedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TextsListAggregatedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TextsListAggregatedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
