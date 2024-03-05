import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DodajpredavacaComponent } from './dodajpredavaca.component';

describe('DodajpredavacaComponent', () => {
  let component: DodajpredavacaComponent;
  let fixture: ComponentFixture<DodajpredavacaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DodajpredavacaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DodajpredavacaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
