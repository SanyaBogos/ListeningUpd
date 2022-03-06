import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BukvitsaBaseComponent } from './bukvitsa-base.component';

describe('BukvitsaComponent', () => {
  let component: BukvitsaBaseComponent;
  let fixture: ComponentFixture<BukvitsaBaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BukvitsaBaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BukvitsaBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
