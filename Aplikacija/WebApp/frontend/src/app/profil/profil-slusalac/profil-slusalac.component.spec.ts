import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfilSlusalacComponent } from './profil-slusalac.component';

describe('ProfilSlusalacComponent', () => {
  let component: ProfilSlusalacComponent;
  let fixture: ComponentFixture<ProfilSlusalacComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfilSlusalacComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfilSlusalacComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
