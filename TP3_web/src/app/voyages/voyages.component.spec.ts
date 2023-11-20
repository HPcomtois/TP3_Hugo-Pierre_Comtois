import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VoyagesComponent } from './voyages.component';

describe('VoyagesComponent', () => {
  let component: VoyagesComponent;
  let fixture: ComponentFixture<VoyagesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [VoyagesComponent]
    });
    fixture = TestBed.createComponent(VoyagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
