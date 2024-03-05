import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IzmeniProfilSlusalacComponent } from './izmeni-profil-slusalac.component';

describe('IzmeniProfilSlusalacComponent', () => {
  let component: IzmeniProfilSlusalacComponent;
  let fixture: ComponentFixture<IzmeniProfilSlusalacComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IzmeniProfilSlusalacComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IzmeniProfilSlusalacComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
