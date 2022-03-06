import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BukvitsaImageComponent } from './bukvitsa-image.component';

describe('BukvitsaImageComponent', () => {
  let component: BukvitsaImageComponent;
  let fixture: ComponentFixture<BukvitsaImageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BukvitsaImageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BukvitsaImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
