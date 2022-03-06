import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BukvitsaPageXComponent } from './bukvitsa-page-x.component';

describe('BukvitsaPageXComponent', () => {
  let component: BukvitsaPageXComponent;
  let fixture: ComponentFixture<BukvitsaPageXComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BukvitsaPageXComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BukvitsaPageXComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
