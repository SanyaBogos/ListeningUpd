import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AbstractChangeSettingsComponent } from './abstract-change-settings.component';

describe('AbstractChangeSettingsComponent', () => {
  let component: AbstractChangeSettingsComponent;
  let fixture: ComponentFixture<AbstractChangeSettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AbstractChangeSettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AbstractChangeSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
