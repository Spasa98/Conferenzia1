import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IzgledProfilaPredavacComponent } from './izgled-profila-predavac.component';

describe('IzgledProfilaPredavacComponent', () => {
  let component: IzgledProfilaPredavacComponent;
  let fixture: ComponentFixture<IzgledProfilaPredavacComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IzgledProfilaPredavacComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IzgledProfilaPredavacComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
