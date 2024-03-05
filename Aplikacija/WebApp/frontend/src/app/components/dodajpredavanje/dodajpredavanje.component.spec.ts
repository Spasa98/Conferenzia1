import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DodajpredavanjeComponent } from './dodajpredavanje.component';

describe('DodajpredavanjeComponent', () => {
  let component: DodajpredavanjeComponent;
  let fixture: ComponentFixture<DodajpredavanjeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DodajpredavanjeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DodajpredavanjeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
