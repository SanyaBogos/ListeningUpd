import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AbstractTextComponent } from './abstract-text.component';

describe('AbstractTextComponent', () => {
  let component: AbstractTextComponent;
  let fixture: ComponentFixture<AbstractTextComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AbstractTextComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AbstractTextComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
