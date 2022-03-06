import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SeparatedTextComponent } from './separated-text.component';

describe('SeparatedTextComponent', () => {
  let component: SeparatedTextComponent;
  let fixture: ComponentFixture<SeparatedTextComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SeparatedTextComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SeparatedTextComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
