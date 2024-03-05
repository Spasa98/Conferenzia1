import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfilPredavacComponent } from './profil-predavac.component';

describe('ProfilPredavacComponent', () => {
  let component: ProfilPredavacComponent;
  let fixture: ComponentFixture<ProfilPredavacComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfilPredavacComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfilPredavacComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
