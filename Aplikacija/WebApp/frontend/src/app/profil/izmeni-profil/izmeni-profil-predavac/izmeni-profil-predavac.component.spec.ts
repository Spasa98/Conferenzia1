import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IzmeniProfilPredavacComponent } from './izmeni-profil-predavac.component';

describe('IzmeniProfilPredavacComponent', () => {
  let component: IzmeniProfilPredavacComponent;
  let fixture: ComponentFixture<IzmeniProfilPredavacComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IzmeniProfilPredavacComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IzmeniProfilPredavacComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
