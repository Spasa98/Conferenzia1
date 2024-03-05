import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IzmeniProfilOrganizatorComponent } from './izmeni-profil-organizator.component';

describe('IzmeniProfilOrganizatorComponent', () => {
  let component: IzmeniProfilOrganizatorComponent;
  let fixture: ComponentFixture<IzmeniProfilOrganizatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IzmeniProfilOrganizatorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IzmeniProfilOrganizatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
