import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfilOrganizatorComponent } from './profil-organizator.component';

describe('ProfilOrganizatorComponent', () => {
  let component: ProfilOrganizatorComponent;
  let fixture: ComponentFixture<ProfilOrganizatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfilOrganizatorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfilOrganizatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
