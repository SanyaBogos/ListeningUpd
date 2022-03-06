import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BukvitsaPageComponent } from './bukvitsa-page.component';

describe('BukvitsaPageComponent', () => {
  let component: BukvitsaPageComponent;
  let fixture: ComponentFixture<BukvitsaPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BukvitsaPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BukvitsaPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
