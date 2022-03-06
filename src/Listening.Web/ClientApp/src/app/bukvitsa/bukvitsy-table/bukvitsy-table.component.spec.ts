import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BukvitsyTableComponent } from './bukvitsy-table.component';

describe('BukvitsyTableComponent', () => {
  let component: BukvitsyTableComponent;
  let fixture: ComponentFixture<BukvitsyTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BukvitsyTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BukvitsyTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
