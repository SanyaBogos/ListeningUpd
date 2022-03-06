import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HowToUseEnhComponent } from './how-to-use-enh.component';

describe('HowToUseComponent', () => {
  let component: HowToUseEnhComponent;
  let fixture: ComponentFixture<HowToUseEnhComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HowToUseEnhComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HowToUseEnhComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
