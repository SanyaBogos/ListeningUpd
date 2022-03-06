import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StegSettingsComponent } from './steg-settings.component';

describe('StegSettingsComponent', () => {
  let component: StegSettingsComponent;
  let fixture: ComponentFixture<StegSettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StegSettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StegSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
