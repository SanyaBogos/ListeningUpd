import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DebianFaiComponent } from './debian-fai.component';

describe('DebianFaiComponent', () => {
  let component: DebianFaiComponent;
  let fixture: ComponentFixture<DebianFaiComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DebianFaiComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DebianFaiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
