import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BukvitsaComponent } from './bukvitsa.component';

describe('BukvitsaComponent', () => {
  let component: BukvitsaComponent;
  let fixture: ComponentFixture<BukvitsaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BukvitsaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BukvitsaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
