import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BukvitsyTableXComponent } from './bukvitsy-table-x.component';

describe('BukvitsyTableXComponent', () => {
  let component: BukvitsyTableXComponent;
  let fixture: ComponentFixture<BukvitsyTableXComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BukvitsyTableXComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BukvitsyTableXComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
