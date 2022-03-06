import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JoinedTextComponent } from './joined-text.component';

describe('JoinedTextComponent', () => {
  let component: JoinedTextComponent;
  let fixture: ComponentFixture<JoinedTextComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JoinedTextComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JoinedTextComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
