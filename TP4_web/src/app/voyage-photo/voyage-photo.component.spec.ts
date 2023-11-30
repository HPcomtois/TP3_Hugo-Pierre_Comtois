import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VoyagePhotoComponent } from './voyage-photo.component';

describe('VoyagePhotoComponent', () => {
  let component: VoyagePhotoComponent;
  let fixture: ComponentFixture<VoyagePhotoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [VoyagePhotoComponent]
    });
    fixture = TestBed.createComponent(VoyagePhotoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
